
using Microsoft.EntityFrameworkCore;
using StellarBillingSystem.Models;
namespace StellarBillingSystem.Context
{
    public class BillingContext : DbContext
    {
        public BillingContext() { }

        public BillingContext(DbContextOptions options) : base(options)
        {
        }

        //LogTable
        public DbSet<LogsModel> SBLogs { get; set; }
        public DbSet<ProductMatserModel> SHProductMaster { get; set; }

        public DbSet<CategoryMasterModel> SHCategoryMaster { get; set; }

        public DbSet<BilingSysytemModel> SHCustomerBilling { get; set; }

        public DbSet<CustomerMasterModel> SHCustomerMaster { get; set; }

        public DbSet<DiscountCategoryMasterModel> SHDiscountCategory { get; set; }

        public DbSet<GSTMasterModel> SHGSTMaster { get; set; }

        public DbSet<VoucherCustomerDetailModel> SHVoucherDetails { get; set; }

        public DbSet<GodownModel> SHGodown { get; set; }

        public DbSet<NetDiscountMasterModel> SHNetDiscountMaster { get; set; }

        public DbSet<VoucherMasterModel> SHVoucherMaster { get; set; }

        public DbSet<RackPatrionProductModel> SHRackPartionProduct { get; set; }

        public DbSet<RackMasterModel> SHRackMaster { get; set; }

        public DbSet<PointsReedemDetailsModel> SHPointsReedemDetails { get; set; }

        public DbSet<PointsMasterModel> SHPointsMaster { get; set; }

        public DbSet<ReportModel> SHReportModel { get; set; }

        public DbSet<SignUpModel> SHSignUp { get; set; }

        public DbSet<StaffAdminModel> SHStaffAdmin { get; set; }

        public DbSet<ResourceTypeMasterModel> SHresourceType { get; set; }

        public DbSet<RollAccessMaster> SHrollaccess { get; set; }

        public DbSet<RoleAccessModel> SHRoleaccessModel { get; set; }

        public DbSet<RollTypeMaster> SHrollType { get; set; }

        public DbSet<ScreenMasterModel> SHScreenMaster { get; set; }

        public DbSet<ScreenNameMasterModel> SHScreenName { get; set; }

        public DbSet<GenericReportModel> ShGenericReport { get; set; }

        public DbSet<BillingMasterModel> SHbillmaster { get; set; }

        public DbSet<BillingDetailsModel> SHbilldetails { get; set; }

        public DbSet<PaymentMasterModel> SHPaymentMaster { get; set; }

        public DbSet<PaymentDetailsModel> SHPaymentDetails { get; set; }

        public DbSet<ReedemHistoryModel> SHReedemHistory { get; set; }

        public DbSet<BranchMasterModel> SHBranchMaster { get; set; }

        public DbSet<WebErrorsModel> SHWebErrors { get; set; }

        public DbSet<BillingPointsModel> SHBillingPoints { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=StellarBilling;Integrated Security=True;Trust Server Certificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<WebErrorsModel>()
        .HasKey(i => new { i.ErrodDesc, i.ErrDateTime, i.ScreenName });

            modelBuilder.Entity<BillingPointsModel>().HasKey(i => new { i.BillID, i.CustomerNumber, i.BranchID });
            modelBuilder.Entity<BranchMasterModel>().HasKey(i => new { i.BracnchID, i.BranchName });

            modelBuilder.Entity<BillingMasterModel>().HasKey(i => new { i.BillID, i.BillDate, i.BranchID });
            modelBuilder.Entity<BillingDetailsModel>().HasKey(i => new { i.BillID, i.ProductID, i.BranchID });
            modelBuilder.Entity<GenericReportModel>().HasKey(i => new { i.ReportId, i.BranchID });

            modelBuilder.Entity<StaffAdminModel>().HasKey(i => new { i.StaffID, i.BranchID });
            modelBuilder.Entity<ResourceTypeMasterModel>().HasKey(i => new { i.ResourceTypeID, i.BranchID });
            modelBuilder.Entity<RollAccessMaster>().HasKey(i => new { i.StaffID, i.RollID, i.BranchID });
            modelBuilder.Entity<RoleAccessModel>().HasKey(i => new { i.RollID, i.ScreenID, i.BranchID });
            modelBuilder.Entity<RollTypeMaster>().HasKey(i => new { i.RollID, i.BranchID });
            modelBuilder.Entity<ScreenMasterModel>().HasKey(i => new { i.ScreenId, i.BranchID });
            modelBuilder.Entity<ScreenNameMasterModel>().HasKey(i => new { i.Id });



            modelBuilder.Entity<LogsModel>().HasKey(i => new { i.LogID, i.BranchID });
            modelBuilder.Entity<CategoryMasterModel>().HasKey(i => new { i.CategoryID, i.BranchID });

            modelBuilder.Entity<ProductMatserModel>().HasKey(i => new { i.ProductID, i.BranchID });

            modelBuilder.Entity<CustomerMasterModel>().HasKey(i => new { i.MobileNumber, i.BranchID });

            modelBuilder.Entity<DiscountCategoryMasterModel>().HasKey(i => new { i.CategoryID, i.BranchID });

            modelBuilder.Entity<GSTMasterModel>().HasKey(i => new { i.TaxID, i.BranchID });

            modelBuilder.Entity<VoucherCustomerDetailModel>().HasKey(i => new { i.VoucherID, i.BranchID });

            modelBuilder.Entity<BilingSysytemModel>().HasKey(i => new { i.BillID, i.BranchID });

            modelBuilder.Entity<GodownModel>().HasKey(i => new { i.ProductID, i.BranchID });

            modelBuilder.Entity<NetDiscountMasterModel>().HasKey(i => new { i.NetID, i.BranchID });

            modelBuilder.Entity<VoucherMasterModel>().HasKey(i => new { i.VoucherID, i.BranchID });

            modelBuilder.Entity<RackPatrionProductModel>().HasKey(i => new { i.PartitionID, i.ProductID, i.BranchID });

            modelBuilder.Entity<RackMasterModel>().HasKey(i => new { i.PartitionID, i.RackID, i.BranchID });

            modelBuilder.Entity<PointsReedemDetailsModel>().HasKey(i => new { i.CustomerID, i.BranchID });

            modelBuilder.Entity<PointsMasterModel>().HasKey(i => new { i.PointsID, i.BranchID });

            modelBuilder.Entity<ReportModel>().HasKey(i => new { i.ReportId });

            modelBuilder.Entity<SignUpModel>().HasKey(i => new { i.Username });

            modelBuilder.Entity<PaymentMasterModel>().HasKey(i => new { i.BillId, i.PaymentId, i.BranchID });

            modelBuilder.Entity<PaymentDetailsModel>().HasKey(i => new { i.PaymentDiscription, i.PaymentId, i.BranchID });

            modelBuilder.Entity<ReedemHistoryModel>().HasKey(i => new { i.CustomerNumber, i.DateOfReedem, i.BranchID });


        }
    }
}
