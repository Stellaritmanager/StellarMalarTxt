namespace StellarBillingSystem.Models
{
    public class PointsRedeemBilling
    {
        public PointsRedeemBilling() { }

        private String strCustomerNumber;
        private String strPointsNumber;
        private List<PointsReedemDetailsModel> viewpoints;

        public List<PointsReedemDetailsModel> Viewpoints { get => viewpoints; set => viewpoints = value; }
        public string StrPointsNumber { get => strPointsNumber; set => strPointsNumber = value; }
        public string StrCustomerNumber { get => strCustomerNumber; set => strCustomerNumber = value; }
    }
}
