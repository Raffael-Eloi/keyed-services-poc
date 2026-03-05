using KeyedServices.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace KeyedServices.Factories;

internal class EventHandlerFactory(IServiceProvider serviceProvider) : IEventHandlerFactory
{
    public const string PurchaseOrder = "PurchaseOrder";

    public const string PaymentProcessed = "PaymentProcessed";

    public const string InvoiceProcessed = "InvoiceProcessed";

    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task HandleEvent(string eventName, object eventData)
    {
        var cancellationToken = new CancellationToken();

        if (eventName == PurchaseOrder)
        {
            IEventHandler handler = _serviceProvider.GetRequiredKeyedService<IEventHandler>(PurchaseOrder);
            await handler.HandleAsync(eventData, cancellationToken);
            return;
        }

        else if (eventName == PaymentProcessed) 
        {
            IEventHandler handler = _serviceProvider.GetRequiredKeyedService<IEventHandler>(PaymentProcessed);
            await handler.HandleAsync(eventData, cancellationToken);
            return;
        }

        else if (eventName == InvoiceProcessed) 
        {
            IEventHandler handler = _serviceProvider.GetRequiredKeyedService<IEventHandler>(InvoiceProcessed);
            await handler.HandleAsync(eventData, cancellationToken);
            return;
        }


        throw new InvalidOperationException($"No handler found for event: {eventName}");
    }
}