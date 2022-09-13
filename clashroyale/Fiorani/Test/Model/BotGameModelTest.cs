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

    [Test]
    public void PlayerTest()
    {
        Assert.That(this._model.GetPlayerDeck().Count, Is.EqualTo(4));
        Assert.That(this._model.GetPlayerCardQueue().Count, Is.EqualTo(0));
        Assert.That(this._model.GetPlayerDeployedCards().Count, Is.EqualTo(0));
        Assert.That(this._model.GetPlayerChoosableCards().Count, Is.EqualTo(4));
        Assert.That(this._model.GetPlayerActiveTowers().Count, Is.EqualTo(3));
        Assert.That(this._model.GetPlayerAttackable().Count, Is.EqualTo(3));
    }
    
    [Test]
    public void BotTest()
    {
        Assert.That(this._model.GetBotDeck().Count, Is.EqualTo(4));
        Assert.That(this._model.GetPBotCardQueue().Count, Is.EqualTo(0));
        Assert.That(this._model.GetBotDeployedCards().Count, Is.EqualTo(0));
        Assert.That(this._model.GetBotChoosableCards().Count, Is.EqualTo(4));
        Assert.That(this._model.GetBotActiveTowers().Count, Is.EqualTo(3));
        Assert.That(this._model.GetBotAttackable().Count, Is.EqualTo(3));
    }
    
    [Test]
    public void TargetTest()
    {
        this._model.DeployBotCard(this._model.GetBotChoosableCards()[0]);
        Assert.That(this._model.GetBotAttackable().Count, Is.EqualTo(4));
        Assert.That(this._model.GetBotDeployedCards().Count, Is.EqualTo(1));
        this._model.DeployPlayerCard(this._model.GetPlayerChoosableCards()[0]);
        Assert.That(this._model.GetPlayerAttackable().Count, Is.EqualTo(4));
        Assert.That(this._model.GetPlayerDeployedCards().Count, Is.EqualTo(1));
        this._model.GetPlayerDeployedCards()[0].Position = new Vector2(350, 350); 
        this._model.GetBotDeployedCards()[0].Position = new Vector2(350, 350); 
        this._model.FindAttackableTargets();
        while (true)
        {
            this._model.HandleAttackTargets();
            if (!this._model.GetBotDeployedCards()[0].IsDead())
            {
                this._model.RemoveBotCardFromMap(this._model.GetBotDeployedCards()[0]);
                Assert.That(this._model.GetBotDeployedCards().Count, Is.EqualTo(0));
                Assert.That(this._model.GetBotAttackable().Count, Is.EqualTo(3));
                break;
            } else if (!this._model.GetPlayerDeployedCards()[0].IsDead())
            {
                this._model.RemoveUserCardFromMap(this._model.GetPlayerDeployedCards()[0]);
                Assert.That(this._model.GetPlayerDeployedCards().Count, Is.EqualTo(0));
                Assert.That(this._model.GetPlayerAttackable().Count, Is.EqualTo(3));
                break;
            }
        }
        
        
    }
}