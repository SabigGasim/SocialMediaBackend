namespace SocialMediaBackend.BuildingBlocks.Application;

public enum HandlerResponseStatus
{
    OK, Created, Deleted, NoContent, Modified,
    BadRequest, Conflict, InternalError, NotFound,
    NotModified, Unauthorized, Timeout, NotSupported
}
