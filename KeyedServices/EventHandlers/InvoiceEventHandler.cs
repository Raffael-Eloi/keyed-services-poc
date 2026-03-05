using KeyedServices.Contracts;

namespace KeyedServices.EventHandlers;

internal class InvoiceEventHandler : IEventHandler
{
    public Task HandleAsync(object eventData, CancellationToken cancellationToken)
    {
        // Implement the logic to handle invoice events here
        return Task.CompletedTask;
    }
}