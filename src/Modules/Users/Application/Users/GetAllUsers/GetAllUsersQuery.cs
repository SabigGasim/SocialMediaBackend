﻿using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;

namespace SocialMediaBackend.Modules.Users.Application.Users.GetAllUsers;

public sealed class GetAllUsersQuery : PagedQueryBase<GetAllUsersResponse>
{
    public GetAllUsersQuery(string slug, int page, int pageSize) : base(page, pageSize)
    {
        Slug = slug;
    }

    public string Slug { get; }
}
