﻿using Marten.Events.Aggregation;
using SocialMediaBackend.Modules.Payments.Domain.Subscriptions;
using SocialMediaBackend.Modules.Payments.Domain.Subscriptions.Events;

namespace SocialMediaBackend.Modules.Payments.Infrastructure.Domain.Subscriptions;

internal sealed class SubscriptionProjection : SingleStreamProjection<Subscription, Guid>
{
    public void Apply(Subscription subscription, SubscriptionInitiated @event) => subscription.Apply(@event);
    public void Apply(Subscription subscription, SubscriptionActivated @event) => subscription.Apply(@event);
    public void Apply(Subscription subscription, SubscriptionCancelled @event) => subscription.Apply(@event);
    public void Apply(Subscription subscription, SubscriptionMarkedIncomplete @event) => subscription.Apply(@event);
    public void Apply(Subscription subscription, SubscriptionPastDue @event) => subscription.Apply(@event);
    public void Apply(Subscription subscription, SubscriptionCreated @event) => subscription.Apply(@event);
    public void Apply(Subscription subscription, SubscriptionRenewed @event) => subscription.Apply(@event);
    public void Apply(Subscription subscription, AssigentToCancelAtPeriodEnd @event) => subscription.Apply(@event);
    public void Apply(Subscription subscription, SubscriptionReactivated @event) => subscription.Apply(@event);
}
