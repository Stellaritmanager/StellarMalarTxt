using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StellarBillingSystem_Malar.Models
{
    public class ProductModelMT
    {
        public ProductModelMT() { }

        public string ProductCode { get; set; }
        public string CategoryID {  get; set; }
        public string BrandID {  get; set; }    
        public string SizeID {  get; set; }
        public string ProductName {  get; set; }
        public string Barcode { get; set; }

        public long NoofItem { get; set; }

        public decimal Price { get; set; }
        public string BranchID {  get; set; }
        public bool IsDelete { get; set; }

        [MaxLength(50)]
        public string? Lastupdateduser { get; set; }
        [MaxLength(50)]
        public string? Lastupdateddate { get; set; }
        [MaxLength(50)]
        public string? Lastupdatedmachine { get; set; }

    }
}
