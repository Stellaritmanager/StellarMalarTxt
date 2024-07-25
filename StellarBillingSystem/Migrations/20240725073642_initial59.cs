using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem.Migrations
{
    /// <inheritdoc />
    public partial class initial59 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHRackMaster",
                table: "SHRackMaster");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SHPointsReedemDetails",
                table: "SHPointsReedemDetails");

            migrationBuilder.AlterColumn<string>(
                name: "BranchID",
                table: "SHRackMaster",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "BranchID",
                table: "SHPointsReedemDetails",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHRackMaster",
                table: "SHRackMaster",
                columns: new[] { "PartitionID", "RackID", "BranchID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHPointsReedemDetails",
                table: "SHPointsReedemDetails",
                columns: new[] { "CustomerID", "BranchID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHRackMaster",
                table: "SHRackMaster");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SHPointsReedemDetails",
                table: "SHPointsReedemDetails");

            migrationBuilder.AlterColumn<string>(
                name: "BranchID",
                table: "SHRackMaster",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "BranchID",
                table: "SHPointsReedemDetails",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHRackMaster",
                table: "SHRackMaster",
                columns: new[] { "PartitionID", "RackID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHPointsReedemDetails",
                table: "SHPointsReedemDetails",
                column: "CustomerID");
        }
    }
}
