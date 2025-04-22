using Dapper;
using SocialMediaBackend.Infrastructure.Common;
using SocialMediaBackend.Infrastructure.Data;
using SocialMediaBackend.Infrastructure.Domain.Users;
using System.Text;

namespace SocialMediaBackend.Infrastructure.Domain.Posts;

public class PostRepository(IDbConnectionFactory connectionFactory) : IPostRepository
{
    private readonly IDbConnectionFactory _connectionFactory = connectionFactory;

    public async Task<PagedDto<PostDto>> GetAllAsync(GetAllPostsOptions options, CancellationToken ct = default)
    {
        using var connection = await _connectionFactory.CreateAsync(ct);

        var sqlBuilder = new StringBuilder("""
            SELECT 
                p."Id", 
                p."Text",
                p."LikesCount", 
                p."CommentsCount", 
                p."Created" AS CreatedAt,
                p."LastModified" AS UpdatedAt,
                string_agg(m."Url", ',') AS MediaUrls,
                u."Id" AS UserId,
                u."Username",
                u."Nickname", 
                u."FollowersCount",
                u."FollowingCount",
                u."ProfilePictureUrl"
            FROM "Posts" p
            JOIN "Users" u ON p."UserId" = u."Id"
            LEFT JOIN "Posts_MediaItems" m ON p."Id" = m."PostId"
            WHERE 1=1
            """);


        var parameters = new DynamicParameters();

        var filtersBuilder = new StringBuilder("");

        var identifierIsUsername = AppendUserIdOrUsernameIfNotNull(options, filtersBuilder, parameters);
        AppendPostTextIfNotNull(options, filtersBuilder, parameters);
        AppendDateConstraintsIfNotNull(options, filtersBuilder, parameters);

        var filters = filtersBuilder.ToString();
        sqlBuilder.Append(filters);

        AppendGrouping(sqlBuilder);
        AppendOrderIfNotNull(options, sqlBuilder);
        AppendPagination(options, sqlBuilder, parameters);

        var items = await connection.QueryAsync<PostDto, UserDto, PostDto>(
            sqlBuilder.ToString(),
            (post, user) =>
            {
                post.User = user;
                return post;
            },
            param: parameters,
            splitOn: "UserId"
        );

        var totalCount = await connection.ExecuteScalarAsync<int>(new CommandDefinition($"""
            SELECT COUNT(*)
            FROM "Posts" p
            {(identifierIsUsername ? @"JOIN ""Users"" u ON u.""Id"" = p.""UserId""" : "")}
            WHERE 1=1
            {filters}
            """, parameters, cancellationToken: ct));

        return new(items, options.Page, options.PageSize, totalCount);


        #region Local Functions
        ///Returnes a boolean represnting if the identifier is a username
        static bool AppendUserIdOrUsernameIfNotNull(GetAllPostsOptions options, StringBuilder sql, DynamicParameters parameters)
        {
            if (string.IsNullOrEmpty(options.IdOrUsername?.Trim()))
            {
                return false;
            }

            if (Guid.TryParse(options.IdOrUsername, out var userId))
            {
                sql.Append(@" AND p.""UserId"" = @UserId");
                parameters.Add("UserId", userId);
                return false;
            }

            sql.Append(@" AND u.""Username"" ILIKE @Username");
            parameters.Add("Username", $"%{options.IdOrUsername}%");
            return true;
        }

        static void AppendDateConstraintsIfNotNull(GetAllPostsOptions options, StringBuilder sql, DynamicParameters parameters)
        {
            if (options.Since.HasValue)
            {
                sql.Append(@" AND p.""Created"" >= @Since");
                parameters.Add("Since", options.Since.Value.ToDateTime(TimeOnly.MinValue));
            }

            if (options.Until.HasValue)
            {
                sql.Append(@" AND p.""Created"" <= @Until");
                parameters.Add("Until", options.Until.Value.ToDateTime(TimeOnly.MaxValue));
            }
        }

        static void AppendOrderIfNotNull(GetAllPostsOptions options, StringBuilder sql)
        {
            var orderDirection = options.Order == Order.Descending ? "DESC" : "ASC";
            sql.Append(@$" ORDER BY p.""Created"" {orderDirection}");
        }

        static void AppendPagination(GetAllPostsOptions options, StringBuilder sql, DynamicParameters parameters)
        {
            sql.Append(" LIMIT @Limit OFFSET @Offset");
            parameters.Add("Limit", options.PageSize);
            parameters.Add("Offset", (options.Page - 1) * options.PageSize);
        }

        static void AppendPostTextIfNotNull(GetAllPostsOptions options, StringBuilder sql, DynamicParameters parameters)
        {
            if (!string.IsNullOrWhiteSpace(options.Text))
            {
                sql.Append(@" AND p.""Text"" ILIKE @Text");
                parameters.Add("Text", $"%{options.Text}%");
            }
        }

        static void AppendGrouping(StringBuilder sqlBuilder)
        {
            sqlBuilder.Append("""
             GROUP BY 
                p."Id", 
                u."Id"
            """);
        }
        #endregion
    }
}