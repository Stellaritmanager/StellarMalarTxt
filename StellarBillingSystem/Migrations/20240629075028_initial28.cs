using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem.Migrations
{
    /// <inheritdoc />
    public partial class initial28 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BarcodeId",
                table: "SHProductMaster",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SHbilldetails",
                columns: table => new
                {
                    BillID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProducrID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NetPrice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Totalprice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalDiscount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    Lastupdateduser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lastupdateddate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lastupdatedmachine = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHbilldetails", x => new { x.BillID, x.ProducrID });
                });

            migrationBuilder.CreateTable(
                name: "SHbillmaster",
                columns: table => new
                {
                    BillID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BillDate = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CustomerNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Totalprice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalDiscount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NetPrice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    Lastupdateduser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lastupdateddate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lastupdatedmachine = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHbillmaster", x => new { x.BillID, x.BillDate });
                });

            migrationBuilder.CreateTable(
                name: "SHPaymentDetails",
                columns: table => new
                {
                    PaymentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PaymentDiscription = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PaymentMode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentTransactionNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHPaymentDetails", x => new { x.PaymentDiscription, x.PaymentId });
                });

            migrationBuilder.CreateTable(
                name: "SHPaymentMaster",
                columns: table => new
                {
                    BillId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PaymentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CustomerNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReedemPoints = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Balance = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHPaymentMaster", x => new { x.BillId, x.PaymentId });
                });

            migrationBuilder.CreateTable(
                name: "SHReedemHistory",
                columns: table => new
                {
                    CustomerNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateOfReedem = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReedemPoints = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHReedemHistory", x => new { x.CustomerNumber, x.DateOfReedem });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SHbilldetails");

            migrationBuilder.DropTable(
                name: "SHbillmaster");

            migrationBuilder.DropTable(
                name: "SHPaymentDetails");

            migrationBuilder.DropTable(
                name: "SHPaymentMaster");

            migrationBuilder.DropTable(
                name: "SHReedemHistory");

            migrationBuilder.DropColumn(
                name: "BarcodeId",
                table: "SHProductMaster");
        }
    }
}
