using Refit;
using SocialMediaBackend.Application.Comments.CreateComment;
using SocialMediaBackend.Application.Comments.DeleteComment;
using SocialMediaBackend.Application.Comments.EditComment;
using SocialMediaBackend.Application.Comments.GetAllPostComments;
using SocialMediaBackend.Application.Comments.GetAllReplies;
using SocialMediaBackend.Application.Comments.GetComment;
using SocialMediaBackend.Application.Comments.LikeComment;
using SocialMediaBackend.Application.Comments.ReplyToComment;
using SocialMediaBackend.Application.Comments.UnlikeComment;
using SocialMediaBackend.Application.Posts.CreatePost;
using SocialMediaBackend.Application.Posts.DeletePost;
using SocialMediaBackend.Application.Posts.GetAllPosts;
using SocialMediaBackend.Application.Posts.GetPost;
using SocialMediaBackend.Application.Posts.LikePost;
using SocialMediaBackend.Application.Posts.UnlikePost;
using SocialMediaBackend.Application.Posts.UpdatePost;
using SocialMediaBackend.Application.Users.CreateUser;
using SocialMediaBackend.Application.Users.DeleteUser;
using SocialMediaBackend.Application.Users.Follows.AcceptFollowRequest;
using SocialMediaBackend.Application.Users.Follows.FollowUser;
using SocialMediaBackend.Application.Users.Follows.RejectFollowRequet;
using SocialMediaBackend.Application.Users.Follows.UnfollowUser;
using SocialMediaBackend.Application.Users.GetAllUsers;
using SocialMediaBackend.Application.Users.GetFullUserDetails;
using SocialMediaBackend.Application.Users.GetUser;
using SocialMediaBackend.Application.Users.Privacy.ChangeProfileVisibility;
using SocialMediaBackend.Application.Users.UpdateNickname;
using SocialMediaBackend.Application.Users.UpdateUsername;

namespace SocialMediaBackend.Sdk;

[Headers("Authorization Bearer")]
public interface ISocialMediaApi
{
    #region Users

    [Post(ApiEndpoints.Users.Create)]
    Task<CreateUserResponse> CreateUserAsync(CreateUserRequest request);

    [Delete(ApiEndpoints.Users.Delete)]
    Task DeleteUserAsync(DeleteUserRequest request);

    [Get(ApiEndpoints.Users.GetAll)]
    Task<GetAllUsersResponse> GetAllUsersResponse(GetAllUsersRequest request);

    [Get(ApiEndpoints.Users.Get)]
    Task<GetUserResponse> GetUserAsync(GetUserRequest request);

    [Get(ApiEndpoints.Users.Me)]
    Task<GetFullUserDetailsResponse> GetThisUserAsync();

    [Patch(ApiEndpoints.Users.PatchUsername)]
    Task UpdateUsernameAsync(UpdateUsernameRequest request);

    [Patch(ApiEndpoints.Users.PatchNickname)]
    Task UpdateNicknameAsync(UpdateNicknameRequest request);

    #region Follows

    [Post(ApiEndpoints.Users.AcceptFollow)]
    Task AcceptFollowRequestAsync(AcceptFollowRequestRequest request);

    [Post(ApiEndpoints.Users.Follow)]
    Task FollowUserAsync(FollowUserRequest request);

    [Post(ApiEndpoints.Users.RejectFollow)]
    Task RejectFollowAsync(RejectFollowRequestRequest request);

    [Delete(ApiEndpoints.Users.Unfollow)]
    Task UnfollowUserAsync(UnfollowUserRequest request);

    #endregion
    #region Privacy

    [Patch(ApiEndpoints.Users.Privacy.ChangeProfileVisibility)]
    Task SetPrivacyVisibilityAsync(ChangeProfileVisibilityRequest request);

    #endregion
    #endregion
    #region Posts

    [Post(ApiEndpoints.Posts.Create)]
    Task<CreatePostResponse> CreatePostAsync(CreatePostRequest request);

    [Delete(ApiEndpoints.Posts.Delete)]
    Task DeletePostAsync(DeletePostRequest request);

    [Get(ApiEndpoints.Posts.Get)]
    Task<GetPostResponse> GetPostAsync(GetPostRequest request);

    [Get(ApiEndpoints.Posts.GetAll)]
    Task<GetAllPostsResponse> GetAllPostsAsync(GetAllPostsRequest request);

    [Post(ApiEndpoints.Posts.Like)]
    Task LikePostAsync(LikePostRequest request);

    [Delete(ApiEndpoints.Posts.Unlike)]
    Task UnlikePostAsync(UnlikePostRequest request);

    [Patch(ApiEndpoints.Posts.Patch)]
    Task UpdatePostAsync(UpdatePostRequest request);

    #endregion
    #region Comments

    [Post(ApiEndpoints.Posts.Comment)]
    Task<CreateCommentResponse> CreateCommentAsync(CreateCommentRequest request);

    [Delete(ApiEndpoints.Posts.DeleteComment)]
    Task DeleteCommentAsync(DeleteCommentRequest request);

    [Patch(ApiEndpoints.Comments.Patch)]
    Task UpdateCommentAsync(EditCommentRequest request);

    [Get(ApiEndpoints.Comments.Get)]
    Task<GetCommentResponse> GetCommentAsync(GetCommentRequest request);
    
    [Get(ApiEndpoints.Posts.GetAllPostComments)]
    Task<GetAllPostCommentsResponse> GetAllCommentAsync(GetAllPostCommentsRequest request);

    [Post(ApiEndpoints.Comments.Like)]
    Task LikeCommentAsync(LikeCommentRequest request);

    [Delete(ApiEndpoints.Comments.Unlike)]
    Task UnlikeCommentAsync(UnlikeCommentRequest request);
    
    [Post(ApiEndpoints.Comments.Reply)]    
    Task<ReplyToCommentResponse> ReplyToCommentResponseAsync(ReplyToCommentRequest request);

    [Get(ApiEndpoints.Comments.GetReplies)]
    Task<GetAllRepliesResponse> GetRepliesAsync(GetAllRepliesRequest request);

    #endregion
}
