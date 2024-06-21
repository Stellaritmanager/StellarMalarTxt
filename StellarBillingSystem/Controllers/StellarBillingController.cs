using ClassLibrary1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StellarBillingSystem.Context;

namespace HealthCare.Controllers
{
    public class StellarBillingController : Controller
    {
        private BillingContext _billingsoftware;

        public StellarBillingController(BillingContext billingsoftware)
        {
            _billingsoftware = billingsoftware;
        }

        [HttpPost]

        public async Task<IActionResult> AddCategory(CategoryMasterModel model , string buttonType)
        {
            var existingCategory = await _billingsoftware.SHCategoryMaster.FindAsync(model.CategoryID);
            if (existingCategory != null)
            {
                existingCategory.CategoryID = model.CategoryID;
                existingCategory.CategoryName = model.CategoryName;
                existingCategory.LastUpdatedDate = DateTime.Now.ToString();
                existingCategory.LastUpdatedUser = /*User.Claims.First().Value.ToString();*/ "Admin";
                existingCategory.LastUpdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();    

                _billingsoftware.Entry(existingCategory).State = EntityState.Modified;
            }
            else
            {
                model.LastUpdatedDate = DateTime.Now.ToString();
                model.LastUpdatedUser = /*User.Claims.First().Value.ToString();*/  "Admin";
                model.LastUpdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();


                _billingsoftware.SHCategoryMaster.Add(model);
            }
                

            await _billingsoftware.SaveChangesAsync();

            ViewBag.Message = "Saved Successfully";
           
            return View("CategoryMasterModel");
        }


        [HttpPost]

        public async Task<IActionResult> AddProduct(ProductMatserModel model)
        {
            var existingProduct  = await _billingsoftware.SHProductMaster.FindAsync(model.ProductID);
            if (existingProduct != null)
            {
                existingProduct.ProductID = model.ProductID;
                existingProduct.CategoryID = model.CategoryID;
                existingProduct.ProductName = model.ProductName;
                existingProduct.Brandname = model.Brandname;
                existingProduct.Price = model.Price;
                existingProduct.Discount = model.Discount;
                existingProduct.TotalAmount = model.TotalAmount;
                existingProduct.LastUpdatedDate = DateTime.Now.ToString();
                existingProduct.LastUpdatedUser = User.Claims.First().Value.ToString();
                existingProduct.LastUpdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                _billingsoftware.Entry(existingProduct).State = EntityState.Modified;

            }
            else
            {

                model.LastUpdatedDate = DateTime.Now.ToString();
                model.LastUpdatedUser = User.Claims.First().Value.ToString();
                model.LastUpdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                _billingsoftware.SHProductMaster.Add(model);
            }
            

            await _billingsoftware.SaveChangesAsync();


            ViewBag.Message = "Saved Successfully";

            return View("ProductMasterModel" , model);
        }

        [HttpPost]

        public async Task<IActionResult> CustomerBill(BilingSysytemModel model)
        {
            var existingbill = await _billingsoftware.SHCustomerBilling.FindAsync(model.BillID);
            if (existingbill != null)
            {
                existingbill.BillID = model.BillID;
                existingbill.CustomerName = model.CustomerName;
                existingbill.Date = model.Date;
                existingbill.CustomerNumber = model.CustomerNumber;
                existingbill.Items = model.Items;
                existingbill.Rate = model.Rate;
                existingbill.Quantity= model.Quantity;
                existingbill.Discount = model.Discount;
                existingbill.Tax = model.Tax;
                existingbill.DiscountPrice = model.DiscountPrice;
                existingbill.TotalAmount = model.TotalAmount;
                existingbill.PointsNumber = model.PointsNumber;
                existingbill.VoucherNumber = model.VoucherNumber;
                existingbill.CategoryBasedDiscount = model.CategoryBasedDiscount;
                existingbill.TotalAmount = model.TotalAmount;
                existingbill.LastUpdatedDate = DateTime.Now.ToString();
                existingbill.LastUpdatedUser = User.Claims.First().Value.ToString();
                existingbill.LastUpdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                _billingsoftware.Entry(existingbill).State = EntityState.Modified;

            }
            else
            {

                model.LastUpdatedDate = DateTime.Now.ToString();
                model.LastUpdatedUser = User.Claims.First().Value.ToString();
                model.LastUpdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            }

            await _billingsoftware.SaveChangesAsync();


            ViewBag.Message = "Saved Successfully";

            return View("CustomerBilling" , model);
        }



        [HttpPost]
        public async Task<IActionResult> AddCustomer(CustomerMasterModel model)
        {
            var existingCustomer = await _billingsoftware.SHCustomerMaster.FindAsync(model.CustomerID);
            if (existingCustomer != null)
            {
                existingCustomer.CustomerID = model.CustomerID;
                existingCustomer.CustomerName = model.CustomerName;
                existingCustomer.DateofBirth = model.DateofBirth;
                existingCustomer.Gender = model.Gender;
                existingCustomer.Address = model.Address;
                existingCustomer.City = model.City;
                existingCustomer.MobileNumber = model.MobileNumber;
                existingCustomer.PointsReedem = model.PointsReedem;
                existingCustomer.VoucherDiscount = model.VoucherDiscount;
                existingCustomer.VoucherNumber = model.VoucherNumber;
                existingCustomer.LastUpdatedDate = DateTime.Now.ToString();
                existingCustomer.LastUpdatedUser = User.Claims.First().Value.ToString();
                existingCustomer.LastUpdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                _billingsoftware.Entry(existingCustomer).State = EntityState.Modified;

            }
            else
            {
                model.LastUpdatedDate = DateTime.Now.ToString();
                model.LastUpdatedUser = User.Claims.First().Value.ToString();
                model.LastUpdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                _billingsoftware.SHCustomerMaster.Add(model);

            }
            _billingsoftware.SHCustomerMaster.Add(model);

            await _billingsoftware.SaveChangesAsync();

            ViewBag.Message = "Saved Successfully";
            
            return View("CustomerMaster" , model);
        }

        [HttpPost]
        public async Task<IActionResult> AddDiscountCategory(DiscountCategoryMasterModel model)
        {
            var existingDiscountCategory = await _billingsoftware.SHDiscountCategory.FindAsync(model.CategoryID);
            if (existingDiscountCategory != null)
            {
                existingDiscountCategory.CategoryID = model.CategoryID;
                existingDiscountCategory.CategoryName = model.CategoryName;
                existingDiscountCategory.DiscountPrice = model.DiscountPrice;
                existingDiscountCategory.LastUpdatedDate = DateTime.Now.ToString();
                existingDiscountCategory.LastUpdatedUser = User.Claims.First().Value.ToString();
                existingDiscountCategory.LastUpdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                _billingsoftware.Entry(existingDiscountCategory).State = EntityState.Modified;

            }
            else
            {
                model.LastUpdatedDate = DateTime.Now.ToString();
                model.LastUpdatedUser = User.Claims.First().Value.ToString();
                model.LastUpdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                _billingsoftware.SHDiscountCategory.Add(model);

            }


            await _billingsoftware.SaveChangesAsync();

            ViewBag.Message = "Saved Successfully";

            return View("DiscountCategoryMaster", model);

        }

        [HttpPost]

        public async Task<IActionResult> AddGST(GSTMasterModel model)
        {
            var existingGst = await _billingsoftware.SHGSTMaster.FindAsync(model.TaxID);
            if (existingGst != null)
            {
                existingGst.TaxID = model.TaxID;
                existingGst.SGST  = model.SGST;
                existingGst.CGST = model.CGST;
                existingGst.OtherTax = model.OtherTax;
                existingGst.LastUpdatedDate = DateTime.Now.ToString();
                existingGst.LastUpdatedUser = User.Claims.First().Value.ToString();
                existingGst.LastUpdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                _billingsoftware.Entry(existingGst).State = EntityState.Modified;
            }
            else
            {
                model.LastUpdatedDate = DateTime.Now.ToString();
                model.LastUpdatedUser = User.Claims.First().Value.ToString();
                model.LastUpdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                _billingsoftware.SHGSTMaster.Add(model);

            }

            await _billingsoftware.SaveChangesAsync();

            ViewBag.Message = "Saved Successfully";

            return View("GSTMasterModel", model);


        }



        [HttpPost]

        public async Task<IActionResult> AddVoucherDetails(VoucherCustomerDetailModel model)
        {
            var existingVoucher = await _billingsoftware.SHVoucherDetails.FindAsync(model.VoucherID);
            if (existingVoucher != null)
            {
                existingVoucher.VoucherID = model.VoucherID;
                existingVoucher.CustomerID = model.CustomerID;
                existingVoucher.ExpiryDate = model.ExpiryDate;
                existingVoucher.LastUpdatedDate = DateTime.Now.ToString();
                existingVoucher.LastUpdatedUser = User.Claims.First().Value.ToString();
                existingVoucher.LastUpdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                _billingsoftware.Entry(existingVoucher).State = EntityState.Modified;
            }
            else
            {
                model.LastUpdatedDate = DateTime.Now.ToString();
                model.LastUpdatedUser = User.Claims.First().Value.ToString();
                model.LastUpdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                _billingsoftware.SHVoucherDetails.Add(model);

            }


            await _billingsoftware.SaveChangesAsync();

            ViewBag.Message = "Saved Successfully";

            return View("VoucherCustomerDetails", model);

        }

        [HttpPost]

        public async Task<IActionResult> AddNetDiscount(NetDicsountMasterModel model)
        {
            var existingnetdiscount = await _billingsoftware.SHNetDiscountMaster.FindAsync(model.NetDiscountID);
            if (existingnetdiscount == null)
            {
                existingnetdiscount.NetDiscountID = model.NetDiscountID;
                existingnetdiscount.NetDiscount = model.NetDiscount;
                existingnetdiscount.LastUpdatedDate = DateTime.Now.ToString();
                existingnetdiscount.LastUpdatedUser = User.Claims.First().Value.ToString();
                existingnetdiscount.LastUpdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                _billingsoftware.Entry(existingnetdiscount).State = EntityState.Modified;
            }
            else
            {

                model.LastUpdatedDate = DateTime.Now.ToString();
                model.LastUpdatedUser = User.Claims.First().Value.ToString();
                model.LastUpdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                _billingsoftware.SHNetDiscountMaster.Add(model);

            }

            await _billingsoftware.SaveChangesAsync();

            ViewBag.Message = "Saved Successfully";

            return View("NetDicsountMaster", model);

        }

        [HttpPost]

        public async Task<IActionResult> AddVoucher(VoucherMasterModel model)
        {
            var existingvoucher = await _billingsoftware.SHVoucherMaster.FindAsync(model.VoucherID);
            if(existingvoucher == null)
            {
                existingvoucher.VoucherID = model.VoucherID;
                existingvoucher.VoucherNumber = model.VoucherNumber;
                existingvoucher.VocherCost = model.VocherCost;
                existingvoucher.ExpiryDate = model.ExpiryDate;  
                existingvoucher.VocherDetails = model.VocherDetails;
                existingvoucher.LastUpdatedDate = DateTime.Now.ToString();
                existingvoucher.LastUpdatedUser = User.Claims.First().Value.ToString();
                existingvoucher.LastUpdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                _billingsoftware.Entry(existingvoucher).State = EntityState.Modified;

            }
            else
            {
                model.LastUpdatedDate = DateTime.Now.ToString();
                model.LastUpdatedUser = User.Claims.First().Value.ToString();
                model.LastUpdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                _billingsoftware.SHVoucherMaster.Add(model);


            }
            await _billingsoftware.SaveChangesAsync();

            ViewBag.Message = "Saved Successfully";

            return View("VoucherMaster", model);

        }



        [HttpPost]

        public async Task<IActionResult> AddPoints(PointsMasterModel model)
        {
            var existingpoints = await _billingsoftware.SHPointsMaster.FindAsync(model.PointsID);
            if (existingpoints == null)
            {
                existingpoints.PointsID = model.PointsID;
                existingpoints.NetPrice = model.NetPrice;
                existingpoints.LastUpdatedDate = DateTime.Now.ToString();
                existingpoints.LastUpdatedUser = User.Claims.First().Value.ToString();
                existingpoints.LastUpdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                _billingsoftware.Entry(existingpoints).State = EntityState.Modified;

            }
            else
            {
                model.LastUpdatedDate = DateTime.Now.ToString();
                model.LastUpdatedUser = User.Claims.First().Value.ToString();
                model.LastUpdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();


                _billingsoftware.SHPointsMaster.Add(model);

            }

            await _billingsoftware.SaveChangesAsync();

            ViewBag.Message = "Saved Successfully";

            return View("PointsMaster", model);

        }


        [HttpPost]

        public async Task<IActionResult> Addrack(RackMasterModel model)
        {
            var existingrack = await _billingsoftware.SHRackMaster.FindAsync(model.PartitionID , model.RackID);
            if (existingrack == null)
            {
                existingrack.RackID = model.RackID;
                existingrack.PartitionID = model.PartitionID;
                existingrack.FacilityName = model.FacilityName;
                existingrack.LastUpdatedDate = DateTime.Now.ToString();
                existingrack.LastUpdatedUser = User.Claims.First().Value.ToString();
                existingrack.LastUpdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                _billingsoftware.Entry(existingrack).State = EntityState.Modified;

            }
            else
            {
                model.LastUpdatedDate = DateTime.Now.ToString();
                model.LastUpdatedUser = User.Claims.First().Value.ToString();
                model.LastUpdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                _billingsoftware.SHRackMaster.Add(model);

            }

            await _billingsoftware.SaveChangesAsync();

            ViewBag.Message = "Saved Successfully";

            return View("RackMaster", model);

        }


        public IActionResult CategoryMasterModel()
        {
            return View();
        }

        public IActionResult ProductMasterModel()
        {
            return View();
        }

        public IActionResult CustomerBilling()
        {
            return View();
        }

        public IActionResult CustomerMaster()
        {
            return View();
        }

        public IActionResult DiscountCategoryMaster()
        {
            return View();
        }

        public IActionResult GSTMasterModel()
        {
            return View();
        }

       public IActionResult VoucherCustomerDetails()
        {
            return View();
        }

        public IActionResult NetDicsountMaster()
        {
            return View();
        }

        public IActionResult PointsMaster()
        {
            return View();
        }

        public IActionResult PointsReedemDetails()
        {
            return View();
        }

        public IActionResult RackMaster()
        {
            return View();
        }

        public IActionResult RackPatrionProduct()
        {
            return View();
        }

        public IActionResult VoucherMaster()
        {
            return View();
        }

        public IActionResult GodownModel()
        {
            return View();
        }

        public IActionResult ReportModel()
        {
            return View();
        }
    }
}
