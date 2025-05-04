using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeparateUsersFromAuthor4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostLike_Users_UserId1",
                table: "PostLike");

            migrationBuilder.DropIndex(
                name: "IX_PostLike_UserId1",
                table: "PostLike");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "PostLike");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "PostLike",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_PostLike_UserId1",
                table: "PostLike",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_PostLike_Users_UserId1",
                table: "PostLike",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
