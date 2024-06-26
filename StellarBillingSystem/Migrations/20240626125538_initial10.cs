using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem.Migrations
{
    /// <inheritdoc />
    public partial class initial10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHRackPartionProduct",
                table: "SHRackPartionProduct");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "SHRackPartionProduct");

            migrationBuilder.RenameColumn(
                name: "Rack",
                table: "SHRackPartionProduct",
                newName: "Noofitems");

            migrationBuilder.AlterColumn<string>(
                name: "ProductID",
                table: "SHRackPartionProduct",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Isdelete",
                table: "SHRackPartionProduct",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHRackPartionProduct",
                table: "SHRackPartionProduct",
                columns: new[] { "PartitionID", "ProductID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHRackPartionProduct",
                table: "SHRackPartionProduct");

            migrationBuilder.DropColumn(
                name: "Isdelete",
                table: "SHRackPartionProduct");

            migrationBuilder.RenameColumn(
                name: "Noofitems",
                table: "SHRackPartionProduct",
                newName: "Rack");

            migrationBuilder.AlterColumn<string>(
                name: "ProductID",
                table: "SHRackPartionProduct",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "SHRackPartionProduct",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHRackPartionProduct",
                table: "SHRackPartionProduct",
                column: "PartitionID");
        }
    }
}
