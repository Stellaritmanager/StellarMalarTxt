
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using StellarBillingSystem.Business;
using StellarBillingSystem.Context;
using StellarBillingSystem.Models;

namespace StellarBillingSystem.Controllers
{
    [Authorize]
    public class StellarBillingController : Controller
    {
        private BillingContext _billingsoftware;

        public StellarBillingController(BillingContext billingsoftware)
        {
            _billingsoftware = billingsoftware;
        }


        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryMasterModel model, string buttonType)
        {
            if (buttonType == "Save")
            {
                var existingCategory = await _billingsoftware.SHCategoryMaster.FindAsync(model.CategoryID);
                if (existingCategory != null)
                {
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
                    model.LastUpdatedUser = User.Claims.First().Value.ToString();
                    model.LastUpdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();


                    _billingsoftware.SHCategoryMaster.Add(model);
                }


                await _billingsoftware.SaveChangesAsync();

                ViewBag.Message = "Saved Successfully";
            }
                return View("CategoryMasterModel");
            
        }

        [HttpPost]

        public async Task<IActionResult> AddProduct(ProductMatserModel model,string buttonType)
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
            else if (buttonType == "Retrieve")
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

            await _billingsoftware.SaveChangesAsync();

            ViewBag.Message = "Saved Successfully";

            return View("CustomerMaster", model);
        }

        [HttpPost]
        public async Task<IActionResult> AddDiscountCategory(DiscountCategoryMasterModel model)
        {
            BusinessClassBilling business = new BusinessClassBilling(_billingsoftware);
            ViewData["discountcategoryid"] = business.GetcategoryID();

            var existingDiscountCategory = await _billingsoftware.SHDiscountCategory.FindAsync(model.CategoryID);
            if (existingDiscountCategory != null)
            {
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

        public async Task<IActionResult> AddNetDiscount(NetDicsountMasterModel model)
        {
            var existingnetdiscount = await _billingsoftware.SHNetDiscountMaster.FindAsync(model.NetDiscountID);
            if (existingnetdiscount != null)
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
            if (existingvoucher != null)
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
        public async Task<IActionResult> Addpoinsredeem(PointsReedemDetailsModel model)
        {
            BusinessClassBilling business = new BusinessClassBilling(_billingsoftware);
            ViewData["pointreedemcustomerid"] = business.GetCustomerid();

            var existingpointsredeem = await _billingsoftware.SHPointsReedemDetails.FindAsync(model.CustomerID);
            if (existingpointsredeem != null)
            {
                existingpointsredeem.CustomerID = model.CustomerID;
                existingpointsredeem.TotalPoints = model.TotalPoints;
                existingpointsredeem.ExpiryDate = model.ExpiryDate;
                existingpointsredeem.LastUpdatedDate = DateTime.Now.ToString();
                existingpointsredeem.LastUpdatedUser = User.Claims.First().Value.ToString();
                existingpointsredeem.LastUpdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                _billingsoftware.Entry(existingpointsredeem).State = EntityState.Modified;
            }
            else
            {
                model.LastUpdatedDate = DateTime.Now.ToString();
                model.LastUpdatedUser = User.Claims.First().Value.ToString();
                model.LastUpdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                _billingsoftware.SHPointsReedemDetails.Add(model);
            }

            await _billingsoftware.SaveChangesAsync();

            ViewBag.Message = "Saved Successfully";

            return View("PointsReedemDetails", model);
        }


        [HttpPost]

        public async Task<IActionResult> Addrack(RackMasterModel model)
        {
            var existingrack = await _billingsoftware.SHRackMaster.FindAsync(model.PartitionID, model.RackID);
            if (existingrack != null)
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

        [HttpPost]

        public async Task<IActionResult> AddGodownstock(GodownModel model)
        {
            var existinggodownstock = await _billingsoftware.SHGodown.FindAsync(model.ProductID);
            if (existinggodownstock != null)
            {
                existinggodownstock.ProductID = model.ProductID;
                existinggodownstock.NumberofStocks = model.NumberofStocks;
                existinggodownstock.NumberofStocksinRack = model.NumberofStocksinRack;
                existinggodownstock.Description = model.Description;
                existinggodownstock.LastUpdatedDate = DateTime.Now.ToString();
                existinggodownstock.LastUpdatedUser = User.Claims.First().Value.ToString();
                existinggodownstock.LastUpdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                _billingsoftware.Entry(existinggodownstock).State = EntityState.Modified;
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



        public IActionResult CategoryMasterModel()
        {
            return View();
        }

        public IActionResult ProductMasterModel()
        {
            BusinessClassBilling business = new BusinessClassBilling(_billingsoftware);
            ViewData["categoryid"] = business.GetCatid();
            ProductMatserModel obj = new ProductMatserModel();
            return View("ProductMasterModel", obj);

        }

        public IActionResult CustomerMaster()
        {
            return View();
        }

        public IActionResult DiscountCategoryMaster()
        {
            BusinessClassBilling business = new BusinessClassBilling(_billingsoftware);
            ViewData["discountcategoryid"] = business.GetcategoryID();
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
            BusinessClassBilling business = new BusinessClassBilling(_billingsoftware);
            ViewData["pointreedemcustomerid"] = business.GetCustomerid();
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
                existingbill.Quantity = model.Quantity;
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

                _billingsoftware.SHCustomerBilling.Add(model);
            }

            await _billingsoftware.SaveChangesAsync();


            ViewBag.Message = "Saved Successfully";

            return View("CustomerBilling", model);
        }


        public IActionResult CustomerBilling()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> PointsOperation(BilingSysytemModel model)
        {
            BusinessClassBilling business = new BusinessClassBilling(_billingsoftware);

            var customer = await business.GetCustomerById(model.CustomerNumber);

            if (customer == null)
            {
                ViewBag.Error = "Customer not found";
            }
            else
            {
                ViewBag.Customers = customer.Where(c => c.MobileNumber == model.CustomerNumber).ToList();

            }


            return View("CustomerBilling", customer);
        }

        /*
                [HttpPost]
                public async Task<IActionResult> AddPoints(string customerId, int pointsToAdd)
                {
                    var customer = await _billingsoftware.GetCustomerById(customerId);

                    if (customer == null)
                    {
                        ViewBag.Error = "Customer not found.";
                        return RedirectToAction("CustomerBilling");
                    }

                    try
                    {
                        await _billingsoftware.AddPointsToCustomer(customerId, pointsToAdd);
                        ViewBag.Message = "Points added successfully.";
                    }
                    catch (ArgumentException ex)
                    {
                        ViewBag.Error = "Failed to add points: " + ex.Message;
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Error = "Error: " + ex.Message;
                    }

                    return RedirectToAction("CustomerBilling");
                }*/


    }
}
