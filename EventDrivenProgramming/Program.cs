using System;
using System.Threading.Tasks;
using System.Timers;

namespace TimerExample;

    class Program
{
    static void Main(string[] args)
    {
        System.Timers.Timer myTimer = new System.Timers.Timer(2000);

        myTimer.Elapsed += MyTimer_Elapsed;
        myTimer.Elapsed += MyTimer_Elapsed1;

        myTimer.Start();
        Console.WriteLine("Press enter to remove the green event");
        Console.ReadLine();

        myTimer.Elapsed -= MyTimer_Elapsed1;
        Console.ReadLine();
    }

    private static void MyTimer_Elapsed1(object? sender, ElapsedEventArgs e)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Elapsed1:{0:HH:mm:ss.fff}", e.SignalTime);
    }

    private static void MyTimer_Elapsed(object? sender, ElapsedEventArgs e)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Elapsed: {0:HH:mm:ss.fff}",e.SignalTime);
    }
}