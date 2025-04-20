namespace SocialMediaBackend.Application.Common;

public enum HandlerResponseStatus
{
    OK, Created, Deleted, NoContent, Modified,
    BadRequest, Conflict, InternalError, NotFound,
    NotModified, Unauthorized
}
