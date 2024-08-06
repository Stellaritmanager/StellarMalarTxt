
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Office2021.Excel.RichDataWebImage;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using StellarBillingSystem.Business;
using StellarBillingSystem.Context;
using StellarBillingSystem.Models;
using System.Data;
using System.Linq;

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


        [HttpGet]
        public async Task<IActionResult> GetCategory(CategoryMasterModel model,string buttonType)
        {
            if (TempData["BranchID"] != null)
            {
                model.BranchID = TempData["BranchID"].ToString();
                TempData.Keep("BranchID");
            }

            if (buttonType == "Get")
            {
                var getcategory = await _billingsoftware.SHCategoryMaster.FirstOrDefaultAsync(x => x.CategoryID == model.CategoryID && !x.IsDelete && x.BranchID == model.BranchID);
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

            return View();
        }



        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryMasterModel model, string buttonType)
        {
            if (TempData["BranchID"] != null)
            {
                model.BranchID = TempData["BranchID"].ToString();
                TempData.Keep("BranchID");
            }


            if (buttonType == "Get")
            {
                var getcategory = await _billingsoftware.SHCategoryMaster.FirstOrDefaultAsync(x => x.CategoryID == model.CategoryID && !x.IsDelete && x.BranchID == model.BranchID);
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
                var categorytodelete = await _billingsoftware.SHCategoryMaster.FirstOrDefaultAsync(x => x.CategoryID == model.CategoryID && !x.IsDelete && x.BranchID == model.BranchID);

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
                var categorytoretrieve = await _billingsoftware.SHCategoryMaster.FindAsync(model.CategoryID, model.BranchID);
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
                var existingCategory = await _billingsoftware.SHCategoryMaster.FindAsync(model.CategoryID, model.BranchID);
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
                    model.LastUpdatedUser = User.Claims.First().Value.ToString(); ;
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
            if (TempData["BranchID"] != null)
            {
                model.BranchID = TempData["BranchID"].ToString();
                TempData.Keep("BranchID");
            }

            BusinessClassBilling business = new BusinessClassBilling(_billingsoftware);
            ViewData["categoryid"] = business.GetCatid();
            ViewData["discountid"] = business.Getdiscountid();

            if (buttonType == "Get")
            {
                if (model.ProductID == null && model.BarcodeId == null)
                {
                    ViewBag.ValidationMessage = "Please enter either ProductID or BarcodeID.";
                    return View("ProductMaster", model);
                }
                ProductMatserModel? resultpro;
                if (model.BarcodeId != null && model.BarcodeId != string.Empty)
                    resultpro = await _billingsoftware.SHProductMaster.FirstOrDefaultAsync(x => x.BarcodeId == model.BarcodeId && !x.IsDelete && x.BranchID == model.BranchID);
                else
                    resultpro = await _billingsoftware.SHProductMaster.FirstOrDefaultAsync(x => x.ProductID == model.ProductID && !x.IsDelete && x.BranchID == model.BranchID);

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
                if (string.IsNullOrEmpty(model.ProductID) && string.IsNullOrEmpty(model.BarcodeId))
                {
                    ViewBag.ValidationMessage = "Please enter either ProductID or BarcodeID.";
                    return View("ProductMaster", model);
                }

                var productToDelete = await _billingsoftware.SHProductMaster.FirstOrDefaultAsync(x => (x.ProductID == model.ProductID || x.BarcodeId == model.BarcodeId) && !x.IsDelete && x.BranchID == model.BranchID);
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
                if (string.IsNullOrEmpty(model.ProductID) && string.IsNullOrEmpty(model.BarcodeId))
                {
                    ViewBag.ValidationMessage = "Please enter either ProductID or BarcodeID.";
                    return View("ProductMaster", model);
                }

                var productToRetrieve = await _billingsoftware.SHProductMaster.FirstOrDefaultAsync(x => (x.ProductID == model.ProductID || x.BarcodeId == model.BarcodeId) && x.IsDelete == true && x.BranchID == model.BranchID);
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
                if (string.IsNullOrEmpty(model.ProductID))
                {
                    ViewBag.ValidationMessage = "Please enter  ProductID";
                    return View("ProductMaster", model);
                }

                if (string.IsNullOrEmpty(model.BarcodeId))
                {
                    ViewBag.ValidationMessage = "Please enter  BarcodeID.";
                    return View("ProductMaster", model);
                }


                decimal price;
                if (!decimal.TryParse(model.Price, out price))
                {
                    ViewBag.PriceErrorMessage = "Please enter a valid price.";
                    return View("ProductMaster", model);
                }

                decimal discount;

                decimal totalAmount;

                if (!string.IsNullOrEmpty(model.DiscountCategory) && decimal.TryParse(model.DiscountCategory, out discount))
                {
                    // Perform calculation if a valid discount is provided
                    totalAmount = price - (price * discount / 100);
                }
                else
                {
                    // Use the original price if no discount is provided or if the discount is invalid
                    totalAmount = price;
                }

                model.TotalAmount = totalAmount.ToString();



                var existingProduct = await _billingsoftware.SHProductMaster.FindAsync(model.ProductID, model.BranchID);
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
                    existingProduct.BranchID = model.BranchID;


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
            if (TempData["BranchID"] != null)
            {
                model.BranchID = TempData["BranchID"].ToString();
                TempData.Keep("BranchID");
            }

            BusinessClassBilling business = new BusinessClassBilling(_billingsoftware);
            ViewData["godownproductid"] = business.GetProductid();


            if (buttonType == "DeleteRetrieve")
            {
                var screentoretrieve = await _billingsoftware.SHGodown.FindAsync(model.ProductID, model.DatefofPurchase, model.SupplierInformation, model.BranchID);
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
                var existinggoddown = await _billingsoftware.SHGodown.FindAsync(model.ProductID, model.DatefofPurchase, model.SupplierInformation, model.BranchID);
                if (existinggoddown != null)
                {
                    if (existinggoddown.IsDelete)
                    {
                        ViewBag.ErrorMessage = "Cannot update. GodowmnID is marked as deleted.";
                        return View("GodownModel", model);
                    }

                    existinggoddown.ProductID = model.ProductID;
                    existinggoddown.NumberofStocks = model.NumberofStocks;
                    existinggoddown.DatefofPurchase = model.DatefofPurchase;
                    existinggoddown.SupplierInformation = model.SupplierInformation;
                    existinggoddown.IsDelete = model.IsDelete;
                    existinggoddown.BranchID = model.BranchID;
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
                var goddown = await _billingsoftware.SHGodown.FindAsync(model.ProductID, model.DatefofPurchase, model.SupplierInformation, model.BranchID);
                if (goddown != null)
                {
                    if (goddown.IsDelete)
                    {
                        ViewBag.ErrorMessage = "Cannot update. GodowmnID is marked as deleted.";
                        return View("GodownModel", model);
                    }

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
                var getStock = await _billingsoftware.SHGodown.FirstOrDefaultAsync(x => x.IsDelete == false && x.ProductID == model.ProductID && x.DatefofPurchase == model.DatefofPurchase && x.SupplierInformation == model.SupplierInformation && x.BranchID == model.BranchID);
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
            if (TempData["BranchID"] != null)
            {
                model.BranchID = TempData["BranchID"].ToString();
                TempData.Keep("BranchID");
            }


            var existingCustomer = await _billingsoftware.SHCustomerMaster.FindAsync(model.MobileNumber, model.BranchID);
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
                existingCustomer.BranchID = model.BranchID;
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



            return View("CustomerMaster", model);
        }
        public async Task<IActionResult> GetCustomer(CustomerMasterModel model)
        {
            if (TempData["BranchID"] != null)
            {
                model.BranchID = TempData["BranchID"].ToString();
                TempData.Keep("BranchID");
            }

            if (string.IsNullOrEmpty(model.MobileNumber))
            {
                ViewBag.Message = "Mobile number is required";
                return BadRequest("Mobile number is required");
            }

            var customer = await _billingsoftware.SHCustomerMaster.FindAsync(model.MobileNumber, model.BranchID);

            if (customer == null)
            {
                model = new CustomerMasterModel();
                ViewBag.ErrorMessage = "Mobile Number not found or customer is deleted";
                return View("CustomerMaster", model); // Return an empty model if not found or deleted
            }

            return View("CustomerMaster", customer);
        }

        public async Task<IActionResult> GetDeleteRetrieve(CustomerMasterModel model)
        {
            if (TempData["BranchID"] != null)
            {
                model.BranchID = TempData["BranchID"].ToString();
                TempData.Keep("BranchID");
            }



            if (string.IsNullOrEmpty(model.MobileNumber))
            {
                ViewBag.ErrorMessage = "Mobile number is required";

                return View("CustomerMaster");
            }

            var customer = await _billingsoftware.SHCustomerMaster.FindAsync(model.MobileNumber, model.BranchID);
            if (customer == null)
            {
                model = new CustomerMasterModel();
                ViewBag.ErrorMessage = "Mobile Number not found";
                return View("CustomerMaster", model);
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
            if (TempData["BranchID"] != null)
            {
                model.BranchID = TempData["BranchID"].ToString();
                TempData.Keep("BranchID");
            }



            if (string.IsNullOrEmpty(model.MobileNumber))
            {
                ViewBag.ErrorMessage = "Mobile Number not found";
                return View("Error", new CustomerMasterModel());
            }

            var existingCustomer = await _billingsoftware.SHCustomerMaster.FindAsync(model.MobileNumber, model.BranchID);
            if (existingCustomer == null)
            {
                model = new CustomerMasterModel();
                ViewBag.ErrorMessage = "Mobile Number not found";
                return View("CustomerMaster", model);
            }

            if (existingCustomer.IsDelete)
            {
                ViewBag.ErrorMessage = "Cannot update. GodowmnID is marked as deleted.";
                return View("CustomerMaster", model);
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
            if (TempData["BranchID"] != null)
            {
                model.BranchID = TempData["BranchID"].ToString();
                TempData.Keep("BranchID");
            }



            BusinessClassBilling business = new BusinessClassBilling(_billingsoftware);
            ViewData["discountcategoryid"] = business.GetcategoryID();

            if (buttonType == "Get")
            {
                var getdiscount = await _billingsoftware.SHDiscountCategory.FirstOrDefaultAsync(x => x.CategoryID == model.CategoryID && !x.IsDelete && x.BranchID == model.BranchID);
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
                var deletetodiscount = await _billingsoftware.SHDiscountCategory.FindAsync(model.CategoryID, model.BranchID);
                if (deletetodiscount != null)
                {
                    if (deletetodiscount.IsDelete)
                    {
                        ViewBag.ErrorMessage = "Cannot update. GodowmnID is marked as deleted.";
                        return View("DiscountCategoryMaster", model);
                    }

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
                var discountcategorytoretrieve = await _billingsoftware.SHDiscountCategory.FindAsync(model.CategoryID, model.BranchID);
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

                var existingDiscountCategory = await _billingsoftware.SHDiscountCategory.FindAsync(model.CategoryID, model.BranchID);
                if (existingDiscountCategory != null)
                {
                    if (existingDiscountCategory.IsDelete)
                    {
                        ViewBag.ErrorMessage = "Cannot update. Product is marked as deleted.";
                        return View("DiscountCategoryMaster", model);
                    }
                    existingDiscountCategory.CategoryID = model.CategoryID;
                    existingDiscountCategory.DiscountPrice = model.DiscountPrice;
                    existingDiscountCategory.BranchID = model.BranchID;
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

            if (TempData["BranchID"] != null)
            {
                model.BranchID = TempData["BranchID"].ToString();
                TempData.Keep("BranchID");
            }


            var NetID = "1";

            var existingnetdiscount = await _billingsoftware.SHNetDiscountMaster.FindAsync(NetID, model.BranchID);
            if (existingnetdiscount != null)
            {

                existingnetdiscount.BranchID = model.BranchID;
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

            if (TempData["BranchID"] != null)
            {
                model.BranchID = TempData["BranchID"].ToString();
                TempData.Keep("BranchID");
            }


            var pointsID = "1";

            var existingpoints = await _billingsoftware.SHPointsMaster.FindAsync(pointsID, model.BranchID);
            if (existingpoints != null)
            {

                existingpoints.NetPrice = model.NetPrice;
                existingpoints.NetPoints = model.NetPoints;
                existingpoints.BranchID = model.BranchID;
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

            if (TempData["BranchID"] != null)
            {
                model.BranchID = TempData["BranchID"].ToString();
                TempData.Keep("BranchID");
            }


            BusinessClassBilling business = new BusinessClassBilling(_billingsoftware);
            ViewData["godownproductid"] = business.GetProductid();

            if (buttonType == "Get")
            {

                var result = business.GetRackview(model.PartitionID, model.ProductID, model.BranchID);
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
                var rolltoretrieve = await _billingsoftware.SHRackPartionProduct.FindAsync(model.PartitionID, model.ProductID, model.BranchID);
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
                var rolltoretrieve = await _billingsoftware.SHRackPartionProduct.FindAsync(model.PartitionID, model.ProductID, model.BranchID);
                if (rolltoretrieve != null)
                {
                    if (rolltoretrieve.Isdelete)
                    {
                        ViewBag.ErrorMessage = "Cannot update. Product is marked as deleted.";
                        return View("RackPatrionProduct", model);
                    }

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


            var recstockgodwomn = _billingsoftware.SHGodown.FirstOrDefault(x => x.ProductID == model.ProductID && x.BranchID == model.BranchID);
            if (recstockgodwomn == null)
            {
                ViewBag.entergodowmnMessage = "Please enter the Product and Stock in Godown Master";
                var modelgod = new RackpartitionViewModel
                {
                    Viewrackpartition = new List<RackPatrionProductModel>()
                };
                return View("RackPatrionProduct", modelgod);
            }

            var existingrackpartition = await _billingsoftware.SHRackPartionProduct.FindAsync(model.PartitionID, model.ProductID, model.BranchID);
            if (existingrackpartition != null)
            {
                if (existingrackpartition.Isdelete)
                {
                    ViewBag.ErrorMessage = "Cannot update. Product is marked as deleted.";
                    return View("RackPatrionProduct", model);
                }


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
                        existingrackpartition.BranchID = model.BranchID;
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
                    var recstock = _billingsoftware.SHGodown.FirstOrDefault(x => x.ProductID == model.ProductID && x.BranchID == model.BranchID);
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

            var updatedResult = business.GetRackview(model.PartitionID, model.ProductID, model.BranchID);
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
        public async Task<IActionResult> Edit(string partitionID, string productID, RackpartitionViewModel model)
        {
            if (TempData["BranchID"] != null)
            {
                model.BranchID = TempData["BranchID"].ToString();
                TempData.Keep("BranchID");
            }

            BusinessClassBilling business = new BusinessClassBilling(_billingsoftware);
            ViewData["godownproductid"] = business.GetProductid();

            var RackEdit = await _billingsoftware.SHRackPartionProduct.FindAsync(partitionID, productID, model.BranchID);
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


            var result = business.GetRackview(RackEdit.PartitionID, RackEdit.ProductID, RackEdit.BranchID);
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
            if (TempData["BranchID"] != null)
            {
                viewmodel.BranchID = TempData["BranchID"].ToString();
                TempData.Keep("BranchID");
            }


            BusinessClassBilling business = new BusinessClassBilling(_billingsoftware);
            ViewData["godownproductid"] = business.GetProductid();


            var rackDel = await _billingsoftware.SHRackPartionProduct.FindAsync(partitionID, productID, viewmodel.BranchID);
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
            ViewData["branchid"] = Busbill.Getbranch();


            if (buttontype == "Get")
            {
                if (TempData["BranchID"] != null)
                {
                    model.BranchID = TempData["BranchID"].ToString();
                    TempData.Keep("BranchID");
                }

                var getstaff = await _billingsoftware.SHStaffAdmin.FirstOrDefaultAsync(x => x.StaffID == model.StaffID && x.IsDelete == false && x.BranchID == model.BranchID);
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
                if (TempData["BranchID"] != null)
                {
                    model.BranchID = TempData["BranchID"].ToString();
                    TempData.Keep("BranchID");
                }

                var stafftodelete = await _billingsoftware.SHStaffAdmin.FindAsync(model.StaffID, model.BranchID);
                if (stafftodelete != null)
                {
                    if (stafftodelete.IsDelete)
                    {
                        ViewBag.ErrorMessage = "Cannot update. Product is marked as deleted.";
                        return View("StaffAdmin", model);
                    }

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

                if (TempData["BranchID"] != null)
                {
                    model.BranchID = TempData["BranchID"].ToString();
                    TempData.Keep("BranchID");
                }

               
                var stafftoretrieve = await _billingsoftware.SHStaffAdmin.FindAsync(model.StaffID, model.BranchID);
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



            var staffcheck = await _billingsoftware.SHStaffAdmin.FirstOrDefaultAsync(x => x.StaffID == model.StaffID && x.BranchID == model.BranchID && x.UserName==model.UserName && x.Password == model.Password );

            if (staffcheck == null)
            {



                var existingStaffAdmin = await _billingsoftware.SHStaffAdmin.FindAsync(model.StaffID, model.BranchID);


                if (string.IsNullOrWhiteSpace(model.UserName) || string.IsNullOrWhiteSpace(model.Password))
                {
                    ViewBag.validateMessage = "Username and Password are required.";
                    return View("StaffAdmin", model);
                }

                if (existingStaffAdmin != null)
                {
                    if (existingStaffAdmin.IsDelete)
                    {
                        ViewBag.ErrorMessage = "Cannot update. Product is marked as deleted.";
                        return View("StaffAdmin", model);
                    }

                    existingStaffAdmin.StaffID = model.StaffID;
                    existingStaffAdmin.ResourceTypeID = model.ResourceTypeID;
                    existingStaffAdmin.BranchID = model.BranchID;
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
            }
            else
            {
                StaffAdminModel mod = new StaffAdminModel();
                ViewBag.ExistMessage="Username and Password Already Exist";
                return View("StaffAdmin", mod);
            }
            await _billingsoftware.SaveChangesAsync();

            ViewBag.Message = "Saved Successfully";

            model = new StaffAdminModel();
            return View("StaffAdmin", model);


        }








        public async Task<IActionResult> AddResourceType(ResourceTypeMasterModel model, string buttontype)
        {
            if (TempData["BranchID"] != null)
            {
                model.BranchID = TempData["BranchID"].ToString();
                TempData.Keep("BranchID");
            }


            if (buttontype == "Get")
            {
                var getres = await _billingsoftware.SHresourceType.FirstOrDefaultAsync(x => x.ResourceTypeID == model.ResourceTypeID && x.IsDelete == false && x.BranchID == model.BranchID);
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
                var restodelete = await _billingsoftware.SHresourceType.FindAsync(model.ResourceTypeID, model.BranchID);
                if (restodelete != null)
                {
                    if (restodelete.IsDelete)
                    {
                        ViewBag.ErrorMessage = "Cannot update. Product is marked as deleted.";
                        return View("ResourceTypeMaster", model);
                    }

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
                var restoretrieve = await _billingsoftware.SHresourceType.FindAsync(model.ResourceTypeID, model.BranchID);
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


            var existingres = await _billingsoftware.SHresourceType.FindAsync(model.ResourceTypeID, model.BranchID);

            if (existingres != null)
            {
                if (existingres.IsDelete)
                {
                    ViewBag.ErrorMessage = "Cannot update. Product is marked as deleted.";
                    return View("ResourceTypeMaster", model);
                }

                existingres.ResourceTypeName = model.ResourceTypeName;
                existingres.ResourceTypeID = model.ResourceTypeID;
                existingres.BranchID = model.BranchID;
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

            if (TempData["BranchID"] != null)
            {
                model.BranchID = TempData["BranchID"].ToString();
                TempData.Keep("BranchID");
            }

            BusinessClassBilling businessbill = new BusinessClassBilling(_billingsoftware);
            ViewData["screenid"] = businessbill.GetScreenid();
            ViewData["rollid"] = businessbill.RollAccessType();
            ViewData["staffid"] = businessbill.GetStaffID();

            if (buttontype == "Get")
            {
                var getrol = await _billingsoftware.SHRoleaccessModel.FirstOrDefaultAsync(x => x.RollID == model.RollID && x.ScreenID == model.ScreenID && x.Isdelete == false && x.BranchID == model.BranchID);
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
                var roletodelete = await _billingsoftware.SHRoleaccessModel.FindAsync(model.RollID, model.ScreenID, model.BranchID);
                if (roletodelete != null)
                {
                    if (roletodelete.Isdelete)
                    {
                        ViewBag.ErrorMessage = "Cannot update. Product is marked as deleted.";
                        return View("RoleAccess", model);
                    }

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
                var roltoretrieve = await _billingsoftware.SHRoleaccessModel.FindAsync(model.RollID, model.ScreenID, model.BranchID);
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


            var existingrole = await _billingsoftware.SHRoleaccessModel.FindAsync(model.RollID, model.ScreenID, model.BranchID);

            if (existingrole != null)
            {
                if (existingrole.Isdelete)
                {
                    ViewBag.ErrorMessage = "Cannot update. Product is marked as deleted.";
                    return View("RoleAccess", model);
                }

                existingrole.BranchID = model.BranchID;
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
            if (TempData["BranchID"] != null)
            {
                model.BranchID = TempData["BranchID"].ToString();
                TempData.Keep("BranchID");
            }

            BusinessClassBilling Busbill = new BusinessClassBilling(_billingsoftware);
            ViewData["rollid"] = Busbill.RollAccessType();
            ViewData["staffid"] = Busbill.GetStaffID();


            if (buttontype == "Get")
            {
                var getroll = await _billingsoftware.SHrollaccess.FirstOrDefaultAsync(x => x.StaffID == model.StaffID && x.IsDelete == false && x.BranchID == model.BranchID);
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

                    var rolltodelete = await _billingsoftware.SHrollaccess.FindAsync(model.StaffID, rollName, model.BranchID);
                    if (rolltodelete != null)
                    {
                        if (rolltodelete.IsDelete)
                        {
                            ViewBag.ErrorMessage = "Cannot update. Product is marked as deleted.";
                            return View("RollAccessMaster", model);
                        }

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
                    var rolltoretrieve = await _billingsoftware.SHrollaccess.FindAsync(model.StaffID, rollName, model.BranchID);
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

                    var existingroll = await _billingsoftware.SHrollaccess.FindAsync(model.StaffID, rollName, model.BranchID);

                    if (existingroll != null)
                    {
                        var duplicateRoll = _billingsoftware.SHrollaccess
                     .FirstOrDefault(x => x.RollID == model.RollID && x.StaffID != model.StaffID && x.BranchID == model.BranchID);

                        if (duplicateRoll == null)
                        {
                            ViewBag.ErrorMessage = "RollID already exists Cannot update same ID";
                            return View("RollAccessMaster", model);
                        }

                        if (existingroll.IsDelete)
                        {
                            ViewBag.ErrorMessage = "Cannot update. Product is marked as deleted.";
                            return View("RollAccessMaster", model);
                        }

                        existingroll.BranchID = model.BranchID;
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
                            BranchID = model.BranchID,
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

            if (TempData["BranchID"] != null)
            {
                model.BranchID = TempData["BranchID"].ToString();
                TempData.Keep("BranchID");
            }

            if (buttontype == "Get")
            {
                var getrolltype = await _billingsoftware.SHrollType.FirstOrDefaultAsync(x => x.RollID == model.RollID && x.IsDelete == false && x.BranchID == model.BranchID);
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
                var rolltypetodelete = await _billingsoftware.SHrollType.FindAsync(model.RollID, model.BranchID);
                if (rolltypetodelete != null)
                {
                    if (rolltypetodelete.IsDelete)
                    {
                        ViewBag.ErrorMessage = "Cannot update. Product is marked as deleted.";
                        return View("RollTypeMaster", model);
                    }

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
                var rolltypetoretrieve = await _billingsoftware.SHrollType.FindAsync(model.RollID, model.BranchID);
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


            var existingrolltype = await _billingsoftware.SHrollType.FindAsync(model.RollID, model.BranchID);

            if (existingrolltype != null)
            {
                if (existingrolltype.IsDelete)
                {
                    ViewBag.ErrorMessage = "Cannot update. Product is marked as deleted.";
                    return View("RollTypeMaster", model);
                }

                existingrolltype.BranchID = model.BranchID;
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

            if (TempData["BranchID"] != null)
            {
                model.BranchID = TempData["BranchID"].ToString();
                TempData.Keep("BranchID");
            }


            BusinessClassBilling businessbill = new BusinessClassBilling(_billingsoftware);
            ViewData["screenname"] = businessbill.Screenname();

            if (buttontype == "Get")
            {
                var getscreen = await _billingsoftware.SHScreenMaster.FirstOrDefaultAsync(x => x.ScreenId == model.ScreenId && x.IsDelete == false && x.BranchID == model.BranchID);
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
                var screentodelete = await _billingsoftware.SHScreenMaster.FindAsync(model.ScreenId, model.BranchID);

                if (screentodelete != null)
                {
                    if (screentodelete.IsDelete)
                    {
                        ViewBag.ErrorMessage = "Cannot update. Product is marked as deleted.";
                        return View("ScreenMaster", model);
                    }


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
                var screentoretrieve = await _billingsoftware.SHScreenMaster.FindAsync(model.ScreenId, model.BranchID);
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


            var existingscreen = await _billingsoftware.SHScreenMaster.FindAsync(model.ScreenId, model.BranchID);

            if (existingscreen != null)
            {
                if (existingscreen.IsDelete)
                {
                    ViewBag.ErrorMessage = "Cannot update. Product is marked as deleted.";
                    return View("ScreenMaster", model);
                }

                existingscreen.BranchID = model.BranchID;
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


        /* [HttpPost]
         public IActionResult getproductlist(ProductSelectModel model, string billid, string BillID, string buttonType, string SelectedProductID, string Quantity, string productid, string productname, string unitprice, string billdate, string customernumber)
         {
             if (TempData["BranchID"] != null)
             {
                 model.BranchID = TempData["BranchID"].ToString();
                 TempData.Keep("BranchID");
             }

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
                                    where (product.ProductID.Contains(model.ProductID) || product.BarcodeId.Contains(model.BarcodeID) && product.BranchID == model.BranchID)
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
                     model.Viewproductlist = (from product in _billingsoftware.SHProductMaster
                                              join rack in _billingsoftware.SHRackPartionProduct
                                              on product.ProductID equals rack.ProductID
                                              where (product.ProductID.Contains(model.ProductID) || product.BarcodeId.Contains(model.BarcodeID) && product.BranchID == model.BranchID)
                                              select new { product, rack })
                       .AsEnumerable() // Switch to client-side evaluation
                       .Where(pr => int.Parse(pr.rack.Noofitems) > 0) // Perform the int.Parse on the client side
                       .Select(pr => pr.product)
                       .ToList();
                     return View("ProductList", model);
                 }

                 int quantity;
                 if (!int.TryParse(Quantity, out quantity) || quantity <= 0) // Parse and check if Quantity is valid
                 {
                     ViewBag.enterquantity = "Please enter a valid quantity.";
                     model.Viewproductlist = (from product in _billingsoftware.SHProductMaster
                                              join rack in _billingsoftware.SHRackPartionProduct
                                              on product.ProductID equals rack.ProductID
                                              where (product.ProductID.Contains(model.ProductID) || product.BarcodeId.Contains(model.BarcodeID) && product.BranchID == model.BranchID)
                                              select new { product, rack })
                       .AsEnumerable() // Switch to client-side evaluation
                       .Where(pr => int.Parse(pr.rack.Noofitems) > 0) // Perform the int.Parse on the client side
                       .Select(pr => pr.product)
                       .ToList();

                     return View("ProductList", model);
                 }

                 var selectedProduct = _billingsoftware.SHProductMaster.FirstOrDefault(p => p.ProductID == SelectedProductID);
                 if (selectedProduct != null)
                 {
                     var existingDetail = _billingsoftware.SHbilldetails.FirstOrDefault(b =>
                b.BillID == TempData.Peek("BillID").ToString() && b.ProductID == selectedProduct.ProductID && b.BranchID == model.BranchID);

                     if (existingDetail != null)
                     {
                         existingDetail.BranchID = model.BranchID;
                         existingDetail.Quantity = Quantity;
                     }
                     else
                     {

                         var billDetail = new BillingDetailsModel
                         {
                             BranchID = model.BranchID,
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
         }*/

        [HttpPost]

        public async Task<IActionResult> getCustomerBill(BillProductlistModel model, string buttonType, string BillID, string BillDate, string CustomerNumber, string TotalPrice, BillingMasterModel masterModel, BillingDetailsModel detailModel, string Quantity)
        {


            if (TempData["BranchID"] != null)
            {
                model.BranchID = TempData["BranchID"].ToString();
                TempData.Keep("BranchID");
            }


            BusinessClassBilling Busbill = new BusinessClassBilling(_billingsoftware);
            ViewData["productid"] = Busbill.Getproduct(model.BranchID);



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
                return RedirectToAction("PaymentBilling", new { BillID = model.BillID });
            }



            if (buttonType == "Add Product")
            {

                if (TempData["BranchID"] != null)
                {
                    detailModel.BranchID = TempData["BranchID"].ToString();
                    TempData.Keep("BranchID");
                }

                var productlist = await _billingsoftware.SHProductMaster
                             .Where(p => p.ProductID == model.ProductID && p.BranchID == model.BranchID)
                             .Select(p => new BillingDetailsModel
                             {
                                 ProductID = p.ProductID,
                                 ProductName = p.ProductName,
                                 Price = p.Price,
                                 Quantity = Quantity,
                                 NetPrice = model.NetPrice

                             }).ToListAsync();

                var existingbilldetail = await _billingsoftware.SHbilldetails
            .FirstOrDefaultAsync(x => x.BillID == model.BillID && x.BillDate == model.BillDate && x.CustomerNumber == model.CustomerNumber && x.BranchID == model.BranchID && x.ProductID == model.ProductID);

                if (existingbilldetail != null)
                {
                    existingbilldetail.BillID = detailModel.BillID;
                    existingbilldetail.BillDate = detailModel.BillDate;
                    existingbilldetail.CustomerNumber = detailModel.CustomerNumber;
                    existingbilldetail.ProductID = detailModel.ProductID;
                    existingbilldetail.Discount = detailModel.Discount;
                    existingbilldetail.Price = detailModel.Price;
                    existingbilldetail.Quantity = detailModel.Quantity;
                    existingbilldetail.NetPrice = detailModel.NetPrice;
                    existingbilldetail.Totalprice = detailModel.Totalprice;
                    existingbilldetail.TotalDiscount = detailModel.TotalDiscount;
                    existingbilldetail.ProductName = detailModel.ProductName;
                    existingbilldetail.BranchID = detailModel.BranchID;
                    existingbilldetail.Lastupdateddate = DateTime.Now.ToString();
                    existingbilldetail.Lastupdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                    existingbilldetail.Lastupdateduser = User.Claims.First().Value.ToString();
                    _billingsoftware.Entry(existingbilldetail).State = EntityState.Modified;
                }
                else
                {
                    detailModel.Lastupdateduser = User.Claims.First().Value.ToString();
                    detailModel.Lastupdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                    detailModel.Lastupdateddate = DateTime.Now.ToString();

                    var product = productlist.FirstOrDefault();
                    if (product != null)
                    {
                        // Convert strings to numeric types
                        if (decimal.TryParse(product.Price, out decimal price) && int.TryParse(product.Quantity, out int quantity))
                        {
                            // Perform calculation
                            model.NetPrice = (price * quantity).ToString();
                        }
                        else
                        {
                            // Handle conversion failure
                            ModelState.AddModelError("", "Invalid price or quantity format.");
                            return View("CustomerBilling", model);
                        }

                        // Set detailModel properties
                        detailModel.ProductID = product.ProductID;
                        detailModel.ProductName = product.ProductName;
                        detailModel.Price = product.Price;
                        detailModel.Quantity = Quantity;
                        detailModel.NetPrice = model.NetPrice;
                        detailModel.BillID = BillID;
                        detailModel.BillDate = BillDate;
                        detailModel.CustomerNumber = CustomerNumber;
                    }

                    _billingsoftware.SHbilldetails.Add(detailModel);
                }

                await _billingsoftware.SaveChangesAsync();

                productlist = await _billingsoftware.SHbilldetails
          .Where(d => d.BillID == BillID && d.BillDate == BillDate && d.CustomerNumber == CustomerNumber && d.BranchID == detailModel.BranchID)
          .Select(d => new BillingDetailsModel
          {
              ProductID = d.ProductID,
              ProductName = d.ProductName,
              Price = d.Price,
              Quantity = d.Quantity,
              NetPrice = d.NetPrice
          }).ToListAsync();


                model.Viewbillproductlist = productlist;

                return View("CustomerBilling", model);
            }



            if (buttonType == "Get")
            {
                var billID = model.BillID;
                var billDate = model.BillDate;
                var customerNumber = model.CustomerNumber;

                var updatedMasterex = _billingsoftware.SHbillmaster.FirstOrDefault(m =>
                    m.BillID == billID && m.BillDate == billDate && m.CustomerNumber == customerNumber && m.IsDelete == false && m.BranchID == model.BranchID);

                if (updatedMasterex != null)
                {
                    ViewBag.TotalPrice = updatedMasterex.Totalprice;
                    ViewBag.TotalDiscount = updatedMasterex.TotalDiscount;
                    ViewBag.NetPrice = updatedMasterex.NetPrice;

                    var exbillingDetails = _billingsoftware.SHbilldetails
                        .Where(d => d.BillID == billID && d.BranchID == model.BranchID)
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
    new SqlParameter("@CGSTPercentage", DBNull.Value),
     new SqlParameter("@SGSTPercentage", DBNull.Value),
     new SqlParameter("@CGSTPercentageAmt", DBNull.Value),
     new SqlParameter("@SGSTPercentageAmt", DBNull.Value),
     new SqlParameter("@BranchID", masterModel.BranchID),
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

                await _billingsoftware.Database.ExecuteSqlRawAsync("EXEC InsertBillProduct @BillID, @BillDate, @CustomerNumber, @TotalPrice, @TotalDiscount, @NetPrice,@CGSTPercentage,@SGSTPercentage,@CGSTPercentageAmt,@SGSTPercentageAmt,@BranchID, @IsDelete, @LastUpdatedUser, @LastUpdatedDate, @LastUpdatedMachine, @ProductID, @ProductName, @Discount, @Price, @Quantity", parameter);



                ViewBag.DelMessage = "Deleted Bill Successfully";
                return View("CustomerBilling", model);

            }


            if (buttonType == "Save")
            {

                if (TempData["BranchID"] != null)
                {
                    masterModel.BranchID = TempData["BranchID"].ToString();
                    TempData.Keep("BranchID");
                }

            
                //   var getexistingprice = await _billingsoftware.SHbilldetails.FirstOrDefaultAsync(x => x.BillID == model.BillID && x.BillDate == model.BillDate && x.CustomerNumber == CustomerNumber && x.BranchID == model.BranchID);
                var getexistingprice = await _billingsoftware.SHbilldetails
             .Where(x => x.BillID == model.BillID && x.BillDate == model.BillDate && x.CustomerNumber == CustomerNumber && x.BranchID == model.BranchID)
             .ToListAsync();

                if (getexistingprice!=null )
                {
                    var totalPrice = getexistingprice.Sum(x =>
                    {
                        decimal price;
                        return decimal.TryParse(x.Price, out price) ? price : 0;
                    });

                    var totalNetPrice = getexistingprice.Sum(x =>
                    {
                        decimal netPrice;
                        return decimal.TryParse(x.NetPrice, out netPrice) ? netPrice : 0;
                    });


                    // Retrieve the existing master record
                    var updateMaster = await _billingsoftware.SHbillmaster
                        .FirstOrDefaultAsync(m => m.BillID == model.BillID && m.BranchID == model.BranchID && m.BillDate==model.BillDate && m.CustomerNumber==model.CustomerNumber);

                    if (updateMaster != null)
                    {

                        updateMaster.BillID = masterModel.BillID;
                        updateMaster.BillDate = masterModel.BillDate;
                        updateMaster.CustomerNumber = masterModel.CustomerNumber;
                        updateMaster.Totalprice = totalPrice.ToString("F2");
                        updateMaster.TotalDiscount = masterModel.TotalDiscount;
                        updateMaster.NetPrice = totalNetPrice.ToString("F2");
                        updateMaster.CGSTPercentage = masterModel.CGSTPercentage;
                        updateMaster.CGSTPercentageAmt = masterModel.CGSTPercentageAmt;
                        updateMaster.SGSTPercentage = masterModel.SGSTPercentage;
                        updateMaster.SGSTPercentageAmt = masterModel.SGSTPercentageAmt;
                        updateMaster.BranchID = masterModel.BranchID;
                        updateMaster.Lastupdateduser = User.Claims.First().Value.ToString();
                        updateMaster.Lastupdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                        updateMaster.Lastupdateddate = DateTime.Now.ToString();

                        _billingsoftware.Entry(updateMaster).State = EntityState.Modified;

            var isDeleteValue = (object)masterModel.IsDelete ?? DBNull.Value;

                    }
                    else
                    {
                        masterModel.Totalprice = totalPrice.ToString("F2");
                        masterModel.NetPrice = totalNetPrice.ToString("F2");
                        masterModel.Lastupdateduser = User.Claims.First().Value.ToString();
                        masterModel.Lastupdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                        masterModel.Lastupdateddate = DateTime.Now.ToString();

                        _billingsoftware.SHbillmaster.Add(masterModel);

                    }

                    _billingsoftware.SaveChanges();

                }

                var updatedMaster = await _billingsoftware.SHbillmaster
       .Where(m => m.BillID == masterModel.BillID && m.BranchID == model.BranchID && m.BillDate==masterModel.BillDate&&m.CustomerNumber==masterModel.CustomerNumber)
       .FirstOrDefaultAsync();

                if (updatedMaster != null)
                {
                    ViewBag.TotalPrice = updatedMaster.Totalprice;
                    ViewBag.TotalDiscount = updatedMaster.TotalDiscount;
                    ViewBag.NetPrice = updatedMaster.NetPrice;
                }


                var billingDetails = await _billingsoftware.SHbilldetails
           .Where(d => d.BillID == masterModel.BillID && d.BranchID == model.BranchID && d.BillDate == masterModel.BillDate && d.CustomerNumber == masterModel.CustomerNumber)
           .ToListAsync();

                model.MasterModel = updatedMaster;
                model.Viewbillproductlist = billingDetails;


                ViewBag.Message = "Saved Successfully";
               // return View("CustomerBilling", model);
            }
            return View("CustomerBilling", model);
        }


        public IActionResult DeleteProduct(string productId,BillProductlistModel model)
        {
            if (TempData["BranchID"] != null)
            {
                model.BranchID = TempData["BranchID"].ToString();
                TempData.Keep("BranchID");
            }

            var product = _billingsoftware.SHbilldetails
      .Where(p => p.ProductID == productId)
      .Select(p => new
      {
          p.Quantity,
          p.BranchID,
          p.IsDelete
      })
      .FirstOrDefault();

            if (product != null)
            {
                // Update the IsDelete field to true
                var productToUpdate = _billingsoftware.SHbilldetails
                    .First(p => p.ProductID == productId && p.BranchID == model.BranchID);

                productToUpdate.IsDelete = true;

                // Update SHRackPartionProduct to add back the quantity
                var rackProduct = _billingsoftware.SHRackPartionProduct
                    .FirstOrDefault(r => r.ProductID == productId && r.BranchID == product.BranchID);

                if (rackProduct != null)
                {
                    rackProduct.Noofitems += product.Quantity; // Add the quantity back
                }

                _billingsoftware.SaveChanges();
            }

            return View("CustomerBilling");
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

            BranchMasterModel par = new BranchMasterModel();
            return View("BranchMaster", par);
        }



        public IActionResult StaffAdmin()
        {

            BusinessClassBilling Busbill = new BusinessClassBilling(_billingsoftware);
            ViewData["resoruseid"] = Busbill.GetResourceid();
            ViewData["branchid"] = Busbill.Getbranch();

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

            if (TempData["BranchID"] != null)
            {
                model.BranchID = TempData["BranchID"].ToString();
                TempData.Keep("BranchID");
            }

            BusinessClassBilling Busbill = new BusinessClassBilling(_billingsoftware);
            ViewData["productid"] = Busbill.Getproduct(model.BranchID);


            // Retrieve selected product
            var selectedProduct = _billingsoftware.SHProductMaster.FirstOrDefault(p => p.ProductID ==productid);

            if (selectedProduct != null)
            {
                // Retrieve bill detail for the specified BillID and ProductID
                var billDetail = _billingsoftware.SHbilldetails
                    .Where(b => b.BillID == billid && b.ProductID == productid && b.BranchID == model.BranchID)
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

        public IActionResult PaymentBilling()
        {
            PaymentTableViewModel obj = new PaymentTableViewModel();
            return View(obj);

        }

        [HttpPost]
        public async Task<IActionResult> PaymentAction(PaymentTableViewModel model, string buttonType, string selectedSlotId)
        {
            if (TempData["BranchID"] != null)
            {
                model.BranchID = TempData["BranchID"].ToString();
                TempData.Keep("BranchID");
            }


            if (buttonType == "GetBill")
            {
                var exbill = await _billingsoftware.SHbillmaster.Where(x => x.BillID == model.BillId && x.BillDate ==model.BillDate && x.BranchID == model.BranchID).FirstOrDefaultAsync();
                model.Balance = exbill.NetPrice;
            }

            if(buttonType == "DeletePayment")
            {
                //Delete Details from DB
                //Delete from database
                var selectedDBpayment = _billingsoftware.SHPaymentDetails.Where(x => x.PaymentId == model.PaymentId && x.BranchID == model.BranchID).ToList();
                foreach(var item in selectedDBpayment)
                {
                    _billingsoftware.SHPaymentDetails.Remove(item);
                    _billingsoftware.SaveChanges();
                }


                //Delete Master
                var SelectedPayMas = _billingsoftware.SHPaymentMaster.SingleOrDefault(x =>x.BillId ==model.BillId && x.BillDate == model.BillDate && x.PaymentId == model.PaymentId && x.BranchID == model.BranchID);

                _billingsoftware.SHPaymentMaster.Remove(SelectedPayMas);
                _billingsoftware.SaveChanges();

            }
            if(buttonType == "GetPayment")
            {
                var selectDBpayment = _billingsoftware.SHPaymentDetails.Where(x => x.PaymentId == model.PaymentId && x.BranchID == model.BranchID).ToList();

                var SelectPayMas = _billingsoftware.SHPaymentMaster.SingleOrDefault(x => x.BillId == model.BillId && x.BillDate == model.BillDate && x.PaymentId == model.PaymentId && x.BranchID == model.BranchID);

                if(model.Viewpayment ==null)
                    model.Viewpayment = selectDBpayment;

                model.BillDate = SelectPayMas.BillDate;
                model.PaymentId = SelectPayMas.PaymentId;
                model.BranchID = SelectPayMas.BranchID;
                model.Balance = SelectPayMas.Balance;
                model.BillId = SelectPayMas.BillId;

            }

            if (buttonType == "DeletePaymentDetail")
            {
                //Delete from database
                var selectedDBpayment = _billingsoftware.SHPaymentDetails.SingleOrDefault(x => x.PaymentDiscription == selectedSlotId && x.PaymentId == model.PaymentId && x.BranchID == model.BranchID);
                if (selectedDBpayment != null)
                {
                    _billingsoftware.SHPaymentDetails.Remove(selectedDBpayment);
                    _billingsoftware.SaveChanges();
                }

                //Delete from grid
                var selectedpayment = model.Viewpayment.SingleOrDefault(x => x.PaymentDiscription == selectedSlotId);
                if (selectedpayment != null)
                {
                    model.Viewpayment.Remove(selectedpayment);
                }            

            }



            if(buttonType == "Save")
            {
                var objbillmaster = new PaymentMasterModel()
                {
                    BillDate = model.BillDate,
                    PaymentId =model.PaymentId,
                    BranchID =model.BranchID,
                    Balance = model.Balance,
                    BillId= model.BillId                   
                    
                };

                var objpaymas = _billingsoftware.SHPaymentMaster.Where(x => x.BillId == model.BillId && x.BranchID == model.BranchID && x.PaymentId ==model.PaymentId).FirstOrDefault();

                 if(objpaymas != null)
                {
                    objpaymas.BranchID = model.BranchID;
                    objpaymas.Lastupdateddate = "";
                    objpaymas.Lastupdateduser = "";
                    objpaymas.Lastupdatedmachine = "";
                    objpaymas.Balance = model.Balance;
                    objpaymas.BillDate = model.BillDate;
                    objpaymas.BillId = model.BillId;

                    _billingsoftware.Entry(objpaymas).State = EntityState.Modified;
                }
                 else
                {
                    _billingsoftware.SHPaymentMaster.Add(objbillmaster);
                }

                _billingsoftware.SaveChanges();

                foreach (var objdetail in model.Viewpayment)
                {
                    var obpaydet = _billingsoftware.SHPaymentDetails.Where(x=> x.BranchID==model.BranchID && x.PaymentDiscription ==objdetail.PaymentDiscription&& x.PaymentId == objdetail.PaymentId).FirstOrDefault();

                    if(obpaydet != null)
                    {
                        obpaydet.BranchID = model.BranchID;
                        obpaydet.Lastupdateduser = "";
                        obpaydet.Lastupdatedmachine = "";
                        obpaydet.Lastupdatedmachine = "";
                        obpaydet.PaymentAmount = objdetail.PaymentAmount;
                        obpaydet.PaymentDate = objdetail.PaymentDate;
                        obpaydet.PaymentDiscription = objdetail.PaymentDiscription;
                        obpaydet.PaymentId = objdetail.PaymentId;
                        obpaydet.PaymentMode = objdetail.PaymentMode;
                        obpaydet.PaymentTransactionNumber = objdetail.PaymentTransactionNumber;

                        _billingsoftware.Entry(obpaydet).State = EntityState.Modified;

                    }
                    else
                    {
                        objdetail.PaymentId = model.PaymentId;
                        objdetail.BranchID = model.BranchID;
                        _billingsoftware.SHPaymentDetails.Add(objdetail);
                    }

                    _billingsoftware.SaveChanges();
                }
                                

            }
            if (buttonType == "AddPayment")
            {
                PaymentDetailsModel objNewPayment = new PaymentDetailsModel();
                objNewPayment.PaymentDiscription = model.PaymentId + DateTime.Now.ToString();
                objNewPayment.PaymentId = model.PaymentId;
                objNewPayment.BranchID = model.BranchID;

                List<PaymentDetailsModel> Objlistpayment = new List<PaymentDetailsModel>();
                Objlistpayment.Add(objNewPayment);

                if (model.Viewpayment == null)
                    model.Viewpayment = Objlistpayment;
                else
                    model.Viewpayment.Add(objNewPayment);

            }


            return View("PaymentBilling",model);
        }
    }
}

