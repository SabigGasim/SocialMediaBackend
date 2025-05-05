using Microsoft.AspNetCore.Mvc;
using SocialMediaBackend.BuildingBlocks.Application;

namespace SocialMediaBackend.Modules.Feed.Application.Comments.GetAllReplies;

public record GetAllRepliesRequest([FromRoute] Guid ParentId) : PagedRequest;
