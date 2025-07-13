using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem_skj.Migrations
{
    /// <inheritdoc />
    public partial class initial4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BillIDCombinationModel",
                table: "BillIDCombinationModel");

            migrationBuilder.RenameTable(
                name: "BillIDCombinationModel",
                newName: "Shbillcombinationskj");

            migrationBuilder.AlterColumn<double>(
                name: "WeightOfArticle",
                table: "SHArticleMaster",
                type: "float",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shbillcombinationskj",
                table: "Shbillcombinationskj",
                column: "BranchID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Shbillcombinationskj",
                table: "Shbillcombinationskj");

            migrationBuilder.RenameTable(
                name: "Shbillcombinationskj",
                newName: "BillIDCombinationModel");

            migrationBuilder.AlterColumn<string>(
                name: "WeightOfArticle",
                table: "SHArticleMaster",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BillIDCombinationModel",
                table: "BillIDCombinationModel",
                column: "BranchID");
        }
    }
}
