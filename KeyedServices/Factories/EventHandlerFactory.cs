using KeyedServices.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace KeyedServices.Factories;

internal class EventHandlerFactory(IKeyedServiceProvider serviceProvider) : IEventHandlerFactory
{
    public const string PurchaseOrder = "PurchaseOrder";

    public const string PaymentProcessed = "PaymentProcessed";

    public const string InvoiceProcessed = "InvoiceProcessed";

    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task HandleEvent(string eventName, object eventData)
    {
        var cancellationToken = new CancellationToken();
        IEventHandler handler = _serviceProvider.GetRequiredKeyedService<IEventHandler>(eventName);
        await handler.HandleAsync(eventData, cancellationToken);
    }
}