namespace StellarBillingSystem.Models
{
    public class RollAccessMaster
    {
        public RollAccessMaster() { }

        private string staffID;
        private string rollID;
        private string lastupdatedDate;
        private string lastupdatedMachine;
        private string lastupdateduser;
        private bool isDelete;
        private string branchID;
        public string StaffID { get => staffID; set => staffID = value; }
        public string RollID { get => rollID; set => rollID = value; }
        public string? LastupdatedDate { get => lastupdatedDate; set => lastupdatedDate = value; }
        public string? LastupdatedMachine { get => lastupdatedMachine; set => lastupdatedMachine = value; }
        public string? Lastupdateduser { get => lastupdateduser; set => lastupdateduser = value; }
        public bool IsDelete { get => isDelete; set => isDelete = value; }
        public string BranchID { get => branchID; set => branchID = value; }
    }
}
