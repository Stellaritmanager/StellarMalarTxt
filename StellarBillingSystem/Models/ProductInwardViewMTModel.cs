using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace StellarBillingSystem_Malar.Models
{
    public class ProductInwardViewMTModel
    {
        public ProductInwardModelMT ObjMT { get; set; }
        public IEnumerable<SelectListItem> ProductList { get; set; }
        
    }
}
