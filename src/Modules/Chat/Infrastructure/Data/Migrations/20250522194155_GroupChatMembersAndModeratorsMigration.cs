using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMediaBackend.Modules.Chat.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class GroupChatMembersAndModeratorsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupChat_Chatters",
                schema: "chat");

            migrationBuilder.DropTable(
                name: "GroupChat_Moderators",
                schema: "chat");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                schema: "chat",
                table: "GroupChats",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "chat",
                table: "GroupChats",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "GroupChatMembers",
                schema: "chat",
                columns: table => new
                {
                    GroupChatId = table.Column<Guid>(type: "uuid", nullable: false),
                    MemberId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupChatMembers", x => new { x.GroupChatId, x.MemberId });
                    table.ForeignKey(
                        name: "FK_GroupChatMembers_Chatters_MemberId",
                        column: x => x.MemberId,
                        principalSchema: "chat",
                        principalTable: "Chatters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupChatMembers_GroupChats_GroupChatId",
                        column: x => x.GroupChatId,
                        principalSchema: "chat",
                        principalTable: "GroupChats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_GroupChatMembers_GroupChatId",
                schema: "chat",
                table: "GroupChatMembers",
                column: "GroupChatId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupChatMembers_MemberId",
                schema: "chat",
                table: "GroupChatMembers",
                column: "MemberId");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupChatMembers",
                schema: "chat");

            migrationBuilder.DropTable(
                name: "GroupChatModerators",
                schema: "chat");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "chat",
                table: "GroupChats");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "chat",
                table: "GroupChats");

            migrationBuilder.CreateTable(
                name: "GroupChat_Chatters",
                schema: "chat",
                columns: table => new
                {
                    GroupChatId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatterId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupChat_Chatters", x => new { x.GroupChatId, x.ChatterId });
                    table.ForeignKey(
                        name: "FK_GroupChat_Chatters_Chatters_ChatterId",
                        column: x => x.ChatterId,
                        principalSchema: "chat",
                        principalTable: "Chatters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupChat_Chatters_GroupChats_GroupChatId",
                        column: x => x.GroupChatId,
                        principalSchema: "chat",
                        principalTable: "GroupChats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupChat_Moderators",
                schema: "chat",
                columns: table => new
                {
                    GroupChatId = table.Column<Guid>(type: "uuid", nullable: false),
                    ModeratorId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupChat_Moderators", x => new { x.GroupChatId, x.ModeratorId });
                    table.ForeignKey(
                        name: "FK_GroupChat_Moderators_Chatters_ModeratorId",
                        column: x => x.ModeratorId,
                        principalSchema: "chat",
                        principalTable: "Chatters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupChat_Moderators_GroupChats_GroupChatId",
                        column: x => x.GroupChatId,
                        principalSchema: "chat",
                        principalTable: "GroupChats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupChat_Chatters_ChatterId",
                schema: "chat",
                table: "GroupChat_Chatters",
                column: "ChatterId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupChat_Moderators_ModeratorId",
                schema: "chat",
                table: "GroupChat_Moderators",
                column: "ModeratorId");
        }
    }
}
