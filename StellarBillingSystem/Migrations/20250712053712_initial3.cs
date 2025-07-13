using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem_skj.Migrations
{
    /// <inheritdoc />
    public partial class initial3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BillIDCombinationModel",
                columns: table => new
                {
                    BranchID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CombinationValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IncrementValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lastupdateduser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Lastupdateddate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Lastupdatedmachine = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillIDCombinationModel", x => x.BranchID);
                });

            migrationBuilder.CreateTable(
                name: "SHArticleMaster",
                columns: table => new
                {
                    ArticleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    WeightOfArticle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GoldType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastUpdatedUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastUpdatedmachine = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastUpdatedDate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BranchID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHArticleMaster", x => x.ArticleID);
                });

            migrationBuilder.CreateTable(
                name: "Shbillimagemodelskj",
                columns: table => new
                {
                    ImageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArticleID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lastupdateduser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Lastupdateddate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Lastupdatedmachine = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shbillimagemodelskj", x => x.ImageID);
                });

            migrationBuilder.CreateTable(
                name: "Shbillmasterskj",
                columns: table => new
                {
                    BillID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BranchID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BillDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OverallWeight = table.Column<double>(type: "float", nullable: false),
                    TotalValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LoanValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InitialInterest = table.Column<double>(type: "float", nullable: false),
                    TotalRepayValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NoOfItem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostTenureInterest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tenure = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClosedDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    closedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    Lastupdateduser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Lastupdateddate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Lastupdatedmachine = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shbillmasterskj", x => new { x.BillID, x.BranchID });
                });

            migrationBuilder.CreateTable(
                name: "Shbilldetailsskj",
                columns: table => new
                {
                    BillID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BranchID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ArticleID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    Lastupdateduser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Lastupdateddate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Lastupdatedmachine = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Grossweight = table.Column<double>(type: "float", nullable: false),
                    Netweight = table.Column<double>(type: "float", nullable: false),
                    Reducedweight = table.Column<double>(type: "float", nullable: false),
                    Netmarketprice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Apprisevaluepergram = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Apprisenetvalue = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shbilldetailsskj", x => new { x.ArticleID, x.BranchID, x.BillID });
                    table.ForeignKey(
                        name: "FK_Shbilldetailsskj_Shbillmasterskj_BillID_BranchID",
                        columns: x => new { x.BillID, x.BranchID },
                        principalTable: "Shbillmasterskj",
                        principalColumns: new[] { "BillID", "BranchID" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shbilldetailsskj_BillID_BranchID",
                table: "Shbilldetailsskj",
                columns: new[] { "BillID", "BranchID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillIDCombinationModel");

            migrationBuilder.DropTable(
                name: "SHArticleMaster");

            migrationBuilder.DropTable(
                name: "Shbilldetailsskj");

            migrationBuilder.DropTable(
                name: "Shbillimagemodelskj");

            migrationBuilder.DropTable(
                name: "Shbillmasterskj");
        }
    }
}
