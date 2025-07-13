using StellarBillingSystem.Context;
using StellarBillingSystem.Models;

namespace StellarBillingSystem_skj.Business
{
    public class BusinessBillingSKJ
    {

        private readonly BillingContext _billingContext;
        private readonly IConfiguration _configuration;

        public BusinessBillingSKJ(BillingContext billingContext, IConfiguration configuration)
        {
            _billingContext = billingContext;
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public List<CustomerMasterModel> getCustomerID(string BranchID)
        {
            var customerid = (
                    from pr in _billingContext.SHCustomerMaster
                    where pr.BranchID == BranchID && pr.IsDelete == false
                    select new CustomerMasterModel
                    {
                        CustomerID = pr.CustomerID,
                        MobileNumber = pr.MobileNumber,
                        CustomerName = pr.CustomerName,
                    }
                ).ToList();

            return customerid;
        }

        public List<CategoryMasterModel> getGoldtype(string BranchID)
        {
            var goldtype = (
                    from pr in _billingContext.SHCategoryMaster
                    where pr.BranchID == BranchID && pr.IsDelete == false
                    select new CategoryMasterModel
                    {
                        CategoryName = pr.CategoryName,
                        MarketRate = pr.MarketRate,
                        CategoryID = pr.CategoryID,
                    }
                ).ToList();

            return goldtype;
        }


    }
}
