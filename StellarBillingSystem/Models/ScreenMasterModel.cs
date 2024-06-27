namespace StellarBillingSystem.Models
{
    public class ScreenMasterModel
    {
        public ScreenMasterModel() { }

        private String strScreenId;
        private String strScreenName;
        private String strlastUpdatedDate;
        private String strlastUpdatedUser;
        private String strlastUpdatedMachine;
        private string isdelete;

        public string ScreenId { get => strScreenId; set => strScreenId = value; }
        public string? ScreenName { get => strScreenName; set => strScreenName = value; }
        public string? lastUpdatedDate { get => strlastUpdatedDate; set => strlastUpdatedDate = value; }
        public string? lastUpdatedUser { get => strlastUpdatedUser; set => strlastUpdatedUser = value; }
        public string? lastUpdatedMachine { get => strlastUpdatedMachine; set => strlastUpdatedMachine = value; }
        public string Isdelete { get => isdelete; set => isdelete = value; }
    }
}
