using SocialMediaBackend.BuildingBlocks.Domain.ValueObjects;
using SocialMediaBackend.BuildingBlocks.Tests;
using SocialMediaBackend.Modules.Chat.Application.Chatters.CreateChatter;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;

namespace SocialMediaBackend.Modules.Chat.Tests.Core.Helpers;

public static class CreateChatterCommandFactory
{
    public static CreateChatterCommand Create()
    {
        return new CreateChatterCommand(
            Guid.NewGuid(),
            ChatterId.New(),
            username: TextHelper.CreateRandom(8),
            nickname: TextHelper.CreateRandom(8),
            Media.Create(Media.DefaultProfilePicture.Url, Media.DefaultProfilePicture.FilePath),
            profileIsPublic: true,
            followersCount: 0,
            followingCount: 0
            );
    }

    public static List<CreateChatterCommand> CreateMany(int number)
    {
        var list = new List<CreateChatterCommand>(number);

        for (int i = 0; i < number; i++)
        {
            list.Add(Create());
        }

        return list;
    }
}
