using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem_Malar.Migrations
{
    /// <inheritdoc />
    public partial class initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MTBrandMaster",
                columns: table => new
                {
                    BrandName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BranchID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BrandID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    Lastupdateduser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Lastupdateddate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Lastupdatedmachine = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MTBrandMaster", x => new { x.BrandName, x.BranchID });
                });

            migrationBuilder.CreateTable(
                name: "MTCategoryMaster",
                columns: table => new
                {
                    CategoryName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BranchID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CategoryID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lastupdateduser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Lastupdateddate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Lastupdatedmachine = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SizeID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MTCategoryMaster", x => new { x.CategoryName, x.BranchID });
                });

            migrationBuilder.CreateTable(
                name: "MTProductMaster",
                columns: table => new
                {
                    Barcode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BranchID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CategoryID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BrandID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SizeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NoofItem = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    Lastupdateduser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Lastupdateddate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Lastupdatedmachine = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MTProductMaster", x => new { x.Barcode, x.BranchID });
                });

            migrationBuilder.CreateTable(
                name: "ProductInwardModelMT",
                columns: table => new
                {
                    InvoiceNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BranchID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SupplierName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoofItem = table.Column<long>(type: "bigint", nullable: false),
                    SupplierPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tax = table.Column<double>(type: "float", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Lastupdateduser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Lastupdateddate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Lastupdatedmachine = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInwardModelMT", x => new { x.InvoiceNumber, x.ProductCode, x.BranchID });
                });

            migrationBuilder.CreateTable(
                name: "MTSizeMaster",
                columns: table => new
                {
                    SizeName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BranchID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SizeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lastupdateduser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Lastupdateddate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Lastupdatedmachine = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MTSizeMaster", x => new { x.CategoryName, x.SizeName, x.BranchID });
                    table.ForeignKey(
                        name: "FK_MTSizeMaster_MTCategoryMaster_CategoryName_BranchID",
                        columns: x => new { x.CategoryName, x.BranchID },
                        principalTable: "MTCategoryMaster",
                        principalColumns: new[] { "CategoryName", "BranchID" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MTProductMaster_CategoryID_BrandID_ProductName_SizeID_Barcode_BranchID_ProductCode",
                table: "MTProductMaster",
                columns: new[] { "CategoryID", "BrandID", "ProductName", "SizeID", "Barcode", "BranchID", "ProductCode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MTSizeMaster_CategoryName_BranchID",
                table: "MTSizeMaster",
                columns: new[] { "CategoryName", "BranchID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MTBrandMaster");

            migrationBuilder.DropTable(
                name: "MTProductMaster");

            migrationBuilder.DropTable(
                name: "MTSizeMaster");

            migrationBuilder.DropTable(
                name: "ProductInwardModelMT");

            migrationBuilder.DropTable(
                name: "MTCategoryMaster");
        }
    }
}
