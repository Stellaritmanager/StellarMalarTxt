using System.ComponentModel.DataAnnotations;

namespace StellarBillingSystem_Malar.Models
{
    public class ProductInwardModelMT
    {
        public ProductInwardModelMT() { }

        public string? SupplierName { get; set; }
        public string InvoiceNumber { get; set; }
        public string? InvoiceDate { get; set; }
        public string ProductCode { get; set; }
        public long NoofItem { get; set; }
        public decimal SupplierPrice { get; set; }
        public double Tax { get; set; }
        public decimal Amount { get; set; }
        public string BranchID { get; set; }

        [MaxLength(50)]
        public string? Lastupdateduser { get; set; }
        [MaxLength(50)]
        public string? Lastupdateddate { get; set; }
        [MaxLength(50)]
        public string? Lastupdatedmachine { get; set; }

        public bool IsDelete { get; set; }


    }
}
