using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class chage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "employeeId",
                table: "Employees",
                newName: "NIK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NIK",
                table: "Employees",
                newName: "employeeId");
        }
    }
}
