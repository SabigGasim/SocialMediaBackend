﻿namespace SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;

public class InternalCommand
{
    private InternalCommand() {}

    public Guid Id { get; set; }
    public string Type { get; set; } = default!;
    public string Data { get; set; } = default!;
    public string? Error { get; set; }
    public string? IdempotencyKey { get; set; }
    public bool Processed { get; set; }
    public DateTimeOffset? ProcessedDate { get; set; }
    public DateTimeOffset EnqueueDate { get; set; }
}
