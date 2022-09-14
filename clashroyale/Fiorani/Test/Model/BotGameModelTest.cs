using System.Numerics;
using Fiorani.Source.Model;
using NUnit.Framework;
using Panni.Source.Model.Cards;
using Panni.Source.Model.Cards.Troops;
using Panni.Source.Model.Users;

namespace Fiorani.Test.Model;

public class BotGameModelTest
{
    private BotGameModel _model;
    private List<Card> _botDeck;
    private List<Card> _playerDeck;

    /// <summary>
    /// initialize list of player cards and bot cards and create a new model.
    /// </summary>
    [SetUp]
    public void Initialize()
    {
        var bot = new Bot();
        var user = new User("player");
        this._botDeck = new List<Card>();
        this._botDeck.Add(Wizard.Create(bot, new Vector2(100, 1000)));
        this._botDeck.Add(Wizard.Create(bot, new Vector2(100, 1000)));
        this._botDeck.Add(Wizard.Create(bot, new Vector2(100, 1000)));
        this._botDeck.Add(Wizard.Create(bot, new Vector2(100, 1000)));
        this._playerDeck = new List<Card>();
        this._playerDeck.Add(Wizard.Create(user, new Vector2(100, 100)));
        this._playerDeck.Add(Wizard.Create(user, new Vector2(100, 100)));
        this._playerDeck.Add(Wizard.Create(user, new Vector2(100, 100)));
        this._playerDeck.Add(Wizard.Create(user, new Vector2(100, 100)));
        this._model = new BotGameModel(_playerDeck, _botDeck, user, bot);
    }

    /// <summary>
    /// check if the lists have been created correctly of player.
    /// </summary>
    [Test]
    public void PlayerTest()
    {
        Assert.That(this._model.PlayerCards.Count, Is.EqualTo(4));
        Assert.That(this._model.PlayerCardQueue.Count, Is.EqualTo(0));
        Assert.That(this._model.PlayerDeployedCards.Count, Is.EqualTo(0));
        Assert.That(this._model.PlayerChoosableCards.Count, Is.EqualTo(4));
        Assert.That(this._model.PlayerActiveTowers.Count, Is.EqualTo(3));
        Assert.That(this._model.GetPlayerAttackable().Count, Is.EqualTo(3));
    }

    /// <summary>
    /// check if the lists have been created correctly of bot.
    /// </summary>
    [Test]
    public void BotTest()
    {
        Assert.That(this._model.BotCards.Count, Is.EqualTo(4));
        Assert.That(this._model.BotCardQueue.Count, Is.EqualTo(0));
        Assert.That(this._model.BotDeployedCards.Count, Is.EqualTo(0));
        Assert.That(this._model.BotChoosableCards.Count, Is.EqualTo(4));
        Assert.That(this._model.BotActiveTowers.Count, Is.EqualTo(3));
        Assert.That(this._model.GetBotAttackable().Count, Is.EqualTo(3));
    }

    /// <summary>
    /// check if the target is set correctly and if the attack works.
    /// </summary>
    [Test]
    public void TargetAndAttackTest()
    {
        this._model.DeployBotCard(this._model.BotChoosableCards[0]);
        Assert.That(this._model.GetBotAttackable().Count, Is.EqualTo(4));
        Assert.That(this._model.BotDeployedCards.Count, Is.EqualTo(1));
        this._model.BotDeployedCards[0].Position = new Vector2(350, 350);

        this._model.DeployPlayerCard(this._model.PlayerChoosableCards[0]);
        Assert.That(this._model.GetPlayerAttackable().Count, Is.EqualTo(4));
        Assert.That(this._model.PlayerDeployedCards.Count, Is.EqualTo(1));
        this._model.PlayerDeployedCards[0].Position = new Vector2(350, 350);

        this._model.FindAttackableTargets();
        while (true)
        {
            this._model.HandleAttackTargets();
            if (!this._model.BotDeployedCards[0].IsDead())
            {
                this._model.RemoveBotCardFromMap(this._model.BotDeployedCards[0]);
                Assert.That(this._model.BotDeployedCards.Count, Is.EqualTo(0));
                Assert.That(this._model.GetBotAttackable().Count, Is.EqualTo(3));
                break;
            }
            else if (!this._model.PlayerDeployedCards[0].IsDead())
            {
                this._model.RemoveUserCardFromMap(this._model.PlayerDeployedCards[0]);
                Assert.That(this._model.PlayerDeployedCards.Count, Is.EqualTo(0));
                Assert.That(this._model.GetPlayerAttackable().Count, Is.EqualTo(3));
                break;
            }
        }
    }
}