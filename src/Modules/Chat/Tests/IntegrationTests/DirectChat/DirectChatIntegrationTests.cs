using Autofac;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using SocialMediaBackend.Api.Modules.Chat.Endpoints;
using SocialMediaBackend.BuildingBlocks.Tests;
using SocialMediaBackend.Modules.Chat.Application.Conversations.DirectMessaging.CreateDirectChat;
using SocialMediaBackend.Modules.Chat.Application.Conversations.DirectMessaging.CreateDirectMessage;
using SocialMediaBackend.Modules.Chat.Application.Conversations.DirectMessaging.GetAllDirectMessages;
using SocialMediaBackend.Modules.Chat.Application.Helpers;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.DirectChats;
using SocialMediaBackend.Modules.Chat.Infrastructure.Configuration;
using SocialMediaBackend.Modules.Chat.Infrastructure.Data;
using SocialMediaBackend.Modules.Chat.Tests.Core.Helpers;

namespace SocialMediaBackend.Modules.Chat.Tests.IntegrationTests.DirectChat;

public class DirectChatIntegrationTests(AuthFixture authFixture, App app) : AppTestBase(authFixture, app)
{
    private readonly App _app = app;

    //I don't know who you're or why are you reading this,
    //But I'm really sorry for this piece of $hit
    //Have a nice day
    //
    //
    //Nah seriously. Basically what this does is:
    //1 - Creates chatters directly through commands
    //    As this action doesn't required authorization.
    //2 - Creates direct chats between the test caller
    //    and the other created chatters through the
    //    create chatter API endpoint, as this action
    //    does required authorization and will go through
    //    the AuthCommandHandlerDecorator, which gets
    //    the UserId from the HttpContext.. etc.
    //3 - Those endpoint calls are an array of Task<TestResult<TEndpointResponse>>[]
    //    so to speed up the test, we Task.WhenAll on them.
    //4 - Same Idea for direct messages. This time we create.
    //    a list of 'messages per chat' CreateDirectChatEndpoint calls
    //    for reach direct chat created, then Task.WhenAll on them
    //5 - We calculate the number of direct messages that
    //    exist in the database.
    //6 - We delete 'chatterViewMessagesToDelete' number of messages
    //    from the chatter's UserDirectMessages, so the messages
    //    the user can see. This table is what the user queries
    //    which allows for the 'Delete message only for me' feature,
    //    so when a user deletes a specific message from his
    //    chat view, it's doesn't always affect how the other user
    //    sees the chats from his view.
    //7 - We create a list of 'number of chats' GetAllDirectMessagesEndpoint
    //    calls and Task.WhenAll on them. This should return all
    //    of the test user direct messages view.
    //8 - We calculate the number of messages retrieved.
    //9 - We assert that:
    //    a) Total number of messages in the database
    //       is equal to 'number of chats' * 'messages per chat'
    //    b) The number of messages in the test user's view equals
    //       'number of chats' * 'messages per chat' - 'deleted messages from the user's view'
    //         
    //10 - If you reached this point, kudos to you bro.
    //     Have an actual nice day
    [Fact]
    public async Task GetAllDirectMessages_ShouldReturnViewableMessages()
    {
        //Arrange
        var ct = TestContext.Current.CancellationToken;

        int messagesPerChat = 20;
        int chats = 6;
        int chatterViewMessagesToDelete = 30;
        int otherChattersViewMessagesToDelete = 50;

        var createChatterCommands = CreateChatterCommandFactory.CreateMany(chats);

        var createChatterTasks = createChatterCommands
            .Select(x => CommandExecutor.ExecuteAsync(x, ct))
            .ToArray();

        await Task.WhenAll(createChatterTasks);

        var createDirectChatTasks = createChatterCommands
            .Select(x =>
            {
                return _app.Client.POSTAsync<
                    CreateDirectChatEndpoint,
                    CreateDirectChatRequest,
                    CreateDirectChatResponse>(new CreateDirectChatRequest(x.ChatterId.Value));
            })
            .ToArray();

        var sendDirectMessagesTasks = (await Task.WhenAll(createDirectChatTasks))
            .SelectMany(result =>
            {
                var (_, rsp) = result;

                return ArrayOf(messagesPerChat).Select(_ =>
                {
                    return _app.Client.POSTAsync<
                        SendDirectMessageEndpoint,
                        SendDirectMessageRequest,
                        SendDirectMessageResponse>(new SendDirectMessageRequest(
                            TextHelper.CreateRandom(10),
                            rsp.ChatId));
                });
            })
            .ToArray();

        await Task.WhenAll(sendDirectMessagesTasks);

        int totalMessages = 0;

        await using(var scope = ChatCompositionRoot.BeginLifetimeScope())
        {
            var context = scope.Resolve<ChatDbContext>();

            totalMessages = await context.DirectMessages
                .CountAsync(x => x.SenderId == AdminId, ct);

            var directChats = await context.UserDirectChats
                .Include(x => x.Messages)
                .ToListAsync(ct);

            DeleteMessages(chatterViewMessagesToDelete, otherChattersViewMessagesToDelete, directChats);

            await context.SaveChangesAsync(ct);
        }

        //Act
        var getAllDirectMessagesTasks = createDirectChatTasks.Select(async task =>
        {
            var (res, rsp) = await task;

            var result = await res.Content.ReadAsStringAsync(ct);

            var request = new GetAllDirectMessagesRequest(rsp.ChatId)
            {
                Page = 1,
                PageSize = messagesPerChat,
            };

            return _app.Client.GETAsync<
                    GetAllDirectMessagesEndpoint,
                    GetAllDirectMessagesRequest,
                    GetAllDirectMessagesResponse>(request);
        })
            .ToArray();

        await Task.WhenAll(getAllDirectMessagesTasks);

        int directMessagesCount = 0;

        foreach(var task in getAllDirectMessagesTasks)
        {
            var (_, rsp) = await await task;

            directMessagesCount += rsp.Messages.Count();
        }

        //Assert
        directMessagesCount.ShouldBe(messagesPerChat * chats - chatterViewMessagesToDelete);
        
        totalMessages.ShouldBe(messagesPerChat * chats);
    }

    private static IEnumerable<int> ArrayOf(int messagesPerChat)
    {
        return Enumerable.Range(0, messagesPerChat);
    }

    private static void DeleteMessages(int chatterViewMessagesToDelete, int otherChattersViewMessagesToDelete, List<UserDirectChat> directChats)
    {
        var otherDirectChats = directChats.Where(x => x.ChatterId != AdminId);
        var chatterDirectChats = directChats.Where(x => x.ChatterId == AdminId);

        var otherChattersMessageViews = otherDirectChats
            .SelectMany(x => x.Messages)
            .ToList()[..otherChattersViewMessagesToDelete];

        var chatterMessageView = chatterDirectChats
            .SelectMany(x => x.Messages)
            .ToList()[..chatterViewMessagesToDelete];

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
