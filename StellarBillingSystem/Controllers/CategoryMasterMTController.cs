using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StellarBillingSystem.Context;
using StellarBillingSystem_skj.Business;
using StellarBillingSystem_Malar.Models;
using System.Data;
using StellarBillingSystem_Malar.Business;

namespace StellarBillingSystem_Malar.Controllers
{
    [Authorize]
    public class CategoryMasterMTController : Controller
    {
      
            private BillingContext _billingsoftware;
            private readonly IConfiguration _configuration;


            public CategoryMasterMTController(BillingContext billingsoftware, IConfiguration configuration)
            {
                _billingsoftware = billingsoftware;
                _configuration = configuration;
            }

            public async Task<IActionResult> CategoryMasterMT()
            {
            BusinessCategoryMT bus = new BusinessCategoryMT(_billingsoftware, _configuration);

                CategoryModelMT model = new CategoryModelMT();
                if (TempData["BranchID"] != null)
                {
                    model.BranchID = TempData["BranchID"].ToString();
                    TempData.Keep("BranchID");
                }
                  ViewData["Categorydata"] = await AdditionalCategoryMasterFun(model.BranchID);
                 ViewData["size"] =  bus.Getsize(model.BranchID);

            return View("CategoryMasterMT", model);

            }
            public async Task<DataTable> AdditionalCategoryMasterFun(string branchID)
            {

                // Step 1: Perform the query
                var entities = _billingsoftware.MTCategoryMaster
                                      .Where(e => e.BranchID == branchID && e.IsDelete == false).OrderByDescending(e => e.Lastupdateddate)
                                      .ToList();

                // Step 2: Convert to DataTable
                return BusinessCategoryMT.convertToDataTableCategoryMaster(entities);


            }




            [HttpGet]
            public async Task<IActionResult> GetCategory(CategoryModelMT model, string buttonType)
            {


                if (TempData["BranchID"] != null)
                {
                    model.BranchID = TempData["BranchID"].ToString();
                    TempData.Keep("BranchID");
                }

            BusinessCategoryMT bus = new BusinessCategoryMT(_billingsoftware, _configuration);

            ViewData["size"] = bus.Getsize(model.BranchID);

            if (buttonType == "Get")
                {
                    var getcategory = await _billingsoftware.MTCategoryMaster.FirstOrDefaultAsync(x => x.CategoryName == model.CategoryName && !x.IsDelete && x.BranchID == model.BranchID);
                    if (getcategory != null)
                    {
                        var dataTable = await AdditionalCategoryMasterFun(model.BranchID);

                        // Store the DataTable in ViewData for access in the view
                        ViewData["Categorydata"] = dataTable;
                        return View("CategoryMasterMT", getcategory);
                    }
                    else
                    {
                    CategoryModelMT par = new CategoryModelMT();
                        ViewBag.ErrorMessage = "No value for this Category ID";
                        var dataTable = await AdditionalCategoryMasterFun(model.BranchID);

                        // Store the DataTable in ViewData for access in the view
                        ViewData["Categorydata"] = dataTable;
                        return View("CategoryMasterMT", par);
                    }
                }

                return View();
            }



            [HttpPost]
            public async Task<IActionResult> AddCategory(CategoryModelMT model, string buttonType)
            {

                BusinessCategoryMT business = new BusinessCategoryMT(_billingsoftware, _configuration);
                SatffAdminBusinessClass staffbus = new SatffAdminBusinessClass(_billingsoftware, _configuration);




                if (TempData["BranchID"] != null)
                {
                    model.BranchID = TempData["BranchID"].ToString();
                    TempData.Keep("BranchID");
                }

            ViewData["size"] = business.Getsize(model.BranchID);


            if (buttonType == "Get")
                {
                    var getcategory = await _billingsoftware.MTCategoryMaster.FirstOrDefaultAsync(x => x.CategoryName == model.CategoryName && !x.IsDelete && x.BranchID == model.BranchID);
                    if (getcategory != null)
                    {
                        var dataTable1 = await AdditionalCategoryMasterFun(model.BranchID);

                        // Store the DataTable in ViewData for access in the view
                        ViewData["Categorydata"] = dataTable1;
                        return View("CategoryMasterMT", getcategory);
                    }
                    else
                    {
                    CategoryModelMT par = new CategoryModelMT();
                        ViewBag.ErrorMessage = "No value for this Category ID";
                        var dataTable2 = await AdditionalCategoryMasterFun(model.BranchID);

                        // Store the DataTable in ViewData for access in the view
                        ViewData["Categorydata"] = dataTable2;
                        return View("CategoryMaster", par);
                    }
                }
                else if (buttonType == "Delete")
                {
                    var categorytodelete = await _billingsoftware.MTCategoryMaster.FirstOrDefaultAsync(x => x.CategoryName == model.CategoryName && !x.IsDelete && x.BranchID == model.BranchID);

                    if (categorytodelete != null)
                    {
                        categorytodelete.IsDelete = true;
                        await _billingsoftware.SaveChangesAsync();

                        ViewBag.Message = "Category deleted successfully";

                        var dataTable3 = await AdditionalCategoryMasterFun(model.BranchID);

                        // Store the DataTable in ViewData for access in the view
                        ViewData["Categorydata"] = dataTable3;

                        model = new CategoryModelMT();

                        return View("CategoryMasterMT", model);
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Category not found";
                        var dataTable4 = await AdditionalCategoryMasterFun(model.BranchID);

                        // Store the DataTable in ViewData for access in the view
                        ViewData["Categorydata"] = dataTable4;
                        model = new CategoryModelMT();
                        return View("CategoryMasterMT", model);
                    }


                }


                else if (buttonType == "save")
                {
                   // HttpContext.Session.SetString("BranchID", model.BranchID);

                    var existingCategory = await _billingsoftware.MTCategoryMaster.FirstOrDefaultAsync(x => x.CategoryName == model.CategoryName && x.BranchID == model.BranchID);

                    if (existingCategory != null)
                    {
                        if (existingCategory.IsDelete)
                        {
                            ViewBag.ErrorMessage = "Cannot Save or Update. Category is marked as deleted.";
                            var dataTable6 = await AdditionalCategoryMasterFun(model.BranchID);

                            // Store the DataTable in ViewData for access in the view
                            ViewData["Categorydata"] = dataTable6;
                            return View("CategoryMasterMT", model);
                        }
                        //existingCategory.CategoryID = model.CategoryID;
                        existingCategory.CategoryName = model.CategoryName;
                        existingCategory.SizeID = model.SizeID;
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


                        _billingsoftware.MTCategoryMaster.Add(model);
                    }


                    await _billingsoftware.SaveChangesAsync();

                    ViewBag.Message = "Saved Successfully";
                }
                var dataTable = await AdditionalCategoryMasterFun(model.BranchID);

                // Store the DataTable in ViewData for access in the view
                ViewData["Categorydata"] = dataTable;
                model = new CategoryModelMT();

                return View("CategoryMasterMT", model);
            }







      
    }
}
