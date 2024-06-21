using System;

    public class CategoryMasterModel
    {
        public CategoryMasterModel() { }

        private String strCategoryID;
        private String strCategoryName;
        private String strLastUpdatedDate;
        private String strLastUpdatedUser;
        private String strLastUpdatedmachine;

        public string CategoryID { get => strCategoryID; set => strCategoryID = value; }
        public string? CategoryName { get => strCategoryName; set => strCategoryName = value; }
        public string? LastUpdatedDate { get => strLastUpdatedDate; set => strLastUpdatedDate = value; }
        public string? LastUpdatedUser { get => strLastUpdatedUser; set => strLastUpdatedUser = value; }
        public string? LastUpdatedmachine { get => strLastUpdatedmachine; set => strLastUpdatedmachine = value; }
    }


