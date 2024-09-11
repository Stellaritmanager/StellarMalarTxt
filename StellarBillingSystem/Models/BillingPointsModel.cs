namespace StellarBillingSystem.Models
{
    public class BillingPointsModel
    {
        public BillingPointsModel() { }

        private string billID;
        private string customerNumber;
        private string netPrice;
        private string points;
        private bool isUsed;
        private string dateofReedem;
        private string branchID;

        public string BillID { get => billID; set => billID = value; }
        public string CustomerNumber { get => customerNumber; set => customerNumber = value; }
        public string? NetPrice { get => netPrice; set => netPrice = value; }
        public string? Points { get => points; set => points = value; }
        public bool IsUsed { get => isUsed; set => isUsed = value; }
        public string? DateofReedem { get => dateofReedem; set => dateofReedem = value; }
        public string? BranchID { get => branchID; set => branchID = value; }
    }
}
