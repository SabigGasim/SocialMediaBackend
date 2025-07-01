using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMediaBackend.Modules.Chat.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixDirectChatDeleteBehaviorMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DirectChats_Chatters_FirstChatterId",
                schema: "chat",
                table: "DirectChats");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectChats_Chatters_SecondChatterId",
                schema: "chat",
                table: "DirectChats");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectMessages_Chatters_SenderId",
                schema: "chat",
                table: "DirectMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupMessages_Chatters_SenderId",
                schema: "chat",
                table: "GroupMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDirectChats_Chatters_ChatterId",
                schema: "chat",
                table: "UserDirectChats");

            migrationBuilder.AlterColumn<Guid>(
                name: "ChatterId",
                schema: "chat",
                table: "UserDirectChats",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "SenderId",
                schema: "chat",
                table: "DirectMessages",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "SecondChatterId",
                schema: "chat",
                table: "DirectChats",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "FirstChatterId",
                schema: "chat",
                table: "DirectChats",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_DirectChats_Chatters_FirstChatterId",
                schema: "chat",
                table: "DirectChats",
                column: "FirstChatterId",
                principalSchema: "chat",
                principalTable: "Chatters",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectChats_Chatters_SecondChatterId",
                schema: "chat",
                table: "DirectChats",
                column: "SecondChatterId",
                principalSchema: "chat",
                principalTable: "Chatters",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectMessages_Chatters_SenderId",
                schema: "chat",
                table: "DirectMessages",
                column: "SenderId",
                principalSchema: "chat",
                principalTable: "Chatters",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMessages_Chatters_SenderId",
                schema: "chat",
                table: "GroupMessages",
                column: "SenderId",
                principalSchema: "chat",
                principalTable: "Chatters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDirectChats_Chatters_ChatterId",
                schema: "chat",
                table: "UserDirectChats",
                column: "ChatterId",
                principalSchema: "chat",
                principalTable: "Chatters",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DirectChats_Chatters_FirstChatterId",
                schema: "chat",
                table: "DirectChats");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectChats_Chatters_SecondChatterId",
                schema: "chat",
                table: "DirectChats");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectMessages_Chatters_SenderId",
                schema: "chat",
                table: "DirectMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupMessages_Chatters_SenderId",
                schema: "chat",
                table: "GroupMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDirectChats_Chatters_ChatterId",
                schema: "chat",
                table: "UserDirectChats");

            migrationBuilder.AlterColumn<Guid>(
                name: "ChatterId",
                schema: "chat",
                table: "UserDirectChats",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "SenderId",
                schema: "chat",
                table: "DirectMessages",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "SecondChatterId",
                schema: "chat",
                table: "DirectChats",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "FirstChatterId",
                schema: "chat",
                table: "DirectChats",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectChats_Chatters_FirstChatterId",
                schema: "chat",
                table: "DirectChats",
                column: "FirstChatterId",
                principalSchema: "chat",
                principalTable: "Chatters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectChats_Chatters_SecondChatterId",
                schema: "chat",
                table: "DirectChats",
                column: "SecondChatterId",
                principalSchema: "chat",
                principalTable: "Chatters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectMessages_Chatters_SenderId",
                schema: "chat",
                table: "DirectMessages",
                column: "SenderId",
                principalSchema: "chat",
                principalTable: "Chatters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupMessages_Chatters_SenderId",
                schema: "chat",
                table: "GroupMessages",
                column: "SenderId",
                principalSchema: "chat",
                principalTable: "Chatters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDirectChats_Chatters_ChatterId",
                schema: "chat",
                table: "UserDirectChats",
                column: "ChatterId",
                principalSchema: "chat",
                principalTable: "Chatters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
