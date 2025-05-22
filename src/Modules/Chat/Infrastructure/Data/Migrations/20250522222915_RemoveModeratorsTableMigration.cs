using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMediaBackend.Modules.Chat.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveModeratorsTableMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupChats_Chatters_OwnerId",
                schema: "chat",
                table: "GroupChats");

            migrationBuilder.DropTable(
                name: "GroupChatModerators",
                schema: "chat");

            migrationBuilder.DropIndex(
                name: "IX_GroupChats_OwnerId",
                schema: "chat",
                table: "GroupChats");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "chat",
                table: "GroupChats");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                schema: "chat",
                table: "GroupChats");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "MemberSince",
                schema: "chat",
                table: "GroupChatMembers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<int>(
                name: "Membership",
                schema: "chat",
                table: "GroupChatMembers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MemberSince",
                schema: "chat",
                table: "GroupChatMembers");

            migrationBuilder.DropColumn(
                name: "Membership",
                schema: "chat",
                table: "GroupChatMembers");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                schema: "chat",
                table: "GroupChats",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                schema: "chat",
                table: "GroupChats",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "GroupChatModerators",
                schema: "chat",
                columns: table => new
                {
                    GroupChatId = table.Column<Guid>(type: "uuid", nullable: false),
                    ModeratorId = table.Column<Guid>(type: "uuid", nullable: false),
                    AssignedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupChatModerators", x => new { x.GroupChatId, x.ModeratorId });
                    table.ForeignKey(
                        name: "FK_GroupChatModerators_Chatters_ModeratorId",
                        column: x => x.ModeratorId,
                        principalSchema: "chat",
                        principalTable: "Chatters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupChatModerators_GroupChats_GroupChatId",
                        column: x => x.GroupChatId,
                        principalSchema: "chat",
                        principalTable: "GroupChats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupChats_OwnerId",
                schema: "chat",
                table: "GroupChats",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupChatModerators_GroupChatId",
                schema: "chat",
                table: "GroupChatModerators",
                column: "GroupChatId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupChatModerators_ModeratorId",
                schema: "chat",
                table: "GroupChatModerators",
                column: "ModeratorId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupChats_Chatters_OwnerId",
                schema: "chat",
                table: "GroupChats",
                column: "OwnerId",
                principalSchema: "chat",
                principalTable: "Chatters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
