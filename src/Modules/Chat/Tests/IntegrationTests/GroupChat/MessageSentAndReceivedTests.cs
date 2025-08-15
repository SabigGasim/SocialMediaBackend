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
using Xunit.Internal;

namespace SocialMediaBackend.Modules.Chat.Tests.IntegrationTests.GroupChat;

public class MessageSentAndReceivedFlowRaceConditionTests(ITestOutputHelper output, App app) 
    : AppTestBase(app)
{
    private readonly ITestOutputHelper _output = output;
    private readonly App _app = app;
    private static HttpClient _userClient = default!;
    private static HubConnection _adminHubClient = default!;
    private static HubConnection[] _adminHubClients = default!;
    private static Guid _userId = Guid.Empty;
    private static readonly SemaphoreSlim _setupLocker = new(1, 1);

    private const int _devicesCount = 5;

    protected override async ValueTask SetupAsync()
    {
        await base.SetupAsync();

        await InitializeAsync(_devicesCount);
    }

    [Theory()]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(50)]
    [InlineData(1000)]
    public async Task MessageSentAndReceivedFlow_ShouldWork_WhenAdminReadsMessages_AfterReceivingThemAll(int messagesCount)
    {
        var messagesMarkedAsReceived = 0;
        var locker = new SemaphoreSlim(1, 1);
        await locker.WaitAsync(CancellationToken.None);

        // 1. Create a group chat between the two users
        var createGroupChatReq = new CreateGroupChatRequest(
            Name: "GroupName",
            Members: [AdminId.Value, _userId]);

        var (groupChatResult, groupChatRespone) = await _app.Client
            .POSTAsync<CreateGroupChatEndpoint,
                       CreateGroupChatRequest,
                       CreateGroupChatResponse>(createGroupChatReq);

        groupChatResult.EnsureSuccessStatusCode();

        Guid groupChatId = groupChatRespone.Id;

        // 2. Subscribe to this group chat via SignalR
        using var subscription = _adminHubClient.On(ChatHubMethods.ReceiveGroupMessage, async (CreateGroupMessageMessage msg) =>
        {
            await SendMarkMessageAsReceivedRequest(msg);
            messagesMarkedAsReceived++;
            if (messagesMarkedAsReceived == messagesCount)
            {
                locker.Release();
            }

            _output.WriteLine("Message received: {0}", msg.Text);
        });

        // 3. The new user sends messages to the group chat
        var sendMessageTasks = Enumerable.Range(0, messagesCount)
            .Select(i => SendMessageToGroupChat(_userClient, groupChatId, $"Message {i}"));

        await Task.WhenAll(sendMessageTasks);

        GroupMessageId lastMessageId = await GetLastMessageId(groupChatId);

        // 4. Admin marks all messages as seen via ChatHub
        await WaitForHubClientToTriggerMessageReceived(locker);

        await _adminHubClient.InvokeAsync(ChatHubMethods.MarkGroupMessageAsSeen, groupChatId, CancellationToken.None);

        // 5. Assert
        var messages = await GetAllGroupMessages(groupChatId);

        messages.Count.ShouldBe(messagesCount);

        foreach (var message in messages)
        {
            message.SeenBy.Single(x => x.Id == AdminId).ShouldNotBeNull();
        }

        var adminGroup = await GetAdminGroupChat(groupChatId);

        adminGroup.LastSeenMessageId.ShouldBe(lastMessageId);
        adminGroup.LastReceivedMessageId.ShouldBe(lastMessageId);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(50)]
    [InlineData(1000)]
    public async Task MessageSentAndReceivedFlow_ShouldWork_WhenAdminReadsMessages_RightAfterReceivingAny(int messagesCount)
    {
        var messagesMarkedAsReceived = 0;
        var locker = new SemaphoreSlim(1, 1);
        await locker.WaitAsync(CancellationToken.None);

        // 1. Create a group chat between the two users
        var createGroupChatReq = new CreateGroupChatRequest(
            Name: "GroupName",
            Members: [AdminId.Value, _userId]);

        var (groupChatResult, groupChatRespone) = await _app.Client
            .POSTAsync<CreateGroupChatEndpoint,
                       CreateGroupChatRequest,
                       CreateGroupChatResponse>(createGroupChatReq);

        groupChatResult.EnsureSuccessStatusCode();

        Guid groupChatId = groupChatRespone.Id;

        // 2. Subscribe to this group chat via SignalR
        using var subscription = _adminHubClient.On(ChatHubMethods.ReceiveGroupMessage, async (CreateGroupMessageMessage msg) =>
        {
            await SendMarkMessageAsReceivedRequest(msg);
            await _adminHubClient.InvokeAsync(ChatHubMethods.MarkGroupMessageAsSeen, groupChatId, CancellationToken.None);
            
            messagesMarkedAsReceived++;
            if (messagesMarkedAsReceived == messagesCount)
            {
                locker.Release();
            }

            _output.WriteLine("Message received: {0}", msg.Text);
        });

        // 3. The new user sends messages to the group chat
        var sendMessageTasks = Enumerable.Range(0, messagesCount)
            .Select(i => SendMessageToGroupChat(_userClient, groupChatId, $"Message {i}"))
            .ToArray();

        await Task.WhenAll(sendMessageTasks);

        GroupMessageId lastMessageId = await GetLastMessageId(groupChatId);
        
        // 4. Assert
        await WaitForHubClientToTriggerMessageReceived(locker);

        var messages = await GetAllGroupMessages(groupChatId);

        messages.Count.ShouldBe(messagesCount);

        foreach (var message in messages)
        {
            message.SeenBy.Single(x => x.Id == AdminId).ShouldNotBeNull();
        }

        var adminGroup = await GetAdminGroupChat(groupChatId);

        adminGroup.LastSeenMessageId.ShouldBe(lastMessageId);
        adminGroup.LastReceivedMessageId.ShouldBe(lastMessageId);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(50)]
    [InlineData(1000)]
    public async Task MessageSentAndReceivedFlow_ShouldWork_WhenAdminReadsMessages_RightAfterReceivingAny_FromMultipleDevices(int messagesCount)
    {
        var messagesMarkedAsReceived = 0;
        var locker = new SemaphoreSlim(1, 1);
        await locker.WaitAsync(CancellationToken.None);

        // 1. Create a group chat between the two users
        var createGroupChatReq = new CreateGroupChatRequest(
            Name: "GroupName",
            Members: [AdminId.Value, _userId]);

        var (groupChatResult, groupChatRespone) = await _app.Client
            .POSTAsync<CreateGroupChatEndpoint,
                       CreateGroupChatRequest,
                       CreateGroupChatResponse>(createGroupChatReq);

        groupChatResult.EnsureSuccessStatusCode();

        Guid groupChatId = groupChatRespone.Id;

        // 2. Subscribe to this group chat via SignalR
        var taskLimiterSemaphor = new SemaphoreSlim(Environment.ProcessorCount * 2); // To prevent task exhaustion

        Task SendMarkMessageAsReceivedAndSeenAsync(CreateGroupMessageMessage msg, HubConnection hub, int device)
        {
            _ = Task.Run(async () =>
            {
                await taskLimiterSemaphor.WaitAsync(CancellationToken.None);
                
                await SendMarkMessageAsReceivedRequest(msg);
                await hub.SendAsync(ChatHubMethods.MarkGroupMessageAsSeen, groupChatId, CancellationToken.None);
                if (++messagesMarkedAsReceived == messagesCount * _devicesCount)
                {
                    locker.Release();
                }

                taskLimiterSemaphor.Release();
            });

            _output.WriteLine("Message received: {0}, From device: {1}", msg.Text, device);

            return Task.CompletedTask;
        }

        var subscriptions = _adminHubClients.Select((hub, i) => hub.On<CreateGroupMessageMessage>(
            ChatHubMethods.ReceiveGroupMessage,
            async msg => await SendMarkMessageAsReceivedAndSeenAsync(msg, hub, i)))
            .ToArray();

        // 3. The new user sends messages to the group chat
        var sendMessageTasks = Enumerable.Range(0, messagesCount)
            .Select(i => SendMessageToGroupChat(_userClient, groupChatId, $"Message {i}"))
            .ToArray();

        await Task.WhenAll(sendMessageTasks);

        GroupMessageId lastMessageId = await GetLastMessageId(groupChatId);

        // 4. Assert
        await WaitForHubClientToTriggerMessageReceived(locker);

        subscriptions.ForEach(x => x.Dispose());

        var messages = await GetAllGroupMessages(groupChatId);

        messages.Count.ShouldBe(messagesCount);

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
        await locker.WaitAsync(CancellationToken.None);
        locker.Release();
    }

    private static async Task<UserGroupChat> GetAdminGroupChat(Guid groupChatId)
    {
        await using (var scope = ChatCompositionRoot.BeginLifetimeScope())
        {
            var db = scope.Resolve<ChatDbContext>();
            
            return await db.UserGroupChats
                .Where(x => x.ChatterId == AdminId && x.GroupChatId == new GroupChatId(groupChatId))
                .FirstAsync(CancellationToken.None);
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
                .ToListAsync(CancellationToken.None);
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
                .FirstAsync(CancellationToken.None);

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

    private async Task InitializeAsync(int devicesCount)
    {
        if (_userId != Guid.Empty)
        {
            return;
        }

        await _setupLocker.WaitAsync(CancellationToken.None);
        if (_userId == Guid.Empty)
        {
            _userId = Guid.NewGuid();
            var userToken = await CreateUserAndTokenAsync(_userId);
            _userClient = BuildHttpClient(userToken);
            _adminHubClient = BuildSignalRClient(AdminAuthToken);

            _adminHubClients = new HubConnection[devicesCount];

            for (int i = 0; i < devicesCount; i++)
            {
                _adminHubClients[i] = BuildSignalRClient(AdminAuthToken);
            }

            await StartHubClients([.. _adminHubClients, _adminHubClient]);
        }
        _setupLocker.Release();
    }
}