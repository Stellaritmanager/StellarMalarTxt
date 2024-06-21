using System;


    public class RackPatrionProductModel
    {
        public RackPatrionProductModel() { }

    private String strPartitionID;
    private String strProductID;
    private String strProductName;
    private String strRack;
    private String strLastUpdatedDate;
    private String strLastUpdatedUser;
    private String strLastUpdatedmachine;

    public string PartitionID { get => strPartitionID; set => strPartitionID = value; }
    public string? ProductID { get => strProductID; set => strProductID = value; }
    public string? ProductName { get => strProductName; set => strProductName = value; }
    public string? Rack { get => strRack; set => strRack = value; }
    public string? LastUpdatedDate { get => strLastUpdatedDate; set => strLastUpdatedDate = value; }
    public string? LastUpdatedUser { get => strLastUpdatedUser; set => strLastUpdatedUser = value; }
    public string? LastUpdatedmachine { get => strLastUpdatedmachine; set => strLastUpdatedmachine = value; }
}

