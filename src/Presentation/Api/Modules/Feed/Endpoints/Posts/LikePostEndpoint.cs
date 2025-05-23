﻿using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Feed.Application.Contracts;
using SocialMediaBackend.Modules.Feed.Application.Posts.LikePost;

namespace SocialMediaBackend.Api.Modules.Feed.Endpoints.Posts;

public class LikePostEndpoint(IFeedModule module) : RequestEndpoint<LikePostRequest>(module)
{
    public override void Configure()
    {
        Post(ApiEndpoints.Posts.Like);
        Description(x => x.Accepts<LikePostRequest>());
    }

    public override Task HandleAsync(LikePostRequest req, CancellationToken ct)
    {
        return HandleCommandAsync(new LikePostCommand(req.PostId), ct);
    }
}
