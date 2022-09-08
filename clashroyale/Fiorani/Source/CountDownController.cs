namespace Fiorani.Source;

public class CountDownController
{
    private static Timer? _timer;
    private static int _time;
    private const int DefaultTime = 90;
    private static bool _run;
    private static readonly ConsoleColor DefaultC = Console.ForegroundColor;

    public CountDownController()
    {
        Console.WriteLine("Press R to Start the Timer " + Environment.NewLine);
        _time = 90;
        _run = true;
        RunningThread();
    }
    private static void Run()
    {
        while (_run)
        {
            if (_time > 0)
            {
                _time--;
            }

            Thread.Sleep(1000);
            Console.WriteLine("Time! " + _time);
        }
    }

    static void RunningThread()
    {
        Thread thread = new Thread(Run);
        thread.IsBackground = false;
        thread.Start();
    }

    public int GetTime()
    {
        return _time;
    }

    public void SetTime()
    {
        _time = DefaultTime;
    }

    public void SetRunFalse()
    {
        _run = false;
    }
}