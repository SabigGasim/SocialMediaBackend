using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.Payments.Domain.Payers;
using System.Text.Json.Serialization;

namespace SocialMediaBackend.Modules.Payments.Application.Payers.CreatePayer;

[method: JsonConstructor]
public class CreatePayerCommand(Guid id, PayerId payerId) : InternalCommandBase(id)
{
    public PayerId PayerId { get; } = payerId;
}
