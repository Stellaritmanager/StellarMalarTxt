using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StellarBillingSystem_Malar.Models
{
    public class StockDetailsModel
    {
        public StockDetailsModel() { }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StockID { get; set; }
        public string ProductID { get; set; }
        public long NoofItem { get; set; }

        public string BranchID { get; set; }

        [MaxLength(50)]
        public string? Lastupdateduser { get; set; }
        [MaxLength(50)]
        public string? Lastupdateddate { get; set; }
        [MaxLength(50)]
        public string? Lastupdatedmachine { get; set; }
    }
}
