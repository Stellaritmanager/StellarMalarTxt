using Microsoft.AspNetCore.Mvc.Rendering;
using StellarBillingSystem.Context;
using StellarBillingSystem_Malar.Models;
using System.Data;

namespace StellarBillingSystem_Malar.Business
{
    public class BusinessProductMT
    {
        private readonly BillingContext _billingContext;
        private readonly IConfiguration _configuration;

        public BusinessProductMT(BillingContext billingContext, IConfiguration configuration)
        {
            _billingContext = billingContext;
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public static DataTable convertToDataTableProductMaster(IEnumerable<ProductModelMT> entities)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("ProductCode", typeof(string));
            dataTable.Columns.Add("Barcode", typeof(string));
            dataTable.Columns.Add("ProductName", typeof(string));
            dataTable.Columns.Add("Price", typeof(decimal));
            dataTable.Columns.Add("NoofItem", typeof(long));

            foreach (var entity in entities)
            {
                dataTable.Rows.Add(entity.ProductCode, entity.Barcode, entity.ProductName, entity.Price, entity.NoofItem);
            }

            return dataTable;
        }

        public IEnumerable<SelectListItem> GetCat(int? selectedId)
        {
            return _billingContext.MTCategoryMaster
                .Where(c => c.IsDelete == false)
                .Select(c => new SelectListItem
                {
                    Value = c.CategoryID.ToString(),
                    Text = c.CategoryName
                }).ToList();
        }


        public IEnumerable<SelectListItem> GetSize(string? selectedId)
        {
            return _billingContext.MTSizeMaster
                .Where(c => c.IsDelete == false)
                .Select(c => new SelectListItem
                {
                    Value = c.SizeName,
                    Text = c.SizeName,
                    Selected = !string.IsNullOrEmpty(selectedId) && selectedId == c.SizeName
                }).ToList();
        }



        public IEnumerable<SelectListItem> Getbrand(int? selectedId)
        {
            return _billingContext.MTBrandMaster
                .Where(c => c.IsDelete == false) // Optional: to filter out deleted items
                .Select(c => new SelectListItem
                {
                    Value = c.BrandID.ToString(),
                    Text = c.BrandName,
                    Selected = selectedId.HasValue && selectedId.Value == c.BrandID
                }).ToList();
        }



    }
}
