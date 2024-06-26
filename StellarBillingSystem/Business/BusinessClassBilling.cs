
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

      

        public async Task<List<CustomerMasterModel>> GetCustomerById(string customerNumber)
        {
            var customerpoint = (from shr in _billingContext.SHPointsReedemDetails
                                 join shc in _billingContext.SHCustomerMaster on shr.CustomerID equals shc.CustomerID
                                 join shb in _billingContext.SHCustomerBilling on shc.MobileNumber equals shb.CustomerNumber
                                 select shr.TotalPoints
                                 ).First();
            
            
            
            var customernumber =  (from n in _billingContext.SHCustomerBilling
                                  from p in _billingContext.SHPointsReedemDetails
                                  join s in _billingContext.SHCustomerMaster on n.CustomerNumber equals s.MobileNumber
                                  join pr in _billingContext.SHCustomerMaster on p.CustomerID equals pr.CustomerID
                                  where s.MobileNumber == customerNumber
                                  select new CustomerMasterModel
                                  {

                                      MobileNumber = n.CustomerNumber,
                                      PointsReedem = p.TotalPoints,

                                  }).ToListAsync().Result;

            return customernumber;
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
