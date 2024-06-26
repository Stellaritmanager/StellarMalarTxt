using System;

public class DiscountCategoryMasterModel
{
	public DiscountCategoryMasterModel()
	{
	}

    private String strCategoryID;
    private String strDiscountPrice;
    private String strLastUpdatedDate;
    private String strLastUpdatedUser;
    private String strLastUpdatedmachine;

    public string CategoryID { get => strCategoryID; set => strCategoryID = value; }
    public string? DiscountPrice { get => strDiscountPrice; set => strDiscountPrice = value; }
    public string? LastUpdatedDate { get => strLastUpdatedDate; set => strLastUpdatedDate = value; }
    public string? LastUpdatedUser { get => strLastUpdatedUser; set => strLastUpdatedUser = value; }
    public string? LastUpdatedmachine { get => strLastUpdatedmachine; set => strLastUpdatedmachine = value; }
}
