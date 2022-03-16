using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class NewColomnAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiredToken",
                table: "tb_tr_account",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                table: "tb_tr_account",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "OTP",
                table: "tb_tr_account",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpiredToken",
                table: "tb_tr_account");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                table: "tb_tr_account");

            migrationBuilder.DropColumn(
                name: "OTP",
                table: "tb_tr_account");
        }
    }
}
