namespace StellarBillingSystem_skj.Models
{
    public class BillViewModel
    {
        public BillViewModel() { }


        public BillMasterModelSKJ BillMaster { get; set; }
        public List<BillDetailsModelSKJ> BillDetails { get; set; }
        public List<ArticleModel> Articles { get; set; }
    }
}
