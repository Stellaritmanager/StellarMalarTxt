using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem_skj.Migrations
{
    /// <inheritdoc />
    public partial class initial9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RepledgeArtcileModel_BuyerModel_RepledgeID",
                table: "RepledgeArtcileModel");

            migrationBuilder.DropForeignKey(
                name: "FK_RepledgeArtcileModel_SHArticleMaster_ArticleID",
                table: "RepledgeArtcileModel");

            migrationBuilder.DropForeignKey(
                name: "FK_RepledgeArtcileModel_Shbillmasterskj_BillID_BranchID",
                table: "RepledgeArtcileModel");

            migrationBuilder.DropTable(
                name: "BuyerModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RepledgeArtcileModel",
                table: "RepledgeArtcileModel");

            migrationBuilder.RenameTable(
                name: "RepledgeArtcileModel",
                newName: "Shrepledgeartcile");

            migrationBuilder.RenameIndex(
                name: "IX_RepledgeArtcileModel_RepledgeID",
                table: "Shrepledgeartcile",
                newName: "IX_Shrepledgeartcile_RepledgeID");

            migrationBuilder.RenameIndex(
                name: "IX_RepledgeArtcileModel_BillID_BranchID",
                table: "Shrepledgeartcile",
                newName: "IX_Shrepledgeartcile_BillID_BranchID");

            migrationBuilder.RenameIndex(
                name: "IX_RepledgeArtcileModel_ArticleID",
                table: "Shrepledgeartcile",
                newName: "IX_Shrepledgeartcile_ArticleID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shrepledgeartcile",
                table: "Shrepledgeartcile",
                columns: new[] { "BillID", "ArticleID", "RepledgeID" });

            migrationBuilder.CreateTable(
                name: "Shbuyerrepledge",
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
                    table.PrimaryKey("PK_Shbuyerrepledge", x => x.RepledgeID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Shrepledgeartcile_SHArticleMaster_ArticleID",
                table: "Shrepledgeartcile",
                column: "ArticleID",
                principalTable: "SHArticleMaster",
                principalColumn: "ArticleID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Shrepledgeartcile_Shbillmasterskj_BillID_BranchID",
                table: "Shrepledgeartcile",
                columns: new[] { "BillID", "BranchID" },
                principalTable: "Shbillmasterskj",
                principalColumns: new[] { "BillID", "BranchID" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shrepledgeartcile_Shbuyerrepledge_RepledgeID",
                table: "Shrepledgeartcile",
                column: "RepledgeID",
                principalTable: "Shbuyerrepledge",
                principalColumn: "RepledgeID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shrepledgeartcile_SHArticleMaster_ArticleID",
                table: "Shrepledgeartcile");

            migrationBuilder.DropForeignKey(
                name: "FK_Shrepledgeartcile_Shbillmasterskj_BillID_BranchID",
                table: "Shrepledgeartcile");

            migrationBuilder.DropForeignKey(
                name: "FK_Shrepledgeartcile_Shbuyerrepledge_RepledgeID",
                table: "Shrepledgeartcile");

            migrationBuilder.DropTable(
                name: "Shbuyerrepledge");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shrepledgeartcile",
                table: "Shrepledgeartcile");

            migrationBuilder.RenameTable(
                name: "Shrepledgeartcile",
                newName: "RepledgeArtcileModel");

            migrationBuilder.RenameIndex(
                name: "IX_Shrepledgeartcile_RepledgeID",
                table: "RepledgeArtcileModel",
                newName: "IX_RepledgeArtcileModel_RepledgeID");

            migrationBuilder.RenameIndex(
                name: "IX_Shrepledgeartcile_BillID_BranchID",
                table: "RepledgeArtcileModel",
                newName: "IX_RepledgeArtcileModel_BillID_BranchID");

            migrationBuilder.RenameIndex(
                name: "IX_Shrepledgeartcile_ArticleID",
                table: "RepledgeArtcileModel",
                newName: "IX_RepledgeArtcileModel_ArticleID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RepledgeArtcileModel",
                table: "RepledgeArtcileModel",
                columns: new[] { "BillID", "ArticleID", "RepledgeID" });

            migrationBuilder.CreateTable(
                name: "BuyerModel",
                columns: table => new
                {
                    RepledgeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuyerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Interest = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdatedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedMachine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tenure = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyerModel", x => x.RepledgeID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_RepledgeArtcileModel_BuyerModel_RepledgeID",
                table: "RepledgeArtcileModel",
                column: "RepledgeID",
                principalTable: "BuyerModel",
                principalColumn: "RepledgeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RepledgeArtcileModel_SHArticleMaster_ArticleID",
                table: "RepledgeArtcileModel",
                column: "ArticleID",
                principalTable: "SHArticleMaster",
                principalColumn: "ArticleID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RepledgeArtcileModel_Shbillmasterskj_BillID_BranchID",
                table: "RepledgeArtcileModel",
                columns: new[] { "BillID", "BranchID" },
                principalTable: "Shbillmasterskj",
                principalColumns: new[] { "BillID", "BranchID" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
