using System.Numerics;
using Fiorani.Source;
using NUnit.Framework;
using Panni.Source.Model.Cards;
using Panni.Source.Model.Cards.Troops;
using Panni.Source.Model.Users;

namespace Fiorani.Test;

public class BotGameModelTest
{
    private BotGameModel model;
    private List<Card> BOT_DECK;
    private List<Card> PLAYER_DECK;
    [SetUp]
    public void Initialize()
    {
        var bot = new Bot();
        var user = new User("player");
        this.BOT_DECK = new List<Card>();
        this.BOT_DECK.Add(Wizard.Create(bot, new Vector2(100, 1000)));
        this.BOT_DECK.Add(Wizard.Create(bot, new Vector2(100, 1000)));
        this.BOT_DECK.Add(Wizard.Create(bot, new Vector2(100, 1000)));
        this.BOT_DECK.Add(Wizard.Create(bot, new Vector2(100, 1000)));
        this.PLAYER_DECK = new List<Card>();
        this.PLAYER_DECK.Add(Wizard.Create(user, new Vector2(100, 100)));
        this.PLAYER_DECK.Add(Wizard.Create(user, new Vector2(100, 100)));
        this.PLAYER_DECK.Add(Wizard.Create(user, new Vector2(100, 100)));
        this.PLAYER_DECK.Add(Wizard.Create(user, new Vector2(100, 100)));
        this.model = new BotGameModel(PLAYER_DECK, BOT_DECK, user, bot);
    }

    [Test]
    public void PlayerTest()
    {
        Assert.That(this.model.GetPlayerDeck().Count, Is.EqualTo(4));
        Assert.That(this.model.GetPlayerCardQueue().Count, Is.EqualTo(0));
        Assert.That(this.model.GetPlayerDeployedCards().Count, Is.EqualTo(0));
        Assert.That(this.model.GetPlayerChoosableCards().Count, Is.EqualTo(4));
        Assert.That(this.model.GetPlayerActiveTowers().Count, Is.EqualTo(3));
        Assert.That(this.model.GetPlayerAttackable().Count, Is.EqualTo(3));
    }
    
    [Test]
    public void BotTest()
    {
        Assert.That(this.model.GetBotDeck().Count, Is.EqualTo(4));
        Assert.That(this.model.GetPBotCardQueue().Count, Is.EqualTo(0));
        Assert.That(this.model.GetBotDeployedCards().Count, Is.EqualTo(0));
        Assert.That(this.model.GetBotChoosableCards().Count, Is.EqualTo(4));
        Assert.That(this.model.GetBotActiveTowers().Count, Is.EqualTo(3));
        Assert.That(this.model.GetBotAttackable().Count, Is.EqualTo(3));
    }
    
    [Test]
    public void TargetTest()
    {
        this.model.DeployBotCard(this.model.GetBotChoosableCards()[0]);
        Assert.That(this.model.GetBotAttackable().Count, Is.EqualTo(4));
        Assert.That(this.model.GetBotDeployedCards().Count, Is.EqualTo(1));
        this.model.DeployPlayerCard(this.model.GetPlayerChoosableCards()[0]);
        Assert.That(this.model.GetPlayerAttackable().Count, Is.EqualTo(4));
        Assert.That(this.model.GetPlayerDeployedCards().Count, Is.EqualTo(1));
        this.model.GetPlayerDeployedCards()[0].Position = new Vector2(350, 350); 
        this.model.GetBotDeployedCards()[0].Position = new Vector2(350, 350); 
        this.model.FindAttackableTargets();
        while (true)
        {
            this.model.HandleAttackTargets();
            if (!this.model.GetBotDeployedCards()[0].IsDead())
            {
                this.model.RemoveBotCardFromMap(this.model.GetBotDeployedCards()[0]);
                Assert.That(this.model.GetBotDeployedCards().Count, Is.EqualTo(0));
                Assert.That(this.model.GetBotAttackable().Count, Is.EqualTo(3));
                break;
            } else if (!this.model.GetPlayerDeployedCards()[0].IsDead())
            {
                this.model.RemoveUserCardFromMap(this.model.GetPlayerDeployedCards()[0]);
                Assert.That(this.model.GetPlayerDeployedCards().Count, Is.EqualTo(0));
                Assert.That(this.model.GetPlayerAttackable().Count, Is.EqualTo(3));
                break;
            }
        }
        
        
    }
}