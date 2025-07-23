using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StellarBillingSystem.Context;
using StellarBillingSystem_Malar.Business;
using StellarBillingSystem_Malar.Models;
using StellarBillingSystem_skj.Business;

namespace StellarBillingSystem_Malar.Controllers
{
    [Authorize]
    public class ProductInwardMTController : Controller
    {


        private BillingContext _billingsoftware;
        private readonly IConfiguration _configuration;


        public ProductInwardMTController(BillingContext billingsoftware, IConfiguration configuration)
        {
            _billingsoftware = billingsoftware;
            _configuration = configuration;
        }

        public async Task<IActionResult> ProductInwardMT()
        {
            var viewModel = new ProductInwardViewMTModel
            {
                ObjMT = new ProductInwardModelMT()
            };

            if (TempData["BranchID"] != null)
            {
                viewModel.ObjMT.BranchID = TempData["BranchID"].ToString();
                TempData.Keep("BranchID");
            }

            BusinessProductMT business = new BusinessProductMT(_billingsoftware, _configuration);

            viewModel.ProductList = GetProductList(null);


            return View("ProductInwardMT", viewModel);
        }


        [HttpGet]
        public async Task<IActionResult> GetProductInward(ProductInwardViewMTModel model, string buttonType)
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
                    var product = await _billingsoftware.MTProductInward.FirstOrDefaultAsync(x =>
                        x.ProductCode == model.ObjMT.ProductCode && x.InvoiceNumber == model.ObjMT.InvoiceNumber &&
                        x.BranchID == model.ObjMT.BranchID &&
                        x.IsDelete == false);



                    if (product != null)
                    {
                        input = new ProductInwardModelMT
                        {
                            ProductCode = product.ProductCode,
                            InvoiceNumber = product.InvoiceNumber,
                            SupplierName = product.SupplierName,
                            NoofItem = product.NoofItem,
                            InvoiceDate = product.InvoiceDate,
                            SupplierPrice = product.SupplierPrice,
                            Tax = product.Tax,
                            Amount = product.Amount
                        };
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "No value for this InvoiceNumber and Product Code";

                    }

                    var resultVM = new ProductInwardViewMTModel
                    {
                        ObjMT = input,
                        ProductList = GetProductList(input.ProductCode),

                    };

                    ModelState.Clear();
                    return View("ProductInwardMT", resultVM);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }


            return View();
        }



        [HttpPost]
        public async Task<IActionResult> AddProductInward(ProductInwardViewMTModel model, string buttonType)
        {
            if (TempData["BranchID"] != null)
            {
                model.ObjMT.BranchID = TempData["BranchID"].ToString();
                TempData.Keep("BranchID");
            }

            model.ProductList = GetProductList(null);


            SatffAdminBusinessClass staffbus = new SatffAdminBusinessClass(_billingsoftware, _configuration);

            if (buttonType == "Delete")
            {
                var toDelete = await _billingsoftware.MTProductInward.FirstOrDefaultAsync(x =>
                        x.ProductCode == model.ObjMT.ProductCode && x.InvoiceNumber == model.ObjMT.InvoiceNumber &&
                        x.BranchID == model.ObjMT.BranchID &&
                        x.IsDelete == false);

                if (toDelete != null)
                {
                    toDelete.IsDelete = true;
                    await _billingsoftware.SaveChangesAsync();
                    ViewBag.Message = "Invoice deleted successfully";

                    var removestock = await _billingsoftware.MTProductMaster.FirstOrDefaultAsync(x =>
                         x.ProductCode == model.ObjMT.ProductCode &&
                         x.BranchID == model.ObjMT.BranchID &&
                         x.IsDelete == false);

                    if (removestock != null)
                    {
                        removestock.NoofItem -= toDelete.NoofItem;
                        await _billingsoftware.SaveChangesAsync();
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Invoice not found";
                }

                model = new ProductInwardViewMTModel
                {
                    ObjMT = new ProductInwardModelMT
                    {
                        BranchID = model.ObjMT.BranchID // so AdditionalProductMasterFun works for the same branch
                    },
                    ProductList = GetProductList(null),
                };

                ModelState.Clear();

                return View("ProductInwardMT", model);
            }

            if (buttonType == "save")
            {
                try
                {
                    var existing = await _billingsoftware.MTProductInward.FirstOrDefaultAsync(x =>
                        x.ProductCode == model.ObjMT.ProductCode && x.InvoiceNumber == model.ObjMT.InvoiceNumber &&
                        x.BranchID == model.ObjMT.BranchID &&
                        x.IsDelete == false);
                    string currentTime = DateTime.ParseExact(staffbus.GetFormattedDateTime(), "dd/MM/yyyy HH:mm:ss", null).ToString();



                    if (existing != null)
                    {
                        if (existing.IsDelete)
                        {
                            ViewBag.ErrorMessage = "Cannot Save or Update. Size is marked as deleted.";
                            return View("ProductInwardMT", model);
                        }

                        // Update existing
                        existing.ProductCode = model.ObjMT.ProductCode;
                        existing.InvoiceNumber = model.ObjMT.InvoiceNumber;
                        existing.SupplierName = model.ObjMT.SupplierName;
                        existing.InvoiceDate = model.ObjMT.InvoiceDate;
                        existing.SupplierPrice = model.ObjMT.SupplierPrice;
                        existing.Tax = model.ObjMT.Tax;
                        existing.Amount = model.ObjMT.Amount;
                        existing.BranchID = model.ObjMT.BranchID;
                        existing.NoofItem = model.ObjMT.NoofItem;
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


                        _billingsoftware.MTProductInward.Add(model.ObjMT);
                    }

                    await _billingsoftware.SaveChangesAsync();
                    ViewBag.Message = "Saved Successfully";

                    var addstock = await _billingsoftware.MTProductMaster.FirstOrDefaultAsync(x =>
                        x.ProductCode == model.ObjMT.ProductCode &&
                        x.BranchID == model.ObjMT.BranchID &&
                        x.IsDelete == false);

                    if (addstock != null)
                    {
                        addstock.NoofItem += model.ObjMT.NoofItem;
                        await _billingsoftware.SaveChangesAsync();

                    }

                    ModelState.Clear();

                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                }
            }
            model = new ProductInwardViewMTModel
            {
                ObjMT = new ProductInwardModelMT
                {
                    BranchID = model.ObjMT.BranchID // so AdditionalProductMasterFun works for the same branch
                },

                ProductList = GetProductList(null),

            };


            return View("ProductInwardMT", model);


        }

        private IEnumerable<SelectListItem> GetProductList(string? selectedId)
        {
            return _billingsoftware.MTProductMaster.Where(i=>i.IsDelete ==false && i.BranchID== TempData["BranchID"].ToString()).Select(c => new SelectListItem
            {
                Value = c.ProductCode.ToString(),
                Text = c.ProductName,
                Selected = !string.IsNullOrEmpty(selectedId) && selectedId == c.ProductCode
            }).ToList();
        }


    }
}
