using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult SaveBill([FromBody] BillViewModel vm)
        {
            if (vm == null)
            {
                var raw = new StreamReader(Request.Body).ReadToEndAsync().Result;
                Console.WriteLine("🔍 RAW JSON RECEIVED:\n" + raw);
                return BadRequest("❌ ViewModel is NULL!");
            }

            try
            {
                var billMaster = vm.BillMaster;
                _billingsoftware.Shbillmasterskj.Add(billMaster);

                Console.WriteLine("🟢 BillMaster Added:");
                Console.WriteLine(JsonConvert.SerializeObject(billMaster, Formatting.Indented));

                // Step 1: Save Articles and track their IDs
                var savedArticles = new List<ArticleModel>();
                foreach (var article in vm.Articles)
                {
                    _billingsoftware.SHArticleMaster.Add(article);
                    savedArticles.Add(article);
                }

                Console.WriteLine("🟢 Articles to Save:");
                Console.WriteLine(JsonConvert.SerializeObject(savedArticles, Formatting.Indented));

                // Step 2: Save Articles and BillMaster
                var firstSave = _billingsoftware.SaveChanges();
                Console.WriteLine($"✅ SaveChanges #1 (BillMaster + Articles) = {firstSave}");

                foreach (var a in savedArticles)
                {
                    Console.WriteLine($"🔎 Saved Article ID: {a.ArticleID}");
                }

                // Step 3: Link Articles to BillDetails
                for (int i = 0; i < vm.BillDetails.Count; i++)
                {
                    var detail = vm.BillDetails[i];
                    var article = savedArticles[i];

                    detail.BillID = billMaster.BillID;
                    detail.BranchID = billMaster.BranchID;
                    detail.ArticleID = article.ArticleID;

                    _billingsoftware.Shbilldetailsskj.Add(detail);
                }

                Console.WriteLine("🟢 BillDetails to Save:");
                Console.WriteLine(JsonConvert.SerializeObject(vm.BillDetails, Formatting.Indented));

                // Step 4: Save Details
                var finalSave = _billingsoftware.SaveChanges();
                Console.WriteLine($"✅ SaveChanges #2 (BillDetails) = {finalSave}");

                // 🔍 Entity Tracker State Debug
                Console.WriteLine("🧾 EF Change Tracker:");
                foreach (var entry in _billingsoftware.ChangeTracker.Entries())
                {
                    Console.WriteLine($"▶️ Entity: {entry.Entity.GetType().Name}, State: {entry.State}");
                }

                return Json(new { success = true, message = "✅ Bill saved successfully!" });
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Exception Thrown: " + ex.Message);
                if (ex.InnerException != null)
                    Console.WriteLine("🔍 Inner Exception: " + ex.InnerException.Message);

                return Json(new { success = false, message = "❌ Error: " + ex.Message });
            }
        }



    }


}

