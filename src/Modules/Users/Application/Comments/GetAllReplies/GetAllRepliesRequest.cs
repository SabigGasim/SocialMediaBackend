using Microsoft.AspNetCore.Mvc;
using SocialMediaBackend.Modules.Users.Application.Abstractions;

namespace SocialMediaBackend.Modules.Users.Application.Comments.GetAllReplies;

public record GetAllRepliesRequest([FromRoute]Guid ParentId) : PagedRequest;
