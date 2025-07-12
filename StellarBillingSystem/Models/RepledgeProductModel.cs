namespace StellarBillingSystem_skj.Models
{
    public class RepledgeProductModel
    {
        public RepledgeProductModel() { }

        public  string RepledgeID { get; set; }

        public string BillID { get; set; }

        public string ProductID { get; set; }

        public string IsDelete { get; set; }

        public string? LastUpdatedUser { get; set; }

        public string? LastUpdatedmachine { get; set; }

        public string? LastUpdatedDate { get; set; }
    }
}
