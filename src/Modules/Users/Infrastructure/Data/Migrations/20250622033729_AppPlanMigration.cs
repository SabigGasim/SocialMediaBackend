using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMediaBackend.Modules.Users.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AppPlanMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppSubscriptionProducts",
                schema: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Tier = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSubscriptionProducts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppSubscriptionPlan",
                schema: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PriceAmount = table.Column<int>(type: "integer", nullable: false),
                    PriceCurrency = table.Column<int>(type: "integer", nullable: false),
                    PaymentInterval = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSubscriptionPlan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSubscriptionPlan_AppSubscriptionProducts_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "users",
                        principalTable: "AppSubscriptionProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppSubscriptionPlan_ProductId",
                schema: "users",
                table: "AppSubscriptionPlan",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSubscriptionPlan",
                schema: "users");

            migrationBuilder.DropTable(
                name: "AppSubscriptionProducts",
                schema: "users");
        }
    }
}
