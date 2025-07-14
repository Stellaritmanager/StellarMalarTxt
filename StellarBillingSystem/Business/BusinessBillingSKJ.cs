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

        public DateTime GetCurrentDateTime()
        {
            string useIST = _configuration.GetValue<string>("DateTimeSettings:UseIST");

            DateTime currentDateTime = DateTime.Now;
            if (useIST.ToLower() == "yes")
            {
                // Return the current time
                return currentDateTime;
            }
            else
            {
                // Add 5 hours and 30 minutes to the current time
                return currentDateTime.AddHours(5).AddMinutes(30);
            }
        }

        public string GetFormattedDateTime()
        {
            DateTime currentDateTime = GetCurrentDateTime();
            return currentDateTime.ToString("dd/MM/yyyy HH:mm:ss");
        }

        public static String getbalance(BillingContext billing, string strPayID, string pBillID, string strBranchid, string pPaymentAmount)
        {
            var paymentList = billing.SHPaymentDetails.Where(x => x.PaymentId == strPayID && x.IsDelete == false && x.BranchID == strBranchid).Select(x => x.PaymentAmount).ToList();


            Double dblBalance = 0.0;



            foreach (var strpayment in paymentList)
            {
                if (!(String.IsNullOrEmpty(strpayment)))
                    dblBalance = dblBalance + Double.Parse(strpayment);
            }

            decimal billamount = billing.Shbillmasterskj.Where(x => x.BillID == pBillID  && x.BranchID == strBranchid).Select(x => x.TotalRepayValue).FirstOrDefault();

            if (billamount != null)
                dblBalance = (double)(billamount - Convert.ToDecimal(dblBalance));

            dblBalance = Math.Round(dblBalance, 2);

            return dblBalance.ToString();
        }

        public string GeneratePaymentDescriptionreport(string paymentId)
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            return $"{paymentId}_{timestamp}";
        }

    }
}
