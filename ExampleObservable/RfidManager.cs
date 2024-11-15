using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleManagerObservable;
internal class RfidManager : IObserver<string>
{
    private readonly List<Reader> readers = new();
    private readonly CancellationTokenSource cancellationTokenSource = new();

    public void AddReader(Reader reader)
    {
        readers.Add(reader);
        reader.Subscribe(this);
    }

    public void StartAll()
    {
        //var cancellationTokens = new List<CancellationTokenSource>();
        //foreach (var reader in readers)
        //{
        //    var cts = new CancellationTokenSource();
        //    cancellationTokens.Add(cts);
        //    reader.StartReading(cts.Token);
        //    Task.Run(() =>
        //    {
        //        if (reader.CheckCondition()) Console.WriteLine($"[{reader.name}]Condicion positiva");
        //        Console.WriteLine($"[{reader.name}]Condicion negativa");
        //        cts.Cancel();
        //    });
        //}
        foreach (var reader in readers)
        {
            reader.StartReading(cancellationTokenSource.Token);
        }
        Thread.Sleep(5000);
        cancellationTokenSource.Cancel();
        //StopAll();
        Console.WriteLine("Cancelación solicitada para todas las lecturas.");
        //Thread.Sleep(8000);
    }
    public void OnNext(string value)
    {
        Console.WriteLine(value);
    }
    public void OnError(Exception error)
    {
        Console.WriteLine($"Error detectado: {error.Message}");
        StopAll();
    }
    public void OnCompleted()
    {
        Console.WriteLine("Un lector ha finalizado su tarea.");
    }
    private void StopAll()
    {
        cancellationTokenSource.Cancel();
        Console.WriteLine("Todos los lectores han sido detenidos.");
    }
}
