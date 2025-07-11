﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;

#nullable disable

namespace SocialMediaBackend.Modules.Users.Infrastructure.Data.Migrations
{
    [DbContext(typeof(UsersDbContext))]
    [Migration("20250622033729_AppPlanMigration")]
    partial class AppPlanMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("users")
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SocialMediaBackend.Modules.Users.Domain.AppPlan.AppSubscriptionProduct", b =>
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

                    b.ToTable("AppSubscriptionProducts", "users");
                });

            modelBuilder.Entity("SocialMediaBackend.Modules.Users.Domain.Users.Follows.Follow", b =>
                {
                    b.Property<Guid>("FollowerId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("FollowingId")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("FollowerId", "FollowingId")
                        .HasName("Id");

                    b.HasIndex("FollowingId");

                    b.ToTable("Follows", "users");
                });

            modelBuilder.Entity("SocialMediaBackend.Modules.Users.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<int>("FollowersCount")
                        .HasColumnType("integer");

                    b.Property<int>("FollowingCount")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset>("LastModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("ProfileIsPublic")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users", "users");
                });

            modelBuilder.Entity("SocialMediaBackend.Modules.Users.Domain.AppPlan.AppSubscriptionProduct", b =>
                {
                    b.OwnsMany("SocialMediaBackend.Modules.Users.Domain.AppPlan.AppSubscriptionPlan", "Plans", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("ProductId")
                                .HasColumnType("uuid");

                            b1.HasKey("Id");

                            b1.HasIndex("ProductId");

                            b1.ToTable("AppSubscriptionPlan", "users");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");

                            b1.OwnsOne("SocialMediaBackend.Modules.Payments.Contracts.ProductPrice", "Price", b2 =>
                                {
                                    b2.Property<Guid>("AppSubscriptionPlanId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("PaymentInterval")
                                        .HasColumnType("integer")
                                        .HasColumnName("PaymentInterval");

                                    b2.HasKey("AppSubscriptionPlanId");

                                    b2.ToTable("AppSubscriptionPlan", "users");

                                    b2.WithOwner()
                                        .HasForeignKey("AppSubscriptionPlanId");

                                    b2.OwnsOne("SocialMediaBackend.Modules.Payments.Contracts.MoneyValue", "MoneyValue", b3 =>
                                        {
                                            b3.Property<Guid>("ProductPriceAppSubscriptionPlanId")
                                                .HasColumnType("uuid");

                                            b3.Property<int>("Amount")
                                                .HasColumnType("integer")
                                                .HasColumnName("PriceAmount");

                                            b3.Property<int>("Currency")
                                                .HasColumnType("integer")
                                                .HasColumnName("PriceCurrency");

                                            b3.HasKey("ProductPriceAppSubscriptionPlanId");

                                            b3.ToTable("AppSubscriptionPlan", "users");

                                            b3.WithOwner()
                                                .HasForeignKey("ProductPriceAppSubscriptionPlanId");
                                        });

                                    b2.Navigation("MoneyValue")
                                        .IsRequired();
                                });

                            b1.Navigation("Price")
                                .IsRequired();
                        });

                    b.Navigation("Plans");
                });

            modelBuilder.Entity("SocialMediaBackend.Modules.Users.Domain.Users.Follows.Follow", b =>
                {
                    b.HasOne("SocialMediaBackend.Modules.Users.Domain.Users.User", "Follower")
                        .WithMany("Followings")
                        .HasForeignKey("FollowerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SocialMediaBackend.Modules.Users.Domain.Users.User", "Following")
                        .WithMany("Followers")
                        .HasForeignKey("FollowingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Follower");

                    b.Navigation("Following");
                });

            modelBuilder.Entity("SocialMediaBackend.Modules.Users.Domain.Users.User", b =>
                {
                    b.OwnsOne("SocialMediaBackend.BuildingBlocks.Domain.ValueObjects.Media", "ProfilePicture", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid");

                            b1.Property<string>("FilePath")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("ProfilePictureFilePath");

                            b1.Property<int>("MediaType")
                                .HasColumnType("integer")
                                .HasColumnName("ProfilePictureMediaType");

                            b1.Property<string>("Url")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("ProfilePictureUrl");

                            b1.HasKey("UserId");

                            b1.ToTable("Users", "users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("ProfilePicture")
                        .IsRequired();
                });

            modelBuilder.Entity("SocialMediaBackend.Modules.Users.Domain.Users.User", b =>
                {
                    b.Navigation("Followers");

                    b.Navigation("Followings");
                });
#pragma warning restore 612, 618
        }
    }
}
