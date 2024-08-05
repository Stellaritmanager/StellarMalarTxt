using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem.Migrations
{
    /// <inheritdoc />
    public partial class initial67 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDashboard",
                table: "SHReportModel");

            migrationBuilder.AddColumn<string>(
                name: "IsDashboard",
                table: "ShGenericReport",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDashboard",
                table: "ShGenericReport");

            migrationBuilder.AddColumn<string>(
                name: "IsDashboard",
                table: "SHReportModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
