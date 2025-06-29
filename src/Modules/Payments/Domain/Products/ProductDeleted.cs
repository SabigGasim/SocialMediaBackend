using SocialMediaBackend.BuildingBlocks.Domain.EventSourcing;
using SocialMediaBackend.Modules.Payments.Domain.Products.Events;

namespace SocialMediaBackend.Modules.Payments.Domain.Products;
public record ProductDeleted : StreamEventBase;