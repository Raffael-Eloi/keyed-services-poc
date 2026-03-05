namespace KeyedServices.Contracts;

internal interface IEventHandler
{
    Task HandleAsync(object eventData, CancellationToken cancellationToken);
}