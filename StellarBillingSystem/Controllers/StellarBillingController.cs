
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using StellarBillingSystem.Business;
using StellarBillingSystem.Context;
using StellarBillingSystem.Models;
using System.Data;

namespace HealthCare.Controllers
{
    public class StellarBillingController : Controller
    {
        private BillingContext _billingsoftware;
        private readonly IConfiguration _configuration;


        public StellarBillingController(BillingContext billingsoftware, IConfiguration configuration)
        {
            _billingsoftware = billingsoftware;
            _configuration = configuration;
        }

       

        [HttpPost]

        public async Task<IActionResult> AddCategory(CategoryMasterModel model, string buttonType)
        {
            if (buttonType == "Get")
            {
                var getcategory = await _billingsoftware.SHCategoryMaster.FirstOrDefaultAsync(x => x.CategoryID == model.CategoryID && !x.IsDelete);
                if (getcategory != null)
                {
                    return View("CategoryMaster", getcategory);
                }
                else
                {
                    CategoryMasterModel par = new CategoryMasterModel();
                    ViewBag.ErrorMessage = "No value for this Category ID";
                    return View("CategoryMaster", par);
                }
            }
            else if (buttonType == "Delete")
            {
                var categorytodelete = await _billingsoftware.SHCategoryMaster.FindAsync(model.CategoryID);
                if (categorytodelete != null)
                {
                    categorytodelete.IsDelete = true;
                    await _billingsoftware.SaveChangesAsync();

                    ViewBag.Message = "Category deleted successfully";
                    model = new CategoryMasterModel();
                    return View("CategoryMaster", model);
                }
                else
                {
                    ViewBag.ErrorMessage = "Category not found";
                    model = new CategoryMasterModel();
                    return View("CategoryMaster", model);
                }


            }

            else if (buttonType == "DeleteRetrieve")
            {
                var categorytoretrieve = await _billingsoftware.SHCategoryMaster.FindAsync(model.CategoryID);
                if (categorytoretrieve != null)
                {
                    categorytoretrieve.IsDelete = false;

                    await _billingsoftware.SaveChangesAsync();

                    model.CategoryID = categorytoretrieve.CategoryID;
                    model.CategoryName = categorytoretrieve.CategoryName;

                    ViewBag.Message = "Category retrieved successfully";
                }
                else
                {
                    ViewBag.ErrorMessage = "Category not found";
                    model = new CategoryMasterModel();
                }
                return View("CategoryMaster", model);
            }
            else if (buttonType == "save")
            {
                var existingCategory = await _billingsoftware.SHCategoryMaster.FindAsync(model.CategoryID);
                if (existingCategory != null)
                {
                    if (existingCategory.IsDelete)
                    {
                        ViewBag.ErrorMessage = "Cannot Save or Update. Category is marked as deleted.";
                        return View("CategoryMaster", model);
                    }
                    existingCategory.CategoryID = model.CategoryID;
                    existingCategory.CategoryName = model.CategoryName;
                    existingCategory.LastUpdatedDate = DateTime.Now.ToString();
                    existingCategory.LastUpdatedUser = User.Claims.First().Value.ToString();
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
            }
            model = new CategoryMasterModel();

            return View("CategoryMaster", model);
        }

        [HttpPost]

        public async Task<IActionResult> AddProduct(ProductMatserModel model, string buttonType)
        {
            BusinessClassBilling business = new BusinessClassBilling(_billingsoftware);
            ViewData["categoryid"] = business.GetCatid();
            ViewData["discountid"] = business.Getdiscountid();

            if (buttonType == "Get")
            {
                var resultpro = await _billingsoftware.SHProductMaster.FirstOrDefaultAsync(x => x.ProductID == model.ProductID && !x.IsDelete);
                if (resultpro != null)
                {
                    return View("ProductMaster", resultpro);
                }
                else
                {
                    ProductMatserModel obj = new ProductMatserModel();
                    ViewBag.NoProductMessage = "No value for this product ID";
                    return View("ProductMaster", obj);
                }
            }
            else if (buttonType == "Delete")
            {
                var productToDelete = await _billingsoftware.SHProductMaster.FindAsync(model.ProductID);
                if (productToDelete != null)
                {
                    productToDelete.IsDelete = true;
                    await _billingsoftware.SaveChangesAsync();

                    ViewBag.Message = "Product deleted successfully";

                }
                else
                {
                    ViewBag.ErrorMessage = "Product not found";

                }
                model = new ProductMatserModel();

                return View("ProductMaster", model);
            }
            else if (buttonType == "DeleteRetrieve")
            {
                var productToRetrieve = await _billingsoftware.SHProductMaster.FindAsync(model.ProductID);
                if (productToRetrieve != null)
                {
                    productToRetrieve.IsDelete = false;

                    await _billingsoftware.SaveChangesAsync();

                    model.ProductID = productToRetrieve.ProductID;
                    model.CategoryID = productToRetrieve.CategoryID;
                    model.ProductName = productToRetrieve.ProductName;
                    model.Brandname = productToRetrieve.Brandname;
                    model.Price = productToRetrieve.Price;
                    model.DiscountCategory = productToRetrieve.DiscountCategory;
                    model.TotalAmount = productToRetrieve.TotalAmount;
                    model.BarcodeId = productToRetrieve.BarcodeId;
                    model.SGST = productToRetrieve.SGST;
                    model.CGST = productToRetrieve.CGST;
                    model.OtherTax = productToRetrieve.OtherTax;

                    ViewBag.Message = "Product retrieved successfully";

                }
                else
                {
                    ViewBag.ErrorMessage = "Product not found";
                }

                return View("ProductMaster", model);
            }
            else if (buttonType == "Save")
            {


                decimal price;
                if (!decimal.TryParse(model.Price, out price))
                {
                    ViewBag.PriceErrorMessage = "Please enter a valid price.";
                    return View("ProductMaster", model);
                }

                decimal discount;
                if (!decimal.TryParse(model.DiscountCategory, out discount))
                {
                    ViewBag.DiscountErrorMessage = "Please select a valid discount category.";
                    return View("ProductMaster", model);
                }

                /* // Fetch discount price based on CategoryID
                 if (!string.IsNullOrEmpty(model.ProductID))
                 {
                     var discountCategory = await _billingsoftware.SHDiscountCategory
                         .FirstOrDefaultAsync(x => x.CategoryID == model.CategoryID);
                     if (discountCategory != null)
                     {
                         model.DiscountCategory = discountCategory.DiscountPrice;
                     }
                     else
                     {
                         model.DiscountCategory = "0"; 
                     }
                 }*/



                decimal totalAmount = price - (price * discount / 100);
                model.TotalAmount = totalAmount.ToString();

                var existingProduct = await _billingsoftware.SHProductMaster.FindAsync(model.ProductID);
                if (existingProduct != null)
                {
                    if (existingProduct.IsDelete)
                    {
                        ViewBag.ErrorMessage = "Cannot update. Product is marked as deleted.";
                        return View("ProductMaster", model);
                    }


                    existingProduct.ProductID = model.ProductID;
                    existingProduct.CategoryID = model.CategoryID;
                    existingProduct.ProductName = model.ProductName;
                    existingProduct.BarcodeId = model.BarcodeId;
                    existingProduct.Brandname = model.Brandname;
                    existingProduct.Price = model.Price;
                    existingProduct.DiscountCategory = model.DiscountCategory;
                    existingProduct.SGST = model.SGST;
                    existingProduct.CGST = model.CGST;
                    existingProduct.OtherTax = model.OtherTax;
                    existingProduct.Price = model.Price;
                    existingProduct.DiscountCategory = model.DiscountCategory;
                    existingProduct.TotalAmount = model.TotalAmount;


                    // existingProduct.TotalAmount = model.TotalAmount - (model.Price * model.Discount / 100 = model.TotalAmount);
                    existingProduct.LastUpdatedDate = DateTime.Now.ToString();
                    existingProduct.LastUpdatedUser = User.Claims.First().Value.ToString();
                    existingProduct.LastUpdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                    _billingsoftware.Entry(existingProduct).State = EntityState.Modified;
                }
                else
                {

                    // Convert strings to decimals, calculate TotalAmount, and convert back to string

                    model.LastUpdatedDate = DateTime.Now.ToString();
                    model.LastUpdatedUser = User.Claims.First().Value.ToString();
                    model.LastUpdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                    _billingsoftware.SHProductMaster.Add(model);
                }

                await _billingsoftware.SaveChangesAsync();

                ViewBag.Message = "Saved Successfully";
            }

            model = new ProductMatserModel();


            return View("ProductMaster", model);
        }






        [HttpPost]

        public async Task<IActionResult> GodDown(GodownModel model, string buttonType)
        {
            BusinessClassBilling business = new BusinessClassBilling(_billingsoftware);
            ViewData["godownproductid"] = business.GetProductid();


            if (buttonType == "DeleteRetrieve")
            {
                var screentoretrieve = await _billingsoftware.SHGodown.FindAsync(model.ProductID, model.DatefofPurchase, model.SupplierInformation);
                if (screentoretrieve != null)
                {
                    screentoretrieve.IsDelete = false;

                    await _billingsoftware.SaveChangesAsync();

                    model.ProductID = screentoretrieve.ProductID;
                    model.DatefofPurchase = screentoretrieve.DatefofPurchase;
                    model.NumberofStocks = screentoretrieve.NumberofStocks;
                    model.SupplierInformation = screentoretrieve.SupplierInformation;

                    ViewBag.retMessage = "Deleted Stock retrieved successfully";
                    return View("GodownModel", screentoretrieve);
                }
                else
                {
                    ScreenMasterModel scrn = new ScreenMasterModel();
                    ViewBag.nostockMessage = "Stock not found";
                }
                return View("GodownModel", model);
            }


            if (buttonType == "Save")
            {
                var existinggoddown = await _billingsoftware.SHGodown.FindAsync(model.ProductID, model.DatefofPurchase, model.SupplierInformation);
                if (existinggoddown != null)
                {
                    existinggoddown.ProductID = model.ProductID;
                    existinggoddown.NumberofStocks = model.NumberofStocks;
                    existinggoddown.DatefofPurchase = model.DatefofPurchase;
                    existinggoddown.SupplierInformation = model.SupplierInformation;
                    existinggoddown.IsDelete = model.IsDelete;
                    existinggoddown.LastUpdatedDate = DateTime.Now.ToString();
                    existinggoddown.LastUpdatedUser = User.Claims.First().Value.ToString();
                    existinggoddown.LastUpdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                    _billingsoftware.Entry(existinggoddown).State = EntityState.Modified;

                }

                else
                {

                    model.LastUpdatedDate = DateTime.Now.ToString();
                    model.LastUpdatedUser = User.Claims.First().Value.ToString();
                    model.LastUpdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                    _billingsoftware.SHGodown.Add(model);

                }

                await _billingsoftware.SaveChangesAsync();
                ViewBag.Message = "saved Successfully";

            }
            if (buttonType == "Delete")
            {
                var goddown = await _billingsoftware.SHGodown.FindAsync(model.ProductID, model.DatefofPurchase, model.SupplierInformation);
                if (goddown != null)
                {
                    goddown.IsDelete = true;
                    await _billingsoftware.SaveChangesAsync();

                    ViewBag.delMessage = "Stock deleted successfully";
                    model = new GodownModel();
                    return View("GodownModel", model);
                }
                else
                {
                    ViewBag.nostockMessage = "Stock not found";
                    model = new GodownModel();
                    return View("GodownModel", model);
                }
            }



            if (buttonType == "Get")
            {
                var getStock = await _billingsoftware.SHGodown.FirstOrDefaultAsync(x => x.IsDelete == false && x.ProductID == model.ProductID && x.DatefofPurchase == model.DatefofPurchase && x.SupplierInformation == model.SupplierInformation);
                if (getStock != null)
                {

                    return View("GodownModel", getStock);
                }
                else
                {
                    GodownModel role = new GodownModel();
                    ViewBag.getMessage = "No Data found";
                    return View("GodownModel", model);
                }

            }

            ViewBag.Message = "Saved Successfully";

            model = new GodownModel();

            return View("GodownModel", model);

        }



        [HttpPost]
        public async Task<IActionResult> AddCustomer(CustomerMasterModel model)
        {
            var existingCustomer = await _billingsoftware.SHCustomerMaster.FindAsync(model.MobileNumber);
            if (existingCustomer != null)
            {
                if (existingCustomer.IsDelete)
                {
                    return View("CustomerMaster", model);
                }
                existingCustomer.CustomerID = model.CustomerID;
                existingCustomer.CustomerName = model.CustomerName;
                existingCustomer.DateofBirth = model.DateofBirth;
                existingCustomer.Gender = model.Gender;
                existingCustomer.Address = model.Address;
                existingCustomer.City = model.City;
                existingCustomer.MobileNumber = model.MobileNumber;
                existingCustomer.IsDelete = model.IsDelete;
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

            await _billingsoftware.SaveChangesAsync();

            ViewBag.Message = "Saved Successfully";

            model = new CustomerMasterModel();

            return View("CustomerMaster", new CustomerMasterModel());
        }
        public async Task<IActionResult> GetCustomer(string mobileNumber)
        {
            if (string.IsNullOrEmpty(mobileNumber))
            {
                ViewBag.Message = "Mobile number is required";
                return BadRequest("Mobile number is required");
            }

            var customer = await _billingsoftware.SHCustomerMaster.FindAsync(mobileNumber);

            if (customer == null || customer.IsDelete != false)
            {
                ViewBag.ErrorMessage = "Mobile Number not found or customer is deleted";
                return View("CustomerMaster", new CustomerMasterModel()); // Return an empty model if not found or deleted
            }

            return View("CustomerMaster", customer);
        }

        public async Task<IActionResult> GetDeleteRetrieve(string mobileNumber)
        {
            if (string.IsNullOrEmpty(mobileNumber))
            {
                ViewBag.ErrorMessage = "Mobile number is required";

                return View("CustomerMaster");
            }

            var customer = await _billingsoftware.SHCustomerMaster.FindAsync(mobileNumber);
            if (customer == null)
            {
                ViewBag.ErrorMessage = "Mobile Number not found";
                return View("CustomerMaster", new CustomerMasterModel());
            }

            if (customer.IsDelete == true)
            {
                customer.IsDelete = false;
                customer.LastUpdatedDate = DateTime.Now.ToString();
                customer.LastUpdatedUser = User.Claims.First().Value.ToString();
                customer.LastUpdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                _billingsoftware.Entry(customer).State = EntityState.Modified;
                await _billingsoftware.SaveChangesAsync();
            }
            ViewBag.Message = "Retrieve Successfully";

            return View("CustomerMaster", customer);
        }



        public async Task<IActionResult> DeleteCustomer(string mobileNumber, CustomerMasterModel model)
        {
            if (string.IsNullOrEmpty(mobileNumber))
            {
                ViewBag.ErrorMessage = "Mobile Number not found";
                return View("Error", new CustomerMasterModel());
            }

            var existingCustomer = await _billingsoftware.SHCustomerMaster.FindAsync(mobileNumber);
            if (existingCustomer == null)
            {
                ViewBag.ErrorMessage = "Mobile Number not found";
                return View("CustomerMaster", new CustomerMasterModel());
            }

            existingCustomer.IsDelete = true;
            existingCustomer.LastUpdatedDate = DateTime.Now.ToString();
            existingCustomer.LastUpdatedUser = User.Claims.First().Value.ToString();
            existingCustomer.LastUpdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();

            _billingsoftware.Entry(existingCustomer).State = EntityState.Modified;

            await _billingsoftware.SaveChangesAsync();

            ViewBag.Message = "Deleted Successfully";
            model = new CustomerMasterModel();

            return View("CustomerMaster", model);
        }


        [HttpPost]
        public async Task<IActionResult> AddDiscountCategory(DiscountCategoryMasterModel model, string buttonType)
        {
            BusinessClassBilling business = new BusinessClassBilling(_billingsoftware);
            ViewData["discountcategoryid"] = business.GetcategoryID();

            if (buttonType == "Get")
            {
                var getdiscount = await _billingsoftware.SHDiscountCategory.FirstOrDefaultAsync(x => x.CategoryID == model.CategoryID && !x.IsDelete);
                if (getdiscount != null)
                {
                    return View("DiscountCategoryMaster", getdiscount);
                }
                else
                {
                    DiscountCategoryMasterModel obj = new DiscountCategoryMasterModel();
                    ViewBag.ErrorMessage = "No value for this category ID";
                    return View("DiscountCategoryMaster", obj);
                }
            }

            else if (buttonType == "Delete")
            {
                var deletetodiscount = await _billingsoftware.SHDiscountCategory.FindAsync(model.CategoryID);
                if (deletetodiscount != null)
                {
                    deletetodiscount.IsDelete = true;
                    await _billingsoftware.SaveChangesAsync();

                    ViewBag.Message = "Discount deleted successfully";
                    model = new DiscountCategoryMasterModel();

                    return View("DiscountCategoryMaster", model);
                }
                else
                {
                    DiscountCategoryMasterModel obj = new DiscountCategoryMasterModel();
                    ViewBag.ErrorMessage = "Discount Category not found";
                    return View("DiscountCategoryMaster", obj);
                }
            }

            else if (buttonType == "DeleteRetrieve")
            {
                var discountcategorytoretrieve = await _billingsoftware.SHDiscountCategory.FindAsync(model.CategoryID);
                if (discountcategorytoretrieve != null)
                {
                    discountcategorytoretrieve.IsDelete = false;

                    await _billingsoftware.SaveChangesAsync();

                    model.CategoryID = discountcategorytoretrieve.CategoryID;
                    model.DiscountPrice = discountcategorytoretrieve.DiscountPrice;

                    ViewBag.Message = "Discount category retrieved successfully";
                }
                else
                {
                    ViewBag.ErrorMessage = "Discount category not found";
                }
                return View("DiscountCategoryMaster", model);
            }

            else if (buttonType == "save")
            {

                var existingDiscountCategory = await _billingsoftware.SHDiscountCategory.FindAsync(model.CategoryID);
                if (existingDiscountCategory != null)
                {
                    if (existingDiscountCategory.IsDelete)
                    {
                        ViewBag.ErrorMessage = "Cannot update. Product is marked as deleted.";
                        return View("DiscountCategoryMaster", model);
                    }
                    existingDiscountCategory.CategoryID = model.CategoryID;
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
            }
            model = new DiscountCategoryMasterModel();


            return View("DiscountCategoryMaster", model);

        }

        [HttpPost]

        public async Task<IActionResult> AddGST(GSTMasterModel model)
        {
            var existingGst = await _billingsoftware.SHGSTMaster.FindAsync(model.TaxID);
            if (existingGst != null)
            {
                existingGst.TaxID = model.TaxID;
                existingGst.SGST = model.SGST;
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

        public async Task<IActionResult> AddNetDiscount(NetDiscountMasterModel model)
        {

            var NetID = "1";

            var existingnetdiscount = await _billingsoftware.SHNetDiscountMaster.FindAsync(NetID);
            if (existingnetdiscount != null)
            {

                existingnetdiscount.NetDiscount = model.NetDiscount;
                existingnetdiscount.LastUpdatedDate = DateTime.Now.ToString();
                existingnetdiscount.LastUpdatedUser = User.Claims.First().Value.ToString();
                existingnetdiscount.LastUpdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                _billingsoftware.Entry(existingnetdiscount).State = EntityState.Modified;
            }
            else
            {
                model.NetID = NetID;
                model.LastUpdatedDate = DateTime.Now.ToString();
                model.LastUpdatedUser = User.Claims.First().Value.ToString();
                model.LastUpdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                _billingsoftware.SHNetDiscountMaster.Add(model);

            }

            await _billingsoftware.SaveChangesAsync();

            ViewBag.Message = "Saved Successfully";
            model = new NetDiscountMasterModel();


            return View("NetDiscountMaster", model);

        }

        [HttpPost]

        public async Task<IActionResult> AddVoucher(VoucherMasterModel model)
        {
            var existingvoucher = await _billingsoftware.SHVoucherMaster.FindAsync(model.VoucherID);
            if (existingvoucher == null)
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

            var pointsID = "1";

            var existingpoints = await _billingsoftware.SHPointsMaster.FindAsync(pointsID);
            if (existingpoints != null)
            {

                existingpoints.NetPrice = model.NetPrice;
                existingpoints.NetPoints = model.NetPoints;
                existingpoints.LastUpdatedDate = DateTime.Now.ToString();
                existingpoints.LastUpdatedUser = User.Claims.First().Value.ToString();
                existingpoints.LastUpdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                _billingsoftware.Entry(existingpoints).State = EntityState.Modified;

            }
            else
            {
                model.PointsID = pointsID;
                model.LastUpdatedDate = DateTime.Now.ToString();
                model.LastUpdatedUser = User.Claims.First().Value.ToString();
                model.LastUpdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();


                _billingsoftware.SHPointsMaster.Add(model);

            }

            await _billingsoftware.SaveChangesAsync();

            ViewBag.Message = "Saved Successfully";

            model = new PointsMasterModel();



            return View("PointsMaster", model);

        }


        [HttpPost]

        public async Task<IActionResult> Addrack(RackMasterModel model)
        {
            var existingrack = await _billingsoftware.SHRackMaster.FindAsync(model.PartitionID, model.RackID);
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


        public IActionResult CategoryMaster()
        {
            CategoryMasterModel par = new CategoryMasterModel();
            return View("CategoryMaster", par);
        }

        public IActionResult ProductMaster()
        {
            BusinessClassBilling business = new BusinessClassBilling(_billingsoftware);
            ViewData["categoryid"] = business.GetCatid();
            ViewData["discountid"] = business.Getdiscountid();
            ProductMatserModel obj = new ProductMatserModel();
            return View("ProductMaster", obj);
        }

        [HttpPost]
        public async Task<IActionResult> AddRackPartition(RackPatrionProductModel model, string buttonType, RackpartitionViewModel viewmodel)
        {

            BusinessClassBilling business = new BusinessClassBilling(_billingsoftware);
            ViewData["godownproductid"] = business.GetProductid();

            if (buttonType == "Get")
            {

                var result = business.GetRackview(model.PartitionID, model.ProductID);
                if (result == null || !result.Any())
                {
                    ViewBag.GetMessage = "No data found.";
                    viewmodel.Viewrackpartition = new List<RackPatrionProductModel>();
                }
                else
                {
                    var viewModelList = result.Select(p => new RackPatrionProductModel
                    {
                        ProductID = p.ProductID,
                        PartitionID = p.PartitionID,
                        Noofitems = p.Noofitems
                    }).ToList();

                    viewmodel.Viewrackpartition = viewModelList;
                }

                return View("RackPatrionProduct", viewmodel);
            }

            else if (buttonType == "DeleteRetrieve")
            {
                var rolltoretrieve = await _billingsoftware.SHRackPartionProduct.FindAsync(model.PartitionID, model.ProductID);
                if (rolltoretrieve != null)
                {
                    rolltoretrieve.Isdelete = false;


                    await _billingsoftware.SaveChangesAsync();

                    model.PartitionID = rolltoretrieve.PartitionID;
                    model.ProductID = rolltoretrieve.ProductID;
                    model.Noofitems = rolltoretrieve.Noofitems;

                    ViewBag.retMessage = "Deleted ProductID retrieved successfully";


                }
                else
                {
                    ViewBag.NovalueMessage = "Data Not Found";
                }

                var modelsre = new RackpartitionViewModel
                {
                    Viewrackpartition = new List<RackPatrionProductModel>()
                };
                return View("RackPatrionProduct", modelsre);
            }

            else if (buttonType == "Delete")
            {
                var rolltoretrieve = await _billingsoftware.SHRackPartionProduct.FindAsync(model.PartitionID, model.ProductID);
                if (rolltoretrieve != null)
                {
                    rolltoretrieve.Isdelete = true;


                    await _billingsoftware.SaveChangesAsync();



                    ViewBag.retMessage = "Deleted ProductID  successfully";


                }
                else
                {
                    ViewBag.NovalueMessage = "Data Not Found";
                }

                var modelsdel = new RackpartitionViewModel
                {
                    Viewrackpartition = new List<RackPatrionProductModel>()
                };
                return View("RackPatrionProduct", modelsdel);
            }


            var recstockgodwomn = _billingsoftware.SHGodown.FirstOrDefault(x => x.ProductID == model.ProductID);
            if (recstockgodwomn == null)
            {
                ViewBag.entergodowmnMessage = "Please enter the Product and Stock in Godown Master";
                var modelgod = new RackpartitionViewModel
                {
                    Viewrackpartition = new List<RackPatrionProductModel>()
                };
                return View("RackPatrionProduct", modelgod);
            }

            var existingrackpartition = await _billingsoftware.SHRackPartionProduct.FindAsync(model.PartitionID, model.ProductID);
            if (existingrackpartition != null)
            {
                int newStock;
                if (int.TryParse(model.Noofitems, out newStock))
                {
                    int existingstock;
                    if (int.TryParse(existingrackpartition.Noofitems, out existingstock))
                    {
                        int totalstock = int.Parse(recstockgodwomn.NumberofStocks);
                        int currentstock = totalstock - newStock;

                        if (currentstock < 0)
                        {
                            ViewBag.stockErrorMessage = "Insufficient stock in Godown.";
                            var modelsins = new RackpartitionViewModel
                            {
                                Viewrackpartition = new List<RackPatrionProductModel>()
                            };
                            return View("RackPatrionProduct", modelsins);
                        }

                        recstockgodwomn.NumberofStocks = currentstock.ToString();
                        _billingsoftware.Entry(recstockgodwomn).State = EntityState.Modified;

                        existingrackpartition.PartitionID = model.PartitionID;
                        existingrackpartition.ProductID = model.ProductID;
                        existingrackpartition.Noofitems = model.Noofitems;
                        existingrackpartition.LastUpdatedDate = DateTime.Now.ToString();
                        existingrackpartition.LastUpdatedUser = User.Claims.First().Value.ToString();
                        existingrackpartition.LastUpdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                        _billingsoftware.Entry(existingrackpartition).State = EntityState.Modified;



                        await _billingsoftware.SaveChangesAsync();
                    }
                }
            }
            else
            {
                int newStock;
                if (int.TryParse(model.Noofitems, out newStock))
                {
                    var recstock = _billingsoftware.SHGodown.FirstOrDefault(x => x.ProductID == model.ProductID);
                    if (recstock != null)
                    {
                        int totalstock = int.Parse(recstock.NumberofStocks);
                        int currentstock = totalstock - newStock;

                        if (currentstock < 0)
                        {
                            ViewBag.stockErrorMessage = "Insufficient stock in Godown.";
                            var modelsins = new RackpartitionViewModel
                            {
                                Viewrackpartition = new List<RackPatrionProductModel>()
                            };

                            return View("RackPatrionProduct", modelsins);
                        }

                        recstock.NumberofStocks = currentstock.ToString();
                        _billingsoftware.Entry(recstock).State = EntityState.Modified;
                    }

                    model.LastUpdatedDate = DateTime.Now.ToString();
                    model.LastUpdatedUser = User.Claims.First().Value.ToString();
                    model.LastUpdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                    _billingsoftware.SHRackPartionProduct.Add(model);

                    await _billingsoftware.SaveChangesAsync();
                }
            }

            ViewBag.Message = "Saved Successfully";

            //Repopulate the table after save 

            var updatedResult = business.GetRackview(model.PartitionID, model.ProductID);
            var updatedViewModelList = updatedResult.Select(p => new RackPatrionProductModel
            {
                ProductID = p.ProductID,
                PartitionID = p.PartitionID,
                Noofitems = p.Noofitems
            }).ToList();

            var models = new RackpartitionViewModel
            {
                Viewrackpartition = new List<RackPatrionProductModel>()
            };
            return View("RackPatrionProduct", models);
        }


        // Edit Function for RackPartition Table
        public async Task<IActionResult> Edit(string partitionID, string productID)
        {
            BusinessClassBilling business = new BusinessClassBilling(_billingsoftware);
            ViewData["godownproductid"] = business.GetProductid();

            var RackEdit = await _billingsoftware.SHRackPartionProduct.FindAsync(partitionID, productID);
            if (RackEdit == null)
            {
                ViewBag.NovalueMessage = "No Data Found";
            }

            var rackviewTable = new RackpartitionViewModel
            {
                PartitionID = RackEdit.PartitionID,
                ProductID = RackEdit.ProductID,
                Noofitems = RackEdit.Noofitems,

                Viewrackpartition = new List<RackPatrionProductModel>()

            };


            var result = business.GetRackview(RackEdit.PartitionID, RackEdit.ProductID);
            if (result != null && result.Any())
            {
                var viewModelList = result.Select(p => new RackPatrionProductModel
                {
                    PartitionID = p.PartitionID,
                    ProductID = p.ProductID,
                    Noofitems = p.Noofitems
                }).ToList();

                rackviewTable.Viewrackpartition = viewModelList;
            }


            return View("RackPatrionProduct", rackviewTable);


        }

        // Delete Function for Rack PArtition
        public async Task<IActionResult> Delete(string partitionID, string productID, RackpartitionViewModel viewmodel, RackPatrionProductModel model)
        {
            BusinessClassBilling business = new BusinessClassBilling(_billingsoftware);
            ViewData["godownproductid"] = business.GetProductid();


            var rackDel = await _billingsoftware.SHRackPartionProduct.FindAsync(partitionID, productID);
            if (rackDel != null)
            {
                rackDel.Isdelete = true;
                await _billingsoftware.SaveChangesAsync();
            }
            ViewBag.Delete = "Deleted  Successfully.";

            model = new RackPatrionProductModel();
            viewmodel.Viewrackpartition = new List<RackPatrionProductModel>();

            return View("RackPatrionProduct", viewmodel);
        }

        // staff reg
        [HttpPost]
        public async Task<IActionResult> AddStaff(StaffAdminModel model, string buttontype)
        {
            BusinessClassBilling Busbill = new BusinessClassBilling(_billingsoftware);
            ViewData["resoruseid"] = Busbill.GetResourceid();


            if (buttontype == "Get")
            {
                var getstaff = await _billingsoftware.SHStaffAdmin.FirstOrDefaultAsync(x => x.StaffID == model.StaffID && x.IsDelete == false);
                if (getstaff != null)
                {
                    return View("StaffAdmin", getstaff);
                }
                else
                {
                    StaffAdminModel par = new StaffAdminModel();
                    ViewBag.getMessage = "No Data found for this Staff ID";
                    return View("StaffAdmin", par);
                }
            }
            else if (buttontype == "Delete")
            {
                var stafftodelete = await _billingsoftware.SHStaffAdmin.FindAsync(model.StaffID);
                if (stafftodelete != null)
                {
                    stafftodelete.IsDelete = true;
                    await _billingsoftware.SaveChangesAsync();

                    ViewBag.delMessage = "StaffID deleted successfully";
                    model = new StaffAdminModel();
                    return View("StaffAdmin", model);
                }
                else
                {
                    ViewBag.delnoMessage = "StaffID not found";
                    model = new StaffAdminModel();
                    return View("StaffAdmin", model);
                }

            }

            else if (buttontype == "DeleteRetrieve")
            {
                var stafftoretrieve = await _billingsoftware.SHStaffAdmin.FindAsync(model.StaffID);
                if (stafftoretrieve != null)
                {
                    stafftoretrieve.IsDelete = false;

                    await _billingsoftware.SaveChangesAsync();

                    model.StaffID = stafftoretrieve.StaffID;
                    model.FullName = stafftoretrieve.FullName;
                    model.ResourceTypeID = stafftoretrieve.ResourceTypeID;
                    model.FirstName = stafftoretrieve.FirstName;
                    model.LastName = stafftoretrieve.LastName;
                    model.Initial = stafftoretrieve.Initial;
                    model.Prefix = stafftoretrieve.Prefix;
                    model.PhoneNumber = stafftoretrieve.PhoneNumber;
                    model.DateofBirth = stafftoretrieve.DateofBirth;
                    model.Age = stafftoretrieve.Age;
                    model.Gender = stafftoretrieve.Gender;
                    model.Address1 = stafftoretrieve.Address1;
                    model.City = stafftoretrieve.City;
                    model.State = stafftoretrieve.State;
                    model.Pin = stafftoretrieve.Pin;
                    model.EmailId = stafftoretrieve.EmailId;
                    model.Nationality = stafftoretrieve.Nationality;
                    model.UserName = stafftoretrieve.UserName;
                    model.Password = stafftoretrieve.Password;
                    model.IdProofId = stafftoretrieve.IdProofId;
                    model.IdProofName = stafftoretrieve.IdProofName;

                    ViewBag.retMessage = "Deleted StaffID retrieved successfully";
                }
                else
                {
                    ViewBag.noretMessage = "StaffID not found";
                }
                return View("StaffAdmin", model);
            }


            var existingStaffAdmin = await _billingsoftware.SHStaffAdmin.FindAsync(model.StaffID);


            if (string.IsNullOrWhiteSpace(model.UserName) || string.IsNullOrWhiteSpace(model.Password))
            {
                ViewBag.validateMessage = "Username and Password are required.";
                return View("StaffAdmin", model);
            }

            if (existingStaffAdmin != null)
            {
                existingStaffAdmin.StaffID = model.StaffID;
                existingStaffAdmin.ResourceTypeID = model.ResourceTypeID;
                existingStaffAdmin.FirstName = model.FirstName;
                existingStaffAdmin.LastName = model.LastName;
                existingStaffAdmin.Initial = model.Initial;
                existingStaffAdmin.Prefix = model.Prefix;
                existingStaffAdmin.Age = model.Age;
                existingStaffAdmin.DateofBirth = model.DateofBirth;
                existingStaffAdmin.EmailId = model.EmailId;
                existingStaffAdmin.Address1 = model.Address1;
                existingStaffAdmin.City = model.City;
                existingStaffAdmin.State = model.State;
                existingStaffAdmin.Pin = model.Pin;
                existingStaffAdmin.PhoneNumber = model.PhoneNumber;
                existingStaffAdmin.EmailId = model.EmailId;
                existingStaffAdmin.Nationality = model.Nationality;
                existingStaffAdmin.UserName = model.UserName;
                existingStaffAdmin.Password = model.Password;
                existingStaffAdmin.IdProofId = model.IdProofId;
                existingStaffAdmin.IdProofName = model.IdProofName;
                existingStaffAdmin.LastupdatedDate = DateTime.Now.ToString();
                existingStaffAdmin.LastupdatedUser = User.Claims.First().Value.ToString();
                existingStaffAdmin.LastUpdatedMachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                _billingsoftware.Entry(existingStaffAdmin).State = EntityState.Modified;

            }
            else
            {

                model.LastupdatedDate = DateTime.Now.ToString();
                model.LastupdatedUser = User.Claims.First().Value.ToString();
                model.LastUpdatedMachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                _billingsoftware.SHStaffAdmin.Add(model);
            }
            await _billingsoftware.SaveChangesAsync();

            ViewBag.Message = "Saved Successfully";

            model = new StaffAdminModel();
            return View("StaffAdmin", model);


        }








        public async Task<IActionResult> AddResourceType(ResourceTypeMasterModel model, string buttontype)
        {

            if (buttontype == "Get")
            {
                var getres = await _billingsoftware.SHresourceType.FirstOrDefaultAsync(x => x.ResourceTypeID == model.ResourceTypeID && x.IsDelete == false);
                if (getres != null)
                {
                    return View("ResourceTypeMaster", getres);
                }
                else
                {
                    ResourceTypeMasterModel res = new ResourceTypeMasterModel();
                    ViewBag.getMessage = "No Data found for this ResourceTypeID";
                    return View("ResourceTypeMaster", res);
                }
            }
            else if (buttontype == "Delete")
            {
                var restodelete = await _billingsoftware.SHresourceType.FindAsync(model.ResourceTypeID);
                if (restodelete != null)
                {
                    restodelete.IsDelete = true;
                    await _billingsoftware.SaveChangesAsync();

                    ViewBag.delMessage = "ResourceTypeID deleted successfully";
                    model = new ResourceTypeMasterModel();

                    return View("ResourceTypeMaster", model);
                }
                else
                {
                    ResourceTypeMasterModel res = new ResourceTypeMasterModel();
                    ViewBag.delnoMessage = "ResourceTypeID not found";
                    return View("ResourceTypeMaster", res);
                }

            }

            else if (buttontype == "DeleteRetrieve")
            {
                var restoretrieve = await _billingsoftware.SHresourceType.FindAsync(model.ResourceTypeID);
                if (restoretrieve != null)
                {
                    restoretrieve.IsDelete = false;

                    await _billingsoftware.SaveChangesAsync();

                    model.ResourceTypeName = restoretrieve.ResourceTypeName;
                    model.ResourceTypeID = restoretrieve.ResourceTypeID;

                    ViewBag.retMessage = "Deleted ResourceTypeID retrieved successfully";
                }
                else
                {
                    ResourceTypeMasterModel res = new ResourceTypeMasterModel();
                    ViewBag.noretMessage = "ResourceTypeID not found";
                    return View("ResourceTypeMaster", res);
                }
                return View("ResourceTypeMaster", model);
            }


            var existingres = await _billingsoftware.SHresourceType.FindAsync(model.ResourceTypeID);

            if (existingres != null)
            {
                existingres.ResourceTypeName = model.ResourceTypeName;
                existingres.ResourceTypeID = model.ResourceTypeID;
                existingres.lastUpdatedDate = DateTime.Now.ToString();
                existingres.lastUpdatedUser = User.Claims.First().Value.ToString();
                existingres.lastUpdatedMachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                _billingsoftware.Entry(existingres).State = EntityState.Modified;

            }
            else
            {

                model.lastUpdatedDate = DateTime.Now.ToString();
                model.lastUpdatedUser = User.Claims.First().Value.ToString();
                model.lastUpdatedMachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                _billingsoftware.SHresourceType.Add(model);
            }
            await _billingsoftware.SaveChangesAsync();

            ViewBag.Message = "Saved Successfully";
            model = new ResourceTypeMasterModel();

            return View("ResourceTypeMaster", model);


        }


        public async Task<IActionResult> AddRoleaccess(RoleAccessModel model, string buttontype)
        {

            BusinessClassBilling businessbill = new BusinessClassBilling(_billingsoftware);
            ViewData["screenid"] = businessbill.GetScreenid();
            ViewData["rollid"] = businessbill.RollAccessType();
            ViewData["staffid"] = businessbill.GetStaffID();

            if (buttontype == "Get")
            {
                var getrol = await _billingsoftware.SHRoleaccessModel.FirstOrDefaultAsync(x => x.RollID == model.RollID && x.ScreenID == model.ScreenID && x.Isdelete == false);
                if (getrol != null)
                {
                    return View("RoleAccess", getrol);
                }
                else
                {
                    RoleAccessModel role = new RoleAccessModel();
                    ViewBag.getMessage = "No Data found for this RollID";
                    return View("RoleAccess", role);
                }
            }
            else if (buttontype == "Delete")
            {
                var roletodelete = await _billingsoftware.SHRoleaccessModel.FindAsync(model.RollID, model.ScreenID);
                if (roletodelete != null)
                {
                    roletodelete.Isdelete = true;
                    await _billingsoftware.SaveChangesAsync();

                    ViewBag.delMessage = "RollID deleted successfully";
                    model = new RoleAccessModel();

                    return View("RoleAccess", model);
                }
                else
                {
                    ViewBag.delnoMessage = "RollID not found";
                    model = new RoleAccessModel();

                    return View("RoleAccess", model);
                }

            }

            else if (buttontype == "DeleteRetrieve")
            {
                var roltoretrieve = await _billingsoftware.SHRoleaccessModel.FindAsync(model.RollID, model.ScreenID);
                if (roltoretrieve != null)
                {
                    roltoretrieve.Isdelete = false;

                    await _billingsoftware.SaveChangesAsync();

                    model.RollID = roltoretrieve.RollID;
                    model.ScreenID = roltoretrieve.ScreenID;
                    model.Access = roltoretrieve.Access;
                    model.Authorized = roltoretrieve.Authorized;

                    ViewBag.retMessage = "Deleted RollID retrieved successfully";
                }
                else
                {
                    ViewBag.noretMessage = "RollID not found";
                }
                return View("RoleAccess", model);
            }


            var existingrole = await _billingsoftware.SHRoleaccessModel.FindAsync(model.RollID, model.ScreenID);

            if (existingrole != null)
            {
                existingrole.RollID = model.RollID;
                existingrole.ScreenID = model.ScreenID;
                existingrole.Access = model.Access;
                existingrole.Authorized = model.Authorized;
                existingrole.lastUpdatedDate = DateTime.Now.ToString();
                existingrole.lastUpdatedUser = User.Claims.First().Value.ToString();
                existingrole.lastUpdatedMachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                _billingsoftware.Entry(existingrole).State = EntityState.Modified;

            }
            else
            {

                model.lastUpdatedDate = DateTime.Now.ToString();
                model.lastUpdatedUser = User.Claims.First().Value.ToString();
                model.lastUpdatedMachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                _billingsoftware.SHRoleaccessModel.Add(model);
            }
            await _billingsoftware.SaveChangesAsync();

            ViewBag.Message = "Saved Successfully";
            model = new RoleAccessModel();

            return View("RoleAccess", model);
        }

        public async Task<IActionResult> AddRollmaster(RollAccessMaster model, string buttontype, List<string> SelectedRollNames)
        {
            BusinessClassBilling Busbill = new BusinessClassBilling(_billingsoftware);
            ViewData["rollid"] = Busbill.RollAccessType();
            ViewData["staffid"] = Busbill.GetStaffID();


            if (buttontype == "Get")
            {
                var getroll = await _billingsoftware.SHrollaccess.FirstOrDefaultAsync(x => x.StaffID == model.StaffID && x.IsDelete == false);
                if (getroll != null)
                {
                    return View("RollAccessMaster", getroll);
                }
                else
                {
                    RollAccessMaster roll = new RollAccessMaster();
                    ViewBag.getMessage = "No Data found for this RollID";
                    return View("RollAccessMaster", roll);
                }
            }
            else if (buttontype == "Delete")
            {
                foreach (var rollName in SelectedRollNames)
                {

                    var rolltodelete = await _billingsoftware.SHrollaccess.FindAsync(model.StaffID, rollName);
                    if (rolltodelete != null)
                    {
                        rolltodelete.IsDelete = true;
                        await _billingsoftware.SaveChangesAsync();

                        ViewBag.delMessage = "RollID deleted successfully";
                        model = new RollAccessMaster();


                        return View("RollAccessMaster", model);
                    }

                    else
                    {
                        ViewBag.delnoMessage = "RollID not found";
                        model = new RollAccessMaster();
                        return View("RollAccessMaster", model);
                    }
                }


            }

            else if (buttontype == "DeleteRetrieve")
            {
                foreach (var rollName in SelectedRollNames)
                {
                    var rolltoretrieve = await _billingsoftware.SHrollaccess.FindAsync(model.StaffID, rollName);
                    if (rolltoretrieve != null)
                    {
                        rolltoretrieve.IsDelete = false;

                        await _billingsoftware.SaveChangesAsync();

                        model.RollID = rolltoretrieve.RollID;
                        model.StaffID = rolltoretrieve.StaffID;

                        ViewBag.retMessage = "Deleted RollID retrieved successfully";
                        return View("RollAccessMaster", rolltoretrieve);
                    }
                    else
                    {
                        ViewBag.noretMessage = "RollID not found";
                    }
                }
                ViewBag.noretMessage = "RollID not found";
                return View("RollAccessMaster", model);
            }


            else if (buttontype == "Save")
            {
                foreach (var rollName in SelectedRollNames)
                {

                    var existingroll = await _billingsoftware.SHrollaccess.FindAsync(model.StaffID, rollName);

                    if (existingroll != null)
                    {
                        var duplicateRoll = _billingsoftware.SHrollaccess
                     .FirstOrDefault(x => x.RollID == model.RollID && x.StaffID != model.StaffID);

                        if (duplicateRoll == null)
                        {
                            ViewBag.ErrorMessage = "RollID already exists Cannot update same ID";
                            return View("RollAccessMaster", model);
                        }

                        existingroll.RollID = model.RollID;
                        existingroll.StaffID = model.StaffID;
                        existingroll.LastupdatedDate = DateTime.Now.ToString();
                        existingroll.Lastupdateduser = User.Claims.First().Value.ToString();
                        existingroll.LastupdatedMachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                        _billingsoftware.Entry(existingroll).State = EntityState.Modified;

                    }
                    else
                    {
                        var newAccess = new RollAccessMaster
                        {
                            StaffID = model.StaffID,
                            RollID = rollName,
                            LastupdatedDate = DateTime.Now.ToString(),
                            Lastupdateduser = User.Claims.First().Value.ToString(),
                            LastupdatedMachine = Request.HttpContext.Connection.RemoteIpAddress.ToString()
                        };
                        _billingsoftware.SHrollaccess.Add(newAccess);

                    }
                }
                await _billingsoftware.SaveChangesAsync();

                ViewBag.Message = "Saved Successfully";
            }
            model = new RollAccessMaster();
            return View("RollAccessMaster", model);


        }

        public async Task<IActionResult> AddRolltype(RollTypeMaster model, string buttontype)
        {

            if (buttontype == "Get")
            {
                var getrolltype = await _billingsoftware.SHrollType.FirstOrDefaultAsync(x => x.RollID == model.RollID && x.IsDelete == false);
                if (getrolltype != null)
                {
                    return View("RollTypeMaster", getrolltype);
                }
                else
                {
                    RollTypeMaster rolltype = new RollTypeMaster();
                    ViewBag.getMessage = "No Data found for this RollID";
                    return View("RollTypeMaster", rolltype);
                }
            }
            else if (buttontype == "Delete")
            {
                var rolltypetodelete = await _billingsoftware.SHrollType.FindAsync(model.RollID);
                if (rolltypetodelete != null)
                {
                    rolltypetodelete.IsDelete = true;
                    await _billingsoftware.SaveChangesAsync();

                    ViewBag.delMessage = "RollID deleted successfully";
                    model = new RollTypeMaster();
                    return View("RollTypeMaster", model);
                }
                else
                {
                    RollTypeMaster rolltype = new RollTypeMaster();
                    ViewBag.delnoMessage = "RollID not found";

                    return View("RollTypeMaster", rolltype);
                }

            }

            else if (buttontype == "DeleteRetrieve")
            {
                var rolltypetoretrieve = await _billingsoftware.SHrollType.FindAsync(model.RollID);
                if (rolltypetoretrieve != null)
                {
                    rolltypetoretrieve.IsDelete = false;

                    await _billingsoftware.SaveChangesAsync();

                    model.RollID = rolltypetoretrieve.RollID;
                    model.RollName = rolltypetoretrieve.RollName;

                    ViewBag.retMessage = "Deleted RollID retrieved successfully";
                }
                else
                {

                    ViewBag.noretMessage = "RollID not found";
                }
                return View("RollTypeMaster", model);
            }


            var existingrolltype = await _billingsoftware.SHrollType.FindAsync(model.RollID);

            if (existingrolltype != null)
            {
                existingrolltype.RollID = model.RollID;
                existingrolltype.RollName = model.RollName;
                existingrolltype.LastupdatedDate = DateTime.Now.ToString();
                existingrolltype.LastupdatedUser = User.Claims.First().Value.ToString();
                existingrolltype.LastupdatedMachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                _billingsoftware.Entry(existingrolltype).State = EntityState.Modified;

            }
            else
            {

                model.LastupdatedDate = DateTime.Now.ToString();
                model.LastupdatedUser = User.Claims.First().Value.ToString();
                model.LastupdatedMachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                _billingsoftware.SHrollType.Add(model);
            }
            await _billingsoftware.SaveChangesAsync();

            ViewBag.Message = "Saved Successfully";

            model = new RollTypeMaster();
            return View("RollTypeMaster", model);

        }

        public async Task<IActionResult> Addscreen(ScreenMasterModel model, string buttontype)
        {

            BusinessClassBilling businessbill = new BusinessClassBilling(_billingsoftware);
            ViewData["screenname"] = businessbill.Screenname();

            if (buttontype == "Get")
            {
                var getscreen = await _billingsoftware.SHScreenMaster.FirstOrDefaultAsync(x => x.ScreenId == model.ScreenId && x.IsDelete == false);
                if (getscreen != null)
                {
                    return View("ScreenMaster", getscreen);
                }
                else
                {
                    ScreenMasterModel screen = new ScreenMasterModel();
                    ViewBag.getMessage = "No Data found for this ScreenId";
                    return View("ScreenMaster", screen);
                }

            }

            else if (buttontype == "Delete")
            {
                var screentodelete = await _billingsoftware.SHScreenMaster.FindAsync(model.ScreenId);

                if (screentodelete != null)
                {
                    screentodelete.IsDelete = true;
                    await _billingsoftware.SaveChangesAsync();
                    ViewBag.delMessage = "ScreenId deleted successfully";
                    model = new ScreenMasterModel();

                    return View("ScreenMaster", model);
                }
                else
                {

                    ScreenMasterModel scrn = new ScreenMasterModel();
                    ViewBag.delnoMessage = "ScreenId not found";
                    return View("ScreenMaster", scrn);
                }

            }

            else if (buttontype == "DeleteRetrieve")
            {
                var screentoretrieve = await _billingsoftware.SHScreenMaster.FindAsync(model.ScreenId);
                if (screentoretrieve != null)
                {
                    screentoretrieve.IsDelete = false;

                    await _billingsoftware.SaveChangesAsync();

                    model.ScreenId = screentoretrieve.ScreenId;
                    model.ScreenName = screentoretrieve.ScreenName;

                    ViewBag.retMessage = "Deleted ScreenId retrieved successfully";
                }
                else
                {
                    ScreenMasterModel scrn = new ScreenMasterModel();
                    ViewBag.noretMessage = "ScreenId not found";
                }
                return View("ScreenMaster", model);
            }


            var existingscreen = await _billingsoftware.SHScreenMaster.FindAsync(model.ScreenId);

            if (existingscreen != null)
            {
                existingscreen.ScreenId = model.ScreenId;
                existingscreen.ScreenName = model.ScreenName;
                existingscreen.lastUpdatedDate = DateTime.Now.ToString();
                existingscreen.lastUpdatedUser = User.Claims.First().Value.ToString();
                existingscreen.lastUpdatedMachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                _billingsoftware.Entry(existingscreen).State = EntityState.Modified;

            }
            else
            {

                model.lastUpdatedDate = DateTime.Now.ToString();
                model.lastUpdatedUser = User.Claims.First().Value.ToString();
                model.lastUpdatedMachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                _billingsoftware.SHScreenMaster.Add(model);
            }
            await _billingsoftware.SaveChangesAsync();

            ViewBag.Message = "Saved Successfully";
            model = new ScreenMasterModel();
            return View("ScreenMaster", model);

        }

        //customer Billing


        [HttpPost]
        public IActionResult getproductlist(ProductSelectModel model, string billid, string BillID, string buttonType, string SelectedProductID, string Quantity, string productid, string productname, string unitprice, string billdate, string customernumber)
        {
            if (string.IsNullOrEmpty(model.ProductID) && string.IsNullOrEmpty(model.BarcodeID))
            {
                ViewBag.ValidationMessage = "Please enter either ProductID or BarcodeID.";
                return View("ProductList", model);
            }

            if (buttonType == "Search")
            {
                var productList = (from product in _billingsoftware.SHProductMaster
                                   join rack in _billingsoftware.SHRackPartionProduct
                                   on product.ProductID equals rack.ProductID
                                   where (product.ProductID.Contains(model.ProductID) || product.BarcodeId.Contains(model.BarcodeID))
                                   select new { product, rack })
                      .AsEnumerable() // Switch to client-side evaluation
                      .Where(pr => int.Parse(pr.rack.Noofitems) > 0) // Perform the int.Parse on the client side
                      .Select(pr => pr.product)
                      .ToList();

                if (productList.Count == 0)
                {
                    ViewBag.NotfoundMessage = "No products found.";
                }

                model.Viewproductlist = productList;
                model.ProductID = model.ProductID;
                model.BarcodeID = model.BarcodeID;
                return View("ProductList", model);

            }
            else if (buttonType == "Load")
            {

                if (string.IsNullOrEmpty(SelectedProductID))
                {
                    ViewBag.notselect = "Please select a product.";
                    model.Viewproductlist = _billingsoftware.SHProductMaster
                        .Where(p => p.ProductID.Contains(model.ProductID) || p.BarcodeId.Contains(model.BarcodeID))
                        .ToList();
                    return View("ProductList", model);
                }

                int quantity;
                if (!int.TryParse(Quantity, out quantity) || quantity <= 0) // Parse and check if Quantity is valid
                {
                    ViewBag.enterquantity = "Please enter a valid quantity.";
                    model.Viewproductlist = _billingsoftware.SHProductMaster
                        .Where(p => p.ProductID.Contains(model.ProductID) || p.BarcodeId.Contains(model.BarcodeID))
                        .ToList();
                    return View("ProductList", model);
                }

                var selectedProduct = _billingsoftware.SHProductMaster.FirstOrDefault(p => p.ProductID == SelectedProductID);
                if (selectedProduct != null)
                {
                    var existingDetail = _billingsoftware.SHbilldetails.FirstOrDefault(b =>
               b.BillID == TempData.Peek("BillID").ToString() && b.ProductID == selectedProduct.ProductID);

                    if (existingDetail != null)
                    {

                        existingDetail.Quantity = Quantity;
                    }
                    else
                    {

                        var billDetail = new BillingDetailsModel
                        {
                            BillID = TempData.Peek("BillID").ToString(),
                            BillDate = TempData.Peek("BillDate").ToString(),
                            CustomerNumber = TempData.Peek("CustomerNumber").ToString(),
                            ProductID = selectedProduct.ProductID,
                            ProductName = selectedProduct.ProductName,
                            Price = selectedProduct.TotalAmount,
                            Quantity = Quantity,
                        };

                        _billingsoftware.SHbilldetails.Add(billDetail);

                    }

                    _billingsoftware.SaveChanges();
                    return RedirectToAction("CustomerBilling", new
                    {
                        billid = TempData.Peek("BillID").ToString(),
                        billdate = TempData.Peek("BillDate").ToString(),
                        customernumber = TempData.Peek("CustomerNumber").ToString(),
                        productid = selectedProduct.ProductID,
                        productname = selectedProduct.ProductName,
                        price = selectedProduct.TotalAmount,
                        quantity = Quantity,


                    });
                }
            }


            return View("ProductList", model);
        }

        [HttpPost]

        public async Task<IActionResult> getCustomerBill(BillProductlistModel model, string buttonType, string BillID, string BillDate, string CustomerNumber, string TotalPrice, BillingMasterModel masterModel, BillingDetailsModel detailModel)
        {
            //Code for print the Bill 
            if (buttonType == "Download Bill")
            {
                String Query = "Select SD.BillID,Convert(varchar(10),SD.BillDate,101) as BillDate,SD.ProductID,Sp.ProductName, SD.Price,SD.Quantity,SD.CustomerNumber as CustomerName, SD.CustomerNumber,\r\nSD.TotalDiscount,SD.Totalprice as DetailTotalprice, SB.Totalprice as MasterTotalprice  from SHbilldetails SD inner join SHbillmaster SB \r\non SD.BillID= SB.BillID\r\ninner join SHProductMaster SP\r\non SD.ProductID = sp.ProductID\r\n where sd.IsDelete=0 AND sd.BillID ='" + BillID + "'";

                var Table = BusinessClassCommon.DataTable(_billingsoftware, Query);

                BusinessClassBilling objbilling = new BusinessClassBilling(_billingsoftware);

                return File(objbilling.PrintBillDetails(Table), "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "Bill_" + TempData["BillID"] + ".docx");
            }

            if (buttonType == "Payment")
            {
                return RedirectToAction("PaymentScreen", new { BillID = model.BillID });
            }


            if (buttonType == "Get Product")
            {

                TempData["BillID"] = BillID;
                TempData["BillDate"] = BillDate;
                TempData["CustomerNumber"] = CustomerNumber;

                return RedirectToAction("ProductList", new { BillID = model.BillID, BillDate = model.BillDate, CustomerNumber = model.CustomerNumber });
            }

            if (buttonType == "Get")
            {
                var billID = model.BillID;
                var billDate = model.BillDate;
                var customerNumber = model.CustomerNumber;

                var updatedMasterex = _billingsoftware.SHbillmaster.FirstOrDefault(m =>
                    m.BillID == billID && m.BillDate == billDate && m.CustomerNumber == customerNumber && m.IsDelete == false);

                if (updatedMasterex != null)
                {
                    ViewBag.TotalPrice = updatedMasterex.Totalprice;
                    ViewBag.TotalDiscount = updatedMasterex.TotalDiscount;
                    ViewBag.NetPrice = updatedMasterex.NetPrice;

                    var exbillingDetails = _billingsoftware.SHbilldetails
                        .Where(d => d.BillID == billID)
                        .ToList();

                    // Prepare the view model to pass to the view
                    var viewModel = new BillProductlistModel
                    {
                        MasterModel = new BillingMasterModel
                        {
                            BillID = billID,
                            BillDate = updatedMasterex.BillDate,
                            CustomerNumber = updatedMasterex.CustomerNumber
                        },
                        Viewbillproductlist = exbillingDetails,
                        BillID = billID,
                        BillDate = updatedMasterex.BillDate,
                        CustomerNumber = updatedMasterex.CustomerNumber
                    };

                    return View("CustomerBilling", viewModel);
                }
                else
                {

                    BillProductlistModel promodel = new BillProductlistModel();
                    ViewBag.Getnotfound = "No Data Found For This ID";

                    return View("CustomerBilling", promodel);
                }


            }

            if (buttonType == "Delete Bill")
            {
                var parameter = new[]
         {
    new SqlParameter("@BillID", masterModel.BillID),
    new SqlParameter("@BillDate", DBNull.Value), // Set to DBNull.Value to indicate empty/null
    new SqlParameter("@CustomerNumber", DBNull.Value),
    new SqlParameter("@TotalPrice", DBNull.Value),
    new SqlParameter("@TotalDiscount", DBNull.Value),
    new SqlParameter("@NetPrice", DBNull.Value),
    new SqlParameter("@IsDelete", "Y"), // Assuming isDeleteValue is set to 'Y'
    new SqlParameter("@LastUpdatedUser", DBNull.Value),
    new SqlParameter("@LastUpdatedDate", DBNull.Value),
    new SqlParameter("@LastUpdatedMachine", DBNull.Value),
    new SqlParameter("@ProductID", DBNull.Value),
    new SqlParameter("@ProductName", DBNull.Value),
    new SqlParameter("@Discount", DBNull.Value),
    new SqlParameter("@Price", DBNull.Value),
    new SqlParameter("@Quantity", DBNull.Value),
   };

                await _billingsoftware.Database.ExecuteSqlRawAsync("EXEC InsertBillProduct @BillID, @BillDate, @CustomerNumber, @TotalPrice, @TotalDiscount, @NetPrice, @IsDelete, @LastUpdatedUser, @LastUpdatedDate, @LastUpdatedMachine, @ProductID, @ProductName, @Discount, @Price, @Quantity", parameter);



                ViewBag.DelMessage = "Deleted Bill Successfully";
                return View("CustomerBilling", model);

            }


            var isDeleteValue = (object)masterModel.IsDelete ?? DBNull.Value;

            var parameters = new[]
   {
        new SqlParameter("@BillID", masterModel.BillID),
        new SqlParameter("@BillDate", masterModel.BillDate),
        new SqlParameter("@CustomerNumber", masterModel.CustomerNumber),
        new SqlParameter("@TotalPrice", masterModel.Totalprice ?? (object)DBNull.Value),
        new SqlParameter("@TotalDiscount", masterModel.TotalDiscount ?? (object)DBNull.Value),
        new SqlParameter("@NetPrice", masterModel.NetPrice ?? (object)DBNull.Value),
       new SqlParameter("@IsDelete", "N"),
        new SqlParameter("@LastUpdatedUser", User.Claims.First().Value.ToString()),
        new SqlParameter("@LastUpdatedDate", DateTime.Now.ToString()),
        new SqlParameter("@LastUpdatedMachine", Request.HttpContext.Connection.RemoteIpAddress.ToString()),
        new SqlParameter("@ProductID", detailModel.ProductID),
        new SqlParameter("@ProductName", detailModel.ProductName ?? (object)DBNull.Value),
        new SqlParameter("@Discount", detailModel.Discount ?? (object)DBNull.Value),
        new SqlParameter("@Price", detailModel.Price ?? (object)DBNull.Value),
        new SqlParameter("@Quantity", detailModel.Quantity ?? (object)DBNull.Value),

    };
            await _billingsoftware.Database.ExecuteSqlRawAsync("EXEC InsertBillProduct @BillID, @BillDate, @CustomerNumber, @TotalPrice,@TotalDiscount,@NetPrice,@IsDelete,@LastUpdatedUser, @LastUpdatedDate, @LastUpdatedMachine, @ProductID, @ProductName, @Discount, @Price, @Quantity", parameters);
            ViewBag.SaveMessage = "save successfully";

            var updatedMaster = await _billingsoftware.SHbillmaster
       .Where(m => m.BillID == masterModel.BillID)
       .FirstOrDefaultAsync();

            if (updatedMaster != null)
            {
                ViewBag.TotalPrice = updatedMaster.Totalprice;
                ViewBag.TotalDiscount = updatedMaster.TotalDiscount;
                ViewBag.NetPrice = updatedMaster.NetPrice;
            }


            var billingDetails = await _billingsoftware.SHbilldetails
       .Where(d => d.BillID == masterModel.BillID)
       .ToListAsync();

            model.MasterModel = updatedMaster;
            model.Viewbillproductlist = billingDetails;


            ViewBag.Message = "Saved Successfully";
            return View("CustomerBilling", model);
        }








        public IActionResult ProductList(string BillID, string BillDate, string CustomerNumber, string PaymentId)
        {
            if (string.IsNullOrEmpty(BillID) && TempData["BillID"] != null)
            {
                BillID = TempData["BillID"].ToString();
                BillDate = TempData["BillDate"].ToString();
                CustomerNumber = TempData["CustomerNumber"].ToString();

            }

            var model = new ProductSelectModel
            {
                Viewproductlist = new List<ProductMatserModel>() // Initialize with an empty list
            };

            ViewBag.BillID = BillID;
            ViewBag.BillDate = BillDate;
            ViewBag.CustomerNumber = CustomerNumber;


            return View(model);


        }




        

        public IActionResult RollTypeMaster()
        {
            RollTypeMaster rolltype = new RollTypeMaster();

            return View("RollTypeMaster", rolltype);
        }

        public IActionResult BranchMaster()
        {
            
            return View();
        }



        public IActionResult StaffAdmin()
        {

            BusinessClassBilling Busbill = new BusinessClassBilling(_billingsoftware);
            ViewData["resoruseid"] = Busbill.GetResourceid();

            StaffAdminModel par = new StaffAdminModel();

            return View("StaffAdmin", par);
        }

        public IActionResult ResourceTypeMaster()
        {
            ResourceTypeMasterModel res = new ResourceTypeMasterModel();

            return View("ResourceTypeMaster", res);
        }

        public IActionResult RollAccessMaster()
        {
            BusinessClassBilling Busbill = new BusinessClassBilling(_billingsoftware);
            ViewData["rollid"] = Busbill.RollAccessType();
            ViewData["staffid"] = Busbill.GetStaffID();


            RollAccessMaster roll = new RollAccessMaster();

            return View("RollAccessMaster", roll);
        }

        public IActionResult RoleAccess()
        {
            BusinessClassBilling businessbill = new BusinessClassBilling(_billingsoftware);
            ViewData["screenid"] = businessbill.GetScreenid();
            ViewData["rollid"] = businessbill.RollAccessType();
            ViewData["staffid"] = businessbill.GetStaffID();


            RoleAccessModel role = new RoleAccessModel();
            return View("RoleAccess", role);
        }

        public IActionResult ScreenMaster()
        {
            BusinessClassBilling businessbill = new BusinessClassBilling(_billingsoftware);
            ViewData["screenname"] = businessbill.Screenname();


            ScreenMasterModel screen = new ScreenMasterModel();
            return View("ScreenMaster", screen);
        }




        public IActionResult CustomerMaster()
        {
            CustomerMasterModel obj = new CustomerMasterModel();

            return View("CustomerMaster", obj);
        }

        public IActionResult DiscountCategoryMaster()
        {
            BusinessClassBilling business = new BusinessClassBilling(_billingsoftware);
            ViewData["discountcategoryid"] = business.GetcategoryID();
            DiscountCategoryMasterModel obj = new DiscountCategoryMasterModel();
            return View("DiscountCategoryMaster", obj);
        }

        public IActionResult GSTMasterModel()
        {
            return View();
        }

        public IActionResult VoucherCustomerDetails()
        {
            return View();
        }

        public IActionResult NetDiscountMaster()
        {
            NetDiscountMasterModel par = new NetDiscountMasterModel();
            return View("NetDiscountMaster", par);
        }

        public IActionResult PointsMaster()
        {
            PointsMasterModel par = new PointsMasterModel();
            return View("PointsMaster", par);
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
            BusinessClassBilling business = new BusinessClassBilling(_billingsoftware);
            ViewData["godownproductid"] = business.GetProductid();

            var model = new RackpartitionViewModel
            {
                Viewrackpartition = new List<RackPatrionProductModel>()
            };



            return View(model);
        }

        public IActionResult VoucherMaster()
        {
            return View();
        }

        public IActionResult GodownModel()
        {
            BusinessClassBilling business = new BusinessClassBilling(_billingsoftware);
            ViewData["godownproductid"] = business.GetProductid();
            GodownModel res = new GodownModel();

            return View("GodownModel", res);

        }


        public IActionResult CustomerBilling(string productid, string billid, string SelectedProductID)
        {


            var model = new BillProductlistModel();

            // Retrieve selected product
            var selectedProduct = _billingsoftware.SHProductMaster.FirstOrDefault(p => p.ProductID == productid);

            if (selectedProduct != null)
            {
                // Retrieve bill detail for the specified BillID and ProductID
                var billDetail = _billingsoftware.SHbilldetails
                    .Where(b => b.BillID == billid && b.ProductID == productid)
                    .Select(b => new BillingDetailsModel
                    {
                        ProductID = b.ProductID,
                        ProductName = b.ProductName,
                        Price = selectedProduct.TotalAmount,
                        Quantity = b.Quantity,
                        BillDate = b.BillDate,
                        CustomerNumber = b.CustomerNumber,
                        BillID = b.BillID

                    })
                    .FirstOrDefault();

                if (billDetail != null)
                {
                    model.Viewbillproductlist = new List<BillingDetailsModel> { billDetail };
                    model.ProductID = billDetail.ProductID;
                    model.ProductName = billDetail.ProductName;
                    model.Price = billDetail.Price;
                    model.Quantity = billDetail.Quantity;
                    model.BillID = billDetail.BillID;
                    model.BillDate = billDetail.BillDate;
                    model.CustomerNumber = billDetail.CustomerNumber;

                }
                else
                {
                    // Handle case where no detail is found
                    model.Viewbillproductlist = new List<BillingDetailsModel>();
                }
            }
            else
            {
                // Handle case where selected product is not found
                model.Viewbillproductlist = new List<BillingDetailsModel>();
            }

            return View(model);

        }
        //PaymentScreen

        [HttpPost]
        public async Task<IActionResult> AddPayment(PaymentMasterModel model, PaymentDetailsModel detailsmodel, BillingMasterModel masterModel, string buttonType, List<PaymentDetailsModel> billpayment, string selectedSlotId,
string BillId, string Balance, string BillDate, string PaymentId, string paymentdescription, string CustomerNumber, string ReedemPoints, string action)
        {
            BusinessClassBilling businessbill = new BusinessClassBilling(_billingsoftware);

            ViewBag.PaymentId = PaymentId;
            ViewBag.BillId = BillId;
            ViewBag.Balance = Balance;
            ViewBag.BillDate = BillDate;
            ViewBag.CustomerNumber = CustomerNumber;
            ViewBag.ReedemPoints = ReedemPoints;

            /* if (billPayment == null)
             {
                 billPayment = new List<PaymentTableViewModel>();
             }*/

            if (buttonType == "PaymentReceipt")
            {

                String Query = "SELECT \r\n    SD.BillID,\r\n    CONVERT(varchar(10), SD.BillDate, 101) AS BillDate,\r\n    SD.PaymentId ,\r\n    SB.PaymentDiscription,\r\n\tSB.PaymentDate,\r\n\tSB.PaymentMode,\r\n\tSB.PaymentAmount, \r\n\tSB.PaymentTransactionNumber, \r\n    SD.CustomerNumber AS CustomerName,\r\n    SD.CustomerNumber\r\n\r\n\r\nFROM \r\n    SHPaymentMaster SD\r\nINNER JOIN \r\n    SHPaymentDetails SB ON SD.PaymentId = SB.PaymentId\r\n\r\nWHERE \r\n    SD.IsDelete = 0 AND  SD.CustomerNumber = '" + CustomerNumber + "' AND sd.BillId = '" + BillId + "' AND SD.PaymentId='" + PaymentId + "'";

                var Table = BusinessClassCommon.DataTable(_billingsoftware, Query);

                BusinessClassBilling objbilling = new BusinessClassBilling(_billingsoftware);

                return File(objbilling.PrintpaymentDetails(Table), "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "PaymentReport_" + TempData["BillID"] + ".docx");

            }




            if (buttonType == "AddPayment")
            {
                var newDetail = new PaymentDetailsModel
                {
                    PaymentId = PaymentId,
                    PaymentDiscription = businessbill.GeneratePaymentDescription(PaymentId),
                    PaymentDate = string.Empty,
                    PaymentMode = string.Empty,
                    PaymentTransactionNumber = string.Empty,
                    PaymentAmount = string.Empty

                };


                _billingsoftware.SHPaymentDetails.Add(newDetail);


                _billingsoftware.SaveChanges();


                ViewBag.Slots = _billingsoftware.SHPaymentDetails.Where(b => b.PaymentId == PaymentId && b.IsDelete == false).ToList();

            }
            else if (buttonType == "DeletePayment" && !string.IsNullOrEmpty(selectedSlotId))
            {
                var detail = _billingsoftware.SHPaymentDetails
                    .FirstOrDefault(p => p.PaymentDiscription == selectedSlotId);

                if (detail != null)
                {
                    detail.IsDelete = true;
                    _billingsoftware.SaveChanges();

                    ViewBag.Slots = _billingsoftware.SHPaymentDetails
                        .Where(b => b.PaymentId == PaymentId && b.IsDelete == false)
                        .ToList();
                }
                ViewBag.DeleteMessage = "Deleted Successfully";
                return View("PaymentScreen");
            }






            if (buttonType == "GetBill")
            {

                List<PaymentTableViewModel> modelList = new List<PaymentTableViewModel>();

                var exbill = await _billingsoftware.SHbillmaster.Where(x => x.BillID == BillId).FirstOrDefaultAsync();
                if (exbill != null)
                {


                    var billDetail = _billingsoftware.SHbillmaster
                   .Where(b => b.BillID == BillId)
                   .Select(b => new BillingDetailsModel
                   {

                       BillDate = b.BillDate,
                       CustomerNumber = b.CustomerNumber,
                       BillID = b.BillID,
                       Totalprice = b.Totalprice


                   })
                   .FirstOrDefault();

                    if (billDetail != null)
                    {

                        var paymentModel = new PaymentTableViewModel
                        {
                            BillDate = billDetail.BillDate,
                            CustomerNumber = billDetail.CustomerNumber,
                            BillId = billDetail.BillID,
                            Balance = billDetail.Totalprice
                        };

                        // Add the PaymentTableViewModel to the list
                        modelList.Add(paymentModel);


                        ViewBag.BillDate = paymentModel.BillDate;
                        ViewBag.CustomerNumber = paymentModel.CustomerNumber;
                        ViewBag.BillId = paymentModel.BillId;
                        ViewBag.Balance = paymentModel.Balance;

                        return View(modelList);


                    }
                    else
                    {
                        ViewBag.Message = "No details found for the given Bill ID.";
                    }

                    var exbilldata = _billingsoftware.SHPaymentMaster.FirstOrDefault(x => x.BillId == masterModel.BillID);

                    if (exbilldata != null)
                    {


                        var billDetails = await _billingsoftware.SHPaymentMaster
                     .Where(b => b.BillId == BillId && b.IsDelete == false)
                     .Select(b => new PaymentTableViewModel
                     {
                         PaymentId = b.PaymentId,
                         BillId = b.BillId,
                         Balance = b.Balance,
                         BillDate = b.BillDate,
                         CustomerNumber = b.CustomerNumber,
                         ReedemPoints = b.ReedemPoints,
                         Viewpayment = _billingsoftware.SHPaymentDetails
                             .Where(d => d.PaymentId == b.PaymentId && d.IsDelete == false)
                             .Select(d => new PaymentDetailsModel
                             {
                                 PaymentId = d.PaymentId,
                                 PaymentDiscription = d.PaymentDiscription,
                                 PaymentMode = d.PaymentMode,
                                 PaymentTransactionNumber = d.PaymentTransactionNumber,
                                 PaymentAmount = d.PaymentAmount,
                                 PaymentDate = d.PaymentDate
                             }).ToList()
                     })
                     .ToListAsync();


                        var exbilldataa = _billingsoftware.SHPaymentMaster.FirstOrDefault(x => x.BillId == masterModel.BillID);

                        if (exbilldataa != null)
                        {
                            if (billDetails.Any())
                            {
                                var firstBillDetail = billDetails.First();




                                ViewBag.PaymentId = firstBillDetail.PaymentId;
                                ViewBag.BillId = firstBillDetail.BillId;
                                ViewBag.Balance = firstBillDetail.Balance;
                                ViewBag.BillDate = firstBillDetail.BillDate;
                                ViewBag.CustomerNumber = firstBillDetail.CustomerNumber;
                                //ViewBag.ReedemPoints = firstBillDetail.ReedemPoints;
                                ViewBag.Slots = firstBillDetail.Viewpayment;
                            }
                            else
                            {
                                ViewBag.Message = "No details found for the given Bill ID.";
                            }


                        }
                        return View("PaymentScreen", billDetails);
                    }
                }
            }




            if (buttonType == "GetPaymentDetails")
            {

                var billDetailspay = await _billingsoftware.SHPaymentDetails
        .Where(b => b.PaymentId == PaymentId && b.IsDelete == false)
        .Select(b => new PaymentDetailsModel
        {
            PaymentId = b.PaymentId,
            PaymentDiscription = b.PaymentDiscription,
            PaymentMode = b.PaymentMode,
            PaymentTransactionNumber = b.PaymentTransactionNumber,
            PaymentAmount = b.PaymentAmount,
            PaymentDate = b.PaymentDate
        })
        .ToListAsync();

                if (billDetailspay.Any())
                {
                    ViewBag.Slots = billDetailspay; // Assign the correct model to ViewBag or ViewData
                }
                else
                {
                    ViewBag.Message = "No details found for the given Payment ID.";
                }
            }





            if (buttonType == "GetPoints")
            {
                // Retrieve RedeemPoints based on CustomerNumber
                var customer = await _billingsoftware.SHPaymentMaster
                    .FirstOrDefaultAsync(c => c.CustomerNumber == CustomerNumber);

                if (customer != null)
                {
                    var pointsID = await _billingsoftware.SHPointsMaster
                        .FromSqlRaw("SELECT dbo.GeneratePointsID(" + CustomerNumber + ") AS PointsID", new SqlParameter("@CustomerNumber", CustomerNumber))
                        .Select(p => p.PointsID)
                        .FirstOrDefaultAsync();

                    ViewBag.CustomerNumber = customer.CustomerNumber;
                    ViewBag.ReedemPoints = pointsID;


                    var billDetails = await _billingsoftware.SHPaymentMaster
               .Where(b => b.CustomerNumber == CustomerNumber && b.IsDelete == false)
               .Select(b => new PaymentTableViewModel
               {
                   PaymentId = b.PaymentId,
                   BillId = b.BillId,
                   Balance = b.Balance,
                   CustomerNumber = b.CustomerNumber,
                   ReedemPoints = b.ReedemPoints,
                   Viewpayment = _billingsoftware.SHPaymentDetails
                       .Where(d => d.PaymentId == b.PaymentId && d.IsDelete == false)
                       .Select(d => new PaymentDetailsModel
                       {
                           PaymentId = d.PaymentId,
                           PaymentDiscription = d.PaymentDiscription,
                           PaymentMode = d.PaymentMode,
                           PaymentTransactionNumber = d.PaymentTransactionNumber,
                           PaymentAmount = d.PaymentAmount,
                           PaymentDate = d.PaymentDate
                       }).ToList()
               })
               .ToListAsync();

                    if (billDetails.Any())
                    {
                        var firstBillDetail = billDetails.First();
                        ViewBag.PaymentId = firstBillDetail.PaymentId;
                        ViewBag.BillId = firstBillDetail.BillId;
                        ViewBag.Balance = firstBillDetail.Balance;
                        ViewBag.CustomerNumber = firstBillDetail.CustomerNumber;
                        ViewBag.ReedemPoints = firstBillDetail.ReedemPoints;
                        ViewBag.Slots = firstBillDetail.Viewpayment;
                    }
                    else
                    {
                        ViewBag.Message = "Customer not found.";
                    }
                    return View("PaymentScreen");
                }
            }


            if (buttonType == "Redeem")
            {
                var paymentDetail = billpayment.FirstOrDefault();

                if (paymentDetail == null)
                {
                    ModelState.AddModelError("", "No payment details provided.");
                    return View("PaymentScreen");
                }

                var parameters = new[]
                {
        new SqlParameter("@BillId", model.BillId),
        new SqlParameter("@PaymentId", model.PaymentId),
        new SqlParameter("@CustomerNumber", model.CustomerNumber ?? (object)DBNull.Value),
        new SqlParameter("@ReedemPoints", model.ReedemPoints ?? (object)DBNull.Value),
        new SqlParameter("@Balance", model.Balance ?? (object)DBNull.Value),
         new SqlParameter("BillDate",model.BillDate?? (object)DBNull.Value),
        new SqlParameter("@PaymentDiscription", paymentDetail.PaymentDiscription),
        new SqlParameter("@PaymentMode", paymentDetail.PaymentMode ?? (object)DBNull.Value),
        new SqlParameter("@PaymentTransactionNumber", paymentDetail.PaymentTransactionNumber ?? (object)DBNull.Value),
        new SqlParameter("@PaymentAmount", paymentDetail.PaymentAmount ?? (object)DBNull.Value),
        new SqlParameter("@PaymentDate", paymentDetail.PaymentDate ?? (object)DBNull.Value),
        new SqlParameter("@LastUpdatedUser", User.Claims.First().Value.ToString()),
        new SqlParameter("@LastUpdatedDate", DateTime.Now.ToString()),
        new SqlParameter("@LastUpdatedMachine", Request.HttpContext.Connection.RemoteIpAddress.ToString()),
        new SqlParameter("@Reedem", "Y")
    };

                await _billingsoftware.Database.ExecuteSqlRawAsync(
                    "EXEC InsertBillPayment @BillId, @PaymentId, @CustomerNumber, @ReedemPoints, @Balance,@BillDate, @PaymentDiscription, @PaymentMode, @PaymentTransactionNumber, @PaymentAmount, @PaymentDate, @LastUpdatedUser, @LastUpdatedDate, @LastUpdatedMachine, @Reedem",
                    parameters
                );

                // Save redeem history
                var redeemHistory = new ReedemHistoryModel
                {
                    CustomerNumber = model.CustomerNumber,
                    DateOfReedem = DateTime.Now.ToString(), // Adjust as per your requirements
                    ReedemPoints = model.ReedemPoints, // Adjust as per your requirements
                    Lastupdateduser = User.Claims.First().Value.ToString(),
                    Lastupdateddate = DateTime.Now.ToString(),
                    Lastupdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString()
                };

                _billingsoftware.SHReedemHistory.Add(redeemHistory);
                await _billingsoftware.SaveChangesAsync();

                // Retrieve updated payment details
                var billDetailspay = await _billingsoftware.SHPaymentDetails
                    .Where(b => b.PaymentId == model.PaymentId && b.IsDelete == false)
                    .Select(b => new PaymentDetailsModel
                    {
                        PaymentId = b.PaymentId,
                        PaymentDiscription = b.PaymentDiscription,
                        PaymentMode = b.PaymentMode,
                        PaymentTransactionNumber = b.PaymentTransactionNumber,
                        PaymentAmount = b.PaymentAmount,
                        PaymentDate = b.PaymentDate
                    })
                    .ToListAsync();

                ViewBag.Slots = billDetailspay;
                ViewBag.ReedemMessage = "Reedem Successfully";

            }





            if (buttonType == "Save")
            {

                var paymentDetail = billpayment.FirstOrDefault(p => p.PaymentDiscription == selectedSlotId);


                if (paymentDetail == null)
                {
                    // Handle the case where there is no payment detail to save
                    ModelState.AddModelError("", "No payment details provided.");
                    return View("PaymentScreen");
                }
                else
                {

                    var parameters = new[]
                    {
        new SqlParameter("@BillId", model.BillId),
        new SqlParameter("@PaymentId", model.PaymentId),
        new SqlParameter("@CustomerNumber", model.CustomerNumber ?? (object)DBNull.Value),
        new SqlParameter("@ReedemPoints", model.ReedemPoints ?? (object)DBNull.Value),
        new SqlParameter("@Balance", model.Balance ?? (object)DBNull.Value),
        new SqlParameter("BillDate",model.BillDate?? (object)DBNull.Value),
        new SqlParameter("@PaymentDiscription", paymentDetail.PaymentDiscription),
        new SqlParameter("@PaymentMode", paymentDetail.PaymentMode ?? (object)DBNull.Value),
        new SqlParameter("@PaymentTransactionNumber", paymentDetail.PaymentTransactionNumber ?? (object)DBNull.Value),
        new SqlParameter("@PaymentAmount", paymentDetail.PaymentAmount ?? (object)DBNull.Value),
        new SqlParameter("@PaymentDate", paymentDetail.PaymentDate ?? (object)DBNull.Value),
        new SqlParameter("@LastUpdatedUser", User.Claims.First().Value.ToString()),
        new SqlParameter("@LastUpdatedDate", DateTime.Now.ToString()),
        new SqlParameter("@LastUpdatedMachine", Request.HttpContext.Connection.RemoteIpAddress.ToString()),
        new SqlParameter("@Reedem", "Y")
    };

                    await _billingsoftware.Database.ExecuteSqlRawAsync("EXEC InsertBillPayment @BillId, @PaymentId, @CustomerNumber, @ReedemPoints, @Balance,@BillDate, @PaymentDiscription, @PaymentMode, @PaymentTransactionNumber, @PaymentAmount, @PaymentDate, @LastUpdatedUser, @LastUpdatedDate, @LastUpdatedMachine,@Reedem", parameters);
                }

                _billingsoftware.SaveChanges();
                ViewBag.Message = "Saved Successfully";
                return View("PaymentScreen");
            }

            return View("PaymentScreen");

        }




        public IActionResult PaymentScreen(string BillID, string TotalPrice, string CustomerNumber, PaymentTableViewModel model, string billdate)
        {


            List<PaymentTableViewModel> modelList = new List<PaymentTableViewModel>();

            if (string.IsNullOrEmpty(BillID))
            {

                return View(modelList);
            }

            var billDetail = _billingsoftware.SHbillmaster
                   .Where(b => b.BillID == BillID)
                   .Select(b => new BillingDetailsModel
                   {

                       BillDate = b.BillDate,
                       CustomerNumber = b.CustomerNumber,
                       BillID = b.BillID,
                       Totalprice = b.Totalprice


                   })
                   .FirstOrDefault();

            if (billDetail != null)
            {
             
                var paymentModel = new PaymentTableViewModel
                {
                    BillDate = billDetail.BillDate,
                    CustomerNumber = billDetail.CustomerNumber,
                    BillId = billDetail.BillID,
                    Balance = billDetail.Totalprice
                };

                // Add the PaymentTableViewModel to the list
                modelList.Add(paymentModel);

                
                ViewBag.BillDate = paymentModel.BillDate;
                ViewBag.CustomerNumber = paymentModel.CustomerNumber;
                ViewBag.BillId = paymentModel.BillId;
                ViewBag.Balance = paymentModel.Balance;


                if (!string.IsNullOrEmpty(BillID))

                {
                    string connectionString = _configuration.GetConnectionString("BillingDBConnection");

                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        var command = new SqlCommand("SELECT dbo.GenerateBillID(@BillID)", connection);
                        command.Parameters.AddWithValue("@BillID", BillID);
                        var balance = command.ExecuteScalar();
                        TotalPrice = balance?.ToString() ?? "0";
                    }
                }



               
            }

            return View(modelList);
        }

       




    }


}

    

