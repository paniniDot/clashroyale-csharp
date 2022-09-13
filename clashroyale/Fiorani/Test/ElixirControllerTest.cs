﻿using Fiorani.Source;
using NUnit.Framework;
namespace Fiorani.Test;

public class ElixirControllerTest
{
    private ElixirController _controller;

    [SetUp]
    public void InitializeCountDownController()
    {
        
        this._controller = new ElixirController();
    }

    [Test]
    public void Test()
    {
        Assert.NotNull(this._controller);
        Assert.That(this._controller.GetElixir(), Is.EqualTo(0));
        Thread.Sleep(5500);
        Assert.That(this._controller.GetElixir(), Is.EqualTo(5));
        this._controller.DecrementElixir(5);
        Assert.That(this._controller.GetElixir(), Is.EqualTo(0));
        this._controller.ResetElixir();
        Assert.That(this._controller.GetElixir(), Is.EqualTo(0));
        this._controller.SetRunFalse();
    }
}