using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem.Migrations
{
    /// <inheritdoc />
    public partial class initial61 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHPaymentMaster",
                table: "SHPaymentMaster");

            migrationBuilder.AlterColumn<string>(
                name: "BranchID",
                table: "SHPaymentMaster",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHPaymentMaster",
                table: "SHPaymentMaster",
                columns: new[] { "BillId", "PaymentId", "BranchID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHPaymentMaster",
                table: "SHPaymentMaster");

            migrationBuilder.AlterColumn<string>(
                name: "BranchID",
                table: "SHPaymentMaster",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHPaymentMaster",
                table: "SHPaymentMaster",
                columns: new[] { "BillId", "PaymentId" });
        }
    }
}
