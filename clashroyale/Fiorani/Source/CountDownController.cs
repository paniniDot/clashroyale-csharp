namespace Fiorani.Source;

public class CountDownController
{
    private static int _time;
    private const int DefaultTime = 90;
    private static bool _run;

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