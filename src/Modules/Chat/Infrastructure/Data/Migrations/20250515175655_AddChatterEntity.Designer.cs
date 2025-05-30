﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SocialMediaBackend.Modules.Chat.Infrastructure.Data;

#nullable disable

namespace SocialMediaBackend.Modules.Chat.Infrastructure.Data.Migrations
{
    [DbContext(typeof(ChatDbContext))]
    [Migration("20250515175655_AddChatterEntity")]
    partial class AddChatterEntity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("chat")
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

                    b.ToTable("InternalCommands", "chat");
                });

            modelBuilder.Entity("SocialMediaBackend.Modules.Chat.Domain.Chatters.Chatter", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

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

                    b.ToTable("Chatters", "chat");
                });

            modelBuilder.Entity("SocialMediaBackend.Modules.Chat.Domain.Follows.Follow", b =>
                {
                    b.Property<Guid>("FollowerId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("FollowingId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("FollowedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("FollowedAt");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("FollowerId", "FollowingId")
                        .HasName("Id");

                    b.HasIndex("FollowingId");

                    b.ToTable("Follows", "chat");
                });

            modelBuilder.Entity("SocialMediaBackend.Modules.Chat.Domain.Chatters.Chatter", b =>
                {
                    b.OwnsOne("SocialMediaBackend.BuildingBlocks.Domain.ValueObjects.Media", "ProfilePicture", b1 =>
                        {
                            b1.Property<Guid>("ChatterId")
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

                            b1.HasKey("ChatterId");

                            b1.ToTable("Chatters", "chat");

                            b1.WithOwner()
                                .HasForeignKey("ChatterId");
                        });

                    b.Navigation("ProfilePicture")
                        .IsRequired();
                });

            modelBuilder.Entity("SocialMediaBackend.Modules.Chat.Domain.Follows.Follow", b =>
                {
                    b.HasOne("SocialMediaBackend.Modules.Chat.Domain.Chatters.Chatter", "Follower")
                        .WithMany("Followings")
                        .HasForeignKey("FollowerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SocialMediaBackend.Modules.Chat.Domain.Chatters.Chatter", "Following")
                        .WithMany("Followers")
                        .HasForeignKey("FollowingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Follower");

                    b.Navigation("Following");
                });

            modelBuilder.Entity("SocialMediaBackend.Modules.Chat.Domain.Chatters.Chatter", b =>
                {
                    b.Navigation("Followers");

                    b.Navigation("Followings");
                });
#pragma warning restore 612, 618
        }
    }
}
