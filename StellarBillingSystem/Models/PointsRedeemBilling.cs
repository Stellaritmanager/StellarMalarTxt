namespace StellarBillingSystem.Models
{
    public class PointsRedeemBilling
    {
        public PointsRedeemBilling() { }

        private String strCustomerNumber;
        private String strPointsNumber;
        private string branchID;
        private List<PointsReedemDetailsModel> viewpoints;

        public List<PointsReedemDetailsModel> Viewpoints { get => viewpoints; set => viewpoints = value; }
        public string StrPointsNumber { get => strPointsNumber; set => strPointsNumber = value; }
        public string StrCustomerNumber { get => strCustomerNumber; set => strCustomerNumber = value; }
        public string BranchID { get => branchID; set => branchID = value; }
    }
}
