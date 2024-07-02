using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem.Migrations
{
    /// <inheritdoc />
    public partial class initial29 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProducrID",
                table: "SHbilldetails",
                newName: "ProductID");

            migrationBuilder.AddColumn<string>(
                name: "Lastupdateddate",
                table: "SHReedemHistory",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lastupdatedmachine",
                table: "SHReedemHistory",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lastupdateduser",
                table: "SHReedemHistory",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lastupdateddate",
                table: "SHPaymentMaster",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lastupdatedmachine",
                table: "SHPaymentMaster",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lastupdateduser",
                table: "SHPaymentMaster",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lastupdateddate",
                table: "SHPaymentDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lastupdatedmachine",
                table: "SHPaymentDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lastupdateduser",
                table: "SHPaymentDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "SHbilldetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lastupdateddate",
                table: "SHReedemHistory");

            migrationBuilder.DropColumn(
                name: "Lastupdatedmachine",
                table: "SHReedemHistory");

            migrationBuilder.DropColumn(
                name: "Lastupdateduser",
                table: "SHReedemHistory");

            migrationBuilder.DropColumn(
                name: "Lastupdateddate",
                table: "SHPaymentMaster");

            migrationBuilder.DropColumn(
                name: "Lastupdatedmachine",
                table: "SHPaymentMaster");

            migrationBuilder.DropColumn(
                name: "Lastupdateduser",
                table: "SHPaymentMaster");

            migrationBuilder.DropColumn(
                name: "Lastupdateddate",
                table: "SHPaymentDetails");

            migrationBuilder.DropColumn(
                name: "Lastupdatedmachine",
                table: "SHPaymentDetails");

            migrationBuilder.DropColumn(
                name: "Lastupdateduser",
                table: "SHPaymentDetails");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "SHbilldetails");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "SHbilldetails",
                newName: "ProducrID");
        }
    }
}
