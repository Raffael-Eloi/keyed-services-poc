# Stop Using Switch Statements: Keyed Services in .NET

A proof-of-concept demonstrating how to use **Keyed Services** in .NET to eliminate `switch`/`if-else` blocks when selecting between multiple implementations of the same interface at runtime.

Read the full article on dev.to: [Stop Using Switch Statements: Keyed Services in .NET — A Practical Approach](https://dev.to/raffaeleloi/stop-using-switch-statements-keyed-services-in-net-a-practical-approach-iej)

Read the full article on medium: [Stop Using Switch Statements: Keyed Services in .NET — A Practical Approach](https://medium.com/@raffaeleloi/stop-using-switch-statements-keyed-services-in-net-a-practical-approach-4ce3f106189d)

## The Problem

A common challenge in event-driven systems is routing events to the correct handler. The typical approach looks like this:

```csharp
public async Task HandleEvent(string eventName, object eventData)
{
    IEventHandler handler = eventName switch
    {
        "PurchaseOrder" => serviceProvider.GetRequiredKeyedService<IEventHandler>("PurchaseOrder"),
        "PaymentProcessed" => serviceProvider.GetRequiredKeyedService<IEventHandler>("PaymentProcessed"),
        "InvoiceProcessed" => serviceProvider.GetRequiredKeyedService<IEventHandler>("InvoiceProcessed"),
        _ => throw new ArgumentException($"No handler found for event: {eventName}")
    };

    await handler.HandleAsync(eventData, cancellationToken);
}
```

Every time you add a new event, you need to add another case. This violates the Open/Closed Principle and quickly becomes hard to maintain.

## The Solution

With **Keyed Services** (introduced in .NET 8), you can register multiple implementations of the same interface with distinct keys and resolve them directly — no conditionals needed:

```csharp
public async Task HandleEvent(string eventName, object eventData)
{
    var handler = serviceProvider.GetRequiredKeyedService<IEventHandler>(eventName);
    await handler.HandleAsync(eventData, cancellationToken);
}
```

The event name *is* the key. Adding a new event handler is just a matter of creating the implementation and registering it — zero changes to existing code.

## How It Works

**1. Register handlers with keys**

```csharp
serviceCollection.AddKeyedScoped<IEventHandler, PurchaseOrderEventHandler>(EventKeys.PurchaseOrder);
serviceCollection.AddKeyedScoped<IEventHandler, PaymentEventHandler>(EventKeys.PaymentProcessed);
serviceCollection.AddKeyedScoped<IEventHandler, InvoiceEventHandler>(EventKeys.InvoiceProcessed);
```

**2. Resolve at runtime using the event name as key**

```csharp
internal class EventHandlerFactory(IKeyedServiceProvider serviceProvider) : IEventHandlerFactory
{
    public async Task HandleEvent(string eventName, object eventData)
    {
        var cancellationToken = new CancellationToken();
        IEventHandler handler = serviceProvider.GetRequiredKeyedService<IEventHandler>(eventName);
        await handler.HandleAsync(eventData, cancellationToken);
    }
}
```

**3. Handle events**

```csharp
await eventHandlerFactory.HandleEvent(EventKeys.PurchaseOrder, new { OrderId = 123 });
await eventHandlerFactory.HandleEvent(EventKeys.PaymentProcessed, new { PaymentId = 456 });
await eventHandlerFactory.HandleEvent(EventKeys.InvoiceProcessed, new { InvoiceId = 789 });
```

## When to Use Keyed Services

- Multiple implementations of the same interface
- Runtime-dependent implementation selection
- Event-driven architectures (event handlers, notification channels, etc.)
- Anywhere you'd otherwise reach for a `switch` or `if-else` chain

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download) (or .NET 8+ for the keyed services feature)

## Running

```bash
dotnet run --project KeyedServices
```
