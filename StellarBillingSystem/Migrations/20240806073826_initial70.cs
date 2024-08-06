using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem.Migrations
{
    /// <inheritdoc />
    public partial class initial70 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SHWebErrors",
                columns: table => new
                {
                    ErrodDesc = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ScreenName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ErrDateTime = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MachineName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHWebErrors", x => new { x.ErrodDesc, x.ErrDateTime, x.ScreenName });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SHWebErrors");
        }
    }
}
