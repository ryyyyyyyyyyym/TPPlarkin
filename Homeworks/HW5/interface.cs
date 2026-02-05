public interface INotifier
{
    void Notify(string message);
}

public class ConsoleNotifier : INotifier
{
    public void Notify(string message)
    {
        Console.WriteLine(message);
    }
}

public class FileNotifier : INotifier
{
    public void Notify(string message)
    {
        File.AppendAllText("log.txt", message + "\n");
    }
}

public class Program
{
    public static void Main()
    {
        INotifier[] notifiers = new INotifier[]
        {
            new ConsoleNotifier(),
            new FileNotifier()
        };

        foreach (INotifier notifier in notifiers)
        {
            notifier.Notify("hello");
        }
    }
}