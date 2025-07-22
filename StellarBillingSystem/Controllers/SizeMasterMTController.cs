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
    public class SizeMasterMTController : Controller
    {
        private BillingContext _billingsoftware;
        private readonly IConfiguration _configuration;


        public SizeMasterMTController(BillingContext billingsoftware, IConfiguration configuration)
        {
            _billingsoftware = billingsoftware;
            _configuration = configuration;
        }



        public async Task<IActionResult> SizeMasterMT()
        {
            BusinessSizeMT business = new BusinessSizeMT(_billingsoftware, _configuration);
            SizeMasterModelMT model = new SizeMasterModelMT();

            ViewData["Sizedata"] = await AdditionalSizeMasterFun();
            ViewData["catname"] = business.Getcat();

            return View("SizeMasterMT", model);
        }

        
        public async Task<DataTable> AdditionalSizeMasterFun()
        {

            // Step 1: Perform the query
            var entities = _billingsoftware.MTSizeMaster
                                  .Where(e => e.IsDelete == false).OrderByDescending(e => e.Lastupdateddate)
                                  .ToList();

            // Step 2: Convert to DataTable
            return BusinessSizeMT.convertToDataTableSizeMaster(entities);


        }




        [HttpGet]
        public async Task<IActionResult> GetSize(SizeMasterModelMT model, string buttonType)
        {
            BusinessSizeMT bus = new BusinessSizeMT(_billingsoftware, _configuration);
            ViewData["catname"] = bus.Getcat();



            if (buttonType == "Get")
            {
                var getcategory = await _billingsoftware.MTSizeMaster.FirstOrDefaultAsync(x => x.SizeID == model.SizeID && !x.IsDelete);
                if (getcategory != null)
                {
                    var dataTable = await AdditionalSizeMasterFun();

                    // Store the DataTable in ViewData for access in the view
                    ViewData["Sizedata"] = dataTable;
                    return View("SizeMasterMT", getcategory);
                }
                else
                {
                    SizeMasterModelMT par = new SizeMasterModelMT();
                    ViewBag.ErrorMessage = "No value for this Size ID";
                    var dataTable = await AdditionalSizeMasterFun();

                    // Store the DataTable in ViewData for access in the view
                    ViewData["Sizedata"] = dataTable;
                    return View("SizeMasterMT", par);
                }
            }

            return View();
        }



        [HttpPost]
        public async Task<IActionResult> AddSize(SizeMasterModelMT model, string buttonType)
        {

            BusinessSizeMT bus = new BusinessSizeMT(_billingsoftware, _configuration);
            SatffAdminBusinessClass staffbus = new SatffAdminBusinessClass(_billingsoftware, _configuration);

            ViewData["catname"] = bus.Getcat();


            if (buttonType == "Get")
            {
                var getcategory = await _billingsoftware.MTSizeMaster.FirstOrDefaultAsync(x => x.SizeID == model.SizeID && !x.IsDelete);
                if (getcategory != null)
                {
                    var dataTable1 = await AdditionalSizeMasterFun();

                    // Store the DataTable in ViewData for access in the view
                    ViewData["Sizedata"] = dataTable1;
                    return View("SizeMasterMT", getcategory);
                }
                else
                {
                    SizeMasterModelMT par = new SizeMasterModelMT();
                    ViewBag.ErrorMessage = "No value for this Size ID";
                    var dataTable2 = await AdditionalSizeMasterFun();

                    // Store the DataTable in ViewData for access in the view
                    ViewData["Sizedata"] = dataTable2;
                    return View("SizeMasterMT", par);
                }
            }
            else if (buttonType == "Delete")
            {
                var categorytodelete = await _billingsoftware.MTSizeMaster.FirstOrDefaultAsync(x => x.SizeID == model.SizeID && !x.IsDelete);

                if (categorytodelete != null)
                {
                    categorytodelete.IsDelete = true;
                    await _billingsoftware.SaveChangesAsync();

                    ViewBag.Message = "Size deleted successfully";

                    var dataTable3 = await AdditionalSizeMasterFun();

                    // Store the DataTable in ViewData for access in the view
                    ViewData["Sizedata"] = dataTable3;

                    model = new SizeMasterModelMT();

                    return View("SizeMasterMT", model);
                }
                else
                {
                    ViewBag.ErrorMessage = "Category not found";
                    var dataTable4 = await AdditionalSizeMasterFun();

                    // Store the DataTable in ViewData for access in the view
                    ViewData["Sizedata"] = dataTable4;
                    model = new SizeMasterModelMT();
                    return View("SizeMasterMT", model);
                }


            }


            else if (buttonType == "save")
            {
                // HttpContext.Session.SetString("BranchID", model.BranchID);
                try
                {



                    var existingCategory = await _billingsoftware.MTSizeMaster.FirstOrDefaultAsync(x => x.SizeID == model.SizeID);

                    if (existingCategory != null)
                    {
                        if (existingCategory.IsDelete)
                        {
                            ViewBag.ErrorMessage = "Cannot Save or Update. Size is marked as deleted.";
                            var dataTable6 = await AdditionalSizeMasterFun();

                            // Store the DataTable in ViewData for access in the view
                            ViewData["Sizedata"] = dataTable6;
                            return View("SizeMasterMT", model);
                        }
                        // existingCategory.CategoryID = model.CategoryID;
                        existingCategory.SizeName = model.SizeName;
                        existingCategory.CategoryID = model.CategoryID;
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


                        _billingsoftware.MTSizeMaster.Add(model);
                    }


                    await _billingsoftware.SaveChangesAsync();

                    ViewBag.Message = "Saved Successfully";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                }


                var dataTable = await AdditionalSizeMasterFun();

                // Store the DataTable in ViewData for access in the view
                ViewData["Sizedata"] = dataTable;
                model = new SizeMasterModelMT();

            }
            return View("SizeMasterMT", model);
        }
    }
}

