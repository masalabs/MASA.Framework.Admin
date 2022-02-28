using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Masa.Framework.Admin.Service.User.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "user");

            migrationBuilder.CreateTable(
                name: "department",
                schema: "user",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    describtion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    parent_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    creator = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    creation_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    modifier_time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_department", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "IntegrationEventLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    TimesSent = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegrationEventLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "user_groups",
                schema: "user",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    describtion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    creator = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    creation_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    modifier_time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_groups", x => x.id);
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
                    enable = table.Column<bool>(type: "bit", nullable: false),
                    last_login_time = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    last_update_time = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    creator = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    creation_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    modifier_time = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_group_items",
                schema: "user",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_group_items", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_group_items_user_groups_UserGroupId",
                        column: x => x.UserGroupId,
                        principalSchema: "user",
                        principalTable: "user_groups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "department_users",
                schema: "user",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_department_users", x => x.id);
                    table.ForeignKey(
                        name: "FK_department_users_department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "user",
                        principalTable: "department",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_department_users_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "user",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_department_code",
                schema: "user",
                table: "department",
                column: "code");

            migrationBuilder.CreateIndex(
                name: "IX_department_users_DepartmentId",
                schema: "user",
                table: "department_users",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_department_users_user_id",
                schema: "user",
                table: "department_users",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "index_state_modificationtime",
                table: "IntegrationEventLog",
                columns: new[] { "State", "ModificationTime" });

            migrationBuilder.CreateIndex(
                name: "index_state_timessent_modificationtime",
                table: "IntegrationEventLog",
                columns: new[] { "State", "TimesSent", "ModificationTime" });

            migrationBuilder.CreateIndex(
                name: "IX_user_group_items_UserGroupId",
                schema: "user",
                table: "user_group_items",
                column: "UserGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_user_groups_code",
                schema: "user",
                table: "user_groups",
                column: "code");

            migrationBuilder.CreateIndex(
                name: "IX_user_roles_UserId",
                schema: "user",
                table: "user_roles",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "department_users",
                schema: "user");

            migrationBuilder.DropTable(
                name: "IntegrationEventLog");

            migrationBuilder.DropTable(
                name: "user_group_items",
                schema: "user");

            migrationBuilder.DropTable(
                name: "user_roles",
                schema: "user");

            migrationBuilder.DropTable(
                name: "department",
                schema: "user");

            migrationBuilder.DropTable(
                name: "user_groups",
                schema: "user");

            migrationBuilder.DropTable(
                name: "users",
                schema: "user");
        }
    }
}
