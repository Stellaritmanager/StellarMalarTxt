namespace StellarBillingSystem.Models
{
    public class BillingMasterModel
    {
        public BillingMasterModel() { }

        private string billID;
        private string billDate;
        private string customerNumber;
        private string totalprice;
        private string totalDiscount;
        private string netPrice;
        private bool isDelete;
        private string lastupdateduser;
        private string lastupdateddate;
        private string lastupdatedmachine;

        public string BillID { get => billID; set => billID = value; }
        public string BillDate { get => billDate; set => billDate = value; }
        public string CustomerNumber { get => customerNumber; set => customerNumber = value; }
        public string? Totalprice { get => totalprice; set => totalprice = value; }
        public string? TotalDiscount { get => totalDiscount; set => totalDiscount = value; }
        public string? NetPrice { get => netPrice; set => netPrice = value; }
        public bool IsDelete { get => isDelete; set => isDelete = value; }
        public string? Lastupdateduser { get => lastupdateduser; set => lastupdateduser = value; }
        public string? Lastupdateddate { get => lastupdateddate; set => lastupdateddate = value; }
        public string? Lastupdatedmachine { get => lastupdatedmachine; set => lastupdatedmachine = value; }
    }
}
