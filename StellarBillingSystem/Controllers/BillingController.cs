using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StellarBillingSystem.Context;
using StellarBillingSystem.Models;
using StellarBillingSystem_skj.Business;
using StellarBillingSystem_skj.Models;

namespace StellarBillingSystem_skj.Controllers
{
    [Authorize]
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


            return View();
        }


        [HttpPost]
        public ActionResult SaveBill(string BillDetailsJson, BillMasterModelSKJ BillMaster)
        {
            var billDetails = JsonConvert.DeserializeObject<List<BillDetailsModelSKJ>>(BillDetailsJson);

            /*// Save to DB
            var billId = SaveBillMasterToDb(BillMaster);

            foreach (var item in billDetails)
            {
                item.BillID = billId;
                SaveBillDetailToDb(item);
            }*/

            return RedirectToAction("Success");
        }

    }
}
