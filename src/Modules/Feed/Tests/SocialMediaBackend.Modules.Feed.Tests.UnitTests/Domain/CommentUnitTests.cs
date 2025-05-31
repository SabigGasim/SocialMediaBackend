using Shouldly;
using Microsoft.Extensions.DependencyInjection;
using SocialMediaBackend.Modules.Feed.Tests.Core.Common;
using SocialMediaBackend.Modules.Feed.Domain.Posts;
using SocialMediaBackend.Modules.Feed.Domain.Authors;
using SocialMediaBackend.Modules.Feed.Domain.Comments;
using SocialMediaBackend.Modules.Feed.Tests.Core.Common.Comments;
using SocialMediaBackend.Modules.Feed.Infrastructure.Configuration;
using Autofac;
using SocialMediaBackend.Modules.Feed.Infrastructure.Data;
using SocialMediaBackend.Modules.Feed.Tests.Core.Common.Users;

namespace SocialMediaBackend.Modules.Feed.Tests.UnitTests.Domain;

public class CommentUnitTests(AuthFixture auth, App app) : AppTestBase(auth, app)
{
    [Fact]
    public void Create_ShouldReturnComment()
    {
        // Arrange
        var userId = AuthorId.New();
        var postId = PostId.New();
        var text = "This is a new comment";

        // Act
        var comment = Comment.Create(postId, userId, text, null);

        // Assert
        comment.ShouldNotBeNull();
        comment.Text.ShouldBe(text);
        comment.AuthorId.ShouldBe(userId);
    }

    [Fact]
    public void UpdateComment_ShouldUpdateText_AndSetLastModified()
    {
        // Arrange
        var comment = CommentFactory.Create();
        var text = "Updated comment content";

        var beforeUpdate = TimeProvider.System.GetUtcNow();

        // Act
        comment.Edit(text);

        // Assert
        comment.Text.ShouldBe(text);
        comment.LastModified.ShouldBeGreaterThan(beforeUpdate);
        comment.LastModifiedBy.ShouldBe(comment.Id.ToString());
    }

    [Fact]
    public void AddLike_ShouldWork_WhenUserHasNotLikedYet()
    {
        // Arrange
        var comment = CommentFactory.Create();
        var userId = AuthorId.New();

        // Act
        var like = comment.AddLike(userId);

        // Assert
        like.IsSuccess.ShouldBeTrue();
        comment.LikesCount.ShouldBe(1);
        comment.Likes.Single().UserId.ShouldBe(userId);
        comment.Likes.Single().CommentId.ShouldBe(comment.Id);
    }

    [Fact]
    public void AddLike_ShouldReturnNull_WhenUserAlreadyLiked()
    {
        // Arrange
        var comment = CommentFactory.Create();
        var userId = AuthorId.New();
        comment.AddLike(userId);

        // Act
        var like = comment.AddLike(userId);

        // Assert
        like.IsSuccess.ShouldBeFalse();
        comment.LikesCount.ShouldBe(1);
    }

    [Fact]
    public void RemoveLike_ShouldRemoveLike_WhenUserLiked()
    {
        // Arrange
        var comment = CommentFactory.Create();
        var userId = AuthorId.New();
        comment.AddLike(userId);

        // Act
        var result = comment.RemoveLike(userId);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        comment.LikesCount.ShouldBe(0);
        comment.Likes?.ShouldBeEmpty();
    }

    [Fact]
    public void RemoveLike_ShouldReturnFalse_WhenUserHasNotLiked()
    {
        // Arrange
        var comment = CommentFactory.Create();
        var userId = AuthorId.New();

        // Act
        var result = comment.RemoveLike(userId);

        // Assert
        result.IsSuccess.ShouldBeFalse();
        comment.LikesCount.ShouldBe(0);
        comment.Likes?.ShouldBeEmpty();
    }

    [Fact]
    public async Task AddReply_ShouldReturnReply()
    {
        //Arrange
        await using var scope = FeedCompositionRoot.BeginLifetimeScope();
        var context = scope.Resolve<FeedDbContext>();

        var comment = await context.CreateCommentAsync(ct: TestContext.Current.CancellationToken);
        var replyText = "text";
        var replier = AuthorFactory.Create();

        context.Add(replier);

        //Act
        var reply = comment.AddReply(replier.Id, replyText);
        context.Add(reply);
        await context.SaveChangesAsync(TestContext.Current.CancellationToken);

        //Assert
        reply.ShouldNotBeNull();
        comment.RepliesCount.ShouldBe(1);
        comment.Replies.ShouldNotBeEmpty();
        comment.Replies.Count.ShouldBe(1);
        reply.ParentCommentId.ShouldBe(comment.Id);
        reply.ParentComment.ShouldBe(comment);
        reply.AuthorId.ShouldBe(replier.Id);
        reply.PostId.ShouldBe(comment.PostId);
        reply.Text.ShouldBe(replyText);
    }

    [Fact]
    public async Task RemoveReply_ShouldWork_WhenReplyExists()
    {
        //Arrange
        await using var scope = FeedCompositionRoot.BeginLifetimeScope();
        var context = scope.Resolve<FeedDbContext>();

        var (comment, reply) = await context.CreateCommentWithReplyAsync(TestContext.Current.CancellationToken);

        //Act
        var removed = comment.RemoveReply(reply.Id);

        //Assert
        removed.IsSuccess.ShouldBeTrue();
        comment.RepliesCount.ShouldBe(0);
        comment.Replies?.ShouldBeEmpty();
    }

    [Fact]
    public void RemoveReply_ShouldFail_WhenReplyDoesntExists()
    {
        //Arrange
        var comment = CommentFactory.Create();

        //Act
        var removed = comment.RemoveReply(CommentId.New());

        //Assert
        removed.IsSuccess.ShouldBeFalse();
        comment.RepliesCount.ShouldBe(0);
        comment.Replies?.ShouldBeEmpty();
    }
}
