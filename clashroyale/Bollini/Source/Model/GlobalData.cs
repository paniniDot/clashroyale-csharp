using System.Numerics;
using Bollini.Source.Controller;
using Panni.Source.Model.Cards;
using Panni.Source.Model.Cards.Troops;
using Panni.Source.Model.Users;

namespace Bollini.Source.Model
{

 /// <summary>
 /// Class used to easily provide and instance of User and Bot.
 /// </summary>
 
 public sealed class GlobalData 
{
    /// <summary>
    /// Provides a user instance.
    /// </summary>
    public static readonly User User = SaveController.LoadUser();

    /// <summary>
    /// Provides a bot.
    ///</summary> 
    public static readonly Bot Bot
        = new Bot();

    /// <summary>
    /// Provides the Bot deck.
    ///</summary>
    public static readonly List<Card> BotDeck = new List<Card>
    {
        Wizard.Create(Bot, new Vector2(100, 1000)),
        Wizard.Create(Bot, new Vector2(200, 1000)),
        Wizard.Create(Bot, new Vector2(300, 1000)),
        Wizard.Create(Bot, new Vector2(400, 1000))};

    private GlobalData() 
    {
    }
}
}