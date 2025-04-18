﻿using Microsoft.AspNetCore.Authorization;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Application.Posts.GetAllPosts;
using SocialMediaBackend.Infrastructure.Domain.Posts;

namespace SocialMediaBackend.Api.Endpoints.Posts;

[FastEndpoints.HttpGet(ApiEndpoints.Posts.GetAll), AllowAnonymous]
public class GetAllPostsEndpoints : RequestEndpoint<GetAllPostsRequest, GetAllPostsResponse>
{
    public override Task HandleAsync(GetAllPostsRequest req, CancellationToken ct)
    {
        var order = req.Order?.Trim().ToLower() switch
        {
            "asc" => Order.Ascending,
            "desc" => Order.Descending,
            _ => Order.Unordered
        };

        var query = new GetAllPostsQuery(req.Page, req.PageSize, order,
            req.Text, req.Since, req.Until, req.IdOrUsername);

        return HandleRequestAsync(query, ct);
    }
}
