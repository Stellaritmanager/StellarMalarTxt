using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StellarBillingSystem.Context;
using StellarBillingSystem_Malar.Business;
using StellarBillingSystem_Malar.Models;
using StellarBillingSystem_skj.Business;
using System.Data;

namespace StellarBillingSystem_Malar.Controllers
{
    [Authorize]
    public class ProductMasterMTController : Controller
    {


        private BillingContext _billingsoftware;
        private readonly IConfiguration _configuration;


        public ProductMasterMTController(BillingContext billingsoftware, IConfiguration configuration)
        {
            _billingsoftware = billingsoftware;
            _configuration = configuration;
        }

        public async Task<IActionResult> ProductMasterMT()
        {
            ProductModelMT model = new ProductModelMT();

            if (TempData["BranchID"] != null)
            {
                model.BranchID = TempData["BranchID"].ToString();
                TempData.Keep("BranchID");
            }

            BusinessProductMT business = new BusinessProductMT(_billingsoftware, _configuration);


            ViewData["Productdata"] = await AdditionalProductMasterFun(model.BranchID);
            ViewData["catname"] = business.Getcat();
            ViewData["sizename"] = business.Getsize();
            ViewData["brandname"] = business.Getbrand();

            return View("ProductMasterMT", model);
        }


        public async Task<DataTable> AdditionalProductMasterFun(string BranchID)
        {

            // Step 1: Perform the query
            var entities = _billingsoftware.MTProductMaster
                                  .Where(e => e.IsDelete == false && e.BranchID == BranchID ).OrderByDescending(e => e.Lastupdateddate)
                                  .ToList();

            // Step 2: Convert to DataTable
            return BusinessProductMT.convertToDataTableProductMaster(entities);


        }







        [HttpGet]
        public async Task<IActionResult> GetProduct(ProductModelMT model, string buttonType)
        {

            if (TempData["BranchID"] != null)
            {
                model.BranchID = TempData["BranchID"].ToString();
                TempData.Keep("BranchID");
            }


            BusinessProductMT business = new BusinessProductMT(_billingsoftware, _configuration);
            ViewData["catname"] = business.Getcat();
            ViewData["sizename"] = business.Getsize();
            ViewData["brandname"] = business.Getbrand();




            if (buttonType == "Get")
            {
                var getcategory = await _billingsoftware.MTProductMaster.FirstOrDefaultAsync(x => (x.ProductCode == model.ProductCode || x.Barcode == model.Barcode) &&
          x.BranchID == model.BranchID &&
          x.IsDelete == false);

                if (getcategory != null)
                {
                    var dataTable = await AdditionalProductMasterFun(model.BranchID);;

                    // Store the DataTable in ViewData for access in the view
                    ViewData["Productdata"] = dataTable;
                    return View("ProductMasterMT", getcategory);
                }
                else
                {
                    ProductModelMT par = new ProductModelMT();
                    ViewBag.ErrorMessage = "No value for this Product ID";
                    var dataTable = await AdditionalProductMasterFun(model.BranchID);;

                    // Store the DataTable in ViewData for access in the view
                    ViewData["Productdata"] = dataTable;
                    return View("ProductMasterMT", par);
                }
            }

            return View();
        }



        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductModelMT model, string buttonType)
        {

            if (TempData["BranchID"] != null)
            {
                model.BranchID = TempData["BranchID"].ToString();
                TempData.Keep("BranchID");
            }


            BusinessProductMT business = new BusinessProductMT(_billingsoftware, _configuration);
            ViewData["catname"] = business.Getcat();
            ViewData["sizename"] = business.Getsize();
            ViewData["brandname"] = business.Getbrand();

            SatffAdminBusinessClass staffbus = new SatffAdminBusinessClass(_billingsoftware, _configuration);

          
            if (buttonType == "Delete")
            {
                var categorytodelete = await _billingsoftware.MTProductMaster.FirstOrDefaultAsync(x => (x.ProductCode == model.ProductCode || x.Barcode == model.Barcode) &&
          x.BranchID == model.BranchID &&
          x.IsDelete == false);

                if (categorytodelete != null)
                {
                    categorytodelete.IsDelete = true;
                    await _billingsoftware.SaveChangesAsync();

                    ViewBag.Message = "Product deleted successfully";

                    var dataTable3 = await AdditionalProductMasterFun(model.BranchID);;

                    // Store the DataTable in ViewData for access in the view
                    ViewData["Productdata"] = dataTable3;

                    model = new ProductModelMT();

                    return View("ProductMasterMT", model);
                }
                else
                {
                    ViewBag.ErrorMessage = "Product not found";
                    var dataTable4 = await AdditionalProductMasterFun(model.BranchID);;

                    // Store the DataTable in ViewData for access in the view
                    ViewData["Productdata"] = dataTable4;
                    model = new ProductModelMT();
                    return View("ProductMasterMT", model);
                }


            }


            else if (buttonType == "save")
            {
                // HttpContext.Session.SetString("BranchID", model.BranchID);
                try
                {



                    var existingCategory = await _billingsoftware.MTProductMaster.FirstOrDefaultAsync(x => (x.ProductCode == model.ProductCode || x.Barcode == model.Barcode) &&
          x.BranchID == model.BranchID &&
          x.IsDelete == false);

                    if (existingCategory != null)
                    {
                        if (existingCategory.IsDelete)
                        {
                            ViewBag.ErrorMessage = "Cannot Save or Update. Size is marked as deleted.";
                            var dataTable6 = await AdditionalProductMasterFun(model.BranchID);;

                            // Store the DataTable in ViewData for access in the view
                            ViewData["Productdata"] = dataTable6;
                            return View("ProductMasterMT", model);
                        }

                        existingCategory.ProductCode = model.ProductCode;
                        existingCategory.CategoryID = model.CategoryID;
                        existingCategory.Barcode = model.Barcode;
                        existingCategory.SizeName = model.SizeName;
                        existingCategory.ProductName = model.ProductName;
                        existingCategory.Price = model.Price;
                        existingCategory.NoofItem = model.NoofItem;
                        existingCategory.BranchID = model.BranchID;
                        existingCategory.BrandID = model.BrandID;
                        existingCategory.Lastupdateddate = DateTime.ParseExact(staffbus.GetFormattedDateTime(), "dd/MM/yyyy HH:mm:ss", null).ToString();
                        existingCategory.Lastupdateduser = User.Claims.First().Value.ToString();
                        existingCategory.Lastupdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();


                        _billingsoftware.Entry(existingCategory).State = EntityState.Modified;
                    }
                    else
                    {
                        model.Lastupdateddate = DateTime.ParseExact(staffbus.GetFormattedDateTime(), "dd/MM/yyyy HH:mm:ss", null).ToString();
                        model.Lastupdateduser = User.Claims.First().Value.ToString();
                        model.Lastupdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();


                        _billingsoftware.MTProductMaster.Add(model);
                    }


                    await _billingsoftware.SaveChangesAsync();

                    ViewBag.Message = "Saved Successfully";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                }


                var dataTable = await AdditionalProductMasterFun(model.BranchID);;

                // Store the DataTable in ViewData for access in the view
                ViewData["Productdata"] = dataTable;
                model = new ProductModelMT();

            }
            return View("ProductMasterMT", model);
        }
    }
}

