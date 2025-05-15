using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using System.Text.Json.Serialization;

namespace SocialMediaBackend.Modules.Chat.Application.Chatters.DeleteChatter;

public class DeleteChatterCommand : InternalCommandBase
{
    [JsonConstructor]
    public DeleteChatterCommand(Guid id, ChatterId chatterId) : base(id)
    {
        ChatterId = chatterId;
    }

    public ChatterId ChatterId { get; }
}
