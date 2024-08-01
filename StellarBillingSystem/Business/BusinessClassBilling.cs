
using Microsoft.CodeAnalysis;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StellarBillingSystem.Context;
using StellarBillingSystem.Models;
using System.Data;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace StellarBillingSystem.Business
{
    public class BusinessClassBilling
    {

        private readonly BillingContext _billingContext;

        public BusinessClassBilling(BillingContext billingContext)
        {
            _billingContext = billingContext;
        }

        /*Product Dropdown*/

        public List<CategoryMasterModel> GetCatid()
        {
            var categoryid = (
                    from pr in _billingContext.SHCategoryMaster
                    select new CategoryMasterModel
                    {
                        CategoryID = pr.CategoryID,
                        CategoryName = pr.CategoryName,
                    }).ToList();
            return categoryid;
        }

        /*Points Reedem Details*/

        public List<CustomerMasterModel> GetCustomerid()
        {
            var pointreedemcustomerid = (
                from pr in _billingContext.SHCustomerMaster
                select new CustomerMasterModel
                {
                    CustomerID = pr.CustomerID,
                    CustomerName = pr.CustomerName,
                }).ToList();
            return pointreedemcustomerid;
        }

        /*Discount Category*/
        public List<CategoryMasterModel> GetcategoryID()
        {
            var discountcategoryid = (
                from pr in _billingContext.SHCategoryMaster
                select new CategoryMasterModel
                {
                    CategoryID = pr.CategoryID,
                    CategoryName = pr.CategoryName,

                }).ToList();
            return discountcategoryid;
        }



        /*Godown*/

        public List<ProductMatserModel> GetProductid()
        {
            var godownproductid = (
                from pr in _billingContext.SHProductMaster
                select new ProductMatserModel
                {
                    ProductID = pr.ProductID,
                    ProductName = pr.ProductName,
                }).ToList();
            return godownproductid;
        }



        public List<RackpartitionViewModel> GetRackview(string partitionID, string productID,string BranchID)
        {
            var result = (from p in _billingContext.SHRackPartionProduct
                          where p.PartitionID == partitionID && p.ProductID == productID && p.Isdelete == false && p.BranchID==BranchID
                          select new RackpartitionViewModel
                          {
                              PartitionID = p.PartitionID,
                              ProductID = p.ProductID,
                               Noofitems= p.Noofitems
        
                          }).ToList();
            return result;
        }



        public List<ResourceTypeMasterModel> GetResourceid()
        {
            var resoruseid = (
                    from pr in _billingContext.SHresourceType
                    select new ResourceTypeMasterModel
                    {
                        ResourceTypeID = pr.ResourceTypeID,
                        ResourceTypeName = pr.ResourceTypeName
                    }
               ).ToList();

            return resoruseid;
        }

        public List<RollTypeMaster> RollAccessType()
        {
            var rollid = (
                    from pr in _billingContext.SHrollType
                    select new RollTypeMaster
                    {
                        RollID = pr.RollID,
                        RollName = pr.RollName
                    }
                ).ToList();

            return rollid;
        }

        public List<ScreenNameMasterModel> Screenname()
        {
            var screenname = (
                    from pr in _billingContext.SHScreenName
                    select new ScreenNameMasterModel
                    {
                        ScreenName = pr.ScreenName,
                    }
                ).ToList();

            return screenname;
        }


        public List<StaffAdminModel> GetStaffID()
        {
            var staffid = (
                    from pr in _billingContext.SHStaffAdmin
                    select new StaffAdminModel
                    {
                        StaffID = pr.StaffID,
                        FullName = pr.FullName
                    }
                ).ToList();

            return staffid;
        }

        public List<ScreenMasterModel> GetScreenid()

        {
            var screenid = (
                        from pr in _billingContext.SHScreenMaster
                        select new ScreenMasterModel
                        {
                            ScreenId = pr.ScreenId,
                            
                            ScreenName = pr.ScreenName

                        }).ToList();
            return screenid;
        }

        public List<DiscountCategoryMasterModel> Getdiscountid()

        {
            var discountid = (
                        from pr in _billingContext.SHDiscountCategory
                        select new DiscountCategoryMasterModel
                        {
                            CategoryID = pr.CategoryID,

                            DiscountPrice = pr.DiscountPrice

                        }).ToList();
            return discountid;
        }

        public List<BranchMasterModel> Getbranch()

        {
            var branchid = (
                        from pr in _billingContext.SHBranchMaster
                        where pr.IsDelete==false
                        select new BranchMasterModel
                        {
                            BracnchID = pr.BracnchID,

                            BranchName = pr.BranchName

                        }).ToList();
            return branchid;
        }



        public List<String> GetRoll(string userid,string BranchID)
        {
            var query = from sm in _billingContext.SHScreenMaster
                        join rac in _billingContext.SHRoleaccessModel on sm.ScreenId equals rac.ScreenID
                        join ram in _billingContext.SHrollaccess on rac.RollID equals ram.RollID
                        join sam in _billingContext.SHStaffAdmin on ram.StaffID equals sam.StaffID
                        join s in _billingContext.SHStaffAdmin on sam.StaffID equals s.StaffID
                        where rac.Authorized == "1" && sam.UserName == userid && sam.BranchID== BranchID
                        select sm.ScreenName;

            var result = query.ToList();
            return result;
        }

        public List<String> Getadmin(string userid)
        {
            var query = from sm in _billingContext.SHScreenMaster
                        join rac in _billingContext.SHRoleaccessModel on sm.ScreenId equals rac.ScreenID
                        join ram in _billingContext.SHrollaccess on rac.RollID equals ram.RollID
                        join sam in _billingContext.SHStaffAdmin on ram.StaffID equals sam.StaffID
                        join s in _billingContext.SHStaffAdmin on sam.StaffID equals s.StaffID
                        where rac.Authorized == "1" && sam.UserName == userid 
                        select sm.ScreenName;

            var result = query.ToList();
            return result;
        }


        public List<GenericReportModel> GetReportId()
        {
            var reportid = (
                    from pr in _billingContext.ShGenericReport
                    select new GenericReportModel
                    {
                        ReportName = pr.ReportName,
                    }
                ).ToList();

            return reportid;
        }

        public string GeneratePaymentDescription(string paymentId)
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            return $"{paymentId}_{timestamp}";
        }

        //Procedure to print Bill details
        public byte[] ModifyBillDoc(string pfilepath,DataTable pbillData)
        {
            // Path to your existing Word document
            string filePath = pfilepath;

            // Open the document
            using (var document = DocX.Load(filePath))
            {
                // Replace placeholders with dynamic data
                document.ReplaceText("<<custname>>", pbillData.Rows[0]["CustomerName"].ToString());
                document.ReplaceText("<<custnum>>", pbillData.Rows[0]["CustomerNumber"].ToString());
                document.ReplaceText("<<billdate>>", pbillData.Rows[0]["BillDate"].ToString());
                document.ReplaceText("<<billno>>", pbillData.Rows[0]["BillID"].ToString());
                document.ReplaceText("<<totalamount>>", pbillData.Rows[0]["MasterTotalprice"].ToString());

                //document.ReplaceText("{Placeholder2}", "Dynamic Value 2");

                // Insert a new paragraph
                //  document.InsertParagraph("This is a new paragraph added to the document.").FontSize(14).Bold();

                // Add a table
                var table = document.AddTable(pbillData.Rows.Count+1, 6);
                table.Rows[0].Cells[0].Paragraphs[0].Append("Product ID");
                table.Rows[0].Cells[1].Paragraphs[0].Append("Product Name");
                table.Rows[0].Cells[2].Paragraphs[0].Append("Price");
                table.Rows[0].Cells[3].Paragraphs[0].Append("Quantity");
                table.Rows[0].Cells[4].Paragraphs[0].Append("Total Discount");
                table.Rows[0].Cells[5].Paragraphs[0].Append("Net Price");

                int rowcount = 1;
                //Row data
                foreach (DataRow objRow in pbillData.Rows)
                {

                    table.Rows[rowcount].Cells[0].Paragraphs[0].Append(objRow["ProductID"].ToString());
                    table.Rows[rowcount].Cells[1].Paragraphs[0].Append(objRow["ProductName"].ToString());
                    table.Rows[rowcount].Cells[2].Paragraphs[0].Append(objRow["Price"].ToString());
                    table.Rows[rowcount].Cells[3].Paragraphs[0].Append(objRow["Quantity"].ToString());
                    table.Rows[rowcount].Cells[4].Paragraphs[0].Append(objRow["TotalDiscount"].ToString());
                    table.Rows[rowcount].Cells[5].Paragraphs[0].Append(objRow["DetailTotalprice"].ToString());
                    rowcount++;
                }

                string searchText = "<<billnodet>>";
                Paragraph paragraph = document.Paragraphs.FirstOrDefault(p => p.Text.Contains(searchText));

                if (paragraph != null)
                {
                    paragraph.InsertTableAfterSelf(table);
                }

                document.ReplaceText("<<billnodet>>", String.Empty);

                // Save the document to a MemoryStream
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    document.SaveAs(memoryStream);

                    // Convert the MemoryStream to a byte array
                     return memoryStream.ToArray();
                }

            }
           return null;
        }
        public byte[] PrintBillDetails(DataTable billDetails)
        {
            return ModifyBillDoc("..\\StellarBillingSystem\\Templates\\BillTemplate.docx",billDetails);
        }

        /*        public async Task<bool> UpdateProduct(string productId, bool isDelete)
                {
                    var product = await _billingContext.SHProductMaster.FirstOrDefaultAsync(x => x.ProductID == productId);

                    if (product == null)
                    {
                        return false; // Product not found
                    }

                    product.IsDelete = isDelete;

                    await _billingContext.SaveChangesAsync();

                    return true; // Update successful
                }*/

        /*        public async Task<ProductMatserModel> GetProductmaster(string productID)
                {
                    var product = await (
                            from pp in _billingContext.SHProductMaster
                            where pp.ProductID == productID
                            select new ProductMatserModel
                            {
                                CategoryID = pp.CategoryID,
                                ProductID = pp.ProductID,
                                ProductName = pp.ProductName,
                                Brandname = pp.Brandname,
                                Discount = pp.Discount,
                                TotalAmount = pp.TotalAmount,
                                Price = pp.Price

                            }).FirstOrDefaultAsync();

                    return product;

                }*/



        /*        public async Task AddPointsToCustomer(string customerId, int pointsToAdd)
                {
                    var customer = await _billingContext.SHCustomerMaster.FindAsync(customerId);

                    if (customer == null)
                    {
                        throw new ArgumentException("Customer not found");
                    }

                    customer.PointsReedem += pointsToAdd;
                    await _billingContext.SaveChangesAsync();
                }*/



        public string GeneratePaymentDescriptionreport(string paymentId)
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            return $"{paymentId}_{timestamp}";
        }

        //Procedure to print Bill details
        public byte[] ModifypaymentDoc(string pfilepath, DataTable pbillData)
        {
            // Path to your existing Word document
            string filePath = pfilepath;

            // Open the document
            using (var document = DocX.Load(filePath))
            {
                // Replace placeholders with dynamic data
                document.ReplaceText("<<custname>>", pbillData.Rows[0]["CustomerName"].ToString());
                document.ReplaceText("<<custnum>>", pbillData.Rows[0]["CustomerNumber"].ToString());
                document.ReplaceText("<<billdate>>", pbillData.Rows[0]["BillDate"].ToString());
                document.ReplaceText("<<billno>>", pbillData.Rows[0]["BillID"].ToString());
                document.ReplaceText("<<paymentno>>", pbillData.Rows[0]["PaymentId"].ToString());

                //document.ReplaceText("{Placeholder2}", "Dynamic Value 2");

                // Insert a new paragraph
                //  document.InsertParagraph("This is a new paragraph added to the document.").FontSize(14).Bold();

                // Add a table
                var table = document.AddTable(pbillData.Rows.Count + 1, 5);
                table.Rows[0].Cells[0].Paragraphs[0].Append("Payment Description");
                table.Rows[0].Cells[1].Paragraphs[0].Append("Payment Mode");
                table.Rows[0].Cells[2].Paragraphs[0].Append("Payment Transaction Number");
                table.Rows[0].Cells[3].Paragraphs[0].Append("Payment Amount");
                table.Rows[0].Cells[4].Paragraphs[0].Append("Payment Date");
               

                int rowcount = 1;
                //Row data
                foreach (DataRow objRow in pbillData.Rows)
                {

                    table.Rows[rowcount].Cells[0].Paragraphs[0].Append(objRow["PaymentDiscription"].ToString());
                    table.Rows[rowcount].Cells[1].Paragraphs[0].Append(objRow["PaymentMode"].ToString());
                    table.Rows[rowcount].Cells[2].Paragraphs[0].Append(objRow["PaymentTransactionNumber"].ToString());
                    table.Rows[rowcount].Cells[3].Paragraphs[0].Append(objRow["PaymentAmount"].ToString());
                    table.Rows[rowcount].Cells[4].Paragraphs[0].Append(objRow["PaymentDate"].ToString());
                    rowcount++;
                }

                string searchText = "<<paymentnode>>";
                Paragraph paragraph = document.Paragraphs.FirstOrDefault(p => p.Text.Contains(searchText));

                if (paragraph != null)
                {
                    paragraph.InsertTableAfterSelf(table);
                }

                document.ReplaceText("<<paymentnode>>", String.Empty);

                // Save the document to a MemoryStream
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    document.SaveAs(memoryStream);

                    // Convert the MemoryStream to a byte array
                    return memoryStream.ToArray();
                }

            }
            return null;
        }
        public byte[] PrintpaymentDetails(DataTable billDetails)
        {
            return ModifypaymentDoc("..\\StellarBillingSystem\\Templates\\Payment Template.docx", billDetails);
        }




        public async Task<int> GetBalanceForBillAsync(string billId)
        {
            // Define the output parameter
            var outputParameter = new SqlParameter("@Balance", SqlDbType.Float)
            {
                Direction = ParameterDirection.Output
            };

            // Execute the SQL command with output parameter
            var sql = $"EXEC @Balance = dbo.GenerateBillID @BillId";
            await _billingContext.Database.ExecuteSqlRawAsync(sql, new SqlParameter("@BillId", billId), outputParameter);

            
            if (outputParameter.Value != DBNull.Value)
            {
                return Convert.ToInt32(outputParameter.Value);
            }
            else
            {
                return 0; 
            }
     
        }




    }
    
}
