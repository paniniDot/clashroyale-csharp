using NUnit.Framework;
using Panni.Source.Model.Cards;
using Panni.Source.Model.Cards.Troops;
using Panni.Source.Model.Users;
using Panni.Source.Utilities;
using System.Numerics;
using Panni.Source.Model;

namespace Panni.Tests.Cards;

[TestFixture]
public class CardTest
{ 
    private Card _wiz;
    private User _user;

    private Card _botWiz;
    private Bot _bot;

    [SetUp]
    public void InitializeCard()
    {
        this._user = new User("Panini");
        this._wiz = Wizard.Create(this._user, new Vector2(0, 0));

        this._bot = new Bot();
        this._botWiz = Wizard.Create(this._bot, new Vector2(10, 10));
    }
    
    /// <summary>
    /// Test the initial values given to each property.
    /// </summary>
    [Test]
    public void InitialPropertiesTest()
    {
        
            Assert.NotNull(this._wiz);
            Assert.That(this._wiz.Owner, Is.EqualTo(this._user));
            Assert.That(this._wiz.Position, Is.EqualTo(new Vector2(0, 0)));
            Assert.That(this._wiz.CurrentHp, Is.EqualTo(12000));
            Assert.That(this._wiz.Damage, Is.EqualTo(100));
            Assert.That(this._wiz.Cost, Is.EqualTo(5));
            Assert.That(this._wiz.Range, Is.EqualTo(100));
            Assert.That(this._wiz.CurrentTarget, Is.EqualTo(Optional<IAttackable>.Empty()));
            Assert.IsFalse(this._wiz.IsDead());
        
    }

    /// <summary>
    /// Kill the wizard and then verify if is dead.
    /// </summary>
    [Test]
    public void ReduceLifeUntilIsDeadTest()
    {
        while (this._wiz.CurrentHp > 0)
        {
            this._wiz.ReduceHpBy(300);
        }
        Assert.IsTrue(this._wiz.IsDead());
    }

    /// <summary>
    /// Test if the current target updates correctly.
    /// </summary>
    [Test]
    public void TargetTest()
    {
        Assert.IsFalse(this._wiz.CurrentTarget.IsPresent);
        this._wiz.SetCurrentTarget(this._botWiz);
        Assert.IsTrue(this._wiz.CurrentTarget.IsPresent);
    }

    /// <summary>
    /// Test if the range works correctly.
    /// </summary>
    [Test]
    public void RangeTest()
    {
        Assert.That(this._wiz.Range, Is.GreaterThan(Vector2.Distance(this._wiz.Position, this._botWiz.Position)));
        this._botWiz.Position = new Vector2((float) (this._wiz.Range + 1), (float) (this._wiz.Range + 1));
        Assert.That(this._wiz.Range, Is.LessThan(Vector2.Distance(this._wiz.Position, this._botWiz.Position)));

    }
    
}