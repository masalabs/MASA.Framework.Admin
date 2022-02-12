using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MASA.Framework.Admin.Service.Api.Migrations
{
    public partial class updateState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "State",
                table: "Users",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0,
                comment: "State");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Users");
        }
    }
}
