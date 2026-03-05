using KeyedServices.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace KeyedServices.Factories;

internal class EventHandlerFactory(IKeyedServiceProvider serviceProvider) : IEventHandlerFactory
{
    public async Task HandleEvent(string eventName, object eventData)
    {
        var cancellationToken = new CancellationToken();
        IEventHandler handler = serviceProvider.GetRequiredKeyedService<IEventHandler>(eventName);
        await handler.HandleAsync(eventData, cancellationToken);
    }
}