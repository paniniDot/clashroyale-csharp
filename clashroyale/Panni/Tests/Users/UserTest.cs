using NUnit.Framework;
using Panni.Source.Model.Users;

namespace Panni.Tests.Users;

/// <summary>
/// Test case for User class.
/// </summary>
[TestFixture]
public class UserTest
{
    private const string UserName = "Panini";
    private User _user;

    [SetUp]
    public void InitializeUser()
    {
        this._user = new User(UserName);
    }

    /// <summary>
    /// Test the initial values given to each property.
    /// </summary>
    [Test]
    public void InitialPropertiesTest()
    {
        Assert.Multiple(() =>
        {
            Assert.NotNull(this._user);
            Assert.That(this._user.Name, Is.EqualTo(UserName));
            Assert.Zero(this._user.CurrentXp);
            Assert.Zero(this._user.Plays);
            Assert.Zero(this._user.Wins);
            Assert.Zero(this._user.DestroyedTowers); 
        });
    }

    /// <summary>
    /// Test a step by step level up.
    /// </summary>
    [Test]
    public void LevelUpTest()
    {
        this._user.AwardXps(10);
        Assert.That(this._user.CurrentLevel, Is.EqualTo(UserLevel.Lvl1));
        this._user.AwardXps(10);
        Assert.That(this._user.CurrentLevel, Is.EqualTo(UserLevel.Lvl2));
        this._user.AwardXps(60);
        Assert.That(this._user.CurrentLevel, Is.EqualTo(UserLevel.Lvl3));
    }

    /// <summary>
    /// Test a multiple level up.
    /// </summary>
    [Test]
    public void MultipleLevelUpTest()
    {
        this._user.AwardXps(80);
        Assert.That(this._user.CurrentLevel, Is.EqualTo(UserLevel.Lvl3));
    }

    /// <summary>
    /// Test if with a huge award the user level not goes out of bounds.
    /// </summary>
    [Test]
    public void OutOfBoundsLevelUpTest()
    {
        this._user.AwardXps(3000);
        Assert.That(this._user.CurrentLevel, Is.EqualTo(UserLevel.Lvl5));
    }
}