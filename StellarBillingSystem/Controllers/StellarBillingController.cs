
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StellarBillingSystem.Business;
using StellarBillingSystem.Context;
using StellarBillingSystem.Models;

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
            if (buttonType == "Get")
            {
                var getcategory = await _billingsoftware.SHCategoryMaster.FirstOrDefaultAsync(x => x.CategoryID == model.CategoryID && !x.IsDelete);
                if (getcategory != null)
                {
                    return View("CategoryMasterModel", getcategory);
                }
                else
                {
                    CategoryMasterModel par = new CategoryMasterModel();
                    ViewBag.ErrorMessage = "No value for this Category ID";
                    return View("CategoryMasterModel", par);
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
                    return View("CategoryMasterModel", model);
                }
                else
                {
                    ViewBag.ErrorMessage = "Category not found";
                    return View("CategoryMasterModel", model);
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
                }
                return View("CategoryMasterModel", model);
            }
            else if (buttonType == "save")
            {
                var existingCategory = await _billingsoftware.SHCategoryMaster.FindAsync(model.CategoryID);
                if (existingCategory != null)
                {
                    if (existingCategory.IsDelete)
                    {
                        ViewBag.ErrorMessage = "Cannot Save or Update. Category is marked as deleted.";
                        return View("CategoryMasterModel", model);
                    }
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
            }
            return View("CategoryMasterModel", model);
        }

        [HttpPost]

        public async Task<IActionResult> AddProduct(ProductMatserModel model, string buttonType)
        {
            BusinessClassBilling business = new BusinessClassBilling(_billingsoftware);
            ViewData["categoryid"] = business.GetCatid();

            if (buttonType == "Get")
            {
                var resultpro = await _billingsoftware.SHProductMaster.FirstOrDefaultAsync(x => x.ProductID == model.ProductID && !x.IsDelete);
                if (resultpro != null)
                {
                    return View("ProductMasterModel", resultpro);
                }
                else
                {
                    ProductMatserModel obj = new ProductMatserModel();
                    ViewBag.ErrorMessage = "No value for this product ID";
                    return View("ProductMasterModel", obj);
                }
            }
            else if (buttonType == "Delete")
            {
                var productToDelete = await _billingsoftware.SHProductMaster.FindAsync(model.ProductID);
                if (productToDelete != null)
                {
                    productToDelete.IsDelete = true; // Mark the product as deleted
                    await _billingsoftware.SaveChangesAsync();

                    ViewBag.Message = "Product deleted successfully";
                    return View("ProductMasterModel", model); // Assuming you want to return the view with the same model
                }
                else
                {
                    ViewBag.ErrorMessage = "Product not found";
                    return View("ProductMasterModel", model); // Return the view with the model
                }
            }
            else if (buttonType == "DeleteRetrieve")
            {
                var productToRetrieve = await _billingsoftware.SHProductMaster.FindAsync(model.ProductID);
                if (productToRetrieve != null)
                {
                    productToRetrieve.IsDelete = false; // Set a specific database value to 0

                    await _billingsoftware.SaveChangesAsync();

                    model.ProductID = productToRetrieve.ProductID;
                    model.CategoryID = productToRetrieve.CategoryID;
                    model.ProductName = productToRetrieve.ProductName;
                    model.Brandname = productToRetrieve.Brandname;
                    model.Price = productToRetrieve.Price;
                    model.Discount = productToRetrieve.Discount;
                    model.TotalAmount = productToRetrieve.TotalAmount;

                    ViewBag.Message = "Product retrieved successfully";

                    }
                else
                {
                    ViewBag.ErrorMessage = "Product not found";
                }

                return View("ProductMasterModel", model);
            }
            else if (buttonType == "Save")
            {

                // Fetch discount price based on CategoryID
                if (!string.IsNullOrEmpty(model.CategoryID))
                {
                    var discountCategory = await _billingsoftware.SHDiscountCategory
                        .FirstOrDefaultAsync(x => x.CategoryID == model.CategoryID);
                    if (discountCategory != null)
                    {
                        model.Discount = discountCategory.DiscountPrice;
                    }
                }

                var existingProduct = await _billingsoftware.SHProductMaster.FindAsync(model.ProductID);
                if (existingProduct != null)
                {
                    if (existingProduct.IsDelete)
                    {
                        ViewBag.ErrorMessage = "Cannot update. Product is marked as deleted.";
                        return View("ProductMasterModel", model);
                    }


                    existingProduct.ProductID = model.ProductID;
                    existingProduct.CategoryID = model.CategoryID;
                    existingProduct.ProductName = model.ProductName;
                    existingProduct.Brandname = model.Brandname;
                    existingProduct.Price = model.Price;
                    existingProduct.Discount = model.Discount;
                    decimal price = decimal.Parse(model.Price);
                    decimal discount = decimal.Parse(model.Discount);
                    decimal totalAmount = price - (price * discount / 100);
                    existingProduct.TotalAmount = totalAmount.ToString();

                    // existingProduct.TotalAmount = model.TotalAmount - (model.Price * model.Discount / 100 = model.TotalAmount);
                    existingProduct.LastUpdatedDate = DateTime.Now.ToString();
                    existingProduct.LastUpdatedUser = User.Claims.First().Value.ToString();
                    existingProduct.LastUpdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                    _billingsoftware.Entry(existingProduct).State = EntityState.Modified;
                }
                else
                {

                    // Convert strings to decimals, calculate TotalAmount, and convert back to string
                    decimal price = decimal.Parse(model.Price);
                    decimal discount = decimal.Parse(model.Discount);
                    decimal totalAmount = price - (price * discount / 100);
                    model.TotalAmount = totalAmount.ToString();
                    model.LastUpdatedDate = DateTime.Now.ToString();
                    model.LastUpdatedUser = User.Claims.First().Value.ToString();
                    model.LastUpdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                    _billingsoftware.SHProductMaster.Add(model);
                }

                await _billingsoftware.SaveChangesAsync();

                ViewBag.Message = "Saved Successfully";
            }

            return View("ProductMasterModel", model);
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

        public async Task<IActionResult> GodDown(GodownModel model)
        {
            var existinggoddown = await _billingsoftware.SHGodown.FindAsync(model.ProductID, model.DatefofPurchase, model.SupplierInformation);
            if (existinggoddown != null)
            {
                existinggoddown.ProductID = model.ProductID;
                existinggoddown.NumberofStocks = model.NumberofStocks;
                existinggoddown.DatefofPurchase = model.DatefofPurchase;
                existinggoddown.SupplierInformation = model.SupplierInformation;
               /* existinggoddown.StrIsDelete = model.StrIsDelete;*/
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


            ViewBag.Message = "Saved Successfully";

            return View("GodownModel", model);

        }

       /* [HttpPost]
        public async Task<IActionResult> DeleteGodown(string productID, string dateOfPurchase, string supplierInformation)
        {
            try
            {
                var godownToDelete = await _billingsoftware.SHGodown.FindAsync(productID, dateOfPurchase, supplierInformation);

                if (godownToDelete != null)
                {
                    godownToDelete.IsDelete = true; // Assuming IsDelete is an integer field
                    _billingsoftware.Entry(godownToDelete).State = EntityState.Modified;
                    await _billingsoftware.SaveChangesAsync();
                }

                return RedirectToAction("Index"); // Redirect to a success page or appropriate action
            }
            catch (Exception ex)
            {
                // Handle exceptions
                ViewBag.Error = "An error occurred: " + ex.Message;
                return View("Error"); // or return an appropriate error view
            }
        }*/




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
            
            return View("CustomerMaster" , model);
        }
        public async Task<IActionResult> GetCustomer(string mobileNumber)
        {
            if (string.IsNullOrEmpty(mobileNumber))
            {
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
                return BadRequest("Mobile number is required");
            }

            var customer = await _billingsoftware.SHCustomerMaster.FindAsync(mobileNumber);
            if (customer == null)
            {
                ViewBag.ErrorMessage = "Mobile Number not found";
                return View("Error", new CustomerMasterModel());
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
            ViewBag.DelRetrieve = "Retrieve Successfully";

            return View("CustomerMaster", customer);
        }



        public async Task<IActionResult> DeleteCustomer(string mobileNumber)
        {
            if (string.IsNullOrEmpty(mobileNumber))
            {
                return BadRequest("Mobile number is required");
            }

            var existingCustomer = await _billingsoftware.SHCustomerMaster.FindAsync(mobileNumber);
            if (existingCustomer == null)
            {
                return NotFound("Customer not found");
            }

            existingCustomer.IsDelete = true;
            existingCustomer.LastUpdatedDate = DateTime.Now.ToString();
            existingCustomer.LastUpdatedUser = User.Claims.First().Value.ToString();
            existingCustomer.LastUpdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();

            _billingsoftware.Entry(existingCustomer).State = EntityState.Modified;

            await _billingsoftware.SaveChangesAsync();

            ViewBag.delMessage = "Deleted Successfully";

            return View("CustomerMaster"); // Redirect to the main view or another appropriate view
        }


        [HttpPost]
        public async Task<IActionResult> AddDiscountCategory(DiscountCategoryMasterModel model , string buttonType)
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

        public async Task<IActionResult>AddNetDiscount(NetDiscountMasterModel model)
        {
            var existingnetdiscount = await _billingsoftware.SHNetDiscountMaster.FindAsync(model.NetDiscount);
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

                model.LastUpdatedDate = DateTime.Now.ToString();
                model.LastUpdatedUser = User.Claims.First().Value.ToString();
                model.LastUpdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                _billingsoftware.SHNetDiscountMaster.Add(model);

            }

            await _billingsoftware.SaveChangesAsync();

            ViewBag.Message = "Saved Successfully";

            return View("NetDiscountMaster", model);

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
            if (existingpoints != null)
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
            CategoryMasterModel par = new CategoryMasterModel();
            return View("CategoryMasterModel", par);
        }

        public IActionResult ProductMasterModel()
        {
            BusinessClassBilling business = new BusinessClassBilling(_billingsoftware);
            ViewData["categoryid"] = business.GetCatid();
            ProductMatserModel obj = new ProductMatserModel();
            return View("ProductMasterModel", obj);
        }

        [HttpPost]
        public async Task<IActionResult> AddRackPartition(RackPatrionProductModel model,string buttonType,RackpartitionViewModel viewmodel)
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



            var existingrackpartition = await _billingsoftware.SHRackPartionProduct.FindAsync(model.PartitionID,model.ProductID);
            if (existingrackpartition != null)
            {
                existingrackpartition.PartitionID = model.PartitionID;
                existingrackpartition.ProductID = model.ProductID;
                existingrackpartition.Noofitems = model.Noofitems;
                existingrackpartition.LastUpdatedDate = DateTime.Now.ToString();
                existingrackpartition.LastUpdatedUser = User.Claims.First().Value.ToString();
                existingrackpartition.LastUpdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                _billingsoftware.Entry(existingrackpartition).State = EntityState.Modified;
            }
            else
            {

                model.LastUpdatedDate = DateTime.Now.ToString();
                model.LastUpdatedUser = User.Claims.First().Value.ToString();
                model.LastUpdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                _billingsoftware.SHRackPartionProduct.Add(model);
            }
            await _billingsoftware.SaveChangesAsync();

            ViewBag.Message = "Saved Successfully";

//Repopulate the table after save 

            var updatedResult = business.GetRackview(model.PartitionID, model.ProductID);
            var updatedViewModelList = updatedResult.Select(p => new RackPatrionProductModel
            {
                ProductID = p.ProductID,
                PartitionID = p.PartitionID,
                Noofitems = p.Noofitems
            }).ToList();

            viewmodel.Viewrackpartition = updatedViewModelList;

            return View("RackPatrionProduct", viewmodel);
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
        public async Task<IActionResult> Delete(string partitionID, string productID)
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
            return View("RackPatrionProduct");
        }

// staff reg
        [HttpPost]
        public async Task<IActionResult> AddStaff(StaffAdminModel model,string buttontype)
        {
            BusinessClassBilling Busbill = new BusinessClassBilling(_billingsoftware);
            ViewData["resoruseid"] = Busbill.GetResourceid();


            if (buttontype == "Get")
            {
                var getstaff = await _billingsoftware.SHStaffAdmin.FirstOrDefaultAsync(x => x.StaffID == model.StaffID&&x.IsDelete==false);
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
                    return View("StaffAdmin",stafftodelete);
                }
                else
                {
                    ViewBag.delnoMessage = "StaffID not found";
                    return View("StaffAdmin");
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
                    model.FullName=stafftoretrieve.FullName;
                    model.ResourceTypeID = stafftoretrieve.ResourceTypeID;
                    model.FirstName= stafftoretrieve.FirstName;
                    model.LastName= stafftoretrieve.LastName;
                    model.Initial = stafftoretrieve.Initial;
                    model.Prefix= stafftoretrieve.Prefix;
                    model.PhoneNumber= stafftoretrieve.PhoneNumber;
                    model.DateofBirth= stafftoretrieve.DateofBirth;
                    model.Age= stafftoretrieve.Age;
                    model.Gender= stafftoretrieve.Gender;
                    model.Address1= stafftoretrieve.Address1;
                    model.City = stafftoretrieve.City;
                    model.State= stafftoretrieve.State;
                    model.Pin= stafftoretrieve.Pin;
                    model.EmailId= stafftoretrieve.EmailId;
                    model.Nationality= stafftoretrieve.Nationality;
                    model.UserName= stafftoretrieve.UserName;
                    model.Password= stafftoretrieve.Password;
                    model.IdProofId= stafftoretrieve.IdProofId;
                    model.IdProofName= stafftoretrieve.IdProofName;

                    ViewBag.retMessage = "Deleted StaffID retrieved successfully";
                }
                else
                {
                    ViewBag.noretMessage = "StaffID not found";
                }
                return View("StaffAdmin", model);
            }


            var existingStaffAdmin = await _billingsoftware.SHStaffAdmin.FindAsync(model.StaffID);

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
                    return View("ResourceTypeMaster", restodelete);
                }
                else
                {
                    ViewBag.delnoMessage = "ResourceTypeID not found";
                    return View("ResourceTypeMaster");
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
                    ViewBag.noretMessage = "ResourceTypeID not found";
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
                var getrol = await _billingsoftware.SHRoleaccessModel.FirstOrDefaultAsync(x => x.RollID == model.RollID &&x.ScreenID==model.ScreenID && x.Isdelete == false);
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
                var roletodelete = await _billingsoftware.SHRoleaccessModel.FindAsync(model.RollID,model.ScreenID);
                if (roletodelete != null)
                {
                    roletodelete.Isdelete = true;
                    await _billingsoftware.SaveChangesAsync();

                    ViewBag.delMessage = "RollID deleted successfully";
                    return View("RoleAccess", roletodelete);
                }
                else
                {
                    ViewBag.delnoMessage = "RollID not found";
                    return View("RoleAccess");
                }

            }

            else if (buttontype == "DeleteRetrieve")
            {
                var roltoretrieve = await _billingsoftware.SHRoleaccessModel.FindAsync(model.RollID,model.ScreenID);
                if (roltoretrieve != null)
                {
                    roltoretrieve.Isdelete = false;

                    await _billingsoftware.SaveChangesAsync();

                    model.RollID = roltoretrieve.RollID;
                    model.ScreenID = roltoretrieve.ScreenID;
                    model.Access=roltoretrieve.Access;
                    model.Authorized = roltoretrieve.Authorized;

                    ViewBag.retMessage = "Deleted RollID retrieved successfully";
                }
                else
                {
                    ViewBag.noretMessage = "RollID not found";
                }
                return View("RoleAccess", model);
            }


            var existingrole = await _billingsoftware.SHRoleaccessModel.FindAsync(model.RollID,model.ScreenID);

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
            return View("RoleAccess", model);

        }

        public async Task<IActionResult> AddRollmaster(RollAccessMaster model, string buttontype)
        {
            BusinessClassBilling Busbill = new BusinessClassBilling(_billingsoftware);
            ViewData["resoruseid"] = Busbill.GetResourceid();
            ViewData["staffid"] = Busbill.GetStaffID();


            if (buttontype == "Get")
            {
                var getroll = await _billingsoftware.SHrollaccess.FirstOrDefaultAsync(x => x.RollID == model.RollID && x.StaffID == model.StaffID && x.IsDelete == false);
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
                var rolltodelete = await _billingsoftware.SHrollaccess.FindAsync(model.RollID, model.StaffID);
                if (rolltodelete != null)
                {
                    rolltodelete.IsDelete = true;
                    await _billingsoftware.SaveChangesAsync();

                    ViewBag.delMessage = "RollID deleted successfully";
                    return View("RollAccessMaster", rolltodelete);
                }
                else
                {
                    ViewBag.delnoMessage = "RollID not found";
                    return View("RollAccessMaster");
                }

            }

            else if (buttontype == "DeleteRetrieve")
            {
                var rolltoretrieve = await _billingsoftware.SHrollaccess.FindAsync(model.RollID, model.StaffID);
                if (rolltoretrieve != null)
                {
                    rolltoretrieve.IsDelete = false;

                    await _billingsoftware.SaveChangesAsync();

                    model.RollID = rolltoretrieve.RollID;
                    model.StaffID = rolltoretrieve.StaffID;
                   
                    ViewBag.retMessage = "Deleted RollID retrieved successfully";
                }
                else
                {
                    ViewBag.noretMessage = "RollID not found";
                }
                return View("RollAccessMaster", model);
            }


            var existingroll = await _billingsoftware.SHrollaccess.FindAsync(model.RollID, model.StaffID);

            if (existingroll != null)
            {
                existingroll.RollID = model.RollID;
                existingroll.StaffID = model.StaffID;
                existingroll.LastupdatedDate = DateTime.Now.ToString();
                existingroll.Lastupdateduser = User.Claims.First().Value.ToString();
                existingroll.LastupdatedMachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                _billingsoftware.Entry(existingroll).State = EntityState.Modified;

            }
            else
            {

                model.LastupdatedDate = DateTime.Now.ToString();
                model.Lastupdateduser = User.Claims.First().Value.ToString();
                model.LastupdatedMachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                _billingsoftware.SHrollaccess.Add(model);
            }
            await _billingsoftware.SaveChangesAsync();

            ViewBag.Message = "Saved Successfully";
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
                    return View("RollTypeMaster", rolltypetodelete);
                }
                else
                {
                    ViewBag.delnoMessage = "RollID not found";
                    return View("RollTypeMaster");
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
                    return View("ScreenMaster", screentodelete);
                }
                else
                {
                    ViewBag.delnoMessage = "ScreenId not found";
                    return View("ScreenMaster");
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
            return View("ScreenMaster", model);

        }





        public IActionResult RollTypeMaster()
        {
            RollTypeMaster rolltype = new RollTypeMaster();

            return View("RollTypeMaster", rolltype);
        }

        public IActionResult StaffAdmin()
        {

            BusinessClassBilling Busbill = new BusinessClassBilling(_billingsoftware);
            ViewData["resoruseid"] = Busbill.GetResourceid();

            StaffAdminModel par = new StaffAdminModel();

            return View("StaffAdmin",par);
        }

        public IActionResult ResourceTypeMaster()
        {
            ResourceTypeMasterModel res = new ResourceTypeMasterModel();

            return View("ResourceTypeMaster", res);
        }

        public IActionResult RollAccessMaster()
        {
            BusinessClassBilling Busbill = new BusinessClassBilling(_billingsoftware);
            ViewData["resoruseid"] = Busbill.GetResourceid();
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
            return View();
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
            BusinessClassBilling business = new BusinessClassBilling(_billingsoftware);
            ViewData["godownproductid"] = business.GetProductid();

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
