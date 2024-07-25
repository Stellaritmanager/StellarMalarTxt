namespace StellarBillingSystem.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        private string branchID;

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string BranchID { get => branchID; set => branchID = value; }
    }
}
