using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SocialMediaBackend.BuildingBlocks.Domain;
using System.Reflection;

namespace SocialMediaBackend.Api;

public static class ApiServiceCollectionExtensions
{
    public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration config)
    {
        services.AddAuthentication(o =>
        {
            o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            o.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!)),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = config["Jwt:Issuer"],
                    ValidAudience = config["Jwt:Audience"],
                    ValidateIssuer = true,
                    ValidateAudience = true
                };

                o.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) &&
                            path.StartsWithSegments($"/{ApiEndpoints.ChatHub.Base}"))
                        {
                            context.Token = accessToken;
                        }

                        return Task.CompletedTask;
                    }
                };
            });

        services.AddAuthorization(options =>
        {
            options.FallbackPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
        });

        services
            .AddFastEndpoints()
            .SwaggerDocument()
            .AddHttpContextAccessor()
            ;

        List<Assembly> applicationAssemblies =
        [
            typeof(SocialMediaBackend.Modules.Users.Application.ApplicationServiceCollectionExtensions).Assembly,
            typeof(SocialMediaBackend.Modules.Feed.Application.ApplicationServcieCollectionExtensions).Assembly,
            typeof(SocialMediaBackend.Modules.Chat.Application.ApplicationServiceCollectionExtensions).Assembly
        ];

        services.Scan(s => s.FromAssemblies(applicationAssemblies)
            .AddClasses(c => c.AssignableTo(typeof(IDomainEventNotification<>)))
            .AsImplementedInterfaces()
            .WithTransientLifetime());

        services.AddMediator(opts =>
        {
            opts.ServiceLifetime = ServiceLifetime.Scoped;
        });

        return services;
    }
}
