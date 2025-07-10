using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SocialMediaBackend.Modules.Chat.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class RolesAndPermissionsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Permissions",
                schema: "chat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "chat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChatterRoles",
                schema: "chat",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    ChatterId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatterRoles", x => new { x.ChatterId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_ChatterRoles_Chatters_ChatterId",
                        column: x => x.ChatterId,
                        principalSchema: "chat",
                        principalTable: "Chatters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatterRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "chat",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                schema: "chat",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    PermissionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalSchema: "chat",
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "chat",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "chat",
                table: "Permissions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "Permissions.DirectChat.Create" },
                    { 1, "Permissions.DirectMessage.Create" },
                    { 2, "Permissions.DirectMessage.DeleteForEveryone" },
                    { 3, "Permissions.DirectMessage.DeleteForMe" },
                    { 4, "Permissions.DirectMessage.GetAll" },
                    { 5, "Permissions.DirectMessage.MarkAsSeen" },
                    { 6, "Permissions.GroupChat.Create" },
                    { 7, "Permissions.GroupMessage.Create" },
                    { 8, "Permissions.GroupMessage.DeleteForEveryone" },
                    { 9, "Permissions.GroupMessage.DeleteForMe" },
                    { 10, "Permissions.GroupMessage.GetAll" },
                    { 11, "Permissions.GroupChat.Join" },
                    { 12, "Permissions.GroupChat.KickMember" },
                    { 13, "Permissions.GroupChat.Leave" },
                    { 14, "Permissions.GroupMessage.MarkAsSeen" },
                    { 15, "Permissions.GroupMessage.MarkAsReceived" },
                    { 16, "Permissions.GroupChat.PromoteMember" }
                });

            migrationBuilder.InsertData(
                schema: "chat",
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "ChatterRole" },
                    { 1, "AdminChatterRole" },
                    { 2, "AppBasicPlanRole" },
                    { 3, "AppPlusPlanRole" }
                });

            migrationBuilder.InsertData(
                schema: "chat",
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 0, 0 },
                    { 1, 0 },
                    { 2, 0 },
                    { 3, 0 },
                    { 4, 0 },
                    { 5, 0 },
                    { 6, 0 },
                    { 7, 0 },
                    { 8, 0 },
                    { 9, 0 },
                    { 10, 0 },
                    { 11, 0 },
                    { 12, 0 },
                    { 13, 0 },
                    { 14, 0 },
                    { 15, 0 },
                    { 16, 0 },
                    { 0, 1 },
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 4, 1 },
                    { 5, 1 },
                    { 6, 1 },
                    { 7, 1 },
                    { 8, 1 },
                    { 9, 1 },
                    { 10, 1 },
                    { 11, 1 },
                    { 12, 1 },
                    { 13, 1 },
                    { 14, 1 },
                    { 15, 1 },
                    { 16, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatterRoles_RoleId",
                schema: "chat",
                table: "ChatterRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_Name",
                schema: "chat",
                table: "Permissions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                schema: "chat",
                table: "RolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                schema: "chat",
                table: "Roles",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatterRoles",
                schema: "chat");

            migrationBuilder.DropTable(
                name: "RolePermissions",
                schema: "chat");

            migrationBuilder.DropTable(
                name: "Permissions",
                schema: "chat");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "chat");
        }
    }
}
