using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace SocialMediaBackend.BuildingBlocks.Infrastructure.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message, Exception innerException) : base(message, innerException) { }
    public NotFoundException() : base("The requested resource was not found.") { }
    public NotFoundException(string resourceName)
        : base($"The requested {resourceName} was not found.") { }

    public static void ThrowIfNull(string resourceName, [NotNull] object? value)
    {
        if (value is null)
        {
            throw new NotFoundException(resourceName);
        }
    }

#pragma warning disable CS8777 //Parameter marked [NotNull] is passed to nullable param
    public static void ThrowIfNull([NotNull] object? v1, [NotNull] object? v2,
        [CallerMemberName] string callerMemberName = default!)
    {
        NotFoundException._ThrowIfNull(callerMemberName, v1, v2);
    }

    public static void ThrowIfNull([NotNull] object? v1, [NotNull] object? v2, [NotNull] object? v3,
        [CallerMemberName] string callerMemberName = default!)
    {
        NotFoundException._ThrowIfNull(callerMemberName, v1, v2, v3);
    }

    public static void ThrowIfNull(
        [NotNull] object? v1, [NotNull] object? v2, [NotNull] object? v3, [NotNull] object? v4,
        [CallerMemberName] string callerMemberName = default!)
    {
        NotFoundException._ThrowIfNull(callerMemberName, v1, v2, v3, v4);
    }

    public static void ThrowIfNull(
        [NotNull] object? v1, [NotNull] object? v2, [NotNull] object? v3,
        [NotNull] object? v4, [NotNull] object? v5,
        [CallerMemberName] string callerMemberName = default!)
    {
        NotFoundException._ThrowIfNull(callerMemberName, v1, v2, v3, v4, v5);
    }

    public static void ThrowIfNull(
        [NotNull] object? v1, [NotNull] object? v2, [NotNull] object? v3,
        [NotNull] object? v4, [NotNull] object? v5, [NotNull] object? v6,
        [CallerMemberName] string callerMemberName = default!)
    {
        NotFoundException._ThrowIfNull(callerMemberName, v1, v2, v3, v4, v5, v6);
    }

    public static void ThrowIfNull(
        [NotNull] object? v1, [NotNull] object? v2, [NotNull] object? v3,
        [NotNull] object? v4, [NotNull] object? v5, [NotNull] object? v6,
        [NotNull] object? v7,
        [CallerMemberName] string callerMemberName = default!)
    {
        NotFoundException._ThrowIfNull(callerMemberName, v1, v2, v3, v4, v5, v6, v7);
    }

    public static void ThrowIfNull(
        [NotNull] object? v1, [NotNull] object? v2, [NotNull] object? v3,
        [NotNull] object? v4, [NotNull] object? v5, [NotNull] object? v6,
        [NotNull] object? v7, [NotNull] object? v8,
        [CallerMemberName] string callerMemberName = default!)
    {
        NotFoundException._ThrowIfNull(callerMemberName, v1, v2, v3, v4, v5, v6, v7, v8);
    }

    public static void ThrowIfNull(
        [NotNull] object? v1, [NotNull] object? v2, [NotNull] object? v3,
        [NotNull] object? v4, [NotNull] object? v5, [NotNull] object? v6,
        [NotNull] object? v7, [NotNull] object? v8, [NotNull] object? v9,
        [CallerMemberName] string callerMemberName = default!)
    {
        NotFoundException._ThrowIfNull(callerMemberName, v1, v2, v3, v4, v5, v6, v7, v8, v9);
    }

    public static void ThrowIfNull(
        [NotNull] object? v1, [NotNull] object? v2, [NotNull] object? v3,
        [NotNull] object? v4, [NotNull] object? v5, [NotNull] object? v6,
        [NotNull] object? v7, [NotNull] object? v8, [NotNull] object? v9,
        [NotNull] object? v10,
        [CallerMemberName] string callerMemberName = default!)
    {
        NotFoundException._ThrowIfNull(callerMemberName, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10);
    }

    public static void ThrowIfNull(
    [NotNull] object? v1, [NotNull] object? v2, [NotNull] object? v3,
    [NotNull] object? v4, [NotNull] object? v5, [NotNull] object? v6,
    [NotNull] object? v7, [NotNull] object? v8, [NotNull] object? v9,
    [NotNull] object? v10, [NotNull] object? v11,
        [CallerMemberName] string callerMemberName = default!)
    {
        NotFoundException._ThrowIfNull(callerMemberName, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11);
    }

    public static void ThrowIfNull(
        [NotNull] object? v1, [NotNull] object? v2, [NotNull] object? v3,
        [NotNull] object? v4, [NotNull] object? v5, [NotNull] object? v6,
        [NotNull] object? v7, [NotNull] object? v8, [NotNull] object? v9,
        [NotNull] object? v10, [NotNull] object? v11, [NotNull] object? v12,
        [CallerMemberName] string callerMemberName = default!)
    {
        NotFoundException._ThrowIfNull(callerMemberName, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12);
    }

    public static void ThrowIfNull(
        [NotNull] object? v1, [NotNull] object? v2, [NotNull] object? v3,
        [NotNull] object? v4, [NotNull] object? v5, [NotNull] object? v6,
        [NotNull] object? v7, [NotNull] object? v8, [NotNull] object? v9,
        [NotNull] object? v10, [NotNull] object? v11, [NotNull] object? v12,
        [NotNull] object? v13,
        [CallerMemberName] string callerMemberName = default!)
    {
        NotFoundException._ThrowIfNull(callerMemberName, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13);
    }

    public static void ThrowIfNull(
        [NotNull] object? v1, [NotNull] object? v2, [NotNull] object? v3,
        [NotNull] object? v4, [NotNull] object? v5, [NotNull] object? v6,
        [NotNull] object? v7, [NotNull] object? v8, [NotNull] object? v9,
        [NotNull] object? v10, [NotNull] object? v11, [NotNull] object? v12,
        [NotNull] object? v13, [NotNull] object? v14,
        [CallerMemberName] string callerMemberName = default!)
    {
        NotFoundException._ThrowIfNull(callerMemberName, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14);
    }

    public static void ThrowIfNull(
        [NotNull] object? v1, [NotNull] object? v2, [NotNull] object? v3,
        [NotNull] object? v4, [NotNull] object? v5, [NotNull] object? v6,
        [NotNull] object? v7, [NotNull] object? v8, [NotNull] object? v9,
        [NotNull] object? v10, [NotNull] object? v11, [NotNull] object? v12,
        [NotNull] object? v13, [NotNull] object? v14, [NotNull] object? v15,
        [CallerMemberName]string callerMemberName = default!)
    {
        NotFoundException._ThrowIfNull(callerMemberName, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15);
    }
#pragma warning restore CS8777

    private static void _ThrowIfNull(string callerMemberName, params object?[] resources)
    {
        foreach (var resource in resources)
        {
            if (resource is null)
            {
                throw new NotFoundException($"One or more of requested resources passed to {callerMemberName} was not found");
            }
        }
    }
}

