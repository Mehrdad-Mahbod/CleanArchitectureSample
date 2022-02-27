using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Data.Migrations
{
    public partial class InitDb1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GeneralOffices",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Priority = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "GETDATE()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralOffices", x => x.ID);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "ID",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "00659566-7a80-4697-afef-ec63aabed5f7");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "ID",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "9c134886-a77c-4947-9cbf-effb600fa26a");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "ID",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "c401ce3a-e3b3-489f-8bcb-6cb7da1df646");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "ID",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "4f8aab01-fa3e-4b5c-a91c-fe27312c0a51");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeneralOffices");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "ID",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "f9d60d20-7da3-427f-9d89-582a7c4d627e");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "ID",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "49281c4d-7294-4192-9228-9542b0cfe94e");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "ID",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "a5adcc01-7caa-41dc-9aa4-764c9e7168de");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "ID",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "e4635653-eb5a-492d-a1b8-a26e6c802a21");
        }
    }
}
