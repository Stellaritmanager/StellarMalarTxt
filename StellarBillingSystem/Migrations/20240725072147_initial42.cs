using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem.Migrations
{
    /// <inheritdoc />
    public partial class initial42 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHbillmaster",
                table: "SHbillmaster");

            migrationBuilder.AddColumn<string>(
                name: "BranchID",
                table: "SHVoucherMaster",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BranchID",
                table: "SHVoucherDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BranchID",
                table: "SHScreenMaster",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BranchID",
                table: "SHrollType",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BranchID",
                table: "SHrollaccess",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BranchID",
                table: "SHRoleaccessModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BranchID",
                table: "SHresourceType",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BranchID",
                table: "SHReportModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BranchID",
                table: "SHReedemHistory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BranchID",
                table: "SHRackPartionProduct",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BranchID",
                table: "SHRackMaster",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BranchID",
                table: "SHProductMaster",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BranchID",
                table: "SHPointsReedemDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BranchID",
                table: "SHPointsMaster",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BranchID",
                table: "SHPaymentMaster",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BranchID",
                table: "SHPaymentDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BranchID",
                table: "SHNetDiscountMaster",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BranchID",
                table: "SHGSTMaster",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BranchID",
                table: "SHGodown",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BranchID",
                table: "ShGenericReport",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BranchID",
                table: "SHDiscountCategory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BranchID",
                table: "SHCustomerMaster",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BranchID",
                table: "SHCustomerBilling",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BranchID",
                table: "SHbillmaster",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BranchID",
                table: "SHbilldetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BranchID",
                table: "SBLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHbillmaster",
                table: "SHbillmaster",
                columns: new[] { "BillID", "BillDate", "BranchID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHbillmaster",
                table: "SHbillmaster");

            migrationBuilder.DropColumn(
                name: "BranchID",
                table: "SHVoucherMaster");

            migrationBuilder.DropColumn(
                name: "BranchID",
                table: "SHVoucherDetails");

            migrationBuilder.DropColumn(
                name: "BranchID",
                table: "SHScreenMaster");

            migrationBuilder.DropColumn(
                name: "BranchID",
                table: "SHrollType");

            migrationBuilder.DropColumn(
                name: "BranchID",
                table: "SHrollaccess");

            migrationBuilder.DropColumn(
                name: "BranchID",
                table: "SHRoleaccessModel");

            migrationBuilder.DropColumn(
                name: "BranchID",
                table: "SHresourceType");

            migrationBuilder.DropColumn(
                name: "BranchID",
                table: "SHReportModel");

            migrationBuilder.DropColumn(
                name: "BranchID",
                table: "SHReedemHistory");

            migrationBuilder.DropColumn(
                name: "BranchID",
                table: "SHRackPartionProduct");

            migrationBuilder.DropColumn(
                name: "BranchID",
                table: "SHRackMaster");

            migrationBuilder.DropColumn(
                name: "BranchID",
                table: "SHProductMaster");

            migrationBuilder.DropColumn(
                name: "BranchID",
                table: "SHPointsReedemDetails");

            migrationBuilder.DropColumn(
                name: "BranchID",
                table: "SHPointsMaster");

            migrationBuilder.DropColumn(
                name: "BranchID",
                table: "SHPaymentMaster");

            migrationBuilder.DropColumn(
                name: "BranchID",
                table: "SHPaymentDetails");

            migrationBuilder.DropColumn(
                name: "BranchID",
                table: "SHNetDiscountMaster");

            migrationBuilder.DropColumn(
                name: "BranchID",
                table: "SHGSTMaster");

            migrationBuilder.DropColumn(
                name: "BranchID",
                table: "SHGodown");

            migrationBuilder.DropColumn(
                name: "BranchID",
                table: "ShGenericReport");

            migrationBuilder.DropColumn(
                name: "BranchID",
                table: "SHDiscountCategory");

            migrationBuilder.DropColumn(
                name: "BranchID",
                table: "SHCustomerMaster");

            migrationBuilder.DropColumn(
                name: "BranchID",
                table: "SHCustomerBilling");

            migrationBuilder.DropColumn(
                name: "BranchID",
                table: "SHbillmaster");

            migrationBuilder.DropColumn(
                name: "BranchID",
                table: "SHbilldetails");

            migrationBuilder.DropColumn(
                name: "BranchID",
                table: "SBLogs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHbillmaster",
                table: "SHbillmaster",
                columns: new[] { "BillID", "BillDate" });
        }
    }
}
