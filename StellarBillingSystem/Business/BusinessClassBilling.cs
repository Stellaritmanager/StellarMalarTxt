
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
