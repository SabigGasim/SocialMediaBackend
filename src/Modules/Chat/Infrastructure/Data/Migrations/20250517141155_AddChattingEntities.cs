using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMediaBackend.Modules.Chat.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddChattingEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOnline",
                schema: "chat",
                table: "Chatters",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastSeen",
                schema: "chat",
                table: "Chatters",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.CreateTable(
                name: "DirectChats",
                schema: "chat",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstChatterId = table.Column<Guid>(type: "uuid", nullable: false),
                    SecondChatterId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectChats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DirectChats_Chatters_FirstChatterId",
                        column: x => x.FirstChatterId,
                        principalSchema: "chat",
                        principalTable: "Chatters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DirectChats_Chatters_SecondChatterId",
                        column: x => x.SecondChatterId,
                        principalSchema: "chat",
                        principalTable: "Chatters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GroupChats",
                schema: "chat",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupChats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupChats_Chatters_OwnerId",
                        column: x => x.OwnerId,
                        principalSchema: "chat",
                        principalTable: "Chatters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DirectMessages",
                schema: "chat",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SenderId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatId = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    SentAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DirectMessages_Chatters_SenderId",
                        column: x => x.SenderId,
                        principalSchema: "chat",
                        principalTable: "Chatters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DirectMessages_DirectChats_ChatId",
                        column: x => x.ChatId,
                        principalSchema: "chat",
                        principalTable: "DirectChats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserDirectChats",
                schema: "chat",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatterId = table.Column<Guid>(type: "uuid", nullable: false),
                    DirectChatId = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDirectChats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDirectChats_Chatters_ChatterId",
                        column: x => x.ChatterId,
                        principalSchema: "chat",
                        principalTable: "Chatters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserDirectChats_DirectChats_DirectChatId",
                        column: x => x.DirectChatId,
                        principalSchema: "chat",
                        principalTable: "DirectChats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "GroupMessages",
                schema: "chat",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SenderId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatId = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    SentAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupMessages_Chatters_SenderId",
                        column: x => x.SenderId,
                        principalSchema: "chat",
                        principalTable: "Chatters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupMessages_GroupChats_ChatId",
                        column: x => x.ChatId,
                        principalSchema: "chat",
                        principalTable: "GroupChats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserGroupChat",
                schema: "chat",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatterId = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupChatId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsJoined = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroupChat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserGroupChat_Chatters_ChatterId",
                        column: x => x.ChatterId,
                        principalSchema: "chat",
                        principalTable: "Chatters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGroupChat_GroupChats_GroupChatId",
                        column: x => x.GroupChatId,
                        principalSchema: "chat",
                        principalTable: "GroupChats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserDirectMessages",
                schema: "chat",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SenderId = table.Column<Guid>(type: "uuid", nullable: false),
                    DirectMessageId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserDirectChatId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDirectMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDirectMessages_Chatters_SenderId",
                        column: x => x.SenderId,
                        principalSchema: "chat",
                        principalTable: "Chatters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserDirectMessages_DirectMessages_DirectMessageId",
                        column: x => x.DirectMessageId,
                        principalSchema: "chat",
                        principalTable: "DirectMessages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserDirectMessages_UserDirectChats_UserDirectChatId",
                        column: x => x.UserDirectChatId,
                        principalSchema: "chat",
                        principalTable: "UserDirectChats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupMessage_SeenBy",
                schema: "chat",
                columns: table => new
                {
                    GroupMessageId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatterId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupMessage_SeenBy", x => new { x.GroupMessageId, x.ChatterId });
                    table.ForeignKey(
                        name: "FK_GroupMessage_SeenBy_Chatters_ChatterId",
                        column: x => x.ChatterId,
                        principalSchema: "chat",
                        principalTable: "Chatters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupMessage_SeenBy_GroupMessages_GroupMessageId",
                        column: x => x.GroupMessageId,
                        principalSchema: "chat",
                        principalTable: "GroupMessages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserGroupMessages",
                schema: "chat",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SenderId = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupMessageId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserGroupChatId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroupMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserGroupMessages_Chatters_SenderId",
                        column: x => x.SenderId,
                        principalSchema: "chat",
                        principalTable: "Chatters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGroupMessages_GroupMessages_GroupMessageId",
                        column: x => x.GroupMessageId,
                        principalSchema: "chat",
                        principalTable: "GroupMessages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGroupMessages_UserGroupChat_UserGroupChatId",
                        column: x => x.UserGroupChatId,
                        principalSchema: "chat",
                        principalTable: "UserGroupChat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DirectChats_FirstChatterId",
                schema: "chat",
                table: "DirectChats",
                column: "FirstChatterId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectChats_Id",
                schema: "chat",
                table: "DirectChats",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DirectChats_SecondChatterId",
                schema: "chat",
                table: "DirectChats",
                column: "SecondChatterId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectMessages_ChatId",
                schema: "chat",
                table: "DirectMessages",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectMessages_Id",
                schema: "chat",
                table: "DirectMessages",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DirectMessages_SenderId",
                schema: "chat",
                table: "DirectMessages",
                column: "SenderId");

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

            migrationBuilder.CreateIndex(
                name: "IX_GroupChats_Id",
                schema: "chat",
                table: "GroupChats",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_GroupChats_OwnerId",
                schema: "chat",
                table: "GroupChats",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMessage_SeenBy_ChatterId",
                schema: "chat",
                table: "GroupMessage_SeenBy",
                column: "ChatterId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMessages_ChatId",
                schema: "chat",
                table: "GroupMessages",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMessages_Id",
                schema: "chat",
                table: "GroupMessages",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMessages_SenderId",
                schema: "chat",
                table: "GroupMessages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDirectChats_ChatterId",
                schema: "chat",
                table: "UserDirectChats",
                column: "ChatterId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDirectChats_DirectChatId",
                schema: "chat",
                table: "UserDirectChats",
                column: "DirectChatId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDirectChats_Id",
                schema: "chat",
                table: "UserDirectChats",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserDirectMessages_DirectMessageId",
                schema: "chat",
                table: "UserDirectMessages",
                column: "DirectMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDirectMessages_Id",
                schema: "chat",
                table: "UserDirectMessages",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserDirectMessages_SenderId",
                schema: "chat",
                table: "UserDirectMessages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDirectMessages_UserDirectChatId",
                schema: "chat",
                table: "UserDirectMessages",
                column: "UserDirectChatId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroupChat_ChatterId",
                schema: "chat",
                table: "UserGroupChat",
                column: "ChatterId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroupChat_GroupChatId",
                schema: "chat",
                table: "UserGroupChat",
                column: "GroupChatId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroupChat_Id",
                schema: "chat",
                table: "UserGroupChat",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroupMessages_GroupMessageId",
                schema: "chat",
                table: "UserGroupMessages",
                column: "GroupMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroupMessages_Id",
                schema: "chat",
                table: "UserGroupMessages",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroupMessages_SenderId",
                schema: "chat",
                table: "UserGroupMessages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroupMessages_UserGroupChatId",
                schema: "chat",
                table: "UserGroupMessages",
                column: "UserGroupChatId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupChat_Chatters",
                schema: "chat");

            migrationBuilder.DropTable(
                name: "GroupChat_Moderators",
                schema: "chat");

            migrationBuilder.DropTable(
                name: "GroupMessage_SeenBy",
                schema: "chat");

            migrationBuilder.DropTable(
                name: "UserDirectMessages",
                schema: "chat");

            migrationBuilder.DropTable(
                name: "UserGroupMessages",
                schema: "chat");

            migrationBuilder.DropTable(
                name: "DirectMessages",
                schema: "chat");

            migrationBuilder.DropTable(
                name: "UserDirectChats",
                schema: "chat");

            migrationBuilder.DropTable(
                name: "GroupMessages",
                schema: "chat");

            migrationBuilder.DropTable(
                name: "UserGroupChat",
                schema: "chat");

            migrationBuilder.DropTable(
                name: "DirectChats",
                schema: "chat");

            migrationBuilder.DropTable(
                name: "GroupChats",
                schema: "chat");

            migrationBuilder.DropColumn(
                name: "IsOnline",
                schema: "chat",
                table: "Chatters");

            migrationBuilder.DropColumn(
                name: "LastSeen",
                schema: "chat",
                table: "Chatters");
        }
    }
}
