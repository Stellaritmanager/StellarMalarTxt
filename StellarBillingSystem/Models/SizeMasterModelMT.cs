using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StellarBillingSystem_Malar.Models
{
    public class SizeMasterModelMT
    {
        public SizeMasterModelMT() { }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SizeID { get; set; }
        public string SizeName { get; set; }

        public int CategoryID { get; set; }



        [MaxLength(50)]
        public string? Lastupdateduser { get; set; }
        [MaxLength(50)]
        public string? Lastupdateddate { get; set; }
        [MaxLength(50)]
        public string? Lastupdatedmachine { get; set; }

        public bool IsDelete { get; set; }
    }
}
