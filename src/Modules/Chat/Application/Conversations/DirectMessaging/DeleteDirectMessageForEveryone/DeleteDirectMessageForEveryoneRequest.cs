﻿namespace SocialMediaBackend.Modules.Chat.Application.Conversations.DirectMessaging.DeleteDirectMessageForEveryone;

public record DeleteDirectMessageForEveryoneRequest(Guid ChatId, Guid MessageId);
