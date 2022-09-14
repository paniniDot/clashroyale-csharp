namespace Fiorani.Source.Controller;

/// <summary>
/// Utility class for CountDown in game.
/// </summary>
public class CountDownController
{
    /// <summary>
    /// Game will least 90 seconds.
    /// </summary>
    public int Time { get; private set; }

    private const int DefaultTime = 90;
    private bool _run;

    /// <summary>
    /// build an countdown controller.
    /// </summary>
    public CountDownController()
    {
        this.Time = 90;
        this._run = true;
        var thread = new Thread(Run)
        {
            IsBackground = false
        };
        thread.Start();
    }

    private void Run()
    {
        while (this._run)
        {
            Thread.Sleep(1000);
            if (this.Time > 0)
            {
                this.Time--;
            }

            Console.WriteLine("Time " + this.Time);
        }
    }

    /// <summary>
    /// Restart the timer.
    /// </summary>
    public void ResetTime()
    {
        this.Time = DefaultTime;
    }

    /// <summary>
    /// set run to false.
    /// </summary>
    public void SetRunFalse()
    {
        this._run = false;
    }
}