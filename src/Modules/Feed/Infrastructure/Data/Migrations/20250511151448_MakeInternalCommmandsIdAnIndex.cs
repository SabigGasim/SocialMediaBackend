using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMediaBackend.Modules.Feed.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class MakeInternalCommmandsIdAnIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_InternalCommands_Id",
                schema: "feed",
                table: "InternalCommands",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_InternalCommands_Id",
                schema: "feed",
                table: "InternalCommands");
        }
    }
}
