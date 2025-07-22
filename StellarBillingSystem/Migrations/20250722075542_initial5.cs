using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem_Malar.Migrations
{
    /// <inheritdoc />
    public partial class initial5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MTSizeMaster_MTCategoryMaster_CategoryName_BranchID",
                table: "MTSizeMaster");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MTSizeMaster",
                table: "MTSizeMaster");

            migrationBuilder.DropIndex(
                name: "IX_MTSizeMaster_CategoryName_BranchID",
                table: "MTSizeMaster");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MTCategoryMaster",
                table: "MTCategoryMaster");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MTBrandMaster",
                table: "MTBrandMaster");

            migrationBuilder.DropColumn(
                name: "CategoryName",
                table: "MTSizeMaster");

            migrationBuilder.DropColumn(
                name: "BranchID",
                table: "MTSizeMaster");

            migrationBuilder.DropColumn(
                name: "BranchID",
                table: "MTCategoryMaster");

            migrationBuilder.DropColumn(
                name: "BranchID",
                table: "MTBrandMaster");

            migrationBuilder.RenameColumn(
                name: "SizeID",
                table: "MTProductMaster",
                newName: "SizeName");

            migrationBuilder.RenameIndex(
                name: "IX_MTProductMaster_CategoryID_BrandID_ProductName_SizeID_Barcode_BranchID_ProductCode",
                table: "MTProductMaster",
                newName: "IX_MTProductMaster_CategoryID_BrandID_ProductName_SizeName_Barcode_BranchID_ProductCode");

            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "MTSizeMaster",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryID",
                table: "MTProductMaster",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "BrandID",
                table: "MTProductMaster",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryName",
                table: "MTCategoryMaster",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "BrandName",
                table: "MTBrandMaster",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MTSizeMaster",
                table: "MTSizeMaster",
                columns: new[] { "CategoryID", "SizeName" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MTCategoryMaster",
                table: "MTCategoryMaster",
                column: "CategoryID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MTBrandMaster",
                table: "MTBrandMaster",
                column: "BrandID");

            migrationBuilder.CreateIndex(
                name: "IX_MTProductMaster_BrandID",
                table: "MTProductMaster",
                column: "BrandID");

            migrationBuilder.CreateIndex(
                name: "IX_MTProductMaster_CategoryID_SizeName",
                table: "MTProductMaster",
                columns: new[] { "CategoryID", "SizeName" });

            migrationBuilder.AddForeignKey(
                name: "FK_MTProductMaster_MTBrandMaster_BrandID",
                table: "MTProductMaster",
                column: "BrandID",
                principalTable: "MTBrandMaster",
                principalColumn: "BrandID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MTProductMaster_MTSizeMaster_CategoryID_SizeName",
                table: "MTProductMaster",
                columns: new[] { "CategoryID", "SizeName" },
                principalTable: "MTSizeMaster",
                principalColumns: new[] { "CategoryID", "SizeName" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MTSizeMaster_MTCategoryMaster_CategoryID",
                table: "MTSizeMaster",
                column: "CategoryID",
                principalTable: "MTCategoryMaster",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MTProductMaster_MTBrandMaster_BrandID",
                table: "MTProductMaster");

            migrationBuilder.DropForeignKey(
                name: "FK_MTProductMaster_MTSizeMaster_CategoryID_SizeName",
                table: "MTProductMaster");

            migrationBuilder.DropForeignKey(
                name: "FK_MTSizeMaster_MTCategoryMaster_CategoryID",
                table: "MTSizeMaster");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MTSizeMaster",
                table: "MTSizeMaster");

            migrationBuilder.DropIndex(
                name: "IX_MTProductMaster_BrandID",
                table: "MTProductMaster");

            migrationBuilder.DropIndex(
                name: "IX_MTProductMaster_CategoryID_SizeName",
                table: "MTProductMaster");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MTCategoryMaster",
                table: "MTCategoryMaster");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MTBrandMaster",
                table: "MTBrandMaster");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "MTSizeMaster");

            migrationBuilder.RenameColumn(
                name: "SizeName",
                table: "MTProductMaster",
                newName: "SizeID");

            migrationBuilder.RenameIndex(
                name: "IX_MTProductMaster_CategoryID_BrandID_ProductName_SizeName_Barcode_BranchID_ProductCode",
                table: "MTProductMaster",
                newName: "IX_MTProductMaster_CategoryID_BrandID_ProductName_SizeID_Barcode_BranchID_ProductCode");

            migrationBuilder.AddColumn<string>(
                name: "CategoryName",
                table: "MTSizeMaster",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BranchID",
                table: "MTSizeMaster",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryID",
                table: "MTProductMaster",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "BrandID",
                table: "MTProductMaster",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryName",
                table: "MTCategoryMaster",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "BranchID",
                table: "MTCategoryMaster",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "BrandName",
                table: "MTBrandMaster",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "BranchID",
                table: "MTBrandMaster",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MTSizeMaster",
                table: "MTSizeMaster",
                columns: new[] { "CategoryName", "SizeName", "BranchID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MTCategoryMaster",
                table: "MTCategoryMaster",
                columns: new[] { "CategoryName", "BranchID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MTBrandMaster",
                table: "MTBrandMaster",
                columns: new[] { "BrandName", "BranchID" });

            migrationBuilder.CreateIndex(
                name: "IX_MTSizeMaster_CategoryName_BranchID",
                table: "MTSizeMaster",
                columns: new[] { "CategoryName", "BranchID" });

            migrationBuilder.AddForeignKey(
                name: "FK_MTSizeMaster_MTCategoryMaster_CategoryName_BranchID",
                table: "MTSizeMaster",
                columns: new[] { "CategoryName", "BranchID" },
                principalTable: "MTCategoryMaster",
                principalColumns: new[] { "CategoryName", "BranchID" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
