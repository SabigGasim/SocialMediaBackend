using Microsoft.AspNetCore.Mvc;

namespace SocialMediaBackend.Application.Abstractions;

public abstract record PagedRequest(int PageNumber, int PageSize);
