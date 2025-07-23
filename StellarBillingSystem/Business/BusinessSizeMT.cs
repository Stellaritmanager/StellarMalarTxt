using Microsoft.EntityFrameworkCore;
using StellarBillingSystem.Context;
using StellarBillingSystem_Malar.Models;
using System.Data;

namespace StellarBillingSystem_Malar.Business
{
    public class BusinessSizeMT
    {
        private readonly BillingContext _billingContext;
        private readonly IConfiguration _configuration;

        public BusinessSizeMT(BillingContext billingContext, IConfiguration configuration)
        {
            _billingContext = billingContext;
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public static DataTable convertToDataTableSizeMaster(IEnumerable<SizeMasterModelMT> entities,BillingContext dbContext)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("SizeID", typeof(int));
            dataTable.Columns.Add("CategoryName", typeof(string));
            dataTable.Columns.Add("SizeName", typeof(string));

            foreach (var entity in entities)
            {
                var catName=dbContext.MTCategoryMaster.Where(i=>i.CategoryID==entity.CategoryID).FirstOrDefault();
                dataTable.Rows.Add(entity.SizeID,catName.CategoryName ,entity.SizeName);
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

    }
}
