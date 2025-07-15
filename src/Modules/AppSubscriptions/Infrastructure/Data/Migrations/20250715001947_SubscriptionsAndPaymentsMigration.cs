using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class SubscriptionsAndPaymentsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSubscriptionPlan",
                schema: "app_subscriptions");

            migrationBuilder.CreateTable(
                name: "AppSubscriptionPlans",
                schema: "app_subscriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PriceAmount = table.Column<int>(type: "integer", nullable: false),
                    PriceCurrency = table.Column<int>(type: "integer", nullable: false),
                    PaymentInterval = table.Column<int>(type: "integer", nullable: false),
                    AppSubscriptionProductId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSubscriptionPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSubscriptionPlans_AppSubscriptionProducts_AppSubscriptio~",
                        column: x => x.AppSubscriptionProductId,
                        principalSchema: "app_subscriptions",
                        principalTable: "AppSubscriptionProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                schema: "app_subscriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SubscriberId = table.Column<Guid>(type: "uuid", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Tier = table.Column<int>(type: "integer", nullable: false),
                    ActivatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ExpiresAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CanceledAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Users_SubscriberId",
                        column: x => x.SubscriberId,
                        principalSchema: "app_subscriptions",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionPayments",
                schema: "app_subscriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PayerId = table.Column<Guid>(type: "uuid", nullable: false),
                    AppSubscriptionPlanId = table.Column<Guid>(type: "uuid", nullable: false),
                    PaymentStatus = table.Column<int>(type: "integer", nullable: false),
                    PaidAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ExpiresAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionPayments_AppSubscriptionPlans_AppSubscriptionPl~",
                        column: x => x.AppSubscriptionPlanId,
                        principalSchema: "app_subscriptions",
                        principalTable: "AppSubscriptionPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubscriptionPayments_Users_PayerId",
                        column: x => x.PayerId,
                        principalSchema: "app_subscriptions",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppSubscriptionPlans_AppSubscriptionProductId",
                schema: "app_subscriptions",
                table: "AppSubscriptionPlans",
                column: "AppSubscriptionProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPayments_AppSubscriptionPlanId",
                schema: "app_subscriptions",
                table: "SubscriptionPayments",
                column: "AppSubscriptionPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPayments_PayerId",
                schema: "app_subscriptions",
                table: "SubscriptionPayments",
                column: "PayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_SubscriberId",
                schema: "app_subscriptions",
                table: "Subscriptions",
                column: "SubscriberId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubscriptionPayments",
                schema: "app_subscriptions");

            migrationBuilder.DropTable(
                name: "Subscriptions",
                schema: "app_subscriptions");

            migrationBuilder.DropTable(
                name: "AppSubscriptionPlans",
                schema: "app_subscriptions");

            migrationBuilder.CreateTable(
                name: "AppSubscriptionPlan",
                schema: "app_subscriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    PaymentInterval = table.Column<int>(type: "integer", nullable: false),
                    PriceAmount = table.Column<int>(type: "integer", nullable: false),
                    PriceCurrency = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSubscriptionPlan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSubscriptionPlan_AppSubscriptionProducts_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "app_subscriptions",
                        principalTable: "AppSubscriptionProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppSubscriptionPlan_ProductId",
                schema: "app_subscriptions",
                table: "AppSubscriptionPlan",
                column: "ProductId");
        }
    }
}
