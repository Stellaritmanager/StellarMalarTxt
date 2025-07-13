using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StellarBillingSystem_skj.Migrations
{
    public partial class initial5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Drop existing primary key
            migrationBuilder.DropPrimaryKey(
                name: "PK_Shbilldetailsskj",
                table: "Shbilldetailsskj");

            // 2. Alter ArticleID column type (string → int)
            migrationBuilder.AlterColumn<int>(
                name: "ArticleID",
                table: "Shbilldetailsskj",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            // 3. Re-add composite primary key
            migrationBuilder.AddPrimaryKey(
                name: "PK_Shbilldetailsskj",
                table: "Shbilldetailsskj",
                columns: new[] { "ArticleID", "BranchID", "BillID" });

            // 4. Add foreign key to SHArticleMaster
            migrationBuilder.AddForeignKey(
                name: "FK_Shbilldetailsskj_SHArticleMaster_ArticleID",
                table: "Shbilldetailsskj",
                column: "ArticleID",
                principalTable: "SHArticleMaster",
                principalColumn: "ArticleID",
                onDelete: ReferentialAction.Restrict); // or Cascade
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop FK
            migrationBuilder.DropForeignKey(
                name: "FK_Shbilldetailsskj_SHArticleMaster_ArticleID",
                table: "Shbilldetailsskj");

            // Drop PK
            migrationBuilder.DropPrimaryKey(
                name: "PK_Shbilldetailsskj",
                table: "Shbilldetailsskj");

            // Revert ArticleID column to string
            migrationBuilder.AlterColumn<string>(
                name: "ArticleID",
                table: "Shbilldetailsskj",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            // Re-add original PK
            migrationBuilder.AddPrimaryKey(
                name: "PK_Shbilldetailsskj",
                table: "Shbilldetailsskj",
                columns: new[] { "ArticleID", "BranchID", "BillID" });
        }
    }
}
