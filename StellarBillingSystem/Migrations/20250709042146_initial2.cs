using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem_skj.Migrations
{
    /// <inheritdoc />
    public partial class initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerImageModel_SHCustomerMaster_MobileNumber_CustomerName_BranchID",
                table: "CustomerImageModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SHCustomerMaster",
                table: "SHCustomerMaster");

            migrationBuilder.RenameTable(
                name: "SHCustomerMaster",
                newName: "CustomerMasterModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerMasterModel",
                table: "CustomerMasterModel",
                columns: new[] { "MobileNumber", "CustomerName", "BranchID" });

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerImageModel_CustomerMasterModel_MobileNumber_CustomerName_BranchID",
                table: "CustomerImageModel",
                columns: new[] { "MobileNumber", "CustomerName", "BranchID" },
                principalTable: "CustomerMasterModel",
                principalColumns: new[] { "MobileNumber", "CustomerName", "BranchID" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerImageModel_CustomerMasterModel_MobileNumber_CustomerName_BranchID",
                table: "CustomerImageModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerMasterModel",
                table: "CustomerMasterModel");

            migrationBuilder.RenameTable(
                name: "CustomerMasterModel",
                newName: "SHCustomerMaster");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHCustomerMaster",
                table: "SHCustomerMaster",
                columns: new[] { "MobileNumber", "CustomerName", "BranchID" });

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerImageModel_SHCustomerMaster_MobileNumber_CustomerName_BranchID",
                table: "CustomerImageModel",
                columns: new[] { "MobileNumber", "CustomerName", "BranchID" },
                principalTable: "SHCustomerMaster",
                principalColumns: new[] { "MobileNumber", "CustomerName", "BranchID" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
