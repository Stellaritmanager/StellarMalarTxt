
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

        public DbSet<RollAccessMaster> SHrollaccess {  get; set; }
       
        public DbSet<RoleAccessModel> SHRoleaccessModel { get; set; }

        public DbSet<RollTypeMaster> SHrollType {  get; set; }

        public DbSet<ScreenMasterModel> SHScreenMaster { get; set; }

        public DbSet<ScreenNameMasterModel> SHScreenName { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=StellarBilling;Integrated Security=True;Trust Server Certificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<StaffAdminModel>().HasKey(i => new { i.StaffID });
            modelBuilder.Entity<ResourceTypeMasterModel>().HasKey(i => new { i.ResourceTypeID });
            modelBuilder.Entity<RollAccessMaster>().HasKey(i => new { i.StaffID, i.RollID });
            modelBuilder.Entity<RoleAccessModel>().HasKey(i => new { i.RollID,i.ScreenID});
            modelBuilder.Entity<RollTypeMaster>().HasKey(i => new { i.RollID });
            modelBuilder.Entity<ScreenMasterModel>().HasKey(i => new { i.ScreenId });
            modelBuilder.Entity<ScreenNameMasterModel>().HasKey(i => new { i.Id });
            


            modelBuilder.Entity<LogsModel>().HasKey(i => new { i.LogID });
            modelBuilder.Entity<CategoryMasterModel>().HasKey(i => new { i.CategoryID });

            modelBuilder.Entity<ProductMatserModel>().HasKey(i => new { i.ProductID });

            modelBuilder.Entity<CustomerMasterModel>().HasKey(i => new { i.MobileNumber });

            modelBuilder.Entity<DiscountCategoryMasterModel>().HasKey(i => new { i.CategoryID });

            modelBuilder.Entity<GSTMasterModel>().HasKey(i => new { i.TaxID });

            modelBuilder.Entity<VoucherCustomerDetailModel>().HasKey(i => new { i.VoucherID });

            modelBuilder.Entity<BilingSysytemModel>().HasKey(i => new { i.BillID });

            modelBuilder.Entity<GodownModel>().HasKey(i => new { i.ProductID,i.DatefofPurchase,i.SupplierInformation});

            modelBuilder.Entity<NetDiscountMasterModel>().HasKey(i => new { i.NetDiscount });

            modelBuilder.Entity<VoucherMasterModel>().HasKey(i => new { i.VoucherID });

            modelBuilder.Entity<RackPatrionProductModel>().HasKey(i => new { i.PartitionID,i.ProductID });

            modelBuilder.Entity<RackMasterModel>().HasKey(i => new { i.PartitionID, i.RackID });

            modelBuilder.Entity<PointsReedemDetailsModel>().HasKey(i => new { i.CustomerID });

            modelBuilder.Entity<PointsMasterModel>().HasKey(i => new { i.PointsID });

            modelBuilder.Entity<ReportModel>().HasKey(i => new {i.ReportID});

            modelBuilder.Entity<SignUpModel>().HasKey(i => new { i.Username });


        }
    }
}
