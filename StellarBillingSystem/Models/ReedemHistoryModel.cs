namespace StellarBillingSystem.Models
{
    public class ReedemHistoryModel
    {

        public ReedemHistoryModel() 
        {
        }

        private String customerNumber;
        private String dateOfReedem;
        private String reedemPoints;
        private bool isDelete;

        public string CustomerNumber { get => customerNumber; set => customerNumber = value; }
        public string DateOfReedem { get => dateOfReedem; set => dateOfReedem = value; }
        public string? ReedemPoints { get => reedemPoints; set => reedemPoints = value; }
        public bool IsDelete { get => isDelete; set => isDelete = value; }
    }

   
}
