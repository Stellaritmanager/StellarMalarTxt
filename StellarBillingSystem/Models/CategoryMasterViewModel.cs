namespace StellarBillingSystem.Models
{
    public class CategoryMasterViewModel
    {
        public CategoryMasterViewModel() { }

        private string strCategoryID;
        private string strCategoryName;
        private string strBranchID;

        private List<CategoryMasterModel> viewCategories;

        public string CategoryID { get => strCategoryID; set => strCategoryID = value; }
        public string CategoryName { get => strCategoryName; set => strCategoryName = value; }
        public string BranchID { get => strBranchID; set => strBranchID = value; }
        public List<CategoryMasterModel> ViewCategories { get => viewCategories; set => viewCategories = value; }
    }
}
