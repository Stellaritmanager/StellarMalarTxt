using System;
using System.ComponentModel.DataAnnotations.Schema;

public class ReportModel
{
	public ReportModel()
	{
	}


    private int reportId;
    private string reportName;
    private string reportType;
    private string reportDescription;
    private string reportQuery;
    private string lastUpdateduser;
    private string lastUpdatedmachine;
    private string lastupdateddate;


    public int ReportId { get => reportId; set => reportId = value; }
    public string ReportName { get => reportName; set => reportName = value; }
    public string ReportType { get => reportType; set => reportType = value; }
    public string ReportDescription { get => reportDescription; set => reportDescription = value; }
    public string ReportQuery { get => reportQuery; set => reportQuery = value; }
    public string LastUpdateduser { get => lastUpdateduser; set => lastUpdateduser = value; }
    public string LastUpdatedmachine { get => lastUpdatedmachine; set => lastUpdatedmachine = value; }
    public string Lastupdateddate { get => lastupdateddate; set => lastupdateddate = value; }
}
	