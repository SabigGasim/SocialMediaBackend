using SocialMediaBackend.Modules.Users.Domain.Common.Exceptions;
using SocialMediaBackend.Modules.Users.Domain.Common.ValueObjects;
using SocialMediaBackend.Modules.Users.Domain.Users;
using Tests.Core.Common;
using Shouldly;
using Tests.Core.Common.Posts;
using SocialMediaBackend.Modules.Users.Domain.Feed.Posts;

namespace SocialMediaBackend.Modules.Users.UnitTests.Domain;

public class PostUnitTests(AuthFixture auth, App app) : AppTestBase(auth, app)
{
    [Fact]
    public void Create_ShouldReturnPost_WhenTextIsProvided()
    {
        // Arrange
        var userId = UserId.New();
        var text = "This is a new post";

        // Act
        var post = Post.Create(userId, text);

        // Assert
        post.ShouldNotBeNull();
        post!.Text.ShouldBe(text);
        post.UserId.ShouldBe(userId);
    }

    [Fact]
    public void Create_ShouldReturnPost_WhenMediaIsProvided()
    {
        // Arrange
        var userId = UserId.New();
        var mediaItems = new List<Media> { Media.Create("image.jpg") };

        // Act
        var post = Post.Create(userId, null, mediaItems);

        // Assert
        post.ShouldNotBeNull();
        post!.MediaItems.ShouldNotBeEmpty();
        post.MediaItems.Count.ShouldBe(1);
        post.UserId.ShouldBe(userId);
    }

    [Fact]
    public void Create_ShouldThrow_WhenNeitherTextNorMediaProvided()
    {
        // Arrange
        var userId = UserId.New();

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
        result.ShouldBeTrue();
        post.Text.ShouldBe(text);
        post.LastModified.ShouldBeGreaterThan(beforeUpdate);
        post.LastModifiedBy.ShouldBe(post.Id.ToString());
    }

    [Fact]
    public void AddLike_ShouldWork_WhenUserHasNotLikedYet()
    {
        // Arrange
        var post = PostFactory.Create();
        var userId = UserId.New();

        // Act
        var like = post.AddLike(userId);

        // Assert
        like.ShouldNotBeNull();
        post.LikesCount.ShouldBe(1);
        post.Likes.Single().UserId.ShouldBe(userId);
        post.Likes.Single().PostId.ShouldBe(post.Id);
    }

    [Fact]
    public void AddLike_ShouldReturnNull_WhenUserAlreadyLiked()
    {
        // Arrange
        var post = PostFactory.Create();
        var userId = UserId.New();
        post.AddLike(userId);

        // Act
        var like = post.AddLike(userId);

        // Assert
        like.ShouldBeNull();
        post.LikesCount.ShouldBe(1);
    }

    [Fact]
    public void RemoveLike_ShouldRemoveLike_WhenUserLiked()
    {
        // Arrange
        var post = PostFactory.Create();
        var userId = UserId.New();
        post.AddLike(userId);

        // Act
        var result = post.RemoveLike(userId);

        // Assert
        result.ShouldBeTrue();
        post.LikesCount.ShouldBe(0);
        post.Likes?.ShouldBeEmpty();
    }

    [Fact]
    public void RemoveLike_ShouldReturnFalse_WhenUserHasNotLiked()
    {
        // Arrange
        var post = PostFactory.Create();
        var userId = UserId.New();

        // Act
        var result = post.RemoveLike(userId);

        // Assert
        result.ShouldBeFalse();
        post.LikesCount.ShouldBe(0);
        post.Likes?.ShouldBeEmpty();
    }
}
