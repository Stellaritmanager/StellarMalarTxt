using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem_skj.Migrations
{
    /// <inheritdoc />
    public partial class initial3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerImageModel_CustomerMasterModel_MobileNumber_CustomerName_BranchID",
                table: "CustomerImageModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerMasterModel",
                table: "CustomerMasterModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerImageModel",
                table: "CustomerImageModel");

            migrationBuilder.RenameTable(
                name: "CustomerMasterModel",
                newName: "SHCustomerMaster");

            migrationBuilder.RenameTable(
                name: "CustomerImageModel",
                newName: "ShcustomerImageMaster");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerImageModel_MobileNumber_CustomerName_BranchID",
                table: "ShcustomerImageMaster",
                newName: "IX_ShcustomerImageMaster_MobileNumber_CustomerName_BranchID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHCustomerMaster",
                table: "SHCustomerMaster",
                columns: new[] { "MobileNumber", "CustomerName", "BranchID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShcustomerImageMaster",
                table: "ShcustomerImageMaster",
                column: "ImageID");

            migrationBuilder.AddForeignKey(
                name: "FK_ShcustomerImageMaster_SHCustomerMaster_MobileNumber_CustomerName_BranchID",
                table: "ShcustomerImageMaster",
                columns: new[] { "MobileNumber", "CustomerName", "BranchID" },
                principalTable: "SHCustomerMaster",
                principalColumns: new[] { "MobileNumber", "CustomerName", "BranchID" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShcustomerImageMaster_SHCustomerMaster_MobileNumber_CustomerName_BranchID",
                table: "ShcustomerImageMaster");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SHCustomerMaster",
                table: "SHCustomerMaster");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShcustomerImageMaster",
                table: "ShcustomerImageMaster");

            migrationBuilder.RenameTable(
                name: "SHCustomerMaster",
                newName: "CustomerMasterModel");

            migrationBuilder.RenameTable(
                name: "ShcustomerImageMaster",
                newName: "CustomerImageModel");

            migrationBuilder.RenameIndex(
                name: "IX_ShcustomerImageMaster_MobileNumber_CustomerName_BranchID",
                table: "CustomerImageModel",
                newName: "IX_CustomerImageModel_MobileNumber_CustomerName_BranchID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerMasterModel",
                table: "CustomerMasterModel",
                columns: new[] { "MobileNumber", "CustomerName", "BranchID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerImageModel",
                table: "CustomerImageModel",
                column: "ImageID");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerImageModel_CustomerMasterModel_MobileNumber_CustomerName_BranchID",
                table: "CustomerImageModel",
                columns: new[] { "MobileNumber", "CustomerName", "BranchID" },
                principalTable: "CustomerMasterModel",
                principalColumns: new[] { "MobileNumber", "CustomerName", "BranchID" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
