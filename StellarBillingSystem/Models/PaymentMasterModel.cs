using System.Drawing.Printing;

namespace StellarBillingSystem.Models
{
    public class PaymentMasterModel
    {

        public PaymentMasterModel() 
        { 
        }
        private String strBillId;
        private String strPaymentId;
        private String strCustomerNumber;
        private String strReedemPoints;
        private String strBalance;
        private bool strIsDelete;
        private String lastupdateduser;
        private String lastupdateddate;
        private String lastupdatedmachine;

        public string BillId { get => strBillId; set => strBillId = value; }
        public string PaymentId { get => strPaymentId; set => strPaymentId = value; }
        public string? CustomerNumber { get => strCustomerNumber; set => strCustomerNumber = value; }
        public string? ReedemPoints { get => strReedemPoints; set => strReedemPoints = value; }
        public string? Balance { get => strBalance; set => strBalance = value; }
        public bool IsDelete { get => strIsDelete; set => strIsDelete = value; }
        public string? Lastupdateduser { get => lastupdateduser; set => lastupdateduser = value; }
        public string? Lastupdateddate { get => lastupdateddate; set => lastupdateddate = value; }
        public string? Lastupdatedmachine { get => lastupdatedmachine; set => lastupdatedmachine = value; }
    }
}
