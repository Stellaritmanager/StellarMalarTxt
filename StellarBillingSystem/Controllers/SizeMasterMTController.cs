using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            var business = new BusinessSizeMT(_billingsoftware, _configuration);
            var model = new SizeMasterModelMT();

            var viewModel = new SizeMasterViewModel
            {
                Model = model,
                CategoryList = GetCategoryList(null),
                SizeData = await AdditionalSizeMasterFun()
            };

            return View("SizeMasterMT", viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetSize(SizeMasterViewModel viewModel, string buttonType)
        {
            if (buttonType == "Get")
            {
                var input = viewModel.Model;

                var entity = await _billingsoftware.MTSizeMaster
                    .FirstOrDefaultAsync(x => x.SizeID == input.SizeID && !x.IsDelete);

                if (entity != null)
                {
                    var model = new SizeMasterModelMT
                    {
                        SizeID = entity.SizeID,
                        SizeName = entity.SizeName,
                        CategoryID = entity.CategoryID
                    };

                    var resultVM = new SizeMasterViewModel
                    {
                        Model = model,
                        CategoryList = GetCategoryList(model.CategoryID),
                        SizeData = await AdditionalSizeMasterFun()
                    };

                    // ✅ Clear model state so Razor uses new data, not old form values
                    ModelState.Clear();

                    return View("SizeMasterMT", resultVM);
                }

                // not found fallback
                ViewBag.ErrorMessage = $"No value found for Size ID {input.SizeID}";
            }

            // fallback to empty model
            var emptyVM = new SizeMasterViewModel
            {
                Model = new SizeMasterModelMT(),
                CategoryList = GetCategoryList(null),
                SizeData = await AdditionalSizeMasterFun()
            };

            return View("SizeMasterMT", emptyVM);
        }
        // Helper method to get category list
        private IEnumerable<SelectListItem> GetCategoryList(int? selectedId)
        {
            return _billingsoftware.MTCategoryMaster.Select(c => new SelectListItem
            {
                Value = c.CategoryID.ToString(),
                Text = c.CategoryName,
                Selected = selectedId.HasValue && selectedId.Value == c.CategoryID
            }).ToList();
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




       
        [HttpPost]
        public async Task<IActionResult> AddSize(SizeMasterViewModel viewModel, string buttonType)
        {
            var model = viewModel.Model;
            var message = string.Empty;

            BusinessSizeMT bus = new BusinessSizeMT(_billingsoftware, _configuration);
            SatffAdminBusinessClass staffbus = new SatffAdminBusinessClass(_billingsoftware, _configuration);

            if (buttonType == "Get")
            {
                // ✅ EF query to get size from DB
                var entity = await _billingsoftware.MTSizeMaster
                    .FirstOrDefaultAsync(x => x.SizeID == model.SizeID && !x.IsDelete);

                if (entity != null)
                {
                    // Populate model from DB
                    model = new SizeMasterModelMT
                    {
                        SizeID = entity.SizeID,
                        SizeName = entity.SizeName,
                        CategoryID = entity.CategoryID
                    };

                   
                }
                else
                {
                    ViewBag.ErrorMessage = "No record found for SizeID ";
                   
                }

                var result = new SizeMasterViewModel
                {
                    Model = model,
                    CategoryList = GetCategoryList(model.CategoryID),
                    SizeData = await AdditionalSizeMasterFun()
                };

                ModelState.Clear(); 
                return View("SizeMasterMT", result);
            }
            else if (buttonType == "save")
            {
                try
                {
                    var existing = await _billingsoftware.MTSizeMaster
                        .FirstOrDefaultAsync(x => x.SizeID == model.SizeID);

                    if (existing != null)
                    {
                        if (existing.IsDelete)
                        {
                            ViewBag.ErrorMessage = "Cannot save. Record is marked deleted.";
                            viewModel.SizeData = await AdditionalSizeMasterFun();
                            return View("SizeMasterMT", viewModel);
                        }
                        else
                        {
                            existing.SizeName = model.SizeName;
                            existing.CategoryID = model.CategoryID;
                            existing.Lastupdateddate = DateTime.ParseExact(staffbus.GetFormattedDateTime(), "dd/MM/yyyy HH:mm:ss", null).ToString();
                            existing.Lastupdateduser = User.Claims.First().Value;
                            existing.Lastupdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                            _billingsoftware.Entry(existing).State = EntityState.Modified;
                            await _billingsoftware.SaveChangesAsync();
                            ViewBag.Message = "Updated successfully.";
                        }
                    }
                    else
                    {
                        model.Lastupdateddate = DateTime.ParseExact(staffbus.GetFormattedDateTime(), "dd/MM/yyyy HH:mm:ss", null).ToString();
                        model.Lastupdateduser = User.Claims.First().Value;
                        model.Lastupdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                        _billingsoftware.MTSizeMaster.Add(model);
                        await _billingsoftware.SaveChangesAsync();
                        ViewBag.Message = "Saved successfully.";

                        
                    }

                    viewModel.SizeData = await AdditionalSizeMasterFun();

                    ModelState.Clear();
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                }
            }
            else if (buttonType == "Delete")
            {
                var entity = await _billingsoftware.MTSizeMaster
                    .FirstOrDefaultAsync(x => x.SizeID == model.SizeID && !x.IsDelete);

                if (entity != null)
                {
                    entity.IsDelete = true;
                    await _billingsoftware.SaveChangesAsync();
                    ViewBag.Message = "Deleted successfully.";
                }
                else
                {
                    ViewBag.ErrorMessage = "Size not found.";
                }

                viewModel = new SizeMasterViewModel
                {
                    SizeData = await AdditionalSizeMasterFun(),
                    CategoryList = GetCategoryList(null)
                    
                };

                ModelState.Clear();

            }

            // Always reload dropdown and table
            var resultVM = new SizeMasterViewModel
            {
                Model = new SizeMasterModelMT(),
                CategoryList = GetCategoryList(null),
                SizeData = await AdditionalSizeMasterFun()
            };

            
            return View("SizeMasterMT", resultVM);
        }




    }
   
}

