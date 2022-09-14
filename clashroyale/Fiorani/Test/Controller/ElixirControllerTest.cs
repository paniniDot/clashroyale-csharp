using Fiorani.Source.Controller;
using NUnit.Framework;

namespace Fiorani.Test.Controller;

public class ElixirControllerTest
{
    private ElixirController _controller;
    /// <summary>
    /// Initialize ElixirController.
    /// </summary>
    [SetUp]
    public void Initialize()
    {
        this._controller = new ElixirController();
    }
    /// <summary>
    /// test controller after 5 seconds and reset.
    /// </summary>
    [Test]
    public void Test()
    {
        Assert.NotNull(this._controller);
        Assert.That(this._controller.Elixir, Is.EqualTo(0));
        Thread.Sleep(5500);
        Assert.That(this._controller.Elixir, Is.EqualTo(5));
        this._controller.DecrementElixir(5);
        Assert.That(this._controller.Elixir, Is.EqualTo(0));
        this._controller.ResetElixir();
        Assert.That(this._controller.Elixir, Is.EqualTo(0));
        this._controller.SetRunFalse();
    }
}