using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MASA.Framework.Admin.Service.Authentication.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "authentication");

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
                name: "permission",
                schema: "authentication",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ObjectType = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    resource = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    scope = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    action = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    code = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    enable = table.Column<bool>(type: "bit", nullable: false),
                    parent_permission_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    permission_type = table.Column<int>(type: "int", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    creator = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    creation_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    modifier_time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permission", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                schema: "authentication",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    describe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    number = table.Column<int>(type: "int", nullable: false),
                    enable = table.Column<bool>(type: "bit", nullable: false),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false),
                    creator = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    creation_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    modifier_time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "role_items",
                schema: "authentication",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    parent_role_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    role_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_items", x => x.id);
                    table.ForeignKey(
                        name: "FK_role_items_roles_parent_role_id",
                        column: x => x.parent_role_id,
                        principalSchema: "authentication",
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "role_permission",
                schema: "authentication",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    permissions_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    permission_type = table.Column<int>(type: "int", nullable: false),
                    permission_effect = table.Column<int>(type: "int", nullable: false),
                    role_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_permission", x => x.id);
                    table.ForeignKey(
                        name: "FK_role_permission_roles_role_id",
                        column: x => x.role_id,
                        principalSchema: "authentication",
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IntegrationEventLog_State_TimesSent",
                table: "IntegrationEventLog",
                columns: new[] { "State", "TimesSent" });

            migrationBuilder.CreateIndex(
                name: "IX_role_items_parent_role_id",
                schema: "authentication",
                table: "role_items",
                column: "parent_role_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_permission_role_id",
                schema: "authentication",
                table: "role_permission",
                column: "role_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IntegrationEventLog");

            migrationBuilder.DropTable(
                name: "permission",
                schema: "authentication");

            migrationBuilder.DropTable(
                name: "role_items",
                schema: "authentication");

            migrationBuilder.DropTable(
                name: "role_permission",
                schema: "authentication");

            migrationBuilder.DropTable(
                name: "roles",
                schema: "authentication");
        }
    }
}
