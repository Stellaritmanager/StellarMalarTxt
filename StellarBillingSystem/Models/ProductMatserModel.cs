using DocumentFormat.OpenXml.Presentation;
using System;


    public class ProductMatserModel
    {

        public ProductMatserModel() { }


        private String strProductID;
        private String strCategoryID;
        private String strProductName;
        private String strBrandname;
        private String strPrice;
    private String strDiscountCategory;
    private String strTotalAmount;
        private String strBarcodeId;
        private bool strIsDelete;
    private string sGST;
    private string cGST;
    private string otherTax;

    private String strLastUpdatedDate;
        private String strLastUpdatedUser;
        private String strLastUpdatedmachine;

        public string ProductID { get => strProductID; set => strProductID = value; }
        public string? ProductName { get => strProductName; set => strProductName = value; }
        public string? Brandname { get => strBrandname; set => strBrandname = value; }
        public string? Price { get => strPrice; set => strPrice = value; }

        public string? TotalAmount { get => strTotalAmount; set => strTotalAmount = value; }
        public string? LastUpdatedDate { get => strLastUpdatedDate; set => strLastUpdatedDate = value; }
        public string? LastUpdatedUser { get => strLastUpdatedUser; set => strLastUpdatedUser = value; }
        public string? LastUpdatedmachine { get => strLastUpdatedmachine; set => strLastUpdatedmachine = value; }
        public string? CategoryID { get => strCategoryID; set => strCategoryID = value; }
        public bool IsDelete { get => strIsDelete; set => strIsDelete = value; }
        public string? BarcodeId { get => strBarcodeId; set => strBarcodeId = value; }
    public string? SGST { get => sGST; set => sGST = value; }
    public string? CGST { get => cGST; set => cGST = value; }
    public string? OtherTax { get => otherTax; set => otherTax = value; }
    public string? DiscountCategory { get => strDiscountCategory; set => strDiscountCategory = value; }
}

