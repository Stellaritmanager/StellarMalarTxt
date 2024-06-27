
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
                    //var getbusproduct = await business.GetProductmaster(model.ProductID);

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
                // Retrieve logic: Set a database value to 0 and retrieve values

                var productToRetrieve = await _billingsoftware.SHProductMaster.FindAsync(model.ProductID);
                if (productToRetrieve != null)
                {
                    // Assuming you have a property like IsRetrieved in your model
                    productToRetrieve.IsDelete = false; // Set a specific database value to 0

                    await _billingsoftware.SaveChangesAsync();
                    // Assuming you want to retrieve certain values and display them in textboxes
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
