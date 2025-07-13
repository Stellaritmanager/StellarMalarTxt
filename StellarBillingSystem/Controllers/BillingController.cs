using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StellarBillingSystem.Context;
using StellarBillingSystem.Models;
using StellarBillingSystem_skj.Business;
using StellarBillingSystem_skj.Models;

namespace StellarBillingSystem_skj.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class BillingController : Controller
    {

        private BillingContext _billingsoftware;
        private readonly IConfiguration _configuration;


        public BillingController(BillingContext billingsoftware, IConfiguration configuration)
        {
            _billingsoftware = billingsoftware;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Billing()
        {

            string BranchID = null;

            if (TempData["BranchID"] != null)
            {
                BranchID = TempData["BranchID"].ToString();
                TempData.Keep("BranchID");
            }

            BusinessBillingSKJ Busbill = new BusinessBillingSKJ(_billingsoftware, _configuration);
            ViewData["customerid"] = Busbill.getCustomerID(BranchID);
            var goldTypes = Busbill.getGoldtype(BranchID);

            ViewBag.GoldTypeList = goldTypes;
            ViewBag.BranchID = BranchID;


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveBillWithFiles()
        {
            var form = Request.Form;
            var vmJson = form["vm"];
            BillViewModel vm = JsonConvert.DeserializeObject<BillViewModel>(vmJson);

            if (vm == null)
                return BadRequest("Invalid ViewModel");

            try
            {
                var billMaster = vm.BillMaster;

                // 🔁 Ensure upload folder exists
                string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "BillImage", billMaster.BillID);
                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                // 🔁 Save uploaded files and update their image paths
                foreach (var file in form.Files)
                {
                    string filePath = Path.Combine(uploadPath, file.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    // 🌐 Update the image path in BillImages (relative path)
                    var matchingImage = vm.BillImages.FirstOrDefault(img => img.ImageName == file.FileName);
                    if (matchingImage != null)
                    {
                        matchingImage.ImagePath = $"BillImage/{billMaster.BillID}/{file.FileName}";
                    }
                }

                // 💾 Save BillMaster
                _billingsoftware.Shbillmasterskj.Add(billMaster);

                // 💾 Save Articles
                var savedArticles = new List<ArticleModel>();
                foreach (var article in vm.Articles)
                {
                    _billingsoftware.SHArticleMaster.Add(article);
                    savedArticles.Add(article);
                }
                _billingsoftware.SaveChanges(); // Save BillMaster + Articles

                // 💾 Save BillDetails
                for (int i = 0; i < vm.BillDetails.Count; i++)
                {
                    var detail = vm.BillDetails[i];
                    var article = savedArticles[i];

                    detail.BillID = billMaster.BillID;
                    detail.ArticleID = article.ArticleID;
                    detail.BranchID = billMaster.BranchID;

                    _billingsoftware.Shbilldetailsskj.Add(detail);
                }
                _billingsoftware.SaveChanges();

                // 💾 Save BillImages with ArticleID (use first article if needed)
                // 💾 Save BillImages with ArticleID (assign one-by-one)
                if (vm.BillImages != null && vm.BillImages.Any())
                {
                    for (int i = 0; i < vm.BillImages.Count; i++)
                    {
                        if (i < savedArticles.Count)
                        {
                            vm.BillImages[i].ArticleID = savedArticles[i].ArticleID;
                        }

                        _billingsoftware.Shbillimagemodelskj.Add(vm.BillImages[i]);
                    }
                    _billingsoftware.SaveChanges();
                }


                return Json(new { success = true, message = "✅ Bill and images saved successfully!" });
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Exception: " + ex.Message);
                if (ex.InnerException != null)
                    Console.WriteLine("🔍 Inner: " + ex.InnerException.Message);

                return Json(new { success = false, message = "❌ Error: " + ex.Message });
            }
        }




        [HttpGet]
        public IActionResult GetBill(string billId)
        {
            try
            {
                var billMaster = _billingsoftware.Shbillmasterskj
                    .FirstOrDefault(b => b.BillID == billId && b.IsDelete == false);

                if (billMaster == null)
                    return NotFound("Bill not found");

                var billDetails = _billingsoftware.Shbilldetailsskj
                    .Where(d => d.BillID == billId && d.IsDelete == false)
                    .ToList();

                var articleIdList = billDetails
                    .Select(d => d.ArticleID)
                    .Distinct()
                    .ToList();

                // ✅ Safe in-memory filter
                var allArticles = _billingsoftware.SHArticleMaster
                    .AsNoTracking()
                    .ToList();

                var articles = allArticles
                    .Where(a => articleIdList.Contains(a.ArticleID))
                    .ToList();

                var images = _billingsoftware.Shbillimagemodelskj
    .Where(img => img.BillID == billId)
    .Select(img => new {
        img.ImagePath,
        img.ImageName
    }).ToList();


                return Json(new
                {
                    BillMaster = billMaster,
                    BillDetails = billDetails,
                    Articles = articles,
                    Images = images
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Exception:", ex.Message);
                return StatusCode(500, "Error retrieving bill");
            }
        }




    }


}

