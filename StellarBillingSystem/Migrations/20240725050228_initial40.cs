using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem.Migrations
{
    /// <inheritdoc />
    public partial class initial40 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHStaffAdmin",
                table: "SHStaffAdmin");

            migrationBuilder.AddColumn<string>(
                name: "BranchID",
                table: "SHStaffAdmin",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CGSTPercentage1",
                table: "SHbillmaster",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CGSTPercentageAmt1",
                table: "SHbillmaster",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SGSTPercentage1",
                table: "SHbillmaster",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SGSTPerentageAmt1",
                table: "SHbillmaster",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHStaffAdmin",
                table: "SHStaffAdmin",
                columns: new[] { "StaffID", "BranchID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHStaffAdmin",
                table: "SHStaffAdmin");

            migrationBuilder.DropColumn(
                name: "BranchID",
                table: "SHStaffAdmin");

            migrationBuilder.DropColumn(
                name: "CGSTPercentage1",
                table: "SHbillmaster");

            migrationBuilder.DropColumn(
                name: "CGSTPercentageAmt1",
                table: "SHbillmaster");

            migrationBuilder.DropColumn(
                name: "SGSTPercentage1",
                table: "SHbillmaster");

            migrationBuilder.DropColumn(
                name: "SGSTPerentageAmt1",
                table: "SHbillmaster");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHStaffAdmin",
                table: "SHStaffAdmin",
                column: "StaffID");
        }
    }
}
