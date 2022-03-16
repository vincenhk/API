using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class RenameTableEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "tb_m_employee");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_m_employee",
                table: "tb_m_employee",
                column: "NIK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_m_employee",
                table: "tb_m_employee");

            migrationBuilder.RenameTable(
                name: "tb_m_employee",
                newName: "Employees");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "NIK");
        }
    }
}
