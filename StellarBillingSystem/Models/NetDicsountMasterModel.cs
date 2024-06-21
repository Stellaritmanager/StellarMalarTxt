using System;

public class NetDicsountMasterModel
{
	public NetDicsountMasterModel()
	{
	}

	private String strNetDiscountID;
	private String strNetDiscount;
    private String strLastUpdatedDate;
    private String strLastUpdatedUser;
    private String strLastUpdatedmachine;

    public string NetDiscountID { get => strNetDiscountID; set => strNetDiscountID = value; }
    public string? NetDiscount { get => strNetDiscount; set => strNetDiscount = value; }
    public string? LastUpdatedDate { get => strLastUpdatedDate; set => strLastUpdatedDate = value; }
    public string? LastUpdatedUser { get => strLastUpdatedUser; set => strLastUpdatedUser = value; }
    public string? LastUpdatedmachine { get => strLastUpdatedmachine; set => strLastUpdatedmachine = value; }
}
