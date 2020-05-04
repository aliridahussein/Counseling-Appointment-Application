using Microsoft.EntityFrameworkCore.Migrations;

namespace E_CounsellingWebApplication.Migrations
{
    public partial class Usrcounselor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "AspNetUsers",
                newName: "Category");

            migrationBuilder.CreateTable(
                name: "UserCounselors",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserID = table.Column<string>(nullable: true),
                    CounselorId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCounselors", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCounselors");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "AspNetUsers",
                newName: "CategoryId");
        }
    }
}
