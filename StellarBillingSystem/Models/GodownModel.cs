using System;

public class GodownModel
{
	public GodownModel()
	{
	}

	private String strProductID;
	private String strNumberofStocks;
    private String strDatefofPurchase;
    private String strSupplierInformation;
    private bool strIsDelete;
    private String strLastUpdatedDate;
    private String strLastUpdatedUser;
    private String strLastUpdatedmachine;

    public string ProductID { get => strProductID; set => strProductID = value; }
    public string? NumberofStocks { get => strNumberofStocks; set => strNumberofStocks = value; }
   
    public string? LastUpdatedDate { get => strLastUpdatedDate; set => strLastUpdatedDate = value; }
    public string? LastUpdatedUser { get => strLastUpdatedUser; set => strLastUpdatedUser = value; }
    public string? LastUpdatedmachine { get => strLastUpdatedmachine; set => strLastUpdatedmachine = value; }
    public string DatefofPurchase { get => strDatefofPurchase; set => strDatefofPurchase = value; }
    public string SupplierInformation { get => strSupplierInformation; set => strSupplierInformation = value; }
    public bool IsDelete { get => strIsDelete; set => strIsDelete = value; }

}
