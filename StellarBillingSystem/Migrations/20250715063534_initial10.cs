using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem_skj.Migrations
{
    /// <inheritdoc />
    public partial class initial10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Drop column
            migrationBuilder.DropColumn(
                name: "RepledgeArticleID",
                table: "Shrepledgeartcile");

            // 2. Re-add as identity
            migrationBuilder.AddColumn<int>(
                name: "RepledgeArticleID",
                table: "Shrepledgeartcile",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RepledgeArticleID",
                table: "Shrepledgeartcile");

            migrationBuilder.AddColumn<int>(
                name: "RepledgeArticleID",
                table: "Shrepledgeartcile",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

    }
}
