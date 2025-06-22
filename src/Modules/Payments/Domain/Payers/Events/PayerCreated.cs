using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.Payments.Domain.Payers.Events;

public record PayerCreated(PayerId PayerId) : StreamEventBase;