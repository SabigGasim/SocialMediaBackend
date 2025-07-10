using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SocialMediaBackend.Modules.Feed.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class RolesAndPermissionsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Permissions",
                schema: "feed",
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
                schema: "feed",
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
                name: "AuthorRoles",
                schema: "feed",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorRoles", x => new { x.AuthorId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AuthorRoles_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalSchema: "feed",
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "feed",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                schema: "feed",
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
                        principalSchema: "feed",
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "feed",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "feed",
                table: "Permissions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "Permissions.Posts.Create" },
                    { 1, "Permissions.Posts.Delete" },
                    { 2, "Permissions.Posts.GetAll" },
                    { 3, "Permissions.Posts.Get" },
                    { 4, "Permissions.Posts.Like" },
                    { 5, "Permissions.Posts.Unlike" },
                    { 6, "Permissions.Posts.Update" }
                });

            migrationBuilder.InsertData(
                schema: "feed",
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "AuthorRole" },
                    { 1, "AdminAuthorRole" },
                    { 2, "AppBasicPlanRole" },
                    { 3, "AppPlusPlanRole" }
                });

            migrationBuilder.InsertData(
                schema: "feed",
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
                    { 0, 1 },
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 4, 1 },
                    { 5, 1 },
                    { 6, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorRoles_RoleId",
                schema: "feed",
                table: "AuthorRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_Name",
                schema: "feed",
                table: "Permissions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                schema: "feed",
                table: "RolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                schema: "feed",
                table: "Roles",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorRoles",
                schema: "feed");

            migrationBuilder.DropTable(
                name: "RolePermissions",
                schema: "feed");

            migrationBuilder.DropTable(
                name: "Permissions",
                schema: "feed");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "feed");
        }
    }
}
