using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem.Migrations
{
    /// <inheritdoc />
    public partial class initial60 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHPointsMaster",
                table: "SHPointsMaster");

            migrationBuilder.AlterColumn<string>(
                name: "BranchID",
                table: "SHPointsMaster",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHPointsMaster",
                table: "SHPointsMaster",
                columns: new[] { "PointsID", "BranchID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHPointsMaster",
                table: "SHPointsMaster");

            migrationBuilder.AlterColumn<string>(
                name: "BranchID",
                table: "SHPointsMaster",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHPointsMaster",
                table: "SHPointsMaster",
                column: "PointsID");
        }
    }
}
