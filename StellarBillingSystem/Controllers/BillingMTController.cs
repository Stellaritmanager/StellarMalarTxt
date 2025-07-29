using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StellarBillingSystem.Business;
using StellarBillingSystem.Context;
using StellarBillingSystem.Models;
using StellarBillingSystem_Malar.Business;
using StellarBillingSystem_Malar.Models;

namespace StellarBillingSystem_Malar.Controllers
{
    [Authorize]
    public class BillingMTController : Controller
    {
        private BillingContext _billingsoftware;
        private readonly IConfiguration _configuration;


        public BillingMTController(BillingContext billingsoftware, IConfiguration configuration)
        {
            _billingsoftware = billingsoftware;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult CustomerBilling()
        {
            string branchId = TempData["BranchID"]?.ToString();
            TempData.Keep("BranchID");

            var bus = new BusinessClassBillMT(_billingsoftware, _configuration);
            ViewData["productid"] = bus.Getproduct(branchId);
            ViewData["customer"] = bus.GetcustomerID(branchId);

            return View();
        }

        [HttpPost]
        public IActionResult AddProductInBill([FromBody] BillingDetailsModel product)
        {
            string branchId = TempData["BranchID"]?.ToString();
            TempData.Keep("BranchID");

            var bus = new BusinessClassBillMT(_billingsoftware, _configuration);
            var result = bus.ValidateAndPrepareProduct(product, branchId, out string error);

            if (result == null)
                return BadRequest(error);

            return Json(new { newProduct = result });
        }

        [HttpPost]
        public async Task<IActionResult> SaveBill([FromBody] BillProductlistModel model)
        {
            if (model == null || model.Viewbillproductlist == null || !model.Viewbillproductlist.Any())
                return BadRequest("Missing data");

            string branchId = TempData["BranchID"]?.ToString();
            TempData.Keep("BranchID");

            if (model.BillID != null)
            {
                HttpContext.Session.SetString("BillID", model.BillID);
            }
            else
            {
                HttpContext.Session.SetString("BillID", string.Empty);
            }

            HttpContext.Session.SetString("BranchID", branchId);


            model.BranchID = branchId;

            var bus = new BusinessClassBillMT(_billingsoftware, _configuration);
            string ip = Request.HttpContext.Connection.RemoteIpAddress?.ToString();

            var billID = await bus.SaveBillAsync(model, User, ip);
            return Json(new { success = true, billID });
        }

        [HttpGet]
        public IActionResult GetBillDetail(string billID)
        {
            string branchId = TempData["BranchID"]?.ToString();
            TempData.Keep("BranchID");

            var bus = new BusinessClassBillMT(_billingsoftware, _configuration);
            var result = bus.GetBillDetails(billID, branchId);

            if (!result.success)
                return Json(new { success = false, message = result.message });

            return Json(new { success = true, data = result.data });
        }


        [HttpPost]
        public IActionResult DeleteBill(string billID)
        {
            string branchId = TempData["BranchID"]?.ToString();
            TempData.Keep("BranchID");

            var checkbillpayment = _billingsoftware.SHPaymentMaster.FirstOrDefault(x => x.BillId == billID && x.BranchID == branchId && x.IsDelete == false);
            if (checkbillpayment != null)
            {
                return Json(new { success = false, message = "Cannot Delete Bill!! Delete Payment First" });
            }

            var bus = new BusinessClassBillMT(_billingsoftware, _configuration);
            var isDeleted = bus.DeleteBill(billID, branchId);

            if (isDeleted)
                return Json(new { success = true, message = "Bill Deleted Successfully" });

            return Json(new { success = false, message = "Bill not found" });
        }

        [HttpPost]
        public IActionResult AddCustomerPop(CustomerMasterModel model)
        {
            string branchId = TempData["BranchID"]?.ToString();
            TempData.Keep("BranchID");
            model.BranchID = branchId;

            var bus = new BusinessClassBillMT(_billingsoftware, _configuration);
            string ip = Request.HttpContext.Connection.RemoteIpAddress?.ToString();
            string user = User.Claims.FirstOrDefault()?.Value ?? "System";

            var result = bus.AddOrUpdateCustomer(model, ip, user);
            if (!result.success)
                return BadRequest(result.message);

            return Ok(result.message);
        }
    }

}

