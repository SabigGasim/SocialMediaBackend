using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class SubscriptionRenewalPaymentsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubscriptionRenewalPayments",
                schema: "app_subscriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PayerId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubscriptionId = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.PrimaryKey("PK_SubscriptionRenewalPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionRenewalPayments_Subscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalSchema: "app_subscriptions",
                        principalTable: "Subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubscriptionRenewalPayments_Users_PayerId",
                        column: x => x.PayerId,
                        principalSchema: "app_subscriptions",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "app_subscriptions",
                table: "Permissions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 4, "Permissions.AppPlan.RenewSubscription" },
                    { 5, "Permissions.AppPlan.CancelSubscription" },
                    { 6, "Permissions.AppPlan.CancelSubscriptionAtPeriodEnd" },
                    { 7, "Permissions.AppPlan.ReactivateSubscription" }
                });

            migrationBuilder.InsertData(
                schema: "app_subscriptions",
                table: "RolePermissions",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 4, 0 },
                    { 6, 0 },
                    { 7, 0 },
                    { 4, 1 },
                    { 5, 1 },
                    { 6, 1 },
                    { 7, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionRenewalPayments_PayerId",
                schema: "app_subscriptions",
                table: "SubscriptionRenewalPayments",
                column: "PayerId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionRenewalPayments_SubscriptionId",
                schema: "app_subscriptions",
                table: "SubscriptionRenewalPayments",
                column: "SubscriptionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubscriptionRenewalPayments",
                schema: "app_subscriptions");

            migrationBuilder.DeleteData(
                schema: "app_subscriptions",
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 4, 0 });

            migrationBuilder.DeleteData(
                schema: "app_subscriptions",
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 6, 0 });

            migrationBuilder.DeleteData(
                schema: "app_subscriptions",
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 7, 0 });

            migrationBuilder.DeleteData(
                schema: "app_subscriptions",
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                schema: "app_subscriptions",
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                schema: "app_subscriptions",
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 6, 1 });

            migrationBuilder.DeleteData(
                schema: "app_subscriptions",
                table: "RolePermissions",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 7, 1 });

            migrationBuilder.DeleteData(
                schema: "app_subscriptions",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "app_subscriptions",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "app_subscriptions",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "app_subscriptions",
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7);
        }
    }
}
