using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem.Migrations
{
    /// <inheritdoc />
    public partial class Intial65 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerNumber",
                table: "SHPaymentMaster");

            migrationBuilder.DropColumn(
                name: "ReedemPoints",
                table: "SHPaymentMaster");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerNumber",
                table: "SHPaymentMaster",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReedemPoints",
                table: "SHPaymentMaster",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
