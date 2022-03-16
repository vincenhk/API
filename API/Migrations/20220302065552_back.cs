using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class back : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_m_employee",
                table: "tb_m_employee");

            migrationBuilder.DropColumn(
                name: "id",
                table: "tb_m_employee");

            migrationBuilder.AlterColumn<string>(
                name: "NIK",
                table: "tb_m_employee",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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

            migrationBuilder.AlterColumn<string>(
                name: "NIK",
                table: "tb_m_employee",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "tb_m_employee",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_m_employee",
                table: "tb_m_employee",
                column: "id");
        }
    }
}
