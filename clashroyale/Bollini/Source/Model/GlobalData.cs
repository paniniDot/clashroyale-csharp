using System.Numerics;
using Bollini.Source.Controller;

namespace Bollini.Source.Model
{

/**
 * Class used to easily provide and instance of User and Bot.
 */
public readonly class GlobalData 
{
    /**
   * Provides a user instance.
   */
    public static readonly User USER = SaveController.loadUser();

    /**
   * Provides a bot.
   */
    public static readonly Bot BOT = new Bot();

    /**
   * Provides the Bot deck.
   */
    public static readonly List<Card> BOT_DECK = List.of(
    Wizard.create(BOT, new Vector2(100, 1000)), 
    Barbarian.create(BOT, new Vector2(200, 1000)), 
    Giant.create(BOT, new Vector2(300, 1000)), 
    Wizard.create(BOT, new Vector2(400, 1000)));

    private GlobalData() 
    {
    }
}
}