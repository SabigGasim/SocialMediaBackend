using SocialMediaBackend.BuildingBlocks.Domain.EventSourcing;

namespace SocialMediaBackend.Modules.Payments.Domain.Payers.Events;

public record PayerCreated(PayerId PayerId) : StreamEventBase;