using Autofac;
using FastEndpoints;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using SocialMediaBackend.Api.Modules.Chat.Endpoints;
using SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.CreateGroupChat;
using SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.CreateGroupMessage;
using SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.MarkGroupMessageAsReceived;
using SocialMediaBackend.Modules.Chat.Domain;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;
using SocialMediaBackend.Modules.Chat.Domain.Messages.GroupMessages;
using SocialMediaBackend.Modules.Chat.Infrastructure.Configuration;
using SocialMediaBackend.Modules.Chat.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Chat.Tests.IntegrationTests.GroupChat;

public class MessageSentAndReceivedTests(AuthFixture auth, App app) : AppTestBase(auth, app)
{
    private readonly App _app = app;

    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(50)]
    [InlineData(1000)]
    public async Task MessageSentAndReceivedFlow_ShouldWorkAsExpected(int messagesCount)
    {
        var userId = Guid.NewGuid();
        var messagesMarkedAsReceived = 0;
        var locker = new SemaphoreSlim(1, 1);
        await locker.WaitAsync(TestContext.Current.CancellationToken);

        // 1. Create a second user and get their token
        var userToken = await CreateUserAndTokenAsync(userId);

        // 2. Connect both users to the ChatHub & make an http client for second user
        using var userClient = BuildHttpClient(userToken);

        await using var adminHubClient = BuildSignalRClient(AdminAuthToken);
        await using var userHubClient = BuildSignalRClient(userToken);

        adminHubClient.On(ChatHubMethods.ReceiveGroupMessage, async (CreateGroupMessageMessage msg) =>
        {
            await SendMarkMessageAsReceivedRequest(msg);
            messagesMarkedAsReceived++;
            if (messagesMarkedAsReceived == messagesCount)
            {
                locker.Release();
            }
        });

        await StartHubClients(adminHubClient, userHubClient);

        // 3. Create a group chat between the two users
        var createGroupChatReq = new CreateGroupChatRequest(
            Name: "GroupName",
            Members: [AdminId.Value, userId]);

        var (groupChatResult, groupChatRespone) = await _app.Client
            .POSTAsync<CreateGroupChatEndpoint,
                       CreateGroupChatRequest,
                       CreateGroupChatResponse>(createGroupChatReq);

        groupChatResult.EnsureSuccessStatusCode();

        var groupChatId = groupChatRespone.Id;

        // 4. The new user sends messages to the group chat
        var sendMessageTasks = Enumerable.Range(0, messagesCount)
            .Select(i => SendMessageToGroupChat(userClient, groupChatId, $"Message {i}"))
            .ToArray();

        await Task.WhenAll(sendMessageTasks);

        var lastMessageId = await GetLastMessageId(groupChatId);

        // 5. Admin marks all messages as seen via ChatHub
        await WaitForHubClientToTriggerMessageReceived(locker);

        await adminHubClient.InvokeAsync(ChatHubMethods.MarkGroupMessageAsSeen, groupChatId, TestContext.Current.CancellationToken);

        // 6. Assert all messages sent by the other user are marked as received and seen by admin
        var messages = await GetAllGroupMessages(groupChatId);

        foreach (var message in messages)
        {
            message.SeenBy.Single(x => x.Id == AdminId).ShouldNotBeNull();
        }

        var adminGroup = await GetAdminGroupChat(groupChatId);

        adminGroup.LastSeenMessageId.ShouldBe(lastMessageId);
        adminGroup.LastReceivedMessageId.ShouldBe(lastMessageId);
    }

    private static async Task WaitForHubClientToTriggerMessageReceived(SemaphoreSlim locker)
    {
        await locker.WaitAsync(TestContext.Current.CancellationToken);
        locker.Release();
    }

    private static async Task<UserGroupChat> GetAdminGroupChat(Guid groupChatId)
    {
        await using (var scope = ChatCompositionRoot.BeginLifetimeScope())
        {
            var db = scope.Resolve<ChatDbContext>();
            
            return await db.UserGroupChats
                .Where(x => x.ChatterId == AdminId && x.GroupChatId == new GroupChatId(groupChatId))
                .FirstAsync(TestContext.Current.CancellationToken);
        }
    }

    private static async Task<List<GroupMessage>> GetAllGroupMessages(Guid groupChatId)
    {
        await using (var scope = ChatCompositionRoot.BeginLifetimeScope())
        {
            var db = scope.Resolve<ChatDbContext>();

            return await db.GroupMessages
                .Include(x => x.SeenBy)
                .Where(m => m.ChatId == new GroupChatId(groupChatId))
                .ToListAsync(TestContext.Current.CancellationToken);
        }
    }

    private async Task SendMarkMessageAsReceivedRequest(CreateGroupMessageMessage msg)
    {
        var groupChatId = msg.GroupId;
        var messageId = msg.MessageId;

        var markReceivedReq = new MarkGroupMessageAsReceivedRequest(groupChatId, messageId);
        var markReceivedResp = await _app.Client
            .POSTAsync<MarkGroupMessageAsReceivedEndpoint,
                       MarkGroupMessageAsReceivedRequest>(markReceivedReq);

        markReceivedResp.EnsureSuccessStatusCode();
    }

    private static async Task<GroupMessageId> GetLastMessageId(Guid groupChatId)
    {
        await using (var scope = ChatCompositionRoot.BeginLifetimeScope())
        {
            var db = scope.Resolve<ChatDbContext>();
            var lastMessage = await db.GroupMessages
                .Where(m => m.ChatId == new GroupChatId(groupChatId))
                .OrderByDescending(x => x.Id)
                .FirstAsync(TestContext.Current.CancellationToken);

            return lastMessage.Id;
        }
    }

    private static async Task<TestResult<SendGroupMessageResponse>> SendMessageToGroupChat(
        HttpClient userClient, 
        Guid groupChatId, 
        string message)
    {
        var sendMessageRequest = new SendGroupMessageRequest(groupChatId, message);
        var result = await userClient
            .POSTAsync<SendGroupMessageEndpoint,
                       SendGroupMessageRequest,
                       SendGroupMessageResponse>(sendMessageRequest);

        result.Response.EnsureSuccessStatusCode();

        return result;
    }
}