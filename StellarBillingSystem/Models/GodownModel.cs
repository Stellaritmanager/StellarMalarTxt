using System;

public class GodownModel
{
	public GodownModel()
	{
	}

	private String strProductID;
	private String strNumberofStocks;
    private String strNumberofStocksinRack;
	private String strDescription;
    private String strLastUpdatedDate;
    private String strLastUpdatedUser;
    private String strLastUpdatedmachine;

    public string ProductID { get => strProductID; set => strProductID = value; }
    public string? NumberofStocks { get => strNumberofStocks; set => strNumberofStocks = value; }
    public string? Description { get => strDescription; set => strDescription = value; }
    public string? LastUpdatedDate { get => strLastUpdatedDate; set => strLastUpdatedDate = value; }
    public string? LastUpdatedUser { get => strLastUpdatedUser; set => strLastUpdatedUser = value; }
    public string? LastUpdatedmachine { get => strLastUpdatedmachine; set => strLastUpdatedmachine = value; }
    public string? NumberofStocksinRack { get => strNumberofStocksinRack; set => strNumberofStocksinRack = value; }
}
