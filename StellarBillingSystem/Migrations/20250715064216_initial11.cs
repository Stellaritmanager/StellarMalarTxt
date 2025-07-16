using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem_skj.Migrations
{
    /// <inheritdoc />
    public partial class initial11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RepledgeArticleID",
                table: "Shrepledgeartcile",
                newName: "RepledgeArticleIDS");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RepledgeArticleIDS",
                table: "Shrepledgeartcile",
                newName: "RepledgeArticleID");
        }
    }
}
