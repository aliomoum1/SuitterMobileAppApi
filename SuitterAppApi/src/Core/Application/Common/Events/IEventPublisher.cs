using SuitterAppApi.Shared.Events;

namespace SuitterAppApi.Application.Common.Events;
public interface IEventPublisher : ITransientService
{
    Task PublishAsync(IEvent @event);
}