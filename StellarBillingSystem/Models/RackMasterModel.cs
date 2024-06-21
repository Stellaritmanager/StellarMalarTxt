using System;

public class RackMasterModel
{
    public RackMasterModel()
    {
    }

    private String strPartitionID;
    private String strRackID;
    private String strFacilityName;
    private String strLastUpdatedDate;
    private String strLastUpdatedUser;
    private String strLastUpdatedmachine;

    public string PartitionID { get => strPartitionID; set => strPartitionID = value; }
    public string RackID { get => strRackID; set => strRackID = value; }
    public string? FacilityName { get => strFacilityName; set => strFacilityName = value; }
    public string? LastUpdatedDate { get => strLastUpdatedDate; set => strLastUpdatedDate = value; }
    public string? LastUpdatedUser { get => strLastUpdatedUser; set => strLastUpdatedUser = value; }
    public string? LastUpdatedmachine { get => strLastUpdatedmachine; set => strLastUpdatedmachine = value; }
}
