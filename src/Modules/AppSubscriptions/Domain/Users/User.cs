using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.AppSubscriptions.Domain.Users;

public sealed class User : AggregateRoot<UserId>
{
    private User() {}

    public User(UserId userId) 
    {
        this.Id = userId;
    }
}
