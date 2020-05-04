using Microsoft.EntityFrameworkCore.Migrations;

namespace E_CounsellingWebApplication.Migrations
{
    public partial class addingAppointmentusernames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CounselorName",
                table: "Appointments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Appointments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CounselorName",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Appointments");
        }
    }
}
