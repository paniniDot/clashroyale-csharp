namespace Fiorani.Source.Controller;

/// <summary>
/// Utility class for CountDown in game.
/// </summary>
public class CountDownController
{
    /// <summary>
    /// Game will least 90 seconds.
    /// </summary>
    private static int _time;
    private const int DefaultTime = 90;
    private static bool _run;

    /// <summary>
    /// build an countdown controller.
    /// </summary>
    public CountDownController()
    {
        _time = 90;
        _run = true;
        RunningThread();
    }

    private static void Run()
    {
        while (_run)
        {
            Thread.Sleep(1000);
            if (_time > 0)
            {
                _time--;
            }

            Console.WriteLine("Time " + _time);
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

    /// <returns>the remaining seconds before game ends.</returns>
    public int GetTime()
    {
        return _time;
    }

    /// <summary>
    /// Restart the timer.
    /// </summary>
    public void ResetTime()
    {
        _time = DefaultTime;
    }
    /// <summary>
    /// set run to false.
    /// </summary>
    public void SetRunFalse()
    {
        _run = false;
    }
}