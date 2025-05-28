using System.Text;
using System.Text.Json;
using Autofac;
using FastEndpoints;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using SocialMediaBackend.Api;
using SocialMediaBackend.Api.Modules.Chat.Endpoints;
using SocialMediaBackend.BuildingBlocks.Domain.ValueObjects;
using SocialMediaBackend.BuildingBlocks.Tests;
using SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.CreateGroupChat;
using SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.CreateGroupMessage;
using SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.MarkGroupMessageAsReceived;
using SocialMediaBackend.Modules.Chat.Domain;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;
using SocialMediaBackend.Modules.Chat.Domain.Messages.GroupMessages;
using SocialMediaBackend.Modules.Chat.Infrastructure.Configuration;
using SocialMediaBackend.Modules.Chat.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Chat.Tests.IntegrationTests.GroupChat;

public class MessageSentAndReceivedTests(AuthFixture auth, App app) : AppTestBase(auth, app)
{
    private readonly App _app = app;
    private readonly AuthFixture _auth = auth;

    [Fact]
    public async Task MessageSentAndReceivedFlow_WorksAsExpected()
    {
        // 1. Create a second user and get their token
        var (userId, userToken) = await CreateUserAndTokenAsync();

        // 2. Connect both users to the ChatHub & make an http client for second user
        using var userClient = BuildHttpClient(userToken);

        await using var adminHubClient = BuildSignalRClient(AdminAuthToken);
        await using var userHubClient = BuildSignalRClient(userToken);
        await StartHubClients(adminHubClient, userHubClient);

        // 3. Create a group chat between the two users
        var createGroupChatReq = new CreateGroupChatRequest(
            Name: "GroupName",
            Members: [AdminId.Value, Guid.Parse(userId)]);

        var (groupChatResult, groupChatRespone) = await _app.Client
            .POSTAsync<CreateGroupChatEndpoint,
                       CreateGroupChatRequest, 
                       CreateGroupChatResponse>(createGroupChatReq);

        groupChatResult.EnsureSuccessStatusCode();

        var groupChatId = groupChatRespone.Id;

        // 4. The new user sends messages to the group chat
        GroupMessageId lastMessageId = default!;
        for (int i = 0; i < 20; i++)
        {
            var sendMessageRequest = new SendGroupMessageRequest(groupChatId, $"Message {i + 1}");
            var (res, rsp) = await userClient
                .POSTAsync<SendGroupMessageEndpoint, SendGroupMessageRequest, SendGroupMessageResponse>(sendMessageRequest);

            res.EnsureSuccessStatusCode();

            lastMessageId = new(rsp.Id);
        }

        // 5. Admin marks the last received message as received
        var markReceivedReq = new MarkGroupMessageAsReceivedRequest(groupChatId, lastMessageId.Value);
        var markReceivedResp = await _app.Client
            .POSTAsync<MarkGroupMessageAsReceivedEndpoint, MarkGroupMessageAsReceivedRequest>(markReceivedReq);

        markReceivedResp.EnsureSuccessStatusCode();

        // 6. Assert UserGroupChat.LastReceivedMessageId is set for admin
        using (var scope = ChatCompositionRoot.BeginLifetimeScope())
        {
            var db = scope.Resolve<ChatDbContext>();
            var adminGroup = await db.UserGroupChats
                .Where(x => x.ChatterId == AdminId && x.GroupChatId == new GroupChatId(groupChatId))
                .FirstAsync(TestContext.Current.CancellationToken);

            lastMessageId.ShouldBe(adminGroup!.LastReceivedMessageId);
        }

        // 7. Admin marks all messages as seen via ChatHub
        await adminHubClient.InvokeAsync(ChatHubMethods.MarkGroupMessageAsSeen, groupChatId, TestContext.Current.CancellationToken);

        // 8. Assert all messages sent by the other user are marked as seen by admin
        using (var scope = ChatCompositionRoot.BeginLifetimeScope())
        {
            var db = scope.Resolve<ChatDbContext>();
            var messages = await db.GroupMessages
                .Include(x => x.SeenBy)
                .Where(m => m.ChatId == new GroupChatId(groupChatId))
                .ToListAsync(TestContext.Current.CancellationToken);

            foreach (var msg in messages)
            {
                msg.SeenBy.ShouldContain(x => x.Id == AdminId);
            }

            messages.Last().Id.ShouldBe(lastMessageId);

            var userGroupChat = await db.UserGroupChats
                .Where(x => x.ChatterId == AdminId && x.GroupChatId == new GroupChatId(groupChatId))
                .FirstAsync(TestContext.Current.CancellationToken);

            userGroupChat.LastSeenMessageId.ShouldBe(lastMessageId);
        }
    }

    private static async Task StartHubClients(params HubConnection[] clients)
    {
        await Task.WhenAll(clients.Select(x => x.StartAsync(TestContext.Current.CancellationToken)));
    }

    private HttpClient BuildHttpClient(string userToken)
    {
        return _app.CreateClient(c =>
        {
            c.DefaultRequestHeaders.Authorization = new("Bearer", userToken);
        });
    }

    private async Task<(string userId, string token)> CreateUserAndTokenAsync()
    {
        var userId = Guid.NewGuid().ToString();
        var body = new
        {
            userid = userId,
            email = $"{userId}@test.com",
            customClaims = new { admin = false }
        };

        var json = JsonSerializer.Serialize(body);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        using var client = _auth.CreateClient(o => o.BaseAddress = new Uri("https://localhost:7272"));
        var response = await client.PostAsync("/token", content);
        var token = await response.Content.ReadAsStringAsync();

        // Ensure user exists in ChatDb
        await EnsureChatterExistsAsync(userId);

        return (userId, token);
    }

    // Helper: Ensure a chatter exists in the database
    private static async Task EnsureChatterExistsAsync(string userId)
    {
        using var scope = ChatCompositionRoot.BeginLifetimeScope();
        var context = scope.Resolve<ChatDbContext>();
        var chatterId = new ChatterId(Guid.Parse(userId));
        if (!await context.Chatters.AnyAsync(x => x.Id == chatterId))
        {
            var chatter = Chatter.Create(
                chatterId,
                username: TextHelper.CreateRandom(8),
                nickname: TextHelper.CreateRandom(8),
                Media.Create(Media.DefaultProfilePicture.Url, Media.DefaultProfilePicture.FilePath),
                profileIsPublic: true,
                followersCount: 0,
                followingCount: 0
            );
            await context.Chatters.AddAsync(chatter);
            await context.SaveChangesAsync();
        }
    }

    private HubConnection BuildSignalRClient(string token)
    {
        var baseUrl = _app.Client.BaseAddress!;

        return new HubConnectionBuilder()
            .WithUrl($"{baseUrl}{ApiEndpoints.ChatHub.Connect}", options =>
            {
                options.HttpMessageHandlerFactory = _ => _app.Server.CreateHandler();
                options.AccessTokenProvider = () => Task.FromResult(token)!;
            })
            .WithAutomaticReconnect()
            .Build();
    }
}
