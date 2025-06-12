using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.Payments.Domain.Payers;
using System.Text.Json.Serialization;

namespace SocialMediaBackend.Modules.Payments.Application.Payers.DeletePayer;

[method: JsonConstructor]
public class DeletePayerCommand(Guid id, PayerId payerId) : InternalCommandBase(id)
{
    public PayerId PayerId { get; } = payerId;
}
