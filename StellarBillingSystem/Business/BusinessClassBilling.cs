
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using StellarBillingSystem.Context;
using StellarBillingSystem.Models;

namespace StellarBillingSystem.Business
{
    public class BusinessClassBilling
    {

        private readonly BillingContext _billingContext;

        public BusinessClassBilling(BillingContext billingContext)
        {
            _billingContext = billingContext;
        }

        /*Product Dropdown*/

        public List<CategoryMasterModel> GetCatid()
        {
            var categoryid = (
                    from pr in _billingContext.SHCategoryMaster
                    select new CategoryMasterModel
                    {
                        CategoryID = pr.CategoryID,
                        CategoryName = pr.CategoryName,
                    }).ToList();
            return categoryid;
        }

        /*Points Reedem Details*/

        public List<CustomerMasterModel> GetCustomerid()
        {
            var pointreedemcustomerid = (
                from pr in _billingContext.SHCustomerMaster
                select new CustomerMasterModel
                {
                    CustomerID = pr.CustomerID,
                    CustomerName = pr.CustomerName,
                }).ToList();
            return pointreedemcustomerid;
        }

        /*Discount Category*/
        public List<CategoryMasterModel> GetcategoryID()
        {
            var discountcategoryid = (
                from pr in _billingContext.SHCategoryMaster
                select new CategoryMasterModel
                {
                    CategoryID = pr.CategoryID,
                    CategoryName = pr.CategoryName,

                }).ToList();
            return discountcategoryid;
        }



        /*Godown*/

        public List<ProductMatserModel> GetProductid()
        {
            var godownproductid = (
                from pr in _billingContext.SHProductMaster
                select new ProductMatserModel
                {
                    ProductID = pr.ProductID,
                    ProductName = pr.ProductName,
                }).ToList();
            return godownproductid;
        }



        public List<RackpartitionViewModel> GetRackview(string partitionID, string productID)
        {
            var result = (from p in _billingContext.SHRackPartionProduct
                          where p.PartitionID == partitionID && p.ProductID == productID && p.Isdelete == false
                          select new RackpartitionViewModel
                          {
                              PartitionID = p.PartitionID,
                              ProductID = p.ProductID,
                               Noofitems= p.Noofitems
        
                          }).ToList();
            return result;
        }



        public List<ResourceTypeMasterModel> GetResourceid()
        {
            var resoruseid = (
                    from pr in _billingContext.SHresourceType
                    select new ResourceTypeMasterModel
                    {
                        ResourceTypeID = pr.ResourceTypeID,
                        ResourceTypeName = pr.ResourceTypeName
                    }
               ).ToList();

            return resoruseid;
        }

        public List<RollTypeMaster> RollAccessType()
        {
            var rollid = (
                    from pr in _billingContext.SHrollType
                    select new RollTypeMaster
                    {
                        RollID = pr.RollID,
                        RollName = pr.RollName
                    }
                ).ToList();

            return rollid;
        }

        public List<ScreenNameMasterModel> Screenname()
        {
            var screenname = (
                    from pr in _billingContext.SHScreenName
                    select new ScreenNameMasterModel
                    {
                        ScreenName = pr.ScreenName,
                    }
                ).ToList();

            return screenname;
        }


        public List<StaffAdminModel> GetStaffID()
        {
            var staffid = (
                    from pr in _billingContext.SHStaffAdmin
                    select new StaffAdminModel
                    {
                        StaffID = pr.StaffID,
                        FullName = pr.FullName
                    }
                ).ToList();

            return staffid;
        }

        public List<ScreenMasterModel> GetScreenid()

        {
            var screenid = (
                        from pr in _billingContext.SHScreenMaster
                        select new ScreenMasterModel
                        {
                            ScreenId = pr.ScreenId,
                            
                            ScreenName = pr.ScreenName

                        }).ToList();
            return screenid;
        }


        public List<String> GetRoll(string userid)
        {
            var query = from sm in _billingContext.SHScreenMaster
                        join rac in _billingContext.SHRoleaccessModel on sm.ScreenId equals rac.ScreenID
                        join ram in _billingContext.SHrollaccess on rac.RollID equals ram.RollID
                        join sam in _billingContext.SHStaffAdmin on ram.StaffID equals sam.StaffID
                        where rac.Authorized == "1" && sam.UserName == userid
                        select sm.ScreenName;

            var result = query.ToList();
            return result;
        }

        public List<GenericReportModel> GetReportId()
        {
            var reportid = (
                    from pr in _billingContext.ShGenericReport
                    select new GenericReportModel
                    {
                        ReportName = pr.ReportName,
                    }
                ).ToList();

            return reportid;
        }





        /*        public async Task<bool> UpdateProduct(string productId, bool isDelete)
                {
                    var product = await _billingContext.SHProductMaster.FirstOrDefaultAsync(x => x.ProductID == productId);

                    if (product == null)
                    {
                        return false; // Product not found
                    }

                    product.IsDelete = isDelete;

                    await _billingContext.SaveChangesAsync();

                    return true; // Update successful
                }*/

        /*        public async Task<ProductMatserModel> GetProductmaster(string productID)
                {
                    var product = await (
                            from pp in _billingContext.SHProductMaster
                            where pp.ProductID == productID
                            select new ProductMatserModel
                            {
                                CategoryID = pp.CategoryID,
                                ProductID = pp.ProductID,
                                ProductName = pp.ProductName,
                                Brandname = pp.Brandname,
                                Discount = pp.Discount,
                                TotalAmount = pp.TotalAmount,
                                Price = pp.Price

                            }).FirstOrDefaultAsync();

                    return product;

                }*/



        /*        public async Task AddPointsToCustomer(string customerId, int pointsToAdd)
                {
                    var customer = await _billingContext.SHCustomerMaster.FindAsync(customerId);

                    if (customer == null)
                    {
                        throw new ArgumentException("Customer not found");
                    }

                    customer.PointsReedem += pointsToAdd;
                    await _billingContext.SaveChangesAsync();
                }*/
    }


}
