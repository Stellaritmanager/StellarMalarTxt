using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem.Migrations
{
    /// <inheritdoc />
    public partial class initial45 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHresourceType",
                table: "SHresourceType");

            migrationBuilder.AlterColumn<string>(
                name: "BranchID",
                table: "SHresourceType",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHresourceType",
                table: "SHresourceType",
                columns: new[] { "ResourceTypeID", "BranchID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHresourceType",
                table: "SHresourceType");

            migrationBuilder.AlterColumn<string>(
                name: "BranchID",
                table: "SHresourceType",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHresourceType",
                table: "SHresourceType",
                column: "ResourceTypeID");
        }
    }
}
