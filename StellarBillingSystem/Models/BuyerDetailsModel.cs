namespace StellarBillingSystem_skj.Models
{
    public class BuyerDetailsModel
    {
        public BuyerDetailsModel() { }

        public int BuyerID { get; set; }

        public string BuyerName { get; set;}

        public string BuyerAddress { get; set;}

        public string BuyerPhoneNumber { get; set;}

        public string? LastUpdatedUser { get ; set; }

        public string? LastUpdatedmachine { get ; set ; }

        public string? LastUpdatedDate { get; set; }
    }
}
