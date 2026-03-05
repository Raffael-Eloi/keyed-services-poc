namespace KeyedServices.Contracts;

internal interface IEventHandlerFactory
{
    Task HandleEvent(string eventName, object eventData);
}