using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem_skj.Migrations
{
    /// <inheritdoc />
    public partial class initial8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BuyerModel",
                columns: table => new
                {
                    RepledgeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BuyerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuyerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Interest = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tenure = table.Column<int>(type: "int", nullable: false),
                    BranchID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedMachine = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyerModel", x => x.RepledgeID);
                });

            migrationBuilder.CreateTable(
                name: "RepledgeArtcileModel",
                columns: table => new
                {
                    ArticleID = table.Column<int>(type: "int", nullable: false),
                    BillID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RepledgeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RepledgeArticleID = table.Column<int>(type: "int", nullable: false),
                    BranchID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LastUpdatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedMachine = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepledgeArtcileModel", x => new { x.BillID, x.ArticleID, x.RepledgeID });
                    table.ForeignKey(
                        name: "FK_RepledgeArtcileModel_BuyerModel_RepledgeID",
                        column: x => x.RepledgeID,
                        principalTable: "BuyerModel",
                        principalColumn: "RepledgeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RepledgeArtcileModel_SHArticleMaster_ArticleID",
                        column: x => x.ArticleID,
                        principalTable: "SHArticleMaster",
                        principalColumn: "ArticleID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RepledgeArtcileModel_Shbillmasterskj_BillID_BranchID",
                        columns: x => new { x.BillID, x.BranchID },
                        principalTable: "Shbillmasterskj",
                        principalColumns: new[] { "BillID", "BranchID" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RepledgeArtcileModel_ArticleID",
                table: "RepledgeArtcileModel",
                column: "ArticleID");

            migrationBuilder.CreateIndex(
                name: "IX_RepledgeArtcileModel_BillID_BranchID",
                table: "RepledgeArtcileModel",
                columns: new[] { "BillID", "BranchID" });

            migrationBuilder.CreateIndex(
                name: "IX_RepledgeArtcileModel_RepledgeID",
                table: "RepledgeArtcileModel",
                column: "RepledgeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RepledgeArtcileModel");

            migrationBuilder.DropTable(
                name: "BuyerModel");
        }
    }
}
