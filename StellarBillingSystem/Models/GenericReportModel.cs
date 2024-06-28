using System.ComponentModel.DataAnnotations.Schema;

namespace StellarBillingSystem.Models
{
    public class GenericReportModel
    {
        public GenericReportModel() { }

        private int reportId;
        private string reportName;
        private string reportType;
        private string reportDescription;
        private string reportQuery;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReportId { get => reportId; set => reportId = value; }
        public string ReportName { get => reportName; set => reportName = value; }
        public string ReportType { get => reportType; set => reportType = value; }
        public string ReportDescription { get => reportDescription; set => reportDescription = value; }
        public string ReportQuery { get => reportQuery; set => reportQuery = value; }
    }
}
