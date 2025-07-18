﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Data;

#nullable disable

namespace SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Data.Migrations
{
    [DbContext(typeof(SubscriptionsDbContext))]
    partial class SubscriptionsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("app_subscriptions")
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands.InternalCommand", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("EnqueueDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Error")
                        .HasColumnType("text");

                    b.Property<string>("IdempotencyKey")
                        .HasColumnType("text");

                    b.Property<bool>("Processed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<DateTimeOffset?>("ProcessedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("IdempotencyKey")
                        .IsUnique();

                    NpgsqlIndexBuilderExtensions.AreNullsDistinct(b.HasIndex("IdempotencyKey"), true);

                    b.ToTable("InternalCommands", "app_subscriptions");
                });

            modelBuilder.Entity("SocialMediaBackend.BuildingBlocks.Infrastructure.Messaging.InboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Error")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("OccurredOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Processed")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("ProcessedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Processed");

                    b.ToTable("InboxMessages", "app_subscriptions");
                });

            modelBuilder.Entity("SocialMediaBackend.BuildingBlocks.Infrastructure.Messaging.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Error")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("OccurredOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Processed")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("ProcessedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Processed");

                    b.ToTable("OutboxMessages", "app_subscriptions");
                });

            modelBuilder.Entity("SocialMediaBackend.Modules.AppSubscriptions.Domain.AppPlan.AppSubscriptionPlan", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("AppSubscriptionProductId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AppSubscriptionProductId");

                    b.ToTable("AppSubscriptionPlans", "app_subscriptions");
                });

            modelBuilder.Entity("SocialMediaBackend.Modules.AppSubscriptions.Domain.AppPlan.AppSubscriptionProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("LastModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<int>("Tier")
                        .HasColumnType("integer")
                        .HasColumnName("Tier");

                    b.HasKey("Id");

                    b.ToTable("AppSubscriptionProducts", "app_subscriptions");
                });

            modelBuilder.Entity("SocialMediaBackend.Modules.AppSubscriptions.Domain.Authorization.Permission", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Permissions", "app_subscriptions");

                    b.HasData(
                        new
                        {
                            Id = 2,
                            Name = "Permissions.AppPlan.Subscribe"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Permissions.AppPlan.Unsubscribe"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Permissions.AppPlan.RenewSubscription"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Permissions.AppPlan.CancelSubscriptionAtPeriodEnd"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Permissions.AppPlan.ReactivateSubscription"
                        },
                        new
                        {
                            Id = 1,
                            Name = "Permissions.AppPlan.CreatePlan"
                        },
                        new
                        {
                            Id = 0,
                            Name = "Permissions.AppPlan.CreateProduct"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Permissions.AppPlan.CancelSubscription"
                        });
                });

            modelBuilder.Entity("SocialMediaBackend.Modules.AppSubscriptions.Domain.Authorization.Role", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Roles", "app_subscriptions");

                    b.HasData(
                        new
                        {
                            Id = 0,
                            Name = "UserRole"
                        },
                        new
                        {
                            Id = 1,
                            Name = "AdminUserRole"
                        });
                });

            modelBuilder.Entity("SocialMediaBackend.Modules.AppSubscriptions.Domain.SubscriptionPayments.SubscriptionPayment", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("AppSubscriptionPlanId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("ExpiresAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("LastModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("PaidAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("PayerId")
                        .HasColumnType("uuid");

                    b.Property<int>("PaymentStatus")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AppSubscriptionPlanId");

                    b.HasIndex("PayerId");

                    b.ToTable("SubscriptionPayments", "app_subscriptions");
                });

            modelBuilder.Entity("SocialMediaBackend.Modules.AppSubscriptions.Domain.SubscriptionRenewalPayments.SubscriptionRenewalPayment", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("ExpiresAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("LastModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("PaidAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("PayerId")
                        .HasColumnType("uuid");

                    b.Property<int>("PaymentStatus")
                        .HasColumnType("integer");

                    b.Property<Guid>("SubscriptionId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PayerId");

                    b.HasIndex("SubscriptionId");

                    b.ToTable("SubscriptionRenewalPayments", "app_subscriptions");
                });

            modelBuilder.Entity("SocialMediaBackend.Modules.AppSubscriptions.Domain.Subscriptions.Subscription", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("ActivatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("CanceledAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("ExpiresAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("LastModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<Guid?>("SubscriberId")
                        .HasColumnType("uuid");

                    b.Property<int>("Tier")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SubscriberId")
                        .IsUnique();

                    b.ToTable("Subscriptions", "app_subscriptions");
                });

            modelBuilder.Entity("SocialMediaBackend.Modules.AppSubscriptions.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("LastModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users", "app_subscriptions");
                });

            modelBuilder.Entity("SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Domain.Roles.RolePermission", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<int>("PermissionId")
                        .HasColumnType("integer");

                    b.HasKey("RoleId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.ToTable("RolePermissions", "app_subscriptions");

                    b.HasData(
                        new
                        {
                            RoleId = 0,
                            PermissionId = 2
                        },
                        new
                        {
                            RoleId = 0,
                            PermissionId = 3
                        },
                        new
                        {
                            RoleId = 0,
                            PermissionId = 4
                        },
                        new
                        {
                            RoleId = 0,
                            PermissionId = 6
                        },
                        new
                        {
                            RoleId = 0,
                            PermissionId = 7
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 1
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 0
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 5
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 2
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 3
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 4
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 6
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 7
                        });
                });

            modelBuilder.Entity("SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Domain.Roles.UserRole", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("RoleId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles", "app_subscriptions");
                });

            modelBuilder.Entity("SocialMediaBackend.Modules.AppSubscriptions.Domain.AppPlan.AppSubscriptionPlan", b =>
                {
                    b.HasOne("SocialMediaBackend.Modules.AppSubscriptions.Domain.AppPlan.AppSubscriptionProduct", "AppSubscriptionProduct")
                        .WithMany("Plans")
                        .HasForeignKey("AppSubscriptionProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("SocialMediaBackend.Modules.Payments.Contracts.ProductPrice", "Price", b1 =>
                        {
                            b1.Property<Guid>("AppSubscriptionPlanId")
                                .HasColumnType("uuid");

                            b1.Property<int>("PaymentInterval")
                                .HasColumnType("integer")
                                .HasColumnName("PaymentInterval");

                            b1.HasKey("AppSubscriptionPlanId");

                            b1.ToTable("AppSubscriptionPlans", "app_subscriptions");

                            b1.WithOwner()
                                .HasForeignKey("AppSubscriptionPlanId");

                            b1.OwnsOne("SocialMediaBackend.Modules.Payments.Contracts.MoneyValue", "MoneyValue", b2 =>
                                {
                                    b2.Property<Guid>("ProductPriceAppSubscriptionPlanId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Amount")
                                        .HasColumnType("integer")
                                        .HasColumnName("PriceAmount");

                                    b2.Property<int>("Currency")
                                        .HasColumnType("integer")
                                        .HasColumnName("PriceCurrency");

                                    b2.HasKey("ProductPriceAppSubscriptionPlanId");

                                    b2.ToTable("AppSubscriptionPlans", "app_subscriptions");

                                    b2.WithOwner()
                                        .HasForeignKey("ProductPriceAppSubscriptionPlanId");
                                });

                            b1.Navigation("MoneyValue")
                                .IsRequired();
                        });

                    b.Navigation("AppSubscriptionProduct");

                    b.Navigation("Price")
                        .IsRequired();
                });

            modelBuilder.Entity("SocialMediaBackend.Modules.AppSubscriptions.Domain.SubscriptionPayments.SubscriptionPayment", b =>
                {
                    b.HasOne("SocialMediaBackend.Modules.AppSubscriptions.Domain.AppPlan.AppSubscriptionPlan", "AppSubscriptionPlan")
                        .WithMany()
                        .HasForeignKey("AppSubscriptionPlanId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SocialMediaBackend.Modules.AppSubscriptions.Domain.Users.User", "Payer")
                        .WithMany("SubscriptionPayments")
                        .HasForeignKey("PayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppSubscriptionPlan");

                    b.Navigation("Payer");
                });

            modelBuilder.Entity("SocialMediaBackend.Modules.AppSubscriptions.Domain.SubscriptionRenewalPayments.SubscriptionRenewalPayment", b =>
                {
                    b.HasOne("SocialMediaBackend.Modules.AppSubscriptions.Domain.Users.User", "Payer")
                        .WithMany("SubscriptionRenewalPayments")
                        .HasForeignKey("PayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SocialMediaBackend.Modules.AppSubscriptions.Domain.Subscriptions.Subscription", "Subscription")
                        .WithMany()
                        .HasForeignKey("SubscriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Payer");

                    b.Navigation("Subscription");
                });

            modelBuilder.Entity("SocialMediaBackend.Modules.AppSubscriptions.Domain.Subscriptions.Subscription", b =>
                {
                    b.HasOne("SocialMediaBackend.Modules.AppSubscriptions.Domain.Users.User", "Subscriber")
                        .WithOne("Subscription")
                        .HasForeignKey("SocialMediaBackend.Modules.AppSubscriptions.Domain.Subscriptions.Subscription", "SubscriberId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Subscriber");
                });

            modelBuilder.Entity("SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Domain.Roles.RolePermission", b =>
                {
                    b.HasOne("SocialMediaBackend.Modules.AppSubscriptions.Domain.Authorization.Permission", "Permission")
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SocialMediaBackend.Modules.AppSubscriptions.Domain.Authorization.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Domain.Roles.UserRole", b =>
                {
                    b.HasOne("SocialMediaBackend.Modules.AppSubscriptions.Domain.Authorization.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SocialMediaBackend.Modules.AppSubscriptions.Domain.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SocialMediaBackend.Modules.AppSubscriptions.Domain.AppPlan.AppSubscriptionProduct", b =>
                {
                    b.Navigation("Plans");
                });

            modelBuilder.Entity("SocialMediaBackend.Modules.AppSubscriptions.Domain.Users.User", b =>
                {
                    b.Navigation("Subscription");

                    b.Navigation("SubscriptionPayments");

                    b.Navigation("SubscriptionRenewalPayments");
                });
#pragma warning restore 612, 618
        }
    }
}
