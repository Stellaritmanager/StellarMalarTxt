using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem.Migrations
{
    /// <inheritdoc />
    public partial class Intial87 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHbillmaster",
                table: "SHbillmaster");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SHbilldetails",
                table: "SHbilldetails");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerNumber",
                table: "SHbillmaster",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "BranchID",
                table: "SHbillmaster",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "BillID",
                table: "SHbillmaster",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "SHbillmaster",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "101, 1");

            migrationBuilder.AlterColumn<string>(
                name: "BranchID",
                table: "SHbilldetails",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProductID",
                table: "SHbilldetails",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "BillID",
                table: "SHbilldetails",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "SHbilldetails",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "101, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHbillmaster",
                table: "SHbillmaster",
                columns: new[] { "Id", "BillDate", "BranchID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHbilldetails",
                table: "SHbilldetails",
                columns: new[] { "Id", "ProductID", "BranchID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SHbillmaster",
                table: "SHbillmaster");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SHbilldetails",
                table: "SHbilldetails");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SHbillmaster");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SHbilldetails");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerNumber",
                table: "SHbillmaster",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "BillID",
                table: "SHbillmaster",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "BranchID",
                table: "SHbillmaster",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "BillID",
                table: "SHbilldetails",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "BranchID",
                table: "SHbilldetails",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "ProductID",
                table: "SHbilldetails",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHbillmaster",
                table: "SHbillmaster",
                columns: new[] { "BillID", "BillDate", "BranchID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_SHbilldetails",
                table: "SHbilldetails",
                columns: new[] { "BillID", "ProductID", "BranchID" });
        }
    }
}
