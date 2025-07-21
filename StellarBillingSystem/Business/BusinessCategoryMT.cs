using StellarBillingSystem.Context;
using StellarBillingSystem_Malar.Models;
using System.Data;

namespace StellarBillingSystem_Malar.Business
{
    public class BusinessCategoryMT
    {
        private readonly BillingContext _billingContext;
        private readonly IConfiguration _configuration;

        public BusinessCategoryMT(BillingContext billingContext, IConfiguration configuration)
        {
            _billingContext = billingContext;
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public static DataTable convertToDataTableCategoryMaster(IEnumerable<CategoryModelMT> entities)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("CategoryID", typeof(int));
            dataTable.Columns.Add("CategoryName", typeof(string));

            foreach (var entity in entities)
            {
                dataTable.Rows.Add(entity.CategoryID, entity.CategoryName);
            }

            return dataTable;
        }

        public List<SizeMasterModelMT> Getsize(string branchid)

        {
            var size = (
                        from pr in _billingContext.MTSizeMaster
                        where pr.IsDelete == false && pr.BranchID == branchid
                        select new SizeMasterModelMT
                        {
                            SizeID = pr.SizeID,

                            SizeName = pr.SizeName

                        }).ToList();
            return size;
        }

    }
}
