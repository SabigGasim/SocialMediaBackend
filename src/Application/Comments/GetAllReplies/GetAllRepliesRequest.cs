using Microsoft.AspNetCore.Mvc;
using SocialMediaBackend.Application.Abstractions;

namespace SocialMediaBackend.Application.Comments.GetAllReplies;

public record GetAllRepliesRequest([FromRoute]Guid ParentId) : PagedRequest;
