using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MASA.Framework.Admin.Service.User.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "user");

            migrationBuilder.CreateTable(
                name: "integration_event_log",
                schema: "user",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    TimesSent = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_integration_event_log", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "user",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    account = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    salt = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    gender = table.Column<bool>(type: "bit", nullable: false),
                    cover = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    state = table.Column<int>(type: "int", nullable: false),
                    last_login_time = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    last_update_time = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    creator = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    creation_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    modifier_time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_roles",
                schema: "user",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    role_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_roles", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_roles_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "user",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_roles_UserId",
                schema: "user",
                table: "user_roles",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "integration_event_log",
                schema: "user");

            migrationBuilder.DropTable(
                name: "user_roles",
                schema: "user");

            migrationBuilder.DropTable(
                name: "users",
                schema: "user");
        }
    }
}
