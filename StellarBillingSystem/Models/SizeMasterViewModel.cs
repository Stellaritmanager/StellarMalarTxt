using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace StellarBillingSystem_Malar.Models
{
    public class SizeMasterViewModel
    {
        public SizeMasterModelMT Model { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public DataTable SizeData { get; set; }
    }
}
