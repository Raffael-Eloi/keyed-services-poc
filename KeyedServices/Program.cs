using KeyedServices.Contracts;
using KeyedServices.EventHandlers;
using KeyedServices.Factories;
using Microsoft.Extensions.DependencyInjection;

public class Program 
{
    public static async Task Main(string[] args)
    {
        IServiceCollection serviceCollection = new ServiceCollection();
        serviceCollection.AddKeyedScoped<IEventHandler, PurchaseOrderEventHandler>(EventHandlerFactory.PurchaseOrder);
        serviceCollection.AddKeyedScoped<IEventHandler, PaymentEventHandler>(EventHandlerFactory.PaymentProcessed);
        serviceCollection.AddKeyedScoped<IEventHandler, InvoiceEventHandler>(EventHandlerFactory.InvoiceProcessed);

        ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        IEventHandlerFactory eventHandlerFactory = new EventHandlerFactory(serviceProvider);
        await eventHandlerFactory.HandleEvent(EventHandlerFactory.PurchaseOrder, new { OrderId = 123 });
        await eventHandlerFactory.HandleEvent(EventHandlerFactory.PaymentProcessed, new { PaymentId = 456 });
        await eventHandlerFactory.HandleEvent(EventHandlerFactory.InvoiceProcessed, new { InvoiceId = 789 });
    }
}