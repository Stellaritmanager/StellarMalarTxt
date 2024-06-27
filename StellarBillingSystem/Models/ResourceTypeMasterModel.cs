namespace StellarBillingSystem.Models
{
    public class ResourceTypeMasterModel
    {
        public ResourceTypeMasterModel() { }


        private string strResourceTypeID;
        private string strResourceTypeName;
        private string strlastUpdatedDate;
        private string strlastUpdatedUser;
        private string strlastUpdatedMachine;
        private bool isDelete;

        public string ResourceTypeID { get => strResourceTypeID; set => strResourceTypeID = value; }
        public string? ResourceTypeName { get => strResourceTypeName; set => strResourceTypeName = value; }
        public string? lastUpdatedDate { get => strlastUpdatedDate; set => strlastUpdatedDate = value; }
        public string? lastUpdatedUser { get => strlastUpdatedUser; set => strlastUpdatedUser = value; }
        public string? lastUpdatedMachine { get => strlastUpdatedMachine; set => strlastUpdatedMachine = value; }
        public bool IsDelete { get => isDelete; set => isDelete = value; }
    }
}
