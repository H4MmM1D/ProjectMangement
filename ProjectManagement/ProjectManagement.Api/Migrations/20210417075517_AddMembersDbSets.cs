using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManagement.Api.Migrations
{
    public partial class AddMembersDbSets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberPrivilegeLevel_Member_MemberId",
                table: "MemberPrivilegeLevel");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberPrivilegeLevel_PrivilegeLevels_PrivilegeLevelId",
                table: "MemberPrivilegeLevel");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberRole_Member_MemberId",
                table: "MemberRole");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberRole_Roles_RoleId",
                table: "MemberRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MemberRole",
                table: "MemberRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MemberPrivilegeLevel",
                table: "MemberPrivilegeLevel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Member",
                table: "Member");

            migrationBuilder.RenameTable(
                name: "MemberRole",
                newName: "MemberRoles");

            migrationBuilder.RenameTable(
                name: "MemberPrivilegeLevel",
                newName: "MemberPrivilegeLevels");

            migrationBuilder.RenameTable(
                name: "Member",
                newName: "Members");

            migrationBuilder.RenameIndex(
                name: "IX_MemberRole_MemberId",
                table: "MemberRoles",
                newName: "IX_MemberRoles_MemberId");

            migrationBuilder.RenameIndex(
                name: "IX_MemberPrivilegeLevel_MemberId",
                table: "MemberPrivilegeLevels",
                newName: "IX_MemberPrivilegeLevels_MemberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MemberRoles",
                table: "MemberRoles",
                columns: new[] { "RoleId", "MemberId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MemberPrivilegeLevels",
                table: "MemberPrivilegeLevels",
                columns: new[] { "PrivilegeLevelId", "MemberId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Members",
                table: "Members",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberPrivilegeLevels_Members_MemberId",
                table: "MemberPrivilegeLevels",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberPrivilegeLevels_PrivilegeLevels_PrivilegeLevelId",
                table: "MemberPrivilegeLevels",
                column: "PrivilegeLevelId",
                principalTable: "PrivilegeLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberRoles_Members_MemberId",
                table: "MemberRoles",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberRoles_Roles_RoleId",
                table: "MemberRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberPrivilegeLevels_Members_MemberId",
                table: "MemberPrivilegeLevels");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberPrivilegeLevels_PrivilegeLevels_PrivilegeLevelId",
                table: "MemberPrivilegeLevels");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberRoles_Members_MemberId",
                table: "MemberRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberRoles_Roles_RoleId",
                table: "MemberRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Members",
                table: "Members");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MemberRoles",
                table: "MemberRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MemberPrivilegeLevels",
                table: "MemberPrivilegeLevels");

            migrationBuilder.RenameTable(
                name: "Members",
                newName: "Member");

            migrationBuilder.RenameTable(
                name: "MemberRoles",
                newName: "MemberRole");

            migrationBuilder.RenameTable(
                name: "MemberPrivilegeLevels",
                newName: "MemberPrivilegeLevel");

            migrationBuilder.RenameIndex(
                name: "IX_MemberRoles_MemberId",
                table: "MemberRole",
                newName: "IX_MemberRole_MemberId");

            migrationBuilder.RenameIndex(
                name: "IX_MemberPrivilegeLevels_MemberId",
                table: "MemberPrivilegeLevel",
                newName: "IX_MemberPrivilegeLevel_MemberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Member",
                table: "Member",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MemberRole",
                table: "MemberRole",
                columns: new[] { "RoleId", "MemberId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MemberPrivilegeLevel",
                table: "MemberPrivilegeLevel",
                columns: new[] { "PrivilegeLevelId", "MemberId" });

            migrationBuilder.AddForeignKey(
                name: "FK_MemberPrivilegeLevel_Member_MemberId",
                table: "MemberPrivilegeLevel",
                column: "MemberId",
                principalTable: "Member",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberPrivilegeLevel_PrivilegeLevels_PrivilegeLevelId",
                table: "MemberPrivilegeLevel",
                column: "PrivilegeLevelId",
                principalTable: "PrivilegeLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberRole_Member_MemberId",
                table: "MemberRole",
                column: "MemberId",
                principalTable: "Member",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberRole_Roles_RoleId",
                table: "MemberRole",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
