using System;


    public class ProductMatserModel
    {

        public ProductMatserModel() { }


        private String strProductID;
        private String strCategoryID;
        private String strProductName;
        private String strBrandname;
        private String strPrice;
        private String strDiscount;
        private String strTotalAmount;
        private String strBarcodeId;
        private bool strIsDelete;
        private String strLastUpdatedDate;
        private String strLastUpdatedUser;
        private String strLastUpdatedmachine;

        public string ProductID { get => strProductID; set => strProductID = value; }
        public string? ProductName { get => strProductName; set => strProductName = value; }
        public string? Brandname { get => strBrandname; set => strBrandname = value; }
        public string? Price { get => strPrice; set => strPrice = value; }
        public string? Discount { get => strDiscount; set => strDiscount = value; }
        public string? TotalAmount { get => strTotalAmount; set => strTotalAmount = value; }
        public string? LastUpdatedDate { get => strLastUpdatedDate; set => strLastUpdatedDate = value; }
        public string? LastUpdatedUser { get => strLastUpdatedUser; set => strLastUpdatedUser = value; }
        public string? LastUpdatedmachine { get => strLastUpdatedmachine; set => strLastUpdatedmachine = value; }
        public string? CategoryID { get => strCategoryID; set => strCategoryID = value; }
        public bool IsDelete { get => strIsDelete; set => strIsDelete = value; }
        public string? BarcodeId { get => strBarcodeId; set => strBarcodeId = value; }
}

