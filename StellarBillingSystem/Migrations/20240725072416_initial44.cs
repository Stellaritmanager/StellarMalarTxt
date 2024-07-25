using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem.Migrations
{
    /// <inheritdoc />
    public partial class initial44 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ShGenericReport",
                table: "ShGenericReport");

            migrationBuilder.AlterColumn<string>(
                name: "BranchID",
                table: "ShGenericReport",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShGenericReport",
                table: "ShGenericReport",
                columns: new[] { "ReportId", "BranchID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ShGenericReport",
                table: "ShGenericReport");

            migrationBuilder.AlterColumn<string>(
                name: "BranchID",
                table: "ShGenericReport",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShGenericReport",
                table: "ShGenericReport",
                column: "ReportId");
        }
    }
}
