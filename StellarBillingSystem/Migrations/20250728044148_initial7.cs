using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem_Malar.Migrations
{
    /// <inheritdoc />
    public partial class initial7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHbillmaster",
                table: "SHbillmaster");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SHbilldetails",
                table: "SHbilldetails");

            migrationBuilder.AlterColumn<string>(
                name: "BillDate",
                table: "SHbillmaster",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "BranchID",
                table: "SHbilldetails",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<string>(
                name: "Barcode",
                table: "SHbilldetails",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHbillmaster",
                table: "SHbillmaster",
                columns: new[] { "Id", "BranchID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHbilldetails",
                table: "SHbilldetails",
                columns: new[] { "Id", "Barcode", "BranchID" });

            migrationBuilder.CreateIndex(
                name: "IX_SHbilldetails_Barcode_BranchID",
                table: "SHbilldetails",
                columns: new[] { "Barcode", "BranchID" });

            migrationBuilder.AddForeignKey(
                name: "FK_SHbilldetails_MTProductMaster_Barcode_BranchID",
                table: "SHbilldetails",
                columns: new[] { "Barcode", "BranchID" },
                principalTable: "MTProductMaster",
                principalColumns: new[] { "Barcode", "BranchID" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SHbilldetails_MTProductMaster_Barcode_BranchID",
                table: "SHbilldetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SHbillmaster",
                table: "SHbillmaster");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SHbilldetails",
                table: "SHbilldetails");

            migrationBuilder.DropIndex(
                name: "IX_SHbilldetails_Barcode_BranchID",
                table: "SHbilldetails");

            migrationBuilder.DropColumn(
                name: "Barcode",
                table: "SHbilldetails");

            migrationBuilder.AlterColumn<string>(
                name: "BillDate",
                table: "SHbillmaster",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "BranchID",
                table: "SHbilldetails",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHbillmaster",
                table: "SHbillmaster",
                columns: new[] { "Id", "BillDate", "BranchID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHbilldetails",
                table: "SHbilldetails",
                columns: new[] { "Id", "ProductID", "BranchID" });
        }
    }
}
