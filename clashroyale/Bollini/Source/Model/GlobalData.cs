﻿namespace Bollini.Source.Model;

/**
 * Class used to easily provide and instance of User and Bot.
 */
public final class GlobalData {
    /**
   * Provides a user instance.
   */
    public static final User USER = SaveController.loadUser();

    /**
   * Provides a bot.
   */
    public static final Bot BOT = new Bot();

    /**
   * Provides the Bot deck.
   */
    public static final List<Card> BOT_DECK = List.of(
    Wizard.create(BOT, new Vector2(100, 1000)), 
    Barbarian.create(BOT, new Vector2(200, 1000)), 
    Giant.create(BOT, new Vector2(300, 1000)), 
    Wizard.create(BOT, new Vector2(400, 1000)));

    private GlobalData() {
    }
}
