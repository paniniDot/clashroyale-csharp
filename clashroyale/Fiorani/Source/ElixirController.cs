namespace Fiorani.Source;

public class ElixirController
{
    private static int _elixir;
    private static bool _run;

    public ElixirController()
    {
        _elixir = 0;
        _run = true;
        RunningThread();
    }

    private static void Run()
    {
        while (_run)
        {
            Thread.Sleep(1000);
            if (_elixir < 10)
            {
                _elixir++;
            }
            Console.WriteLine("Elixir " + _elixir);
        }
    }

    private static void RunningThread()
    {
        var thread = new Thread(Run)
        {
            IsBackground = false
        };
        thread.Start();
    }

    public int GetElixir()
    {
        return _elixir;
    }

    public void SetElixir()
    {
        _elixir = 0;
    }

    public void SetRunFalse()
    {
        _run = false;
    }

    public bool DecrementElixir(int n)
    {
        if (_elixir >= n)
        {
            _elixir -= n;
            return true;
        }

        return false;
    }
}