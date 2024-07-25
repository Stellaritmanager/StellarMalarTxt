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
        private string CGSTPercentage;
        private string SGSTPercentage;
        private string CGSTPercentageAmt;
        private string SGSTPercentageAmt;
        private bool isDelete;
        private string lastupdateduser;
        private string lastupdateddate;
        private string lastupdatedmachine;
        private string branchID;

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
        public string? CGSTPercentage1 { get => CGSTPercentage; set => CGSTPercentage = value; }
        public string? SGSTPercentage1 { get => SGSTPercentage; set => SGSTPercentage = value; }
        public string? CGSTPercentageAmt1 { get => CGSTPercentageAmt; set => CGSTPercentageAmt = value; }
        public string? SGSTPerentageAmt1 { get => SGSTPercentageAmt; set => SGSTPercentageAmt = value; }
        public string BranchID { get => branchID; set => branchID = value; }
    }
}
