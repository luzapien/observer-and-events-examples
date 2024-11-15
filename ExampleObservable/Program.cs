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

        //Sensor sensor = new Sensor();
        //Display display = new Display();

        //// El display se subscribe al sensor
        //display.SubscribeDisplay(sensor);

        //sensor.NotifyTemperature(25);
        //sensor.NotifyTemperature(30);
        //sensor.NotifyTemperature(-5);


        //sensor.EndTransmission();

        //// Se cancela la subscripción
        //display.Unsubscribe();
    }
}