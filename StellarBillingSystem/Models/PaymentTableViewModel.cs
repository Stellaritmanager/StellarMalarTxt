namespace StellarBillingSystem.Models
{
    public class PaymentTableViewModel
    {
        public PaymentTableViewModel() 
        {
        }

        private String strBillId;
        private String strPaymentId;
        private String strCustomerNumber;
        private String strReedemPoints;
        private String strBalance;
        private String strBillDate;
        private bool strIsDelete;
        private List<PaymentDetailsModel> viewpayment;

        public string BillId { get => strBillId; set => strBillId = value; }
        public string PaymentId { get => strPaymentId; set => strPaymentId = value; }
        public string? CustomerNumber { get => strCustomerNumber; set => strCustomerNumber = value; }
        public string? ReedemPoints { get => strReedemPoints; set => strReedemPoints = value; }
        public string? Balance { get => strBalance; set => strBalance = value; }
        public bool IsDelete { get => strIsDelete; set => strIsDelete = value; }
        public List<PaymentDetailsModel> Viewpayment { get => viewpayment; set => viewpayment = value; }
        public string BillDate { get => strBillDate; set => strBillDate = value; }
    }
}
