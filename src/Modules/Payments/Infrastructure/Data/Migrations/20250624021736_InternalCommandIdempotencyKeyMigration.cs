using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMediaBackend.Modules.Payments.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InternalCommandIdempotencyKeyMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdempotencyKey",
                schema: "payments",
                table: "InternalCommands",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InternalCommands_IdempotencyKey",
                schema: "payments",
                table: "InternalCommands",
                column: "IdempotencyKey",
                unique: true)
                .Annotation("Npgsql:NullsDistinct", true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_InternalCommands_IdempotencyKey",
                schema: "payments",
                table: "InternalCommands");

            migrationBuilder.DropColumn(
                name: "IdempotencyKey",
                schema: "payments",
                table: "InternalCommands");
        }
    }
}
