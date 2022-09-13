using System.Numerics;
using NUnit.Framework;
using Panni.Source.Model.Towers;
using Panni.Source.Model.Users;

namespace Panni.Tests.Towers;

/// <summary>
/// Test case for Tower class.
/// </summary>
[TestFixture]
public class TowerTest
{
    private Tower _myTower;
    private User _user;

    private Tower _enemyTower;
    private Bot _bot;

    [SetUp]
    public void InitializeTowers()
    {
        this._user = new User("Panini");
        this._myTower = KingTower.Create(this._user, new Vector2(0, 0));

        this._bot = new Bot();
        this._enemyTower = KingTower.Create(this._bot, new Vector2(10, 10));
    }
    
    /// <summary>
    /// Test the initial values given to each property.
    /// </summary>
    [Test]
    public void InitialPropertiesTest()
    {
        Assert.Multiple(() =>
        {
            Assert.NotNull(this._myTower);
            Assert.That(this._myTower.Owner, Is.EqualTo(this._user));
            Assert.That(this._myTower.Position, Is.EqualTo(new Vector2(0, 0)));
            Assert.That(this._myTower.CurrentHp, Is.EqualTo(15000));
            Assert.That(this._myTower.Damage, Is.EqualTo(150));
            Assert.That(this._myTower.Range, Is.EqualTo(100));
            Assert.IsFalse(this._myTower.IsActive);
            Assert.IsFalse(this._myTower.IsDead());
            Assert.IsFalse(this._myTower.CurrentTarget.IsPresent);
        });
    }
    
    /// <summary>
    /// Kill the Tower and then verify if is dead.
    /// </summary>
    [Test]
    public void ReduceLifeUntilIsDeadTest()
    {
        while (this._myTower.CurrentHp > 0)
        {
            this._myTower.ReduceHpBy(300);
        }
        Assert.IsTrue(this._myTower.IsDead());
    }

    /// <summary>
    /// Test if the current target updates correctly.
    /// </summary>
    [Test]
    public void TargetTest()
    {
        Assert.IsFalse(this._myTower.CurrentTarget.IsPresent);
        this._myTower.SetCurrentTarget(this._enemyTower);
        Assert.IsTrue(this._myTower.CurrentTarget.IsPresent);
    }
}