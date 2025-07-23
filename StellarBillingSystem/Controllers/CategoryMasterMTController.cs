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


            CategoryModelMT model = new CategoryModelMT();

            ViewData["Categorydata"] = await AdditionalCategoryMasterFun();


            return View("CategoryMasterMT", model);

        }
        public async Task<DataTable> AdditionalCategoryMasterFun()
        {

            // Step 1: Perform the query
            var entities = _billingsoftware.MTCategoryMaster
                                  .Where(e => e.IsDelete == false).OrderByDescending(e => e.Lastupdateddate)
                                  .ToList();

            // Step 2: Convert to DataTable
            return BusinessCategoryMT.convertToDataTableCategoryMaster(entities);


        }




        [HttpGet]
        public async Task<IActionResult> GetCategory(CategoryModelMT model, string buttonType)
        {


            /*if (TempData["BranchID"] != null)
            {
                model.BranchID = TempData["BranchID"].ToString();
                TempData.Keep("BranchID");
            }*/

            BusinessCategoryMT bus = new BusinessCategoryMT(_billingsoftware, _configuration);



            if (buttonType == "Get")
            {
                var getcategory = await _billingsoftware.MTCategoryMaster.FirstOrDefaultAsync(x => x.CategoryID == model.CategoryID && !x.IsDelete);
                if (getcategory != null)
                {
                    var dataTable = await AdditionalCategoryMasterFun();

                    // Store the DataTable in ViewData for access in the view
                    ViewData["Categorydata"] = dataTable;
                    return View("CategoryMasterMT", getcategory);
                }
                else
                {
                    CategoryModelMT par = new CategoryModelMT();
                    ViewBag.ErrorMessage = "No value for this Category ID";
                    var dataTable = await AdditionalCategoryMasterFun();

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








            if (buttonType == "Get")
            {
                var getcategory = await _billingsoftware.MTCategoryMaster.FirstOrDefaultAsync(x => x.CategoryID == model.CategoryID && !x.IsDelete);
                if (getcategory != null)
                {
                    var dataTable1 = await AdditionalCategoryMasterFun();

                    // Store the DataTable in ViewData for access in the view
                    ViewData["Categorydata"] = dataTable1;
                    return View("CategoryMasterMT", getcategory);
                }
                else
                {
                    CategoryModelMT par = new CategoryModelMT();
                    ViewBag.ErrorMessage = "No value for this Category ID";
                    var dataTable2 = await AdditionalCategoryMasterFun();

                    // Store the DataTable in ViewData for access in the view
                    ViewData["Categorydata"] = dataTable2;
                    return View("CategoryMaster", par);
                }
            }
            else if (buttonType == "Delete")
            {
                var categorytodelete = await _billingsoftware.MTCategoryMaster.FirstOrDefaultAsync(x => x.CategoryID == model.CategoryID && !x.IsDelete);

                if (categorytodelete != null)
                {
                    categorytodelete.IsDelete = true;
                    await _billingsoftware.SaveChangesAsync();

                    ViewBag.Message = "Category deleted successfully";

                    var dataTable3 = await AdditionalCategoryMasterFun();

                    // Store the DataTable in ViewData for access in the view
                    ViewData["Categorydata"] = dataTable3;

                    model = new CategoryModelMT();

                    return View("CategoryMasterMT", model);
                }
                else
                {
                    ViewBag.ErrorMessage = "Category not found";
                    var dataTable4 = await AdditionalCategoryMasterFun();

                    // Store the DataTable in ViewData for access in the view
                    ViewData["Categorydata"] = dataTable4;
                    model = new CategoryModelMT();
                    return View("CategoryMasterMT", model);
                }


            }


            else if (buttonType == "save")
            {
                // HttpContext.Session.SetString("BranchID", model.BranchID);
                try
                {



                    var existingCategory = await _billingsoftware.MTCategoryMaster.FirstOrDefaultAsync(x => x.CategoryID == model.CategoryID);

                    if (existingCategory != null)
                    {
                        if (existingCategory.IsDelete)
                        {
                            ViewBag.ErrorMessage = "Cannot Save or Update. Category is marked as deleted.";
                            var dataTable6 = await AdditionalCategoryMasterFun();

                            // Store the DataTable in ViewData for access in the view
                            ViewData["Categorydata"] = dataTable6;
                            return View("CategoryMasterMT", model);
                        }
                        // existingCategory.CategoryID = model.CategoryID;
                        existingCategory.CategoryName = model.CategoryName;
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
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                }


                var dataTable = await AdditionalCategoryMasterFun();

                // Store the DataTable in ViewData for access in the view
                ViewData["Categorydata"] = dataTable;
                model = new CategoryModelMT();

            }
            return View("CategoryMasterMT", model);
        }
    }
}









