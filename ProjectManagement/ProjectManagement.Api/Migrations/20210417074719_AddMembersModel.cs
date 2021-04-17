using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManagement.Api.Migrations
{
    public partial class AddMembersModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PrivilegeLevels",
                keyColumn: "Id",
                keyValue: new Guid("b2ae4957-0693-42ed-beb7-3af378d5d370"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("11efb2df-6c8d-4c6e-8c19-a84ebd0a6b51"));

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    MemberTitle = table.Column<string>(nullable: false),
                    Username = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MemberPrivilegeLevel",
                columns: table => new
                {
                    MemberId = table.Column<Guid>(nullable: false),
                    PrivilegeLevelId = table.Column<Guid>(nullable: false),
                    Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberPrivilegeLevel", x => new { x.PrivilegeLevelId, x.MemberId });
                    table.ForeignKey(
                        name: "FK_MemberPrivilegeLevel_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemberPrivilegeLevel_PrivilegeLevels_PrivilegeLevelId",
                        column: x => x.PrivilegeLevelId,
                        principalTable: "PrivilegeLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MemberRole",
                columns: table => new
                {
                    MemberId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false),
                    Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberRole", x => new { x.RoleId, x.MemberId });
                    table.ForeignKey(
                        name: "FK_MemberRole_Member_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Member",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemberRole_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MemberPrivilegeLevel_MemberId",
                table: "MemberPrivilegeLevel",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberRole_MemberId",
                table: "MemberRole",
                column: "MemberId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemberPrivilegeLevel");

            migrationBuilder.DropTable(
                name: "MemberRole");

            migrationBuilder.DropTable(
                name: "Member");

            migrationBuilder.InsertData(
                table: "PrivilegeLevels",
                columns: new[] { "Id", "CreationDate", "LastModifiedDate", "ModifiedBy", "PrivilegeLevelTitle" },
                values: new object[] { new Guid("b2ae4957-0693-42ed-beb7-3af378d5d370"), new DateTime(2021, 4, 16, 22, 31, 39, 244, DateTimeKind.Local).AddTicks(8139), new DateTime(2021, 4, 16, 22, 31, 39, 244, DateTimeKind.Local).AddTicks(8236), "Admin", "Level 1" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreationDate", "LastModifiedDate", "ModifiedBy", "RoleTitle" },
                values: new object[] { new Guid("11efb2df-6c8d-4c6e-8c19-a84ebd0a6b51"), new DateTime(2021, 4, 16, 22, 31, 39, 208, DateTimeKind.Local).AddTicks(2446), new DateTime(2021, 4, 16, 22, 31, 39, 241, DateTimeKind.Local).AddTicks(8702), "Admin", "Admin" });
        }
    }
}
