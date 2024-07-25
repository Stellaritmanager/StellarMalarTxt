using System.ComponentModel.DataAnnotations.Schema;

namespace StellarBillingSystem.Models
{
    public class LogsModel
    {
        private String logMessage;
        private String machineName;
        private String logDate;
        private String userName;
        private String logScreen;
        private String logType;
        private int logID;
        private int? att1;
        private string branchID;

        public String? LogMessage { get => logMessage; set => logMessage = value; }
        public String? MachineName { get => machineName; set => machineName = value; }
        public String? LogDate { get => logDate; set => logDate = value; }
        public String? UserName { get => userName; set => userName = value; }
        public String? LogScreens { get => logScreen; set => logScreen = value; }
        public String? LogType { get => logType; set => logType = value; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LogID { get => logID; set => logID = value; }
        public int? Att1 { get => att1; set => att1 = value; }
        public string BranchID { get => branchID; set => branchID = value; }
    }
}
