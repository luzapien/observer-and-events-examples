using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorTemperatureObservable;
public class Sensor : IObservable<int>
{
    private List<IObserver<int>> observers;

    public Sensor()
    {
        observers = new List<IObserver<int>>();
    }

    public IDisposable Subscribe(IObserver<int> observer)
    {
        if (!observers.Contains(observer))
            observers.Add(observer);

        //Retormanos un objeto de tipo IDisposable para poder cancelar la susbcripción
        return new Unsubscriber(observers, observer);
    }

    public void NotifyTemperature(int temperature)
    {
        foreach (var observer in observers)
        {
            if (temperature < 0)
                observer.OnError(new Exception("Temperature out of range"));
            else
                observer.OnNext(temperature);
        }
    }

    public void EndTransmission()
    {
        foreach (var observer in observers.ToArray())
            observer.OnCompleted();

        observers.Clear();
    }

    private class Unsubscriber : IDisposable
    {
        private List<IObserver<int>> _observers;
        private IObserver<int> _observer;

        public Unsubscriber(List<IObserver<int>> observers, IObserver<int> observer)
        {
            _observers = observers;
            _observer = observer;
        }

        public void Dispose()
        {
            if (_observer != null && _observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }
}
