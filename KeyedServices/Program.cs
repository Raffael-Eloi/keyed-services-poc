using KeyedServices.Constants;
using KeyedServices.Contracts;
using KeyedServices.EventHandlers;
using KeyedServices.Factories;
using Microsoft.Extensions.DependencyInjection;

public class Program 
{
    public static async Task Main(string[] args)
    {
        IServiceCollection serviceCollection = new ServiceCollection();
        serviceCollection.AddKeyedScoped<IEventHandler, PurchaseOrderEventHandler>(EventKeys.PurchaseOrder);
        serviceCollection.AddKeyedScoped<IEventHandler, PaymentEventHandler>(EventKeys.PaymentProcessed);
        serviceCollection.AddKeyedScoped<IEventHandler, InvoiceEventHandler>(EventKeys.InvoiceProcessed);

        ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        IEventHandlerFactory eventHandlerFactory = new EventHandlerFactory(serviceProvider);
        await eventHandlerFactory.HandleEvent(EventKeys.PurchaseOrder, new { OrderId = 123 });
        await eventHandlerFactory.HandleEvent(EventKeys.PaymentProcessed, new { PaymentId = 456 });
        await eventHandlerFactory.HandleEvent(EventKeys.InvoiceProcessed, new { InvoiceId = 789 });
    }
}