using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMediaBackend.Modules.Chat.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSendersFromUserMessages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDirectMessages_Chatters_SenderId",
                schema: "chat",
                table: "UserDirectMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroupChat_Chatters_ChatterId",
                schema: "chat",
                table: "UserGroupChat");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroupChat_GroupChats_GroupChatId",
                schema: "chat",
                table: "UserGroupChat");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroupMessages_Chatters_SenderId",
                schema: "chat",
                table: "UserGroupMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroupMessages_UserGroupChat_UserGroupChatId",
                schema: "chat",
                table: "UserGroupMessages");

            migrationBuilder.DropIndex(
                name: "IX_UserGroupMessages_SenderId",
                schema: "chat",
                table: "UserGroupMessages");

            migrationBuilder.DropIndex(
                name: "IX_UserDirectMessages_SenderId",
                schema: "chat",
                table: "UserDirectMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserGroupChat",
                schema: "chat",
                table: "UserGroupChat");

            migrationBuilder.DropColumn(
                name: "SenderId",
                schema: "chat",
                table: "UserGroupMessages");

            migrationBuilder.DropColumn(
                name: "SenderId",
                schema: "chat",
                table: "UserDirectMessages");

            migrationBuilder.RenameTable(
                name: "UserGroupChat",
                schema: "chat",
                newName: "UserGroupChats",
                newSchema: "chat");

            migrationBuilder.RenameIndex(
                name: "IX_UserGroupChat_Id",
                schema: "chat",
                table: "UserGroupChats",
                newName: "IX_UserGroupChats_Id");

            migrationBuilder.RenameIndex(
                name: "IX_UserGroupChat_GroupChatId",
                schema: "chat",
                table: "UserGroupChats",
                newName: "IX_UserGroupChats_GroupChatId");

            migrationBuilder.RenameIndex(
                name: "IX_UserGroupChat_ChatterId",
                schema: "chat",
                table: "UserGroupChats",
                newName: "IX_UserGroupChats_ChatterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserGroupChats",
                schema: "chat",
                table: "UserGroupChats",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroupChats_Chatters_ChatterId",
                schema: "chat",
                table: "UserGroupChats",
                column: "ChatterId",
                principalSchema: "chat",
                principalTable: "Chatters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroupChats_GroupChats_GroupChatId",
                schema: "chat",
                table: "UserGroupChats",
                column: "GroupChatId",
                principalSchema: "chat",
                principalTable: "GroupChats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroupMessages_UserGroupChats_UserGroupChatId",
                schema: "chat",
                table: "UserGroupMessages",
                column: "UserGroupChatId",
                principalSchema: "chat",
                principalTable: "UserGroupChats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserGroupChats_Chatters_ChatterId",
                schema: "chat",
                table: "UserGroupChats");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroupChats_GroupChats_GroupChatId",
                schema: "chat",
                table: "UserGroupChats");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroupMessages_UserGroupChats_UserGroupChatId",
                schema: "chat",
                table: "UserGroupMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserGroupChats",
                schema: "chat",
                table: "UserGroupChats");

            migrationBuilder.RenameTable(
                name: "UserGroupChats",
                schema: "chat",
                newName: "UserGroupChat",
                newSchema: "chat");

            migrationBuilder.RenameIndex(
                name: "IX_UserGroupChats_Id",
                schema: "chat",
                table: "UserGroupChat",
                newName: "IX_UserGroupChat_Id");

            migrationBuilder.RenameIndex(
                name: "IX_UserGroupChats_GroupChatId",
                schema: "chat",
                table: "UserGroupChat",
                newName: "IX_UserGroupChat_GroupChatId");

            migrationBuilder.RenameIndex(
                name: "IX_UserGroupChats_ChatterId",
                schema: "chat",
                table: "UserGroupChat",
                newName: "IX_UserGroupChat_ChatterId");

            migrationBuilder.AddColumn<Guid>(
                name: "SenderId",
                schema: "chat",
                table: "UserGroupMessages",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SenderId",
                schema: "chat",
                table: "UserDirectMessages",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserGroupChat",
                schema: "chat",
                table: "UserGroupChat",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroupMessages_SenderId",
                schema: "chat",
                table: "UserGroupMessages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDirectMessages_SenderId",
                schema: "chat",
                table: "UserDirectMessages",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDirectMessages_Chatters_SenderId",
                schema: "chat",
                table: "UserDirectMessages",
                column: "SenderId",
                principalSchema: "chat",
                principalTable: "Chatters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroupChat_Chatters_ChatterId",
                schema: "chat",
                table: "UserGroupChat",
                column: "ChatterId",
                principalSchema: "chat",
                principalTable: "Chatters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroupChat_GroupChats_GroupChatId",
                schema: "chat",
                table: "UserGroupChat",
                column: "GroupChatId",
                principalSchema: "chat",
                principalTable: "GroupChats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroupMessages_Chatters_SenderId",
                schema: "chat",
                table: "UserGroupMessages",
                column: "SenderId",
                principalSchema: "chat",
                principalTable: "Chatters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroupMessages_UserGroupChat_UserGroupChatId",
                schema: "chat",
                table: "UserGroupMessages",
                column: "UserGroupChatId",
                principalSchema: "chat",
                principalTable: "UserGroupChat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
