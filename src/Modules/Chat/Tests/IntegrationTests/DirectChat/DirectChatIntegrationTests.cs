using Autofac;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using SocialMediaBackend.Api.Modules.Chat.Endpoints;
using SocialMediaBackend.BuildingBlocks.Tests;
using SocialMediaBackend.Modules.Chat.Application.Conversations.DirectMessaging.CreateDirectChat;
using SocialMediaBackend.Modules.Chat.Application.Conversations.DirectMessaging.CreateDirectMessage;
using SocialMediaBackend.Modules.Chat.Application.Conversations.DirectMessaging.GetAllDirectMessages;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.DirectChats;
using SocialMediaBackend.Modules.Chat.Infrastructure.Configuration;
using SocialMediaBackend.Modules.Chat.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Chat.Tests.IntegrationTests.DirectChat;

public class DirectChatIntegrationTests(AuthFixture authFixture, App app) : AppTestBase(authFixture, app)
{
    private readonly App _app = app;

    [Fact]
    public async Task GetAllDirectMessages_ShouldReturnViewableMessages()
    {
        //Arrange
        var ct = TestContext.Current.CancellationToken;

        int messagesPerChat = 20;
        int chats = 6;
        int adminViewMessagesToDelete = 30;
        int otherChattersViewMessagesToDelete = 50;

        var chatterIds = await CreateChattersAsync(numberOfChatters: chats, ct);
        var directChats = await SendCreateDirectChatRequests(chatterIds);
        await SendDirectMessageRequets(messagesPerChat, directChats);

        int totalMessages = 0;

        await using (var scope = ChatCompositionRoot.BeginLifetimeScope())
        {
            var context = scope.Resolve<ChatDbContext>();

            totalMessages = await context.DirectMessages.CountAsync(x => x.SenderId == AdminId, ct);

            var userDirectChats = await context.UserDirectChats
                .Include(x => x.Messages)
                .Where(x => chatterIds.Any(c => c == x.ChatterId) || x.ChatterId == AdminId)
                .ToArrayAsync(ct);

            DeleteMessages(adminViewMessagesToDelete, otherChattersViewMessagesToDelete, userDirectChats);

            await context.SaveChangesAsync(ct);
        }

        //Act
        var userDirectMessages = await SendGetAllDirectMessagesRequest(messagesPerChat, directChats);

        //Assert
        var viewableMessagesCount = messagesPerChat * chats - adminViewMessagesToDelete;

        userDirectMessages.Length.ShouldBe(viewableMessagesCount);

        totalMessages.ShouldBe(messagesPerChat * chats);
    }

    private async Task<GetDirectMessageResponse[]> SendGetAllDirectMessagesRequest(int messagesPerChat, CreateDirectChatResponse[] directChats)
    {
        var getAllDirectMessagesTasks = directChats.Select(chat =>
        {
            var request = new GetAllDirectMessagesRequest(chat.ChatId)
            {
                Page = 1,
                PageSize = messagesPerChat,
            };

            return _app.Client.GETAsync<
                    GetAllDirectMessagesEndpoint,
                    GetAllDirectMessagesRequest,
                    GetAllDirectMessagesResponse>(request);
        });

        return (await Task.WhenAll(getAllDirectMessagesTasks)).Select(x => x.Result).SelectMany(x => x.Messages).ToArray();
    }

    private async Task SendDirectMessageRequets(int messagesPerChat, IEnumerable<CreateDirectChatResponse> directChats)
    {
        var sendDirectMessageRequests = directChats
                    .SelectMany(chat =>
                    {
                        return ArrayOf(messagesPerChat).Select(_ =>
                        {
                            return _app.Client.POSTAsync<
                                SendDirectMessageEndpoint,
                                SendDirectMessageRequest,
                                SendDirectMessageResponse>(new SendDirectMessageRequest(
                                    Text: TextHelper.CreateRandom(10),
                                    chat.ChatId));
                        });
                    });

        await Task.WhenAll(sendDirectMessageRequests);
    }

    private static async Task<ChatterId[]> CreateChattersAsync(int numberOfChatters, CancellationToken ct)
    {
        var tasks = ArrayOf(numberOfChatters).Select(_ => CreateChatterAsync(ct));
        return (await Task.WhenAll(tasks)).ToArray();
    }

    private async Task<CreateDirectChatResponse[]> SendCreateDirectChatRequests(IEnumerable<ChatterId> chatterIds)
    {
        var createDirectChatRequests = chatterIds
            .Select(x =>
            {
                return _app.Client.POSTAsync<
                    CreateDirectChatEndpoint,
                    CreateDirectChatRequest,
                    CreateDirectChatResponse>(new CreateDirectChatRequest(x.Value));
            });

        return (await Task.WhenAll(createDirectChatRequests)).Select(x => x.Result).ToArray();
    }

    private static IEnumerable<int> ArrayOf(int messagesPerChat)
    {
        return Enumerable.Range(0, messagesPerChat);
    }

    private static void DeleteMessages(
        int chatterViewMessagesToDelete, int otherChattersViewMessagesToDelete, UserDirectChat[] userDirectChats)
    {
        var otherChattersMessageViews = userDirectChats
            .Where(x => x.ChatterId != AdminId)
            .SelectMany(x => x.Messages)
            .Take(otherChattersViewMessagesToDelete)
            .ToArray();

        var chatterMessageView = userDirectChats
            .Where(x => x.ChatterId == AdminId)
            .SelectMany(x => x.Messages)
            .Take(chatterViewMessagesToDelete)
            .ToArray();

        foreach (var message in otherChattersMessageViews)
        {
            message.UserDirectChat.DeleteMessage(message.DirectMessageId);
        }

        foreach (var message in chatterMessageView)
        {
            message.UserDirectChat.DeleteMessage(message.DirectMessageId);
        }
    }
}
