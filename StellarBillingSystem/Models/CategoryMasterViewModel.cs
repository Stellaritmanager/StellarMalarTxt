namespace StellarBillingSystem.Models
{
    public class CategoryMasterViewModel
    {
        public CategoryMasterViewModel() { }

        private string strCategoryID;
        private string strCategoryName;
        private string strBranchID;
        private int strTotalCount;
        private int strCurrentPage;
        private int strPageSize;
        private List<CategoryMasterModel> viewCategories;

        public string CategoryID { get => strCategoryID; set => strCategoryID = value; }
        public string CategoryName { get => strCategoryName; set => strCategoryName = value; }
        public string BranchID { get => strBranchID; set => strBranchID = value; }
        public List<CategoryMasterModel> ViewCategories { get => viewCategories; set => viewCategories = value; }
        public int TotalCount { get => strTotalCount; set => strTotalCount = value; }
        public int CurrentPage { get => strCurrentPage; set => strCurrentPage = value; }
        public int PageSize { get => strPageSize; set => strPageSize = value; }
    }
}
