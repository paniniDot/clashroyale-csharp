﻿using Fiorani.Source.Controller;
using NUnit.Framework;

namespace Fiorani.Test.Controller;

public class CountDownControllerTest
{
    private CountDownController _controller;

    [SetUp]
    public void InitializeCountDownController()
    {
        this._controller = new CountDownController();
    }

    [Test]
    public void Test()
    {
        Assert.NotNull(this._controller);
        Assert.That(this._controller.Time, Is.EqualTo(90));
        Thread.Sleep(5050);
        Assert.That(this._controller.Time, Is.EqualTo(85));
        this._controller.ResetTime();
        Assert.That(this._controller.Time, Is.EqualTo(90));
        this._controller.SetRunFalse();
    }
}