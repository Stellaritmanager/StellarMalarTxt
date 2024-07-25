using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem.Migrations
{
    /// <inheritdoc />
    public partial class initial62 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHPaymentDetails",
                table: "SHPaymentDetails");

            migrationBuilder.AlterColumn<string>(
                name: "BranchID",
                table: "SHPaymentDetails",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHPaymentDetails",
                table: "SHPaymentDetails",
                columns: new[] { "PaymentDiscription", "PaymentId", "BranchID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHPaymentDetails",
                table: "SHPaymentDetails");

            migrationBuilder.AlterColumn<string>(
                name: "BranchID",
                table: "SHPaymentDetails",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHPaymentDetails",
                table: "SHPaymentDetails",
                columns: new[] { "PaymentDiscription", "PaymentId" });
        }
    }
}
