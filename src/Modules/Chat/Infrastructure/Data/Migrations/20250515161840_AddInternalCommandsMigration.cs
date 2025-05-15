using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMediaBackend.Modules.Chat.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddInternalCommandsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "chat");

            migrationBuilder.CreateTable(
                name: "InternalCommands",
                schema: "chat",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Data = table.Column<string>(type: "text", nullable: false),
                    Error = table.Column<string>(type: "text", nullable: true),
                    Processed = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    ProcessedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    EnqueueDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternalCommands", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InternalCommands_Id",
                schema: "chat",
                table: "InternalCommands",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InternalCommands",
                schema: "chat");
        }
    }
}
