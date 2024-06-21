using System;

public class ReportModel
{
	public ReportModel()
	{
	}

	private String strReportID;
    private String strFromDate;
    private String strToDate;
    private String strLastUpdatedDate;
    private String strLastUpdatedUser;
    private String strLastUpdatedmachine;

    public string ReportID { get => strReportID; set => strReportID = value; }
    public string? FromDate { get => strFromDate; set => strFromDate = value; }
    public string? ToDate { get => strToDate; set => strToDate = value; }
    public string? LastUpdatedDate { get => strLastUpdatedDate; set => strLastUpdatedDate = value; }
    public string? LastUpdatedUser { get => strLastUpdatedUser; set => strLastUpdatedUser = value; }
    public string? LastUpdatedmachine { get => strLastUpdatedmachine; set => strLastUpdatedmachine = value; }

}
	