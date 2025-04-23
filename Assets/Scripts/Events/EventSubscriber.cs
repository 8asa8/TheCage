using System;

public static class EventSubscriber
{
    // Method to subscribe to an event by name
    public static void SubscribeToEvent(string eventName, EventHandler<object> eventHandler)
    {
        EventPublisher<object> publisher = EventRegistry.GetEventPublisher(eventName);
        if (publisher != null)
        {
            publisher.Event += eventHandler;
        }
        else
        {
            Console.WriteLine($"Event '{eventName}' does not exist. Subscribe failed.");
        }
    }

    // Method to unsubscribe from an event by name
    public static void UnsubscribeFromEvent(string eventName, EventHandler<object> eventHandler)
    {
        EventPublisher<object> publisher = EventRegistry.GetEventPublisher(eventName);
        if (publisher != null)
        {
            publisher.Event -= eventHandler;
        }
        else
        {
            Console.WriteLine($"Event '{eventName}' does not exist. Unsubscribe failed.");
        }
    }
}