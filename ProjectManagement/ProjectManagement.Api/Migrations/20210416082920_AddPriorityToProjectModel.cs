using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManagement.Api.Migrations
{
    public partial class AddPriorityToProjectModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Priority",
                table: "Projects",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Projects");
        }
    }
}
