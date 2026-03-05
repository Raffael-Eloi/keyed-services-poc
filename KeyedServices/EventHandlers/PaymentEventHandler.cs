using KeyedServices.Contracts;

namespace KeyedServices.EventHandlers;

internal class PaymentEventHandler : IEventHandler
{
    public Task HandleAsync(object eventData, CancellationToken cancellationToken)
    {
        // Implement the logic to handle payment events here
        return Task.CompletedTask;
    }
}