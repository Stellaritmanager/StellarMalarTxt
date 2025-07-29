using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem_Malar.Migrations
{
    /// <inheritdoc />
    public partial class initial9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CloseBy",
                table: "SHbillmaster",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CloseDate",
                table: "SHbillmaster",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CloseBy",
                table: "SHbillmaster");

            migrationBuilder.DropColumn(
                name: "CloseDate",
                table: "SHbillmaster");
        }
    }
}
