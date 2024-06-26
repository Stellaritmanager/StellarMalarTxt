using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem.Migrations
{
    /// <inheritdoc />
    public partial class initial3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SHReportModel",
                columns: table => new
                {
                    ReportID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FromDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedmachine = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHReportModel", x => x.ReportID);
                });

            migrationBuilder.CreateTable(
                name: "SHSignUp",
                columns: table => new
                {
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHSignUp", x => x.Username);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SHReportModel");

            migrationBuilder.DropTable(
                name: "SHSignUp");
        }
    }
}
