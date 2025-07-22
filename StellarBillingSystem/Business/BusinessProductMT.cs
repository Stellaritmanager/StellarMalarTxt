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
                dataTable.Rows.Add(entity.ProductCode, entity.Barcode,entity.ProductName,entity.Price,entity.NoofItem);
            }

            return dataTable;
        }

        public List<CategoryModelMT> Getcat()

        {
            var catname = (
                        from pr in _billingContext.MTCategoryMaster
                        where pr.IsDelete == false
                        select new CategoryModelMT
                        {
                            CategoryID = pr.CategoryID,

                            CategoryName = pr.CategoryName

                        }).ToList();

            return catname;
        }

        public List<SizeMasterModelMT> Getsize()

        {
            var sizename = (
                        from pr in _billingContext.MTSizeMaster
                        where pr.IsDelete == false
                        select new SizeMasterModelMT
                        {
                            SizeID = pr.SizeID,

                            SizeName = pr.SizeName

                        }).ToList();

            return sizename;
        }

        public List<BrandMasterModelMT> Getbrand()

        {
            var brandname = (
                        from pr in _billingContext.MTBrandMaster
                        where pr.IsDelete == false
                        select new BrandMasterModelMT
                        {
                            BrandID = pr.BrandID,

                            BrandName = pr.BrandName

                        }).ToList();

            return brandname;
        }
    }
}
