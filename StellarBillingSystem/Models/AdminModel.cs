namespace StellarBillingSystem.Models
{
    public class AdminModel
    {
        public AdminModel() { }

        private string userName;
        private string branchID;

        public string UserName { get => userName; set => userName = value; }
        public string BranchID { get => branchID; set => branchID = value; }
    }
}
