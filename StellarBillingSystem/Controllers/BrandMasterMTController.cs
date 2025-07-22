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
    public class BrandMasterMTController : Controller
    {
        private BillingContext _billingsoftware;
        private readonly IConfiguration _configuration;


        public BrandMasterMTController(BillingContext billingsoftware, IConfiguration configuration)
        {
            _billingsoftware = billingsoftware;
            _configuration = configuration;
        }

        public async Task<IActionResult> BrandMasterMT()
        {
            BrandMasterModelMT model = new BrandMasterModelMT();

            ViewData["Branddata"] = await AdditionalBrandMasterFun();


            return View("BrandMasterMT", model);

        }



        public async Task<DataTable> AdditionalBrandMasterFun()
        {

            // Step 1: Perform the query
            var entities = _billingsoftware.MTBrandMaster
                                  .Where(e => e.IsDelete == false).OrderByDescending(e => e.Lastupdateddate)
                                  .ToList();

            // Step 2: Convert to DataTable
            return BusinessCategoryMT.convertToDataTableBrandMaster(entities);


        }




        [HttpGet]
        public async Task<IActionResult> GetBrand(BrandMasterModelMT model, string buttonType)
        {



            BusinessCategoryMT bus = new BusinessCategoryMT(_billingsoftware, _configuration);



            if (buttonType == "Get")
            {
                var getcategory = await _billingsoftware.MTBrandMaster.FirstOrDefaultAsync(x => x.BrandID == model.BrandID && !x.IsDelete);
                if (getcategory != null)
                {
                    var dataTable = await AdditionalBrandMasterFun();

                    // Store the DataTable in ViewData for access in the view
                    ViewData["Branddata"] = dataTable;
                    return View("BrandMasterMT", getcategory);
                }
                else
                {
                    BrandMasterModelMT par = new BrandMasterModelMT();
                    ViewBag.ErrorMessage = "No value for this Brand ID";
                    var dataTable = await AdditionalBrandMasterFun();

                    // Store the DataTable in ViewData for access in the view
                    ViewData["Branddata"] = dataTable;
                    return View("BrandMasterMT", par);
                }
            }

            return View();
        }



        [HttpPost]
        public async Task<IActionResult> AddBrand(BrandMasterModelMT model, string buttonType)
        {

            BusinessCategoryMT business = new BusinessCategoryMT(_billingsoftware, _configuration);
            SatffAdminBusinessClass staffbus = new SatffAdminBusinessClass(_billingsoftware, _configuration);








            if (buttonType == "Get")
            {
                var getcategory = await _billingsoftware.MTBrandMaster.FirstOrDefaultAsync(x => x.BrandID == model.BrandID && !x.IsDelete);
                if (getcategory != null)
                {
                    var dataTable1 = await AdditionalBrandMasterFun();

                    // Store the DataTable in ViewData for access in the view
                    ViewData["Branddata"] = dataTable1;
                    return View("BrandMasterMT", getcategory);
                }
                else
                {
                    BrandMasterModelMT par = new BrandMasterModelMT();
                    ViewBag.ErrorMessage = "No value for this Brand ID";
                    var dataTable2 = await AdditionalBrandMasterFun();

                    // Store the DataTable in ViewData for access in the view
                    ViewData["Branddata"] = dataTable2;
                    return View("BrandMasterMT", par);
                }
            }
            else if (buttonType == "Delete")
            {
                var categorytodelete = await _billingsoftware.MTBrandMaster.FirstOrDefaultAsync(x => x.BrandID == model.BrandID && !x.IsDelete);

                if (categorytodelete != null)
                {
                    categorytodelete.IsDelete = true;
                    await _billingsoftware.SaveChangesAsync();

                    ViewBag.Message = "Brand deleted successfully";

                    var dataTable3 = await AdditionalBrandMasterFun();

                    // Store the DataTable in ViewData for access in the view
                    ViewData["Branddata"] = dataTable3;

                    model = new BrandMasterModelMT();

                    return View("BrandMasterMT", model);
                }
                else
                {
                    ViewBag.ErrorMessage = "Brand not found";
                    var dataTable4 = await AdditionalBrandMasterFun();

                    // Store the DataTable in ViewData for access in the view
                    ViewData["Branddata"] = dataTable4;
                    model = new BrandMasterModelMT();
                    return View("BrandMasterMT", model);
                }


            }


            else if (buttonType == "save")
            {
                // HttpContext.Session.SetString("BranchID", model.BranchID);
                try
                {



                    var existingCategory = await _billingsoftware.MTBrandMaster.FirstOrDefaultAsync(x => x.BrandID == model.BrandID);

                    if (existingCategory != null)
                    {
                        if (existingCategory.IsDelete)
                        {
                            ViewBag.ErrorMessage = "Cannot Save or Update. Brand is marked as deleted.";
                            var dataTable6 = await AdditionalBrandMasterFun();

                            // Store the DataTable in ViewData for access in the view
                            ViewData["Branddata"] = dataTable6;
                            return View("BrandMasterMT", model);
                        }
                        // existingCategory.BrandID = model.BrandID;
                        existingCategory.BrandName = model.BrandName;
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


                        _billingsoftware.MTBrandMaster.Add(model);
                    }


                    await _billingsoftware.SaveChangesAsync();

                    ViewBag.Message = "Saved Successfully";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                }


                var dataTable = await AdditionalBrandMasterFun();

                // Store the DataTable in ViewData for access in the view
                ViewData["Branddata"] = dataTable;
                model = new BrandMasterModelMT();

            }
            return View("BrandMasterMT", model);
        }
    }

}

