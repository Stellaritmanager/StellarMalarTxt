using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StellarBillingSystem_Malar.Models
{
    public class BrandMasterModelMT
    {
        public BrandMasterModelMT() { }

       
        public int BrandID { get; set; }

        public string BrandName { get; set; }

       

        public bool IsDelete { get; set; }

        [MaxLength(50)]
        public string? Lastupdateduser { get; set; }
        [MaxLength(50)]
        public string? Lastupdateddate { get; set; }
        [MaxLength(50)]
        public string? Lastupdatedmachine { get; set; }
    }
}
