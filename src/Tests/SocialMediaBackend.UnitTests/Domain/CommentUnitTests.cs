using Shouldly;
using SocialMediaBackend.Domain.Comments;
using SocialMediaBackend.Domain.Users;
using Tests.Core.Common.Comments;
using Tests.Core.Common;
using SocialMediaBackend.Domain.Posts;

namespace SocialMediaBackend.UnitTests.Domain;

public class CommentUnitTests : TestBase
{
    [Fact]
    public void Create_ShouldReturnComment()
    {
        // Arrange
        var userId = UserId.New();
        var postId = PostId.New();
        var text = "This is a new comment";

        // Act
        var comment = Comment.Create(postId, userId, text, null);

        // Assert
        comment.ShouldNotBeNull();
        comment!.Text.ShouldBe(text);
        comment.UserId.ShouldBe(userId);
    }

    [Fact]
    public void UpdateComment_ShouldUpdateText_AndSetLastModified()
    {
        // Arrange
        var comment = CommentFactory.Create();
        var text = "Updated comment content";

        var beforeUpdate = TimeProvider.System.GetUtcNow();

        // Act
        var result = comment.Edit(text);

        // Assert
        result.ShouldBeTrue();
        comment.Text.ShouldBe(text);
        comment.LastModified.ShouldBeGreaterThan(beforeUpdate);
        comment.LastModifiedBy.ShouldBe(comment.Id.ToString());
    }

    [Fact]
    public void AddLike_ShouldWork_WhenUserHasNotLikedYet()
    {
        // Arrange
        var comment = CommentFactory.Create();
        var userId = UserId.New();

        // Act
        var like = comment.AddLike(userId);

        // Assert
        like.ShouldNotBeNull();
        comment.LikesCount.ShouldBe(1);
        comment.Likes.Single().UserId.ShouldBe(userId);
        comment.Likes.Single().CommentId.ShouldBe(comment.Id);
    }

    [Fact]
    public void AddLike_ShouldReturnNull_WhenUserAlreadyLiked()
    {
        // Arrange
        var comment = CommentFactory.Create();
        var userId = UserId.New();
        comment.AddLike(userId);

        // Act
        var like = comment.AddLike(userId);

        // Assert
        like.ShouldBeNull();
        comment.LikesCount.ShouldBe(1);
    }

    [Fact]
    public void RemoveLike_ShouldRemoveLike_WhenUserLiked()
    {
        // Arrange
        var comment = CommentFactory.Create();
        var userId = UserId.New();
        comment.AddLike(userId);

        // Act
        var result = comment.RemoveLike(userId);

        // Assert
        result.ShouldBeTrue();
        comment.LikesCount.ShouldBe(0);
        comment.Likes?.ShouldBeEmpty();
    }

    [Fact]
    public void RemoveLike_ShouldReturnFalse_WhenUserHasNotLiked()
    {
        // Arrange
        var comment = CommentFactory.Create();
        var userId = UserId.New();

        // Act
        var result = comment.RemoveLike(userId);

        // Assert
        result.ShouldBeFalse();
        comment.LikesCount.ShouldBe(0);
        comment.Likes?.ShouldBeEmpty();
    }

    [Fact]
    public async Task AddReply_ShouldReturnReply()
    {
        //Arrange
        var comment = CommentFactory.Create();
        var replyText = "text";
        var replier = UserId.New();

        _dbContext.Add(comment);

        //Act
        var reply = comment.AddReply(replier, replyText);
        _dbContext.Add(reply);
        await _dbContext.SaveChangesAsync();

        //Assert
        reply.ShouldNotBeNull();
        comment.RepliesCount.ShouldBe(1);
        comment.Replies?.ShouldNotBeEmpty();
        comment.Replies!.Count.ShouldBe(1);
        reply.ParentCommentId.ShouldBe(comment.Id);
        reply.ParentComment.ShouldBe(comment);
        reply.UserId.ShouldBe(replier);
        reply.PostId.ShouldBe(comment.PostId);
        reply.Text.ShouldBe(replyText);
    }

    [Fact]
    public async Task RemoveReply_ShouldWork_WhenReplyExists()
    {
        //Arrange
        var (comment, reply) = await _dbContext.CreateCommentWithReplyAsync();

        //Act
        var removed = comment.RemoveReply(reply.Id);

        //Assert
        removed.ShouldBeTrue();
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
        removed.ShouldBeFalse();
        comment.RepliesCount.ShouldBe(0);
        comment.Replies?.ShouldBeEmpty();
    }
}
