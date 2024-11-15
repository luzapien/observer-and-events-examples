using SensorTemperatureObservable;

public class Program
{
    public static void Main()
    {

        Sensor sensor = new Sensor();
        Display display = new Display();

        // El display se subscribe al sensor
        display.SubscribeDisplay(sensor);

        sensor.NotifyTemperature(25);
        sensor.NotifyTemperature(30);
        sensor.NotifyTemperature(-5);


        sensor.EndTransmission();

        // Se cancela la subscripción
        display.Unsubscribe();
    }
}
