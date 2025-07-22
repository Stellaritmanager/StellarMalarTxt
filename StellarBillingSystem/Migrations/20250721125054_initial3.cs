using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem_Malar.Migrations
{
    public partial class initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Add IsDelete column to ProductInwardModelMT
            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "ProductInwardModelMT",
                type: "bit",
                nullable: false,
                defaultValue: false);

            // Change IsDelete in MTCategoryMaster from string to bool
            migrationBuilder.AlterColumn<bool>(
                name: "IsDelete",
                table: "MTCategoryMaster",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            // ⚠ Drop CategoryID (string)
            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "MTCategoryMaster");

            // ✅ Add CategoryID (int, identity)
            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "MTCategoryMaster",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop CategoryID (int)
            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "MTCategoryMaster");

            // Add back CategoryID (string)
            migrationBuilder.AddColumn<string>(
                name: "CategoryID",
                table: "MTCategoryMaster",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            // Revert IsDelete in MTCategoryMaster
            migrationBuilder.AlterColumn<string>(
                name: "IsDelete",
                table: "MTCategoryMaster",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            // Drop IsDelete from ProductInwardModelMT
            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "ProductInwardModelMT");
        }
    }
}
