using Dapper;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.Modules.Feed.Infrastructure.Data;
using SocialMediaBackend.Modules.Feed.Infrastructure.Domain.Authors;

namespace SocialMediaBackend.Modules.Feed.Infrastructure.Domain.Posts;

public class PostRepository(IDbConnectionFactory connectionFactory) : IPostRepository
{
    private readonly IDbConnectionFactory _connectionFactory = connectionFactory;

    public async Task<PagedDto<PostDto>> GetAllAsync(GetAllPostsOptions options, CancellationToken ct = default)
    {
        using var connection = await _connectionFactory.CreateAsync(ct);

        var parameters = new DynamicParameters();
        var filters = BuildFilters(options, parameters);

        var baseSql = $"""
            SELECT
                p."Id",
                p."Text",
                p."LikesCount",
                p."CommentsCount",
                p."Created"              AS CreatedAt,
                p."LastModified"         AS UpdatedAt,
                string_agg(m."Url", ',') AS MediaUrls,
                u."Id"                   AS AuthorId,
                u."Username",
                u."Nickname",
                u."FollowersCount",
                u."FollowingCount",
                u."ProfilePictureUrl"
            FROM {Schema.Feed}."Posts" p
            JOIN {Schema.Feed}."Authors" u ON p."AuthorId" = u."Id"
            LEFT JOIN {Schema.Feed}."Posts_MediaItems" m ON p."Id" = m."PostId"
            {GetFollowsJoin(options)}
            WHERE {string.Join(" AND ", filters)}
            GROUP BY
                p."Id",
                u."Id"
                {GetGroupByExtras(options)}
            ORDER BY p."Created" {GetOrderDirection(options.Order)}
            LIMIT @PageSize OFFSET @Offset;
        """;

        // Query paged items
        var items = await connection.QueryAsync<PostDto, AuthorDto, PostDto>(
            command: new CommandDefinition(baseSql, parameters, cancellationToken: ct),
            (post, author) =>
            {
                post.Author = author;
                return post;
            },
            splitOn: "AuthorId"
        );

        // Query total count
        var countSql = $"""
            SELECT COUNT(*)
            FROM {Schema.Feed}."Posts" p
            JOIN {Schema.Feed}."Authors" u ON p."AuthorId" = u."Id"
            {GetFollowsJoin(options)}
            WHERE {string.Join(" AND ", filters)};
        """;

        var totalCount = await connection.ExecuteScalarAsync<int>(
            new CommandDefinition(countSql, parameters, cancellationToken: ct)
        );


        return new(items, options.Page, options.PageSize, totalCount);
    }

    #region SQL Builders
    private static List<string> BuildFilters(GetAllPostsOptions options, DynamicParameters parameters)
    {
        var filtersCount = CalculateFiltersCount(options);

        var filters = new List<string>(capacity: filtersCount) { "1=1" };

        if (!string.IsNullOrWhiteSpace(options.IdOrUsername))
        {
            if (Guid.TryParse(options.IdOrUsername, out var AuthorId))
            {
                filters.Add("p.\"AuthorId\" = @AuthorId");
                parameters.Add("AuthorId", AuthorId);
            }
            else
            {
                filters.Add("u.\"Username\" ILIKE @Username");
                parameters.Add("Username", $"%{options.IdOrUsername}%");
            }
        }

        if (!string.IsNullOrWhiteSpace(options.Text))
        {
            filters.Add("p.\"Text\" ILIKE @Text");
            parameters.Add("Text", $"%{options.Text}%");
        }

        if (options.Since.HasValue)
        {
            filters.Add("p.\"Created\" >= @Since");
            parameters.Add("Since", options.Since.Value.ToDateTime(TimeOnly.MinValue));
        }

        if (options.Until.HasValue)
        {
            filters.Add("p.\"Created\" <= @Until");
            parameters.Add("Until", options.Until.Value.ToDateTime(TimeOnly.MaxValue));
        }

        if (!options.IsAdmin)
        {
            filters.Add("(u.\"ProfileIsPublic\" = TRUE OR f.\"FollowerId\" IS NOT NULL OR u.\"Id\" = @RequestingUserId)");
            parameters.Add("RequestingUserId", options.RequestingUserId);
        }

        parameters.Add("PageSize", options.PageSize);
        parameters.Add("Offset", (options.Page - 1) * options.PageSize);

        return filters;
    }

    private static int CalculateFiltersCount(GetAllPostsOptions options)
    {
        int initialCount = 4; //1=1, PageSize, Page, Order

        if (!string.IsNullOrEmpty(options.Text)) initialCount++;
        if (!string.IsNullOrEmpty(options.IdOrUsername)) initialCount++;
        if (options.Since.HasValue) initialCount++;
        if (options.Until.HasValue) initialCount++;
        if (options.IsAdmin == false)
            if (options.RequestingUserId.HasValue) initialCount++;

        return initialCount;
    }

    private static string GetFollowsJoin(GetAllPostsOptions opts)
        => opts.IsAdmin
            ? string.Empty
            : $"LEFT JOIN {Schema.Feed}.\"Follows\" f ON f.\"FollowingId\" = u.\"Id\" AND f.\"FollowerId\" = @RequestingUserId";

    private static string GetGroupByExtras(GetAllPostsOptions opts)
        => opts.IsAdmin
            ? string.Empty
            : ", f.\"FollowerId\"";

    private static string GetOrderDirection(Order order)
        => order == Order.Ascending ? "ASC" : "DESC";
    #endregion
}
