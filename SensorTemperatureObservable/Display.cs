using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorTemperatureObservable;
public class Display : IObserver<int>
{
    private IDisposable unsubscriber;

    public void SubscribeDisplay(IObservable<int> provider)
    {
        if (provider != null)
            unsubscriber = provider.Subscribe(this);
    }

    public void OnNext(int value)
    {
        Console.WriteLine($"Temperatura actual: {value}°C");
    }

    public void OnError(Exception error)
    {
        Console.WriteLine($"Error recibido: {error.Message}");
    }

    public void OnCompleted()
    {
        Console.WriteLine("No hay más datos de temperatura.");
    }

    public void Unsubscribe()
    {
        unsubscriber.Dispose();
    }
}