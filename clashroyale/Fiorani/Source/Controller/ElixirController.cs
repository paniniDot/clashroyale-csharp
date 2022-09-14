namespace Fiorani.Source.Controller;

/// <summary>
/// Utility class for elixir in game.
/// </summary>
public class ElixirController
{
    public int Elixir { get; private set; }
    private bool _run;

    /// <summary>
    /// build an elixir controller.
    /// </summary>
    public ElixirController()
    {
        this.Elixir = 0;
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
            if (this.Elixir < 10)
            {
                this.Elixir++;
            }

            Console.WriteLine("Elixir " + this.Elixir);
        }
    }

    /// <summary>
    /// set elixir value to 0.
    /// </summary>
    public void ResetElixir()
    {
        this.Elixir = 0;
    }

    /// <summary>
    /// set run to false.
    /// </summary>
    public void SetRunFalse()
    {
        this._run = false;
    }

    /// <summary>
    /// decrement elixir if enough.
    /// </summary>
    /// <param name="n">the amount of elixir to be taken.</param>
    /// /// <returns>true if decremented.</returns>
    public bool DecrementElixir(int n)
    {
        if (this.Elixir >= n)
        {
            this.Elixir -= n;
            return true;
        }

        return false;
    }
}