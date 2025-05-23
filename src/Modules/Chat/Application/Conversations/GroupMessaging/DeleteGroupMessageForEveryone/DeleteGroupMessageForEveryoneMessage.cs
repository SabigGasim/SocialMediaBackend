﻿using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands.Realtime;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.DeleteGroupMessageForEveryone;

public record DeleteGroupMessageMessage(Guid MessageId) : IRealtimeMessage;