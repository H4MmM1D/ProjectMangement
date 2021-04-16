using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManagement.Api.Migrations
{
    public partial class RoleAndPrivilegeLevelInitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PrivilegeLevels",
                columns: new[] { "Id", "CreationDate", "LastModifiedDate", "ModifiedBy", "PrivilegeLevelTitle" },
                values: new object[] { new Guid("b2ae4957-0693-42ed-beb7-3af378d5d370"), new DateTime(2021, 4, 16, 22, 31, 39, 244, DateTimeKind.Local).AddTicks(8139), new DateTime(2021, 4, 16, 22, 31, 39, 244, DateTimeKind.Local).AddTicks(8236), "Admin", "Level 1" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreationDate", "LastModifiedDate", "ModifiedBy", "RoleTitle" },
                values: new object[] { new Guid("11efb2df-6c8d-4c6e-8c19-a84ebd0a6b51"), new DateTime(2021, 4, 16, 22, 31, 39, 208, DateTimeKind.Local).AddTicks(2446), new DateTime(2021, 4, 16, 22, 31, 39, 241, DateTimeKind.Local).AddTicks(8702), "Admin", "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PrivilegeLevels",
                keyColumn: "Id",
                keyValue: new Guid("b2ae4957-0693-42ed-beb7-3af378d5d370"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("11efb2df-6c8d-4c6e-8c19-a84ebd0a6b51"));
        }
    }
}
