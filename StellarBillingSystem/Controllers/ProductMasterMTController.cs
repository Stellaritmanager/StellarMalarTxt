using DocumentFormat.OpenXml.Vml.Office;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
            var viewModel = new ProductViewModelMT
            {
                ObjMT = new ProductModelMT()
            };

            if (TempData["BranchID"] != null)
            {
                viewModel.ObjMT.BranchID = TempData["BranchID"].ToString();
                TempData.Keep("BranchID");
            }

            BusinessProductMT business = new BusinessProductMT(_billingsoftware, _configuration);

            viewModel.ProductData = await AdditionalProductMasterFun(viewModel.ObjMT.BranchID);
            viewModel.CatgeoryList = GetCategoryList(null);
            viewModel.SizeList = GetSizeList(null);
            viewModel.BranchList = GetbrandList(null);

            return View("ProductMasterMT", viewModel);
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
        public async Task<IActionResult> GetProduct(ProductViewModelMT model, string buttonType)
        {
            if (TempData["BranchID"] != null)
            {
                model.ObjMT.BranchID = TempData["BranchID"].ToString();
                TempData.Keep("BranchID");
            }

            var input = model.ObjMT;

            try
            {



                BusinessProductMT business = new BusinessProductMT(_billingsoftware, _configuration);
               

                if (buttonType == "Get")
                {
                    var product = await _billingsoftware.MTProductMaster.FirstOrDefaultAsync(x =>
                        (x.ProductCode == model.ObjMT.ProductCode || x.Barcode == model.ObjMT.Barcode) &&
                        x.BranchID == model.ObjMT.BranchID &&
                        x.IsDelete == false);

                    

                    if (product != null)
                    {
                        input = new ProductModelMT
                        {
                            ProductCode = product.ProductCode,
                            Barcode = product.Barcode,
                            ProductName = product.ProductName,
                            NoofItem = product.NoofItem,
                            Price = product.Price,
                            CategoryID = product.CategoryID,
                            SizeName = product.SizeName,
                            BrandID = product.BrandID
                        };
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "No value for this Product ID";
                       // model.ObjMT = new ProductModelMT();
                    }

                    var resultVM = new ProductViewModelMT
                    {
                        ObjMT = input,
                        CatgeoryList = GetCategoryList(input.CategoryID),
                        SizeList = GetSizeList(input.SizeName),
                        BranchList = GetbrandList(input.BrandID),
                        ProductData = await AdditionalProductMasterFun(model.ObjMT.BranchID)
                };

                    ModelState.Clear(); // important to reflect new values
                    return View("ProductMasterMT", resultVM);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
        
            
            return View();
        }

        

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductViewModelMT model, string buttonType)
        {
            if (TempData["BranchID"] != null)
            {
                model.ObjMT.BranchID = TempData["BranchID"].ToString();
                TempData.Keep("BranchID");
            }

            model.ProductData = await AdditionalProductMasterFun(model.ObjMT.BranchID);
            model.CatgeoryList = GetCategoryList(null);
            model.SizeList = GetSizeList(null);
            model.BranchList = GetbrandList(null);

            SatffAdminBusinessClass staffbus = new SatffAdminBusinessClass(_billingsoftware, _configuration);

            if (buttonType == "Delete")
            {
                var toDelete = await _billingsoftware.MTProductMaster.FirstOrDefaultAsync(x =>
                    (x.ProductCode == model.ObjMT.ProductCode || x.Barcode == model.ObjMT.Barcode) &&
                    x.BranchID == model.ObjMT.BranchID &&
                    x.IsDelete == false);

                if (toDelete != null)
                {
                    toDelete.IsDelete = true;
                    await _billingsoftware.SaveChangesAsync();
                    ViewBag.Message = "Product deleted successfully";
                }
                else
                {
                    ViewBag.ErrorMessage = "Product not found";
                }

                model = new ProductViewModelMT
                {
                    ObjMT = new ProductModelMT
                    {
                        BranchID = model.ObjMT.BranchID // so AdditionalProductMasterFun works for the same branch
                    },
                    ProductData = await AdditionalProductMasterFun(model.ObjMT.BranchID),
                    CatgeoryList = GetCategoryList(null),
                    SizeList = GetSizeList(null),
                    BranchList = GetbrandList(null)
                };

                ModelState.Clear();

                return View("ProductMasterMT", model);
            }

            if (buttonType == "save")
            {
                try
                {
                    var existing = await _billingsoftware.MTProductMaster.FirstOrDefaultAsync(x =>
                        (x.ProductCode == model.ObjMT.ProductCode || x.Barcode == model.ObjMT.Barcode) &&
                        x.BranchID == model.ObjMT.BranchID &&
                        x.IsDelete == false);

                    string currentTime = DateTime.ParseExact(staffbus.GetFormattedDateTime(), "dd/MM/yyyy HH:mm:ss", null).ToString();

                    if (existing != null)
                    {
                        if (existing.IsDelete)
                        {
                            ViewBag.ErrorMessage = "Cannot Save or Update. Size is marked as deleted.";
                            model.ProductData = await AdditionalProductMasterFun(model.ObjMT.BranchID);
                            return View("ProductMasterMT", model);
                        }

                        // Update existing
                        existing.ProductCode = model.ObjMT.ProductCode;
                        existing.CategoryID = model.ObjMT.CategoryID;
                        existing.Barcode = model.ObjMT.Barcode;
                        existing.SizeName = model.ObjMT.SizeName;
                        existing.ProductName = model.ObjMT.ProductName;
                        existing.Price = model.ObjMT.Price;
                        existing.NoofItem = model.ObjMT.NoofItem;
                        existing.BranchID = model.ObjMT.BranchID;
                        existing.BrandID = model.ObjMT.BrandID;
                        existing.Lastupdateddate = currentTime;
                        existing.Lastupdateduser = User.Claims.First().Value;
                        existing.Lastupdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                        _billingsoftware.Entry(existing).State = EntityState.Modified;
                    }
                    else
                    {
                        model.ObjMT.Lastupdateddate = currentTime;
                        model.ObjMT.Lastupdateduser = User.Claims.First().Value;
                        model.ObjMT.Lastupdatedmachine = Request.HttpContext.Connection.RemoteIpAddress.ToString();

                        
                        _billingsoftware.MTProductMaster.Add(model.ObjMT);
                    }

                    await _billingsoftware.SaveChangesAsync();
                    ViewBag.Message = "Saved Successfully";
                

                
                model.ProductData = await AdditionalProductMasterFun(model.ObjMT.BranchID);
                    
                ModelState.Clear();

                   
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                }
            }
            model = new ProductViewModelMT
            {
                ObjMT = new ProductModelMT
                {
                    BranchID = model.ObjMT.BranchID // so AdditionalProductMasterFun works for the same branch
                },
                ProductData = await AdditionalProductMasterFun(model.ObjMT.BranchID),
                CatgeoryList = GetCategoryList(null),
                SizeList = GetSizeList(null),
                BranchList = GetbrandList(null)
            };


            return View("ProductMasterMT", model);

           
        }

        private IEnumerable<SelectListItem> GetCategoryList(int? selectedId)
        {
            return _billingsoftware.MTCategoryMaster.Select(c => new SelectListItem
            {
                Value = c.CategoryID.ToString(),
                Text = c.CategoryName,
                Selected = selectedId.HasValue && selectedId.Value == c.CategoryID
            }).ToList();
        }

        private IEnumerable<SelectListItem> GetSizeList(string? selectedId)
        {
            return _billingsoftware.MTSizeMaster.Select(c => new SelectListItem
            {
                Value = c.SizeName.ToString(),
                Text = c.SizeName,
                Selected = !string.IsNullOrEmpty(selectedId) && selectedId == c.SizeName
            }).ToList();
        }

        private IEnumerable<SelectListItem> GetbrandList(int? selectedId)
        {
            return _billingsoftware.MTBrandMaster.Select(c => new SelectListItem
            {
                Value = c.BrandID.ToString(),
                Text = c.BrandName,
                Selected = selectedId.HasValue && selectedId.Value == c.BrandID
            }).ToList();
        }


    }
}

