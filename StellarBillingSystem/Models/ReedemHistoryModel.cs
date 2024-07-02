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
        private String lastupdateduser;
        private String lastupdateddate;
        private String lastupdatedmachine;

        public string CustomerNumber { get => customerNumber; set => customerNumber = value; }
        public string DateOfReedem { get => dateOfReedem; set => dateOfReedem = value; }
        public string? ReedemPoints { get => reedemPoints; set => reedemPoints = value; }
        public bool IsDelete { get => isDelete; set => isDelete = value; }
        public string? Lastupdateduser { get => lastupdateduser; set => lastupdateduser = value; }
        public string? Lastupdateddate { get => lastupdateddate; set => lastupdateddate = value; }
        public string? Lastupdatedmachine { get => lastupdatedmachine; set => lastupdatedmachine = value; }
    }

   
}
