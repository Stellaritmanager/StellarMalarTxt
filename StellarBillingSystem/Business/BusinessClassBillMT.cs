using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StellarBillingSystem.Context;
using StellarBillingSystem.Models;
using StellarBillingSystem_Malar.Models;
using System.Security.Claims;

namespace StellarBillingSystem_Malar.Business
{
    public class BusinessClassBillMT : Controller
    {
       
            private readonly BillingContext _db;
            private readonly IConfiguration _config;

            public BusinessClassBillMT(BillingContext db, IConfiguration config)
            {
                _db = db;
                _config = config;
            }

            public string GetFormattedDateTime()
            {
                return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }

            public List<ProductModelMT> Getproduct(string branchId)
            {
                return _db.MTProductMaster
                    .Where(p => !p.IsDelete && p.BranchID == branchId)
                  .ToList();
            }

        public List<CustomerMasterModel> GetcustomerID(string branchId)
        {
            return _db.SHCustomerMaster
                .Where(c => !c.IsDelete && c.BranchID == branchId)
                .ToList();
        }


        public BillingDetailsModel ValidateAndPrepareProduct(BillingDetailsModel product, string branchID, out string error)
            {
                error = null;

                if (string.IsNullOrEmpty(product.Barcode))
                {
                    var byProductId = _db.MTProductMaster.FirstOrDefault(x => x.ProductCode == product.ProductID && !x.IsDelete && x.BranchID == branchID);
                    if (byProductId != null)
                        product.Barcode = byProductId.Barcode;
                }

                if (string.IsNullOrEmpty(product.ProductID))
                {
                    var byBarcode = _db.MTProductMaster.FirstOrDefault(x => x.Barcode == product.Barcode && !x.IsDelete && x.BranchID == branchID);
                    if (byBarcode != null)
                        product.ProductID = byBarcode.ProductCode;
                }

                var prod = _db.MTProductMaster.FirstOrDefault(x => x.Barcode == product.Barcode && !x.IsDelete && x.BranchID == branchID);
                if (prod == null)
                {
                    error = "Product not found";
                    return null;
                }

                if (!long.TryParse(product.Quantity, out long qty))
                {
                    error = "Invalid quantity format.";
                    return null;
                }

                if (prod.NoofItem < qty)
                {
                    error = "Out of stock";
                    return null;
                }

                product.ProductName = prod.ProductName;
                product.Price = prod.Price.ToString("0.00");
                product.NetPrice = (prod.Price * qty).ToString("0.00");

                return product;
            }

            public async Task<string> SaveBillAsync(BillProductlistModel model, ClaimsPrincipal user, string ipAddress)
            {
                string userName = user?.Identity?.Name ?? "System";

                var existingMaster = await _db.SHbillmaster
                    .FirstOrDefaultAsync(x => x.BillID == model.BillID && x.BranchID == model.BranchID);

                BillingMasterModel master;

                if (existingMaster != null)
                {
                    existingMaster.BillDate = model.BillDate;
                    existingMaster.CustomerNumber = model.CustomerNumber;
                    existingMaster.Totalprice = model.Totalprice;
                    existingMaster.TotalDiscount = model.TotalDiscount;
                    existingMaster.NetPrice = model.NetPrice;
                    existingMaster.CGSTPercentage = model.CGSTPercentage;
                    existingMaster.SGSTPercentage = model.SGSTPercentage;
                    existingMaster.CGSTPercentageAmt = model.CGSTPercentageAmt;
                    existingMaster.SGSTPercentageAmt = model.SGSTPercentageAmt;
                    existingMaster.Lastupdateduser = userName;
                    existingMaster.Lastupdatedmachine = ipAddress;
                    existingMaster.Lastupdateddate = GetFormattedDateTime();
                    master = existingMaster;
                }
                else
                {
                    master = new BillingMasterModel
                    {
                        BillID = model.BillID,
                        BillDate = model.BillDate,
                        CustomerNumber = model.CustomerNumber,
                        Totalprice = model.Totalprice,
                        TotalDiscount = model.TotalDiscount,
                        NetPrice = model.NetPrice,
                        CGSTPercentage = model.CGSTPercentage,
                        SGSTPercentage = model.SGSTPercentage,
                        CGSTPercentageAmt = model.CGSTPercentageAmt,
                        SGSTPercentageAmt = model.SGSTPercentageAmt,
                        BranchID = model.BranchID,
                        Billby = userName,
                        BillInsertion = true,
                        Lastupdateddate = GetFormattedDateTime(),
                        Lastupdatedmachine = ipAddress,
                        Lastupdateduser = userName
                    };

                    await _db.SHbillmaster.AddAsync(master);
                }

                foreach (var item in model.Viewbillproductlist)
                {
                    var detail = await _db.SHbilldetails.FirstOrDefaultAsync(x =>
                        x.BillID == model.BillID && x.ProductID == item.ProductID &&
                        x.BranchID == model.BranchID && !x.IsDelete);

                    long updateQty = 0;

                    if (detail != null)
                    {
                        detail.ProductName = item.ProductName;
                        detail.Price = item.Price;
                        detail.NetPrice = item.NetPrice;
                        detail.Lastupdateduser = userName;
                        detail.Lastupdatedmachine = ipAddress;
                        detail.Lastupdateddate = GetFormattedDateTime();
                        detail.IsDelete = false;

                        if (detail.Quantity != item.Quantity)
                        {
                            updateQty = Convert.ToInt64(item.Quantity) - Convert.ToInt64(detail.Quantity);
                            detail.Quantity = item.Quantity;
                        }
                    }
                    else
                    {
                        updateQty = Convert.ToInt64(item.Quantity);

                        var newDetail = new BillingDetailsModel
                        {
                            BillID = model.BillID,
                            ProductID = item.ProductID,
                            ProductName = item.ProductName,
                            Price = item.Price,
                            Quantity = item.Quantity,
                            NetPrice = item.NetPrice,
                            BranchID = model.BranchID,
                            Lastupdateduser = userName,
                            Lastupdatedmachine = ipAddress,
                            Lastupdateddate = GetFormattedDateTime(),
                            IsDelete = false,
                            Barcode = item.Barcode
                        };

                        await _db.SHbilldetails.AddAsync(newDetail);
                    }

                    var stock = _db.MTProductMaster.FirstOrDefault(x => x.Barcode == item.Barcode && !x.IsDelete && x.BranchID == model.BranchID);
                    if (stock != null)
                    {
                        stock.NoofItem -= updateQty;
                    }
                }

                await _db.SaveChangesAsync();
                return master.BillID;
            }

            public (bool success, string message, object data) GetBillDetails(string billID, string branchID)
            {
                var bill = _db.SHbillmaster.FirstOrDefault(x => x.BillID == billID && !x.IsDelete && x.BranchID == branchID);

                if (bill == null)
                    return (false, "Bill not found", null);

                var details = _db.SHbilldetails
                    .Where(d => d.BillID == billID && !d.IsDelete && d.BranchID == branchID)
                    .Select(d => new
                    {
                        ProductID = d.ProductID,
                        ProductName = d.ProductName,
                        Price = d.Price,
                        Quantity = d.Quantity,
                        NetPrice = d.NetPrice
                    }).ToList();

                return (true, "", new
                {
                    customerNumber = bill.CustomerNumber,
                    billDate = bill.BillDate,
                    totalPrice = bill.Totalprice,
                    netPrice = bill.NetPrice,
                    cgstPer = bill.CGSTPercentage,
                    sgstPer = bill.SGSTPercentage,
                    totaldis = bill.TotalDiscount,
                    products = details
                });
            }

            public bool DeleteBill(string billID, string branchID)
            {
                var bill = _db.SHbillmaster.FirstOrDefault(x => x.BillID == billID && !x.IsDelete && x.BranchID == branchID);

                if (bill == null)
                    return false;

                bill.IsDelete = true;

                var details = _db.SHbilldetails.Where(x => x.BillID == billID && !x.IsDelete && x.BranchID == branchID).ToList();
                foreach (var item in details)
                {
                    item.IsDelete = true;

                     var addstock = _db.MTProductMaster.FirstOrDefault(x => x.Barcode == item.Barcode && !x.IsDelete && x.BranchID == branchID);
                     if(addstock!=null)
                     {
                            if (long.TryParse(item.Quantity, out long qtyToAdd))
                            {
                                addstock.NoofItem += qtyToAdd;
                            }

                     }
                 }

                _db.SaveChanges();
                return true;
            }

            public (bool success, string message) AddOrUpdateCustomer(CustomerMasterModel model, string ip, string user)
            {
                var existing = _db.SHCustomerMaster.FirstOrDefault(x => x.MobileNumber == model.MobileNumber && x.BranchID == model.BranchID);

                if (existing != null)
                {
                    if (existing.IsDelete)
                        return (false, "Cannot update. Customer Number is marked as deleted");

                    existing.CustomerName = model.CustomerName;
                    existing.DateofBirth = model.DateofBirth;
                    existing.Gender = model.Gender;
                    existing.Address = model.Address;
                    existing.City = model.City;
                    existing.State = model.State;
                    existing.Country = model.Country;
                    existing.Fathername = model.Fathername;
                    existing.IsDelete = model.IsDelete;
                    existing.LastUpdatedDate = GetFormattedDateTime();
                    existing.LastUpdatedUser = user;
                    existing.LastUpdatedmachine = ip;

                    _db.Entry(existing).State = EntityState.Modified;
                }
                else
                {
                    model.LastUpdatedDate = GetFormattedDateTime();
                    model.LastUpdatedUser = user;
                    model.LastUpdatedmachine = ip;

                    _db.SHCustomerMaster.Add(model);
                }

                _db.SaveChanges();
                return (true, "Customer saved successfully");
            }
        }

    }
