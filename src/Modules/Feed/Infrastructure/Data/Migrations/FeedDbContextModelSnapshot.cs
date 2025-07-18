﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SocialMediaBackend.Modules.Feed.Infrastructure.Data;

#nullable disable

namespace SocialMediaBackend.Modules.Feed.Infrastructure.Data.Migrations
{
    [DbContext(typeof(FeedDbContext))]
    partial class FeedDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("feed")
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

                    b.ToTable("InternalCommands", "feed");
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

                    b.ToTable("InboxMessages", "feed");
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

                    b.ToTable("OutboxMessages", "feed");
                });

            modelBuilder.Entity("SocialMediaBackend.Modules.Feed.Domain.Authorization.Permission", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Permissions", "feed");

                    b.HasData(
                        new
                        {
                            Id = 3,
                            Name = "Permissions.Posts.Get"
                        },
                        new
                        {
                            Id = 0,
                            Name = "Permissions.Posts.Create"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Permissions.Posts.GetAll"
                        },
                        new
                        {
                            Id = 1,
                            Name = "Permissions.Posts.Delete"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Permissions.Posts.Update"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Permissions.Posts.Like"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Permissions.Posts.Unlike"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Permissions.Comments.Create"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Permissions.Comments.Update"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Permissions.Comments.Delete"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Permissions.Comments.Get"
                        },
                        new
                        {
                            Id = 11,
                            Name = "Permissions.Comments.GetAll"
                        },
                        new
                        {
                            Id = 13,
                            Name = "Permissions.Comments.GetAllReplies"
                        },
                        new
                        {
                            Id = 12,
                            Name = "Permissions.Comments.Reply"
                        },
                        new
                        {
                            Id = 14,
                            Name = "Permissions.Comments.Like"
                        },
                        new
                        {
                            Id = 15,
                            Name = "Permissions.Comments.Unlike"
                        });
                });

            modelBuilder.Entity("SocialMediaBackend.Modules.Feed.Domain.Authorization.Role", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Roles", "feed");

                    b.HasData(
                        new
                        {
                            Id = 0,
                            Name = "AuthorRole"
                        },
                        new
                        {
                            Id = 2,
                            Name = "AppBasicPlanRole"
                        },
                        new
                        {
                            Id = 3,
                            Name = "AppPlusPlanRole"
                        },
                        new
                        {
                            Id = 1,
                            Name = "AdminAuthorRole"
                        });
                });

            modelBuilder.Entity("SocialMediaBackend.Modules.Feed.Domain.Authors.Author", b =>
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

                    b.ToTable("Authors", "feed");
                });

            modelBuilder.Entity("SocialMediaBackend.Modules.Feed.Domain.Comments.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("LastModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<int>("LikesCount")
                        .HasColumnType("integer");

                    b.Property<Guid?>("ParentCommentId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PostId")
                        .HasColumnType("uuid");

                    b.Property<int>("RepliesCount")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("ParentCommentId");

                    b.HasIndex("PostId");

                    b.ToTable("Comments", "feed");
                });

            modelBuilder.Entity("SocialMediaBackend.Modules.Feed.Domain.Comments.CommentLike", b =>
                {
                    b.Property<Guid>("CommentId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("CommentId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("CommentLike", "feed");
                });

            modelBuilder.Entity("SocialMediaBackend.Modules.Feed.Domain.Follows.Follow", b =>
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

                    b.ToTable("Follows", "feed");
                });

            modelBuilder.Entity("SocialMediaBackend.Modules.Feed.Domain.Posts.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<int>("CommentsCount")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("LastModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<int>("LikesCount")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Posts", "feed");
                });

            modelBuilder.Entity("SocialMediaBackend.Modules.Feed.Domain.Posts.PostLike", b =>
                {
                    b.Property<Guid>("PostId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("PostId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("PostLike", "feed");
                });

            modelBuilder.Entity("SocialMediaBackend.Modules.Feed.Infrastructure.Domain.Roles.AuthorRole", b =>
                {
                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("AuthorId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AuthorRoles", "feed");
                });

            modelBuilder.Entity("SocialMediaBackend.Modules.Feed.Infrastructure.Domain.Roles.RolePermission", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<int>("PermissionId")
                        .HasColumnType("integer");

                    b.HasKey("RoleId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.ToTable("RolePermissions", "feed");

                    b.HasData(
                        new
                        {
                            RoleId = 0,
                            PermissionId = 3
                        },
                        new
                        {
                            RoleId = 0,
                            PermissionId = 0
                        },
                        new
                        {
                            RoleId = 0,
                            PermissionId = 2
                        },
                        new
                        {
                            RoleId = 0,
                            PermissionId = 1
                        },
                        new
                        {
                            RoleId = 0,
                            PermissionId = 6
                        },
                        new
                        {
                            RoleId = 0,
                            PermissionId = 4
                        },
                        new
                        {
                            RoleId = 0,
                            PermissionId = 5
                        },
                        new
                        {
                            RoleId = 0,
                            PermissionId = 7
                        },
                        new
                        {
                            RoleId = 0,
                            PermissionId = 8
                        },
                        new
                        {
                            RoleId = 0,
                            PermissionId = 9
                        },
                        new
                        {
                            RoleId = 0,
                            PermissionId = 10
                        },
                        new
                        {
                            RoleId = 0,
                            PermissionId = 11
                        },
                        new
                        {
                            RoleId = 0,
                            PermissionId = 13
                        },
                        new
                        {
                            RoleId = 0,
                            PermissionId = 12
                        },
                        new
                        {
                            RoleId = 0,
                            PermissionId = 14
                        },
                        new
                        {
                            RoleId = 0,
                            PermissionId = 15
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 3
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 0
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 2
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 1
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 6
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 4
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 5
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 7
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 8
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 9
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 10
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 11
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 13
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 12
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 14
                        },
                        new
                        {
                            RoleId = 1,
                            PermissionId = 15
                        });
                });

            modelBuilder.Entity("SocialMediaBackend.Modules.Feed.Domain.Authors.Author", b =>
                {
                    b.OwnsOne("SocialMediaBackend.BuildingBlocks.Domain.ValueObjects.Media", "ProfilePicture", b1 =>
                        {
                            b1.Property<Guid>("AuthorId")
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

                            b1.HasKey("AuthorId");

                            b1.ToTable("Authors", "feed");

                            b1.WithOwner()
                                .HasForeignKey("AuthorId");
                        });

                    b.Navigation("ProfilePicture")
                        .IsRequired();
                });

            modelBuilder.Entity("SocialMediaBackend.Modules.Feed.Domain.Comments.Comment", b =>
                {
                    b.HasOne("SocialMediaBackend.Modules.Feed.Domain.Authors.Author", "Author")
                        .WithMany("Comments")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SocialMediaBackend.Modules.Feed.Domain.Comments.Comment", "ParentComment")
                        .WithMany("Replies")
                        .HasForeignKey("ParentCommentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SocialMediaBackend.Modules.Feed.Domain.Posts.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("ParentComment");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("SocialMediaBackend.Modules.Feed.Domain.Comments.CommentLike", b =>
                {
                    b.HasOne("SocialMediaBackend.Modules.Feed.Domain.Comments.Comment", "Comment")
                        .WithMany("Likes")
                        .HasForeignKey("CommentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SocialMediaBackend.Modules.Feed.Domain.Authors.Author", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comment");
                });

            modelBuilder.Entity("SocialMediaBackend.Modules.Feed.Domain.Follows.Follow", b =>
                {
                    b.HasOne("SocialMediaBackend.Modules.Feed.Domain.Authors.Author", "Follower")
                        .WithMany("Followings")
                        .HasForeignKey("FollowerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SocialMediaBackend.Modules.Feed.Domain.Authors.Author", "Following")
                        .WithMany("Followers")
                        .HasForeignKey("FollowingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Follower");

                    b.Navigation("Following");
                });

            modelBuilder.Entity("SocialMediaBackend.Modules.Feed.Domain.Posts.Post", b =>
                {
                    b.HasOne("SocialMediaBackend.Modules.Feed.Domain.Authors.Author", "Author")
                        .WithMany("Posts")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("SocialMediaBackend.BuildingBlocks.Domain.ValueObjects.Media", "MediaItems", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uuid");

                            b1.Property<string>("FilePath")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("FilePath");

                            b1.Property<int>("MediaType")
                                .HasColumnType("integer")
                                .HasColumnName("MediaType");

                            b1.Property<Guid>("PostId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Url")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Url");

                            b1.HasKey("Id");

                            b1.HasIndex("PostId");

                            b1.ToTable("Posts_MediaItems", "feed");

                            b1.WithOwner()
                                .HasForeignKey("PostId");
                        });

                    b.Navigation("Author");

                    b.Navigation("MediaItems");
                });

            modelBuilder.Entity("SocialMediaBackend.Modules.Feed.Domain.Posts.PostLike", b =>
                {
                    b.HasOne("SocialMediaBackend.Modules.Feed.Domain.Posts.Post", "Post")
                        .WithMany("Likes")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SocialMediaBackend.Modules.Feed.Domain.Authors.Author", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");
                });

            modelBuilder.Entity("SocialMediaBackend.Modules.Feed.Infrastructure.Domain.Roles.AuthorRole", b =>
                {
                    b.HasOne("SocialMediaBackend.Modules.Feed.Domain.Authors.Author", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SocialMediaBackend.Modules.Feed.Domain.Authorization.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("SocialMediaBackend.Modules.Feed.Infrastructure.Domain.Roles.RolePermission", b =>
                {
                    b.HasOne("SocialMediaBackend.Modules.Feed.Domain.Authorization.Permission", "Permission")
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SocialMediaBackend.Modules.Feed.Domain.Authorization.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("SocialMediaBackend.Modules.Feed.Domain.Authors.Author", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Followers");

                    b.Navigation("Followings");

                    b.Navigation("Posts");
                });

            modelBuilder.Entity("SocialMediaBackend.Modules.Feed.Domain.Comments.Comment", b =>
                {
                    b.Navigation("Likes");

                    b.Navigation("Replies");
                });

            modelBuilder.Entity("SocialMediaBackend.Modules.Feed.Domain.Posts.Post", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Likes");
                });
#pragma warning restore 612, 618
        }
    }
}
