using SocialMediaBackend.BuildingBlocks.Domain.ValueObjects;
using Shouldly;
using SocialMediaBackend.Modules.Feed.Tests.Core.Common;
using SocialMediaBackend.Modules.Feed.Domain.Authors;
using SocialMediaBackend.Modules.Feed.Domain.Posts;
using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Feed.Tests.Core.Common.Posts;

namespace SocialMediaBackend.Modules.Feed.Tests.UnitTests.Domain;

public class PostUnitTests(App app) : AppTestBase(app)
{
    [Fact]
    public void Create_ShouldReturnPost_WhenTextIsProvided()
    {
        // Arrange
        var userId = AuthorId.New();
        var text = "This is a new post";

        // Act
        var post = Post.Create(userId, text);

        // Assert
        post.IsSuccess.ShouldBeTrue();
        post.Payload.Text.ShouldBe(text);
        post.Payload.AuthorId.ShouldBe(userId);
    }

    [Fact]
    public void Create_ShouldReturnPost_WhenMediaIsProvided()
    {
        // Arrange
        var userId = AuthorId.New();
        var mediaItems = new List<Media> { Media.Create("image.jpg") };

        // Act
        var post = Post.Create(userId, null, mediaItems);

        // Assert
        post.IsSuccess.ShouldBeTrue();
        post.Payload.MediaItems.ShouldNotBeEmpty();
        post.Payload.MediaItems.Count.ShouldBe(1);
        post.Payload.AuthorId.ShouldBe(userId);
    }

    [Fact]
    public void Create_ShouldThrow_WhenNeitherTextNorMediaProvided()
    {
        // Arrange
        var userId = AuthorId.New();

        // Act & Assert
        Should.Throw<BusinessRuleValidationException>(() => Post.Create(userId, null, null));
        Should.Throw<BusinessRuleValidationException>(() => Post.Create(userId, null, []));
    }

    [Fact]
    public void UpdatePost_ShouldUpdateText_AndSetLastModified()
    {
        // Arrange
        var post = PostFactory.Create();
        var text = "Updated post content";

        var beforeUpdate = TimeProvider.System.GetUtcNow();

        // Act
        var result = post.UpdatePost(text);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        post.Text.ShouldBe(text);
        post.LastModified.ShouldBeGreaterThan(beforeUpdate);
        post.LastModifiedBy.ShouldBe(post.Id.ToString());
    }

    [Fact]
    public void AddLike_ShouldWork_WhenUserHasNotLikedYet()
    {
        // Arrange
        var post = PostFactory.Create();
        var userId = AuthorId.New();

        // Act
        var like = post.AddLike(userId);

        // Assert
        like.IsSuccess.ShouldBeTrue();
        post.LikesCount.ShouldBe(1);
        post.Likes.Single().UserId.ShouldBe(userId);
        post.Likes.Single().PostId.ShouldBe(post.Id);
    }

    [Fact]
    public void AddLike_ShouldReturnNull_WhenUserAlreadyLiked()
    {
        // Arrange
        var post = PostFactory.Create();
        var userId = AuthorId.New();
        post.AddLike(userId);

        // Act
        var like = post.AddLike(userId);

        // Assert
        like.IsSuccess.ShouldBeFalse();
        post.LikesCount.ShouldBe(1);
    }

    [Fact]
    public void RemoveLike_ShouldRemoveLike_WhenUserLiked()
    {
        // Arrange
        var post = PostFactory.Create();
        var userId = AuthorId.New();
        post.AddLike(userId);

        // Act
        var result = post.RemoveLike(userId);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        post.LikesCount.ShouldBe(0);
        post.Likes?.ShouldBeEmpty();
    }

    [Fact]
    public void RemoveLike_ShouldReturnFalse_WhenUserHasNotLiked()
    {
        // Arrange
        var post = PostFactory.Create();
        var userId = AuthorId.New();

        // Act
        var result = post.RemoveLike(userId);

        // Assert
        result.IsSuccess.ShouldBeFalse();
        post.LikesCount.ShouldBe(0);
        post.Likes?.ShouldBeEmpty();
    }
}
