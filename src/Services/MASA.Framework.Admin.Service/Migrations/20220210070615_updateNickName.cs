using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MASA.Framework.Admin.Service.Login.Migrations
{
    public partial class updateNickName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "NickName",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                comment: "NickName");

            migrationBuilder.AddColumn<string>(
                name: "Salt",
                table: "Users",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: false,
                defaultValue: "",
                comment: "Salt");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NickName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Salt",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Users",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                comment: "UserName");
        }
    }
}
