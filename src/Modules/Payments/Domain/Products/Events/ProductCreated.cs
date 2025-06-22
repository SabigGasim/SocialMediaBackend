using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.Payments.Domain.Products.Events;

public record ProductCreated(
    string Reference,
    string GatewayProductId, 
    string Owner) : StreamEventBase;