
public delegate void EventHandler<T>(object sender, T args);

public class EventPublisher<T>
{
    public event EventHandler<T> Event;

    public void RaiseEvent(T args)
    {
        Event?.Invoke(this, args);
    }
}
