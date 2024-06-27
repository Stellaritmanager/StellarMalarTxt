using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem.Migrations
{
    /// <inheritdoc />
    public partial class initial15 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SHresourceType",
                columns: table => new
                {
                    ResourceTypeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ResourceTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lastUpdatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lastUpdatedUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lastUpdatedMachine = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHresourceType", x => x.ResourceTypeID);
                });

            migrationBuilder.CreateTable(
                name: "SHRoleaccessModel",
                columns: table => new
                {
                    RollID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ScreenID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Access = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Authorized = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastUpdatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lastUpdatedUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lastUpdatedMachine = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHRoleaccessModel", x => new { x.RollID, x.ScreenID });
                });

            migrationBuilder.CreateTable(
                name: "SHrollaccess",
                columns: table => new
                {
                    StaffID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RollID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LastupdatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastupdatedMachine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lastupdateduser = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHrollaccess", x => new { x.StaffID, x.RollID });
                });

            migrationBuilder.CreateTable(
                name: "SHrollType",
                columns: table => new
                {
                    RollID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RollName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastupdatedUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastupdatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastupdatedMachine = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHrollType", x => x.RollID);
                });

            migrationBuilder.CreateTable(
                name: "SHScreenMaster",
                columns: table => new
                {
                    ScreenId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ScreenName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lastUpdatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lastUpdatedUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lastUpdatedMachine = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHScreenMaster", x => x.ScreenId);
                });

            migrationBuilder.CreateTable(
                name: "SHScreenName",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ScreenName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHScreenName", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SHStaffAdmin",
                columns: table => new
                {
                    StaffID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ResourceTypeID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Initial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prefix = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateofBirth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdProofId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdProofName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastupdatedUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastupdatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedMachine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHStaffAdmin", x => x.StaffID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SHresourceType");

            migrationBuilder.DropTable(
                name: "SHRoleaccessModel");

            migrationBuilder.DropTable(
                name: "SHrollaccess");

            migrationBuilder.DropTable(
                name: "SHrollType");

            migrationBuilder.DropTable(
                name: "SHScreenMaster");

            migrationBuilder.DropTable(
                name: "SHScreenName");

            migrationBuilder.DropTable(
                name: "SHStaffAdmin");
        }
    }
}
