using System.ComponentModel.DataAnnotations;

namespace StellarBillingSystem_skj.Models
{
    public class RepledgeModel
    {
        public RepledgeModel() { }

        [MaxLength(100)]
        public string RepledgeID { get; set; }
        [MaxLength(100)]
        public string? Description { get; set; }

        public string TotalAmount { get; set; }

        public string Intrerest { get; set; }

        public string Tenure { get; set; }

        public string IsDelete { get; set; }
        [MaxLength(50)]
        public string? LastUpdatedUser { get; set; }
        [MaxLength(50)]
        public string? LastUpdatedmachine { get; set; }
        [MaxLength(50)]
        public string? LastUpdatedDate { get; set; }
    }
}
