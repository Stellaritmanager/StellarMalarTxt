using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem.Migrations
{
    /// <inheritdoc />
    public partial class initial64 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SGSTPerentageAmt1",
                table: "SHbillmaster",
                newName: "SGSTPercentageAmt");

            migrationBuilder.RenameColumn(
                name: "SGSTPercentage1",
                table: "SHbillmaster",
                newName: "SGSTPercentage");

            migrationBuilder.RenameColumn(
                name: "CGSTPercentageAmt1",
                table: "SHbillmaster",
                newName: "CGSTPercentageAmt");

            migrationBuilder.RenameColumn(
                name: "CGSTPercentage1",
                table: "SHbillmaster",
                newName: "CGSTPercentage");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SGSTPercentageAmt",
                table: "SHbillmaster",
                newName: "SGSTPerentageAmt1");

            migrationBuilder.RenameColumn(
                name: "SGSTPercentage",
                table: "SHbillmaster",
                newName: "SGSTPercentage1");

            migrationBuilder.RenameColumn(
                name: "CGSTPercentageAmt",
                table: "SHbillmaster",
                newName: "CGSTPercentageAmt1");

            migrationBuilder.RenameColumn(
                name: "CGSTPercentage",
                table: "SHbillmaster",
                newName: "CGSTPercentage1");
        }
    }
}
