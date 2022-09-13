namespace Fiorani.Source.Controller;
/// <summary>
/// Utility class for elixir in game.
/// </summary>
public class ElixirController
{
    private static int _elixir;
    private static bool _run;
    /// <summary>
    /// build an elixir controller.
    /// </summary>
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
    /// <returns>the current elixir owned.</returns>
    public int GetElixir()
    {
        return _elixir;
    }
    /// <summary>
    /// set elixir value to 0.
    /// </summary>
    public void ResetElixir()
    {
        _elixir = 0;
    }
    /// <summary>
    /// set run to false.
    /// </summary>
    public void SetRunFalse()
    {
        _run = false;
    }
    /// <summary>
    /// decrement elixir if enough.
    /// </summary>
    /// <param name="n">the amount of elixir to be taken.</param>
    /// /// <returns>true if decremented.</returns>
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