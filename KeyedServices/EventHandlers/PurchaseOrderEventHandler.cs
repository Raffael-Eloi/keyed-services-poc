using KeyedServices.Contracts;

namespace KeyedServices.EventHandlers;

internal class PurchaseOrderEventHandler : IEventHandler
{
    public Task HandleAsync(object eventData, CancellationToken cancellationToken)
    {
        // Implement the logic to handle purchase events here
        return Task.CompletedTask;
    }
}