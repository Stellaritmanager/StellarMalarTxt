using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem.Migrations
{
    /// <inheritdoc />
    public partial class initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SHCategoryMaster",
                columns: table => new
                {
                    CategoryID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedmachine = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHCategoryMaster", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "SHCustomerBilling",
                columns: table => new
                {
                    BillID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Items = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiscountPrice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PointsNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VoucherNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryBasedDiscount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedmachine = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHCustomerBilling", x => x.BillID);
                });

            migrationBuilder.CreateTable(
                name: "SHCustomerMaster",
                columns: table => new
                {
                    CustomerID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateofBirth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PointsReedem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VoucherDiscount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VoucherNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedmachine = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHCustomerMaster", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "SHDiscountCategory",
                columns: table => new
                {
                    DiscountPrice = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CategoryID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastUpdatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedmachine = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHDiscountCategory", x => x.DiscountPrice);
                });

            migrationBuilder.CreateTable(
                name: "SHGodown",
                columns: table => new
                {
                    ProductID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NumberofStocks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastUpdatedDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastUpdatedUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastUpdatedmachine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberofStocksinRack = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHGodown", x => x.ProductID);
                });

            migrationBuilder.CreateTable(
                name: "SHGSTMaster",
                columns: table => new
                {
                    TaxID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SGST = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CGST = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherTax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedmachine = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHGSTMaster", x => x.TaxID);
                });

            migrationBuilder.CreateTable(
                name: "SHNetDiscountMaster",
                columns: table => new
                {
                    NetDiscountID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NetDiscount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedmachine = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHNetDiscountMaster", x => x.NetDiscountID);
                });

            migrationBuilder.CreateTable(
                name: "SHPointsMaster",
                columns: table => new
                {
                    PointsID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NetPrice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedmachine = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHPointsMaster", x => x.PointsID);
                });

            migrationBuilder.CreateTable(
                name: "SHPointsReedemDetails",
                columns: table => new
                {
                    CustomerID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TotalPoints = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpiryDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedmachine = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHPointsReedemDetails", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "SHProductMaster",
                columns: table => new
                {
                    ProductID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Brandname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedmachine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHProductMaster", x => x.ProductID);
                });

            migrationBuilder.CreateTable(
                name: "SHRackMaster",
                columns: table => new
                {
                    PartitionID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RackID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FacilityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedmachine = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHRackMaster", x => new { x.PartitionID, x.RackID });
                });

            migrationBuilder.CreateTable(
                name: "SHRackPartionProduct",
                columns: table => new
                {
                    PartitionID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rack = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedmachine = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHRackPartionProduct", x => x.PartitionID);
                });

            migrationBuilder.CreateTable(
                name: "SHVoucherDetails",
                columns: table => new
                {
                    VoucherID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CustomerID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpiryDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedmachine = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHVoucherDetails", x => x.VoucherID);
                });

            migrationBuilder.CreateTable(
                name: "SHVoucherMaster",
                columns: table => new
                {
                    VoucherID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VoucherNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VocherCost = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VocherDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpiryDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedmachine = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHVoucherMaster", x => x.VoucherID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SHCategoryMaster");

            migrationBuilder.DropTable(
                name: "SHCustomerBilling");

            migrationBuilder.DropTable(
                name: "SHCustomerMaster");

            migrationBuilder.DropTable(
                name: "SHDiscountCategory");

            migrationBuilder.DropTable(
                name: "SHGodown");

            migrationBuilder.DropTable(
                name: "SHGSTMaster");

            migrationBuilder.DropTable(
                name: "SHNetDiscountMaster");

            migrationBuilder.DropTable(
                name: "SHPointsMaster");

            migrationBuilder.DropTable(
                name: "SHPointsReedemDetails");

            migrationBuilder.DropTable(
                name: "SHProductMaster");

            migrationBuilder.DropTable(
                name: "SHRackMaster");

            migrationBuilder.DropTable(
                name: "SHRackPartionProduct");

            migrationBuilder.DropTable(
                name: "SHVoucherDetails");

            migrationBuilder.DropTable(
                name: "SHVoucherMaster");
        }
    }
}
