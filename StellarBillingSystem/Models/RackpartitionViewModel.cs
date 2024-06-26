namespace StellarBillingSystem.Models
{
    public class RackpartitionViewModel
    {
        public RackpartitionViewModel() { }

        private string partitionID;
        private string productID;
        private string noofitems;

        private List<RackPatrionProductModel> viewrackpartition;

        public string PartitionID { get => partitionID; set => partitionID = value; }
        public string ProductID { get => productID; set => productID = value; }
        public List<RackPatrionProductModel> Viewrackpartition { get => viewrackpartition; set => viewrackpartition = value; }
        public string Noofitems { get => noofitems; set => noofitems = value; }
    }
}
