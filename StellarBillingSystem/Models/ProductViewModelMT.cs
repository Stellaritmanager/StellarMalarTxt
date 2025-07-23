using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace StellarBillingSystem_Malar.Models
{
    public class ProductViewModelMT
    {
        public ProductModelMT ObjMT { get; set; }

        public IEnumerable<SelectListItem> CatgeoryList { get; set; }
        public IEnumerable<SelectListItem> SizeList { get; set; }
        public IEnumerable<SelectListItem> BranchList { get; set; }

        public DataTable ProductData { get; set; }

    }
}
