﻿namespace SocialMediaBackend.Modules.Users.Domain.Users.Follows;

public class UserUnfollowedEvent(UserId followerId, UserId followingId) : FollowEventBase(followerId, followingId);