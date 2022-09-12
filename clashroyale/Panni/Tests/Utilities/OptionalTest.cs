using NUnit.Framework;
using Panni.Source.Utilities;

namespace Panni.Tests.Utilities;

public class OptionalTest
{
    private Optional<int> _optional;

    [SetUp]
    public void SetUpOptional()
    {
        this._optional = Optional<int>.Empty();
    }

    [Test]
    public void EmptyOptionalTest()
    {
        Assert.False(this._optional.IsPresent);
    }

    [Test]
    public void ValuatedOptionalTest()
    {
        this._optional = Optional<int>.Of(3);
        Assert.IsTrue(this._optional.IsPresent);
        Assert.That(this._optional.Get(), Is.EqualTo(3));
    }
    
}