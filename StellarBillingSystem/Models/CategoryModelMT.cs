using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StellarBillingSystem_Malar.Models
{
    public class CategoryModelMT
    {
        public CategoryModelMT() { }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryID {  get; set; }

        public string CategoryName { get; set; }

        public bool IsDelete { get; set; }

        [MaxLength(50)]
        public string? Lastupdateduser { get; set; }
        [MaxLength(50)]
        public string? Lastupdateddate { get; set; }
        [MaxLength(50)]
        public string? Lastupdatedmachine { get; set; }
        public string BranchID { get; set; }

        public string SizeID { get; set; }

    

    }
}
