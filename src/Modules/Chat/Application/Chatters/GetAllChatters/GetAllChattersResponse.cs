using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.Modules.Chat.Application.Chatters.GetChatter;

namespace SocialMediaBackend.Modules.Chat.Application.Chatters.GetAllChatters;

public record GetAllChattersResponse : PagedResponse<GetChatterShortResponse>
{
    public GetAllChattersResponse(
        int PageNumber,
        int PageSize,
        int TotalCount,
        IEnumerable<GetChatterShortResponse> Items) : base(PageNumber, PageSize, TotalCount, Items)
    {
        
    }
}
