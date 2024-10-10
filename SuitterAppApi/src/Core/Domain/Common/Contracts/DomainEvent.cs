using SuitterAppApi.Shared.Events;

namespace SuitterAppApi.Domain.Common.Contracts;
public abstract class DomainEvent : IEvent
{
    public DateTime TriggeredOn { get; protected set; } = DateTime.UtcNow;
}