using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.BuildingBlocks.Infrastructure.Messaging;
using SocialMediaBackend.Modules.Chat.Domain.Authorization;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.DirectChats;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;
using SocialMediaBackend.Modules.Chat.Domain.Messages.DirectMessages;
using SocialMediaBackend.Modules.Chat.Domain.Messages.GroupMessages;

namespace SocialMediaBackend.Modules.Chat.Infrastructure.Data;

public class ChatDbContext : DbContext
{
    public DbSet<Chatter> Chatters { get; set; } = default!;
    public DbSet<GroupChat> GroupChats { get; set; } = default!;
    public DbSet<GroupMessage> GroupMessages { get; set; } = default!;
    public DbSet<UserGroupChat> UserGroupChats { get; set; } = default!;
    public DbSet<UserGroupMessage> UserGroupMessages { get; set; } = default!;
    public DbSet<DirectChat> DirectChats { get; set; } = default!;
    public DbSet<DirectMessage> DirectMessages { get; set; } = default!;
    public DbSet<UserDirectChat> UserDirectChats { get; set; } = default!;
    public DbSet<UserDirectMessage> UserDirectMessages { get; set; } = default!;

    public DbSet<Role> Roles { get; set; } = default!;
    public DbSet<InternalCommand> InternalCommands { get; set; } = default!;
    public DbSet<OutboxMessage> OutboxMessages { get; set; } = default!;
    public DbSet<InboxMessage> InboxMessages { get; set; } = default!;

    protected ChatDbContext() { }

    public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema.Chat);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IInfrastructureMarker).Assembly);
    }
}
