namespace Fiorani.Source;

public class ElixirController
{
    private static Timer? _timer;
    private static int _elixir;
    private static bool _run;
    private static readonly ConsoleColor DefaultC = Console.ForegroundColor;

    public ElixirController()
    {
        Console.WriteLine("Press R to Start the Timer " + Environment.NewLine);
        _elixir = 0;
        _run = true;
        RunningThread();
    }

    private static void Run()
    {
        while (_run)
        {
            if (_elixir < 10)
            {
                _elixir++;
            }

            Thread.Sleep(1000);
            Console.WriteLine("Time! " + _elixir);
        }
    }

    static void RunningThread()
    {
        Thread thread = new Thread(Run);
        thread.IsBackground = false;
        thread.Start();
    }

    public int GetElixir()
    {
        return _elixir;
    }

    public void SetEixir()
    {
        _elixir = 0;
    }

    public void SetRunFalse()
    {
        _run = false;
    }
}