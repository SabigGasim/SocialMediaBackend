using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMediaBackend.Modules.Chat.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddLastSeenAndReceivedMessageMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LastReceivedMessageId",
                schema: "chat",
                table: "UserGroupChats",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastSeenMessageId",
                schema: "chat",
                table: "UserGroupChats",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserGroupChats_LastReceivedMessageId",
                schema: "chat",
                table: "UserGroupChats",
                column: "LastReceivedMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroupChats_LastSeenMessageId",
                schema: "chat",
                table: "UserGroupChats",
                column: "LastSeenMessageId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroupChats_GroupMessages_LastReceivedMessageId",
                schema: "chat",
                table: "UserGroupChats",
                column: "LastReceivedMessageId",
                principalSchema: "chat",
                principalTable: "GroupMessages",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroupChats_GroupMessages_LastSeenMessageId",
                schema: "chat",
                table: "UserGroupChats",
                column: "LastSeenMessageId",
                principalSchema: "chat",
                principalTable: "GroupMessages",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserGroupChats_GroupMessages_LastReceivedMessageId",
                schema: "chat",
                table: "UserGroupChats");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroupChats_GroupMessages_LastSeenMessageId",
                schema: "chat",
                table: "UserGroupChats");

            migrationBuilder.DropIndex(
                name: "IX_UserGroupChats_LastReceivedMessageId",
                schema: "chat",
                table: "UserGroupChats");

            migrationBuilder.DropIndex(
                name: "IX_UserGroupChats_LastSeenMessageId",
                schema: "chat",
                table: "UserGroupChats");

            migrationBuilder.DropColumn(
                name: "LastReceivedMessageId",
                schema: "chat",
                table: "UserGroupChats");

            migrationBuilder.DropColumn(
                name: "LastSeenMessageId",
                schema: "chat",
                table: "UserGroupChats");
        }
    }
}
