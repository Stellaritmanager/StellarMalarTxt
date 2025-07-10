namespace StellarBillingSystem_skj.Models
{
    public class RepledgeModel
    {
        public RepledgeModel() { }

        public string RepledgeID {  get; set; }

        public string? Description { get; set; }

        public string TotalAmount {  get; set; }

        public string Intrerest {  get; set; }

        public string Tenure { get; set; }

        public string IsDelete { get; set; }

        public string? LastUpdatedUser { get; set; }

        public string? LastUpdatedmachine { get; set; }

        public string? LastUpdatedDate { get; set; }
    }
}
