using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem.Migrations
{
    /// <inheritdoc />
    public partial class initial27 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromDate",
                table: "SHReportModel");

            migrationBuilder.DropColumn(
                name: "ToDate",
                table: "SHReportModel");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedUser",
                table: "SHReportModel",
                newName: "LastUpdateduser");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedDate",
                table: "SHReportModel",
                newName: "Lastupdateddate");

            migrationBuilder.RenameColumn(
                name: "ReportID",
                table: "SHReportModel",
                newName: "ReportId");

            migrationBuilder.AlterColumn<string>(
                name: "LastUpdatedmachine",
                table: "SHReportModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastUpdateduser",
                table: "SHReportModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Lastupdateddate",
                table: "SHReportModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            //migrationBuilder.AlterColumn<int>(
            //    name: "ReportId",
            //    table: "SHReportModel",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(450)")
            //    .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "ReportDescription",
                table: "SHReportModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReportName",
                table: "SHReportModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReportQuery",
                table: "SHReportModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReportType",
                table: "SHReportModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ShGenericReport",
                columns: table => new
                {
                    ReportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReportType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReportDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReportQuery = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShGenericReport", x => x.ReportId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShGenericReport");

            migrationBuilder.DropColumn(
                name: "ReportDescription",
                table: "SHReportModel");

            migrationBuilder.DropColumn(
                name: "ReportName",
                table: "SHReportModel");

            migrationBuilder.DropColumn(
                name: "ReportQuery",
                table: "SHReportModel");

            migrationBuilder.DropColumn(
                name: "ReportType",
                table: "SHReportModel");

            migrationBuilder.RenameColumn(
                name: "Lastupdateddate",
                table: "SHReportModel",
                newName: "LastUpdatedDate");

            migrationBuilder.RenameColumn(
                name: "LastUpdateduser",
                table: "SHReportModel",
                newName: "LastUpdatedUser");

            migrationBuilder.RenameColumn(
                name: "ReportId",
                table: "SHReportModel",
                newName: "ReportID");

            migrationBuilder.AlterColumn<string>(
                name: "LastUpdatedDate",
                table: "SHReportModel",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "LastUpdatedUser",
                table: "SHReportModel",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "LastUpdatedmachine",
                table: "SHReportModel",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ReportID",
                table: "SHReportModel",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "FromDate",
                table: "SHReportModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ToDate",
                table: "SHReportModel",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
