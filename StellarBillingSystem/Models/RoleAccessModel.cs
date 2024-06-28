namespace StellarBillingSystem.Models
{
    public class RoleAccessModel
    {
        public RoleAccessModel() { }

        private string strRollID;
        private string strScreenID;
        private string strAccess;
        private string strAuthorized;
        private string strlastUpdatedDate;
        private string strlastUpdatedUser;
        private string strlastUpdatedMachine;
        private bool isdelete;

        public string RollID { get => strRollID; set => strRollID = value; }
        public string ScreenID { get => strScreenID; set => strScreenID = value; }
        public string Access { get => strAccess; set => strAccess = value; }
        public string Authorized { get => strAuthorized; set => strAuthorized = value; }
        public string? lastUpdatedDate { get => strlastUpdatedDate; set => strlastUpdatedDate = value; }
        public string? lastUpdatedUser { get => strlastUpdatedUser; set => strlastUpdatedUser = value; }
        public string? lastUpdatedMachine { get => strlastUpdatedMachine; set => strlastUpdatedMachine = value; }
        public bool Isdelete { get => isdelete; set => isdelete = value; }
    }
}
