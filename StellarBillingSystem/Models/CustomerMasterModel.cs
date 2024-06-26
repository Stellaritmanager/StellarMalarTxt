using System;

public class CustomerMasterModel
{
	public CustomerMasterModel()
	{
	}

	private String strCustomerID;
	private String strCustomerName;
	private String strDateofBirth;
	private String strGender;
	private String strAddress;
	private String strCity;
	private String strMobileNumber;
	private String strPointsReedem;
	private String strVoucherDiscount;
	private String strVoucherNumber;
    private bool strIsDelete;
    private String strLastUpdatedDate;
    private String strLastUpdatedUser;
    private String strLastUpdatedmachine;

    public string CustomerID { get => strCustomerID; set => strCustomerID = value; }
    public string? CustomerName { get => strCustomerName; set => strCustomerName = value; }
    public string? DateofBirth { get => strDateofBirth; set => strDateofBirth = value; }
    public string? Gender { get => strGender; set => strGender = value; }
    public string? Address { get => strAddress; set => strAddress = value; }
    public string? City { get => strCity; set => strCity = value; }
    public string? MobileNumber { get => strMobileNumber; set => strMobileNumber = value; }
    public string? PointsReedem { get => strPointsReedem; set => strPointsReedem = value; }
    public string? VoucherDiscount { get => strVoucherDiscount; set => strVoucherDiscount = value; }
    public string? VoucherNumber { get => strVoucherNumber; set => strVoucherNumber = value; }
    public string? LastUpdatedDate { get => strLastUpdatedDate; set => strLastUpdatedDate = value; }
    public string? LastUpdatedUser { get => strLastUpdatedUser; set => strLastUpdatedUser = value; }
    public string? LastUpdatedmachine { get => strLastUpdatedmachine; set => strLastUpdatedmachine = value; }
    public bool IsDelete { get => strIsDelete; set => strIsDelete = value; }
}
