using ExampleManagerObservable;

public class Program
{
    public static void Main()
    {

        var manager = new RfidManager();

        manager.AddReader(new Reader("Reader 1"));
        manager.AddReader(new Reader("Reader 2"));
        manager.AddReader(new Reader("Reader 3"));

        manager.StartAll();

        Console.ReadLine();

    }
}