using SocialMediaBackend.Application.Common;
using System.Net;

namespace SocialMediaBackend.Api.Mappings;

public static class ApplicationToApiMappings
{
    public static HttpStatusCode MapToHttpStatusCode(this HandlerResponseStatus status)
    {
        return status switch
        {
            HandlerResponseStatus.OK => HttpStatusCode.OK,                         
            HandlerResponseStatus.Created => HttpStatusCode.Created,               
            HandlerResponseStatus.Deleted => HttpStatusCode.NoContent,                    
            HandlerResponseStatus.NoContent => HttpStatusCode.NoContent,           
            HandlerResponseStatus.Modified => HttpStatusCode.OK,                   
            HandlerResponseStatus.BadRequest => HttpStatusCode.BadRequest,         
            HandlerResponseStatus.Conflict => HttpStatusCode.Conflict,             
            HandlerResponseStatus.InternalError => HttpStatusCode.InternalServerError,
            HandlerResponseStatus.NotFound => HttpStatusCode.NotFound,
            HandlerResponseStatus.NotModified => HttpStatusCode.NotModified,
    
            _ => HttpStatusCode.InternalServerError
        };
    }
}
