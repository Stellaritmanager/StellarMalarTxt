using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StellarBillingSystem.Context;
using StellarBillingSystem_skj.Models;
using System.Linq;


[Authorize]
[Route("[controller]/[action]")]
public class RepledgeMasterController : Controller
{
        private BillingContext _billingsoftware;
        private readonly IConfiguration _configuration;


        public RepledgeMasterController(BillingContext billingsoftware, IConfiguration configuration)
        {
            _billingsoftware = billingsoftware;
            _configuration = configuration;
        }

        public IActionResult RepledgeMaster()
        { 
           RepledgeViewModel obj = new RepledgeViewModel();
            return View(obj);
        }

    [HttpPost]
    public IActionResult GetArticles(RepledgeViewModel model,string buttonType)
    {
        if (TempData["BranchID"] != null)
        {
            model.BranchID = TempData["BranchID"].ToString();
            TempData.Keep("BranchID");
        }

        if(buttonType=="Delete")
        {
            var checkrepledge = _billingsoftware.Shbuyerrepledge.FirstOrDefault(x=>x.RepledgeID == model.RepledgeID && x.BranchID == model.BranchID);

            {
                if (checkrepledge != null)
                {
                    checkrepledge.IsDelete = true;

                   
                    var getallarticle = _billingsoftware.Shrepledgeartcile.Where(x=>x.RepledgeID == model.RepledgeID && x.BranchID == model.BranchID).ToList();
                   
                    foreach(var result in getallarticle)
                    {
                        result.IsDelete = true;

                    }

                    _billingsoftware.SaveChanges();
                    ViewBag.Message = "Deleted Successfully";
                }
            }
        }

        // Get all bill articles first
        var allBillArticles = (from d in _billingsoftware.Shbilldetailsskj
                               join a in _billingsoftware.SHArticleMaster on d.ArticleID equals a.ArticleID
                               where d.BillID == model.BillID && d.BranchID == model.BranchID
                               select new ArticleSelection
                               {
                                   ArticleID = a.ArticleID,
                                   ArticleName = a.ArticleName
                               }).ToList();

        // Check if repledge already exists
        var existingBuyer = _billingsoftware.Shbuyerrepledge
            .FirstOrDefault(b => b.RepledgeID == model.RepledgeID && b.BranchID == model.BranchID);

        if (existingBuyer != null)
        {
            // Fill buyer data
            model.BuyerName = existingBuyer.BuyerName;
            model.Address = existingBuyer.Address;
            model.PhoneNumber = existingBuyer.PhoneNumber;
            model.TotalAmount = existingBuyer.TotalAmount;
            model.Interest = existingBuyer.Interest;
            model.Tenure = existingBuyer.Tenure;

            // Get already repledged article IDs
            var alreadySelectedIDs = _billingsoftware.Shrepledgeartcile
                .Where(r => r.RepledgeID == model.RepledgeID && r.BranchID == model.BranchID)
                .Select(r => r.ArticleID)
                .ToList();

            model.SelectedArticleIDs = alreadySelectedIDs;
        }

        model.AvailableArticles = allBillArticles;

        return View("RepledgeMaster", model);
    }


    [HttpPost]
        public IActionResult SaveRepledge(RepledgeViewModel model)
        {

        if (TempData["BranchID"] != null)
        {
            model.BranchID = TempData["BranchID"].ToString();
            TempData.Keep("BranchID");
        }

        var checkdatabuyer = _billingsoftware.Shbuyerrepledge.FirstOrDefault(x => x.RepledgeID == model.RepledgeID);

        if (checkdatabuyer != null)
        {
            checkdatabuyer.RepledgeID = model.RepledgeID;
            checkdatabuyer.BuyerName = model.BuyerName;
            checkdatabuyer.Address = model.Address;
            checkdatabuyer.PhoneNumber = model.PhoneNumber;
            checkdatabuyer.TotalAmount = model.TotalAmount;
            checkdatabuyer.Interest = model.Interest;
            checkdatabuyer.BranchID = model.BranchID;
            checkdatabuyer.Tenure = model.Tenure;
            checkdatabuyer.LastUpdatedMachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            checkdatabuyer.LastUpdatedDate = DateTime.Now.ToString();
            checkdatabuyer.LastUpdatedUser = User.Claims.First().Value.ToString();

        }
        else
        {
            // 1. Save BuyerModelSKJ
            var buyer = new BuyerRepledgeModel
            {
                RepledgeID = model.RepledgeID,
                BuyerName = model.BuyerName,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                TotalAmount = model.TotalAmount,
                Interest = model.Interest,
                BranchID = model.BranchID,
                Tenure = model.Tenure,
                LastUpdatedDate = DateTime.Now.ToString(),
                LastUpdatedUser = User.Claims.First().Value.ToString(),
                LastUpdatedMachine = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
            };

            _billingsoftware.Shbuyerrepledge.Add(buyer);
        }


        // a. Add new selections
        foreach (var articleId in model.SelectedArticleIDs)
        {
            bool exists = _billingsoftware.Shrepledgeartcile.Any(r =>
                r.ArticleID == articleId &&
                r.RepledgeID == model.RepledgeID &&
                r.BranchID == model.BranchID &&
                r.BillID == model.BillID);

            if (!exists)
            {
                var replArticle = new RepledgeArtcileModel
                {
                    ArticleID = articleId,
                    BillID = model.BillID,
                    BranchID = model.BranchID,
                    RepledgeID = model.RepledgeID,
                    LastUpdatedDate = DateTime.Now.ToString(),
                    LastUpdatedUser = User.Claims.First().Value.ToString(),
                    LastUpdatedMachine = Request.HttpContext.Connection.RemoteIpAddress.ToString()
                };

                _billingsoftware.Shrepledgeartcile.Add(replArticle);
            }
        }


        _billingsoftware.SaveChanges();
        ViewBag.Message = "Payment Saved Successfully";
        return RedirectToAction("RepledgeMaster");
        }
    }

