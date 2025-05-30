﻿using NSubstitute;
using SocialMediaBackend.BuildingBlocks.Domain.ValueObjects;
using SocialMediaBackend.BuildingBlocks.Tests;
using SocialMediaBackend.Modules.Users.Domain.Services;
using SocialMediaBackend.Modules.Users.Domain.Users;

namespace SocialMediaBackend.Modules.Users.Tests.Core.Common;

public static class UserFactory
{
    public static async Task<User> CreateAsync(
        string? username = null,
        string nickname = "user",
        bool isPublic = true,
        DateTime? dateTimeOfBirth = null,
        IUserExistsChecker? userExistsChecker = null,
        CancellationToken ct = default)
    {
        var dateOfBirth = GetDateOfBirth(dateTimeOfBirth);
        var service = GetUserExistsService(userExistsChecker);

        username = username ?? TextHelper.CreateRandom(8);
        var result = await User.CreateAsync(username, nickname, dateOfBirth, service, Media.Create(Media.DefaultProfilePicture.Url), ct);

        var user = result.Payload;

        user.ChangeProfilePrivacy(isPublic);
        user.ClearDomainEvents();

        return user!;
    }

    private static DateOnly GetDateOfBirth(DateTime? dateOfBirth)
    {
        return dateOfBirth is not null
                    ? DateOnly.FromDateTime((DateTime)dateOfBirth)
                    : DateOnly.Parse("2000/01/01");
    }

    private static IUserExistsChecker GetUserExistsService(IUserExistsChecker? service)
    {
        if (service is null)
        {
            var mockService = Substitute.For<IUserExistsChecker>();
            mockService.CheckAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns(false);
            service = mockService;
        }

        return service;
    }
}