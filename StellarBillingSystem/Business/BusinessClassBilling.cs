
using DocumentFormat.OpenXml.InkML;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;
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

        public List<CategoryMasterModel> GetCatid(string BranchID)
        {
            var categoryid = (
                    from pr in _billingContext.SHCategoryMaster
                    where pr.BranchID == BranchID
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
        public List<CategoryMasterModel> GetcategoryID(string BranchID)
        {
            var discountcategoryid = (
                from pr in _billingContext.SHCategoryMaster
                where pr.BranchID == BranchID
                select new CategoryMasterModel
                {
                    CategoryID = pr.CategoryID,
                    CategoryName = pr.CategoryName,

                }).ToList();
            return discountcategoryid;
        }



        /*Godown*/

        public List<ProductMatserModel> GetProductid(string BranchID)
        {
            var godownproductid = (
                from pr in _billingContext.SHProductMaster
                where pr.BranchID == BranchID
                select new ProductMatserModel
                {
                    ProductID = pr.ProductID,
                    ProductName = pr.ProductName,
                }).ToList();
            return godownproductid;
        }



        public List<RackpartitionViewModel> GetRackview(string partitionID, string productID, string BranchID)
        {
            var result = (from p in _billingContext.SHRackPartionProduct
                          where p.PartitionID == partitionID && p.ProductID == productID && p.Isdelete == false && p.BranchID == BranchID
                          select new RackpartitionViewModel
                          {
                              PartitionID = p.PartitionID,
                              ProductID = p.ProductID,
                              Noofitems = p.Noofitems

                          }).ToList();
            return result;
        }



        public List<ResourceTypeMasterModel> GetResourceid(string BranchID)
        {
            var resoruseid = (
                    from pr in _billingContext.SHresourceType
                    where pr.BranchID == BranchID
                    select new ResourceTypeMasterModel
                    {
                        ResourceTypeID = pr.ResourceTypeID,
                        ResourceTypeName = pr.ResourceTypeName
                    }
               ).ToList();

            return resoruseid;
        }

        public List<RollTypeMaster> RollAccessType(string BranchID)
        {
            var rollid = (
                    from pr in _billingContext.SHrollType
                    where pr.BranchID == BranchID
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


        public List<StaffAdminModel> GetStaffID(string BranchID)
        {
            var staffid = (
                    from pr in _billingContext.SHStaffAdmin
                    where pr.BranchID == BranchID
                    select new StaffAdminModel
                    {
                        StaffID = pr.StaffID,
                        FullName = pr.FullName
                    }
                ).ToList();

            return staffid;
        }

        public List<ScreenMasterModel> GetScreenid(String BranchID)

        {
            var screenid = (
                        from pr in _billingContext.SHScreenMaster
                        where pr.BranchID == BranchID
                        select new ScreenMasterModel
                        {
                            ScreenId = pr.ScreenId,

                            ScreenName = pr.ScreenName

                        }).ToList();
            return screenid;
        }

        public List<DiscountCategoryMasterModel> Getdiscountid(string BranchID)

        {
            var discountid = (
                        from pr in _billingContext.SHDiscountCategory
                        where pr.BranchID == BranchID
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
                        where pr.IsDelete == false
                        select new BranchMasterModel
                        {
                            BracnchID = pr.BracnchID,

                            BranchName = pr.BranchName

                        }).ToList();
            return branchid;
        }

        public BranchMasterModel Getbranchinitial(string BranchID)

        {
            var branchidini = (
                        from pr in _billingContext.SHBranchMaster
                        where pr.IsDelete == false && pr.BracnchID == BranchID
                        select new BranchMasterModel
                        {
                            BranchInitial = pr.BranchInitial,
                            BranchName = pr.BranchName


                        }).FirstOrDefault();
            return branchidini;
        }

        public StaffAdminModel GetadminRT(string Username)

        {
            var adminRT = (
                        from pr in _billingContext.SHStaffAdmin
                        join p in _billingContext.SHresourceType on pr.ResourceTypeID equals p.ResourceTypeID
                        where pr.IsDelete == false && pr.UserName == Username && p.ResourceTypeName == "Admin"
                        select new StaffAdminModel
                        {
                            UserName = pr.UserName

                        }).FirstOrDefault();
            return adminRT;
        }

        public List<ProductMatserModel> Getproduct(string BranchID)

        {
            var productid = (
                        from product in _billingContext.SHProductMaster
                        join rack in _billingContext.SHGodown
                        on product.ProductID equals rack.ProductID
                        where product.BranchID == BranchID && rack.BranchID == BranchID
                        select new { product, rack })
                  .AsEnumerable()
                  .Where(pr => int.Parse(pr.rack.NumberofStocks) > 0)
                  .Select(pr => pr.product)
                  .ToList();

            return productid;
        }


        public List<String> GetRoll(string userid, string BranchID)
        {
            var query = from sm in _billingContext.SHScreenMaster
                        join rac in _billingContext.SHRoleaccessModel on sm.ScreenId equals rac.ScreenID
                        join ram in _billingContext.SHrollaccess on rac.RollID equals ram.RollID
                        join sam in _billingContext.SHStaffAdmin on ram.StaffID equals sam.StaffID
                        join s in _billingContext.SHStaffAdmin on sam.StaffID equals s.StaffID
                        where rac.Authorized == "1" && sam.UserName == userid && sam.BranchID == BranchID
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


        /*public List<String> Getbranchinitial(string userid, string BranchID)
        {
            var query = from sm in _billingContext.SHStaffAdmin
                        join k in _billingContext.SHBranchMaster on sm.BranchID equals k.BracnchID
                        where sm.UserName == userid && sm.BranchID == BranchID
                        select k.BranchInitial;

            var result = query.ToList();
            return result;
        }*/


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
        public byte[] ModifyBillDoc(string pfilepath, DataTable pbillData)
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
                document.ReplaceText("<<cgst>>", pbillData.Rows[0]["CGSTPercentage"].ToString());
                document.ReplaceText("<<sgst>>", pbillData.Rows[0]["SGSTPercentage"].ToString());
                document.ReplaceText("<<discount>>", pbillData.Rows[0]["TotalDiscount"].ToString());

                //document.ReplaceText("{Placeholder2}", "Dynamic Value 2");

                // Insert a new paragraph
                //  document.InsertParagraph("This is a new paragraph added to the document.").FontSize(14).Bold();

                // Add a table
                var table = document.AddTable(pbillData.Rows.Count + 1, 6);
                table.Rows[0].Cells[0].Paragraphs[0].Append("Product ID").Font("Cambria (Headings)").FontSize(12);
                table.Rows[0].Cells[1].Paragraphs[0].Append("Product Name").Font("Cambria (Headings)").FontSize(12);
                table.Rows[0].Cells[2].Paragraphs[0].Append("Price").Font("Cambria (Headings)").FontSize(12);
                table.Rows[0].Cells[3].Paragraphs[0].Append("Quantity").Font("Cambria (Headings)").FontSize(12);
                table.Rows[0].Cells[4].Paragraphs[0].Append("Total Discount").Font("Cambria (Headings)").FontSize(12);
                table.Rows[0].Cells[5].Paragraphs[0].Append("Net Price").Font("Cambria (Headings)").FontSize(12);

                int rowcount = 1;
                //Row data
                foreach (DataRow objRow in pbillData.Rows)
                {

                    table.Rows[rowcount].Cells[0].Paragraphs[0].Append(objRow["ProductID"].ToString()).Font("Cambria (Headings)").FontSize(12);
                    table.Rows[rowcount].Cells[1].Paragraphs[0].Append(objRow["ProductName"].ToString()).Font("Cambria (Headings)").FontSize(12);
                    table.Rows[rowcount].Cells[2].Paragraphs[0].Append(objRow["Price"].ToString()).Font("Cambria (Headings)").FontSize(12);
                    table.Rows[rowcount].Cells[3].Paragraphs[0].Append(objRow["Quantity"].ToString()).Font("Cambria (Headings)").FontSize(12);
                    table.Rows[rowcount].Cells[4].Paragraphs[0].Append(objRow["DetailDiscount"].ToString()).Font("Cambria (Headings)").FontSize(12);
                    table.Rows[rowcount].Cells[5].Paragraphs[0].Append(objRow["DetailTotalprice"].ToString()).Font("Cambria (Headings)").FontSize(12);
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
        public byte[] PrintBillDetails(DataTable billDetails,string BranchID)
        {
            //var template = _billingContext.SHBranchMaster.FirstOrDefault(x => x.BracnchID == BranchID);

            //var result = _billingContext.Database.SqlQueryRaw<dynamic>(
            //                "select BillTemplate from SHBranchMaster where BracnchID = '"+ BranchID+"'",
            //                1).ToList();

            // Determine the template name based on the BranchID
            string templateName = BranchID == "Lee_Mobile" ? "BillTemplate Branch1.docx" : "BillTemplate Branch2.docx";

            // Combine the template path
            string templatePath = Path.Combine(templateName);

            return ModifyBillDoc(templatePath, billDetails);
        }

        
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


        public async Task<BillingMasterModel> CalculateBillingDetails(string billID, string billDate, string customerNumber, string discount, string cgstPercentage, string sgstPercentage,string BranchID)
        {
            
            var billingDetails = await _billingContext.SHbilldetails
                .Where(x => x.BillID == billID && x.BillDate == billDate && x.CustomerNumber == customerNumber && !x.IsDelete && x.BranchID == BranchID)
                .ToListAsync();

            var billMaster = await _billingContext.SHbillmaster
                .Where(x => x.BillID == billID && x.BillDate == billDate && !x.IsDelete && x.CustomerNumber == customerNumber && x.BranchID == BranchID )
                .FirstOrDefaultAsync();

            if ((billingDetails == null || !billingDetails.Any()) && billMaster ==null)
            {
              
                return null;
            }

            
            // Calculate total price
            decimal totalPrice = billingDetails.Sum(x => decimal.TryParse(x.NetPrice, out decimal price) ? price : 0);

            decimal discountDecimal = 0;
            decimal cgstPercentageDecimal= 0;
            decimal sgstPercentageDecimal = 0;

            if (billMaster != null)
            {

                // Convert percentage and discount from string to decimal onyl if the orginal value has change
                if (discount == null || billMaster.TotalDiscount != discount)
                {
                    discountDecimal = decimal.TryParse(discount, out decimal discountValue) ? discountValue : 0;
                }
                if (cgstPercentage == null || billMaster.CGSTPercentage != cgstPercentage)
                {
                    cgstPercentageDecimal = decimal.TryParse(cgstPercentage, out decimal cgstPercentageValue) ? cgstPercentageValue : 0;
                }

                if (sgstPercentage == null || billMaster.SGSTPercentage != sgstPercentage)
                {
                    sgstPercentageDecimal = decimal.TryParse(sgstPercentage, out decimal sgstPercentageValue) ? sgstPercentageValue : 0;
                }
            }

            // Calculate CGST and SGST amounts
            decimal cgstAmount = (totalPrice * cgstPercentageDecimal) / 100;
            decimal sgstAmount = (totalPrice * sgstPercentageDecimal) / 100;


            var billingmaster = await _billingContext.SHbillmaster
             .Where(x => x.BillID == billID && x.BillDate == billDate && x.CustomerNumber == customerNumber && !x.IsDelete).Select(x => x.NetPrice).FirstOrDefaultAsync();

            decimal billingMasterNetPrice = decimal.TryParse(billingmaster, out decimal NetPrice) ? NetPrice : totalPrice;

            // Calculate total after applying CGST and SGST
            decimal totalWithTaxes = billingMasterNetPrice + cgstAmount + sgstAmount;

            decimal netPrice = totalWithTaxes - discountDecimal;

         

            return new BillingMasterModel
            {
                Totalprice = totalPrice.ToString("F2"),
                CGSTPercentageAmt = cgstAmount.ToString("F2"),
                SGSTPercentageAmt = sgstAmount.ToString("F2"),
                TotalDiscount = discountDecimal.ToString("F2"),
                NetPrice = netPrice.ToString("F2")
            };
        }
    }

   }
