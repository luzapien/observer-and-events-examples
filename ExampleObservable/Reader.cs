using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace ExampleManagerObservable;
public class Reader : IObservable<string>
{
    private readonly List<IObserver<string>> observers = new();
    public readonly string name;



    public Reader(string name)
    {
        this.name = name;
    }

    public IDisposable Subscribe(IObserver<string> observer)
    {
        if (!observers.Contains(observer))
            observers.Add(observer);

        return new Unsubscriber(observers, observer);
    }

    public void StartReading(CancellationToken token)
    {
        Task.Run(() =>
        {
            try
            {
                for (int i = 0; i < 10; i++)
                {
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine($"[{name}] Se detuvo la lectura.");
                        NotifyCompleted();
                        return;
                    }

                    if (i == 5) 
                        throw new Exception($"Error en {name}");

                    NotifyNext($"[{name}] Leyendo datos, iteración {i}");
                    Thread.Sleep(1000);
                }

                NotifyCompleted();
            }
            catch (Exception ex)
            {
                NotifyError(ex);
            }
        });
    }

    public bool CheckCondition()
    {

        return DateTime.Now.Second % 2 == 0; 
    }
    private void NotifyNext(string message)
    {
        foreach (var observer in observers)
            observer.OnNext(message);
    }

    private void NotifyError(Exception ex)
    {
        foreach (var observer in observers)
            observer.OnError(ex);
    }

    private void NotifyCompleted()
    {
        foreach (var observer in observers)
            observer.OnCompleted();
    }

    private class Unsubscriber : IDisposable
    {
        private readonly List<IObserver<string>> _observers;
        private readonly IObserver<string> _observer;

        public Unsubscriber(List<IObserver<string>> observers, IObserver<string> observer)
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

    //public bool Initialize(object data) => true;
    //public object? GetReaderCapabilities() => null;
    //public bool IsConnected() => true;
    //public bool StartRead() => true;
    //public bool StopRead() => true;
    //public bool Disconnect() => true;

}
