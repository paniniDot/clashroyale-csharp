using Panni.Source.Model.Users;
using System.Numerics;

namespace Panni.Source.Model.Cards.Troops;

public class Wizard : Troop
{
    private const int ElixirCost = 5;
    private new const double Range = 100;

    private Wizard(User owner, Vector2 position, double maxHp, double damage) : base(Wizard.ElixirCost, position, owner,
        maxHp, damage, Wizard.Range)
    {
    }

    /// <summary>
    /// </summary>
    /// <param name="user">The owner of this Troop.</param>
    /// <param name="position">Where this Troop has been deployed.</param>
    /// <returns>A new Wizard based on the user level and its position.</returns>
    public static Troop Create(User user, Vector2 position)
    {
        return user.CurrentLevel switch
        {
            UserLevel.Lvl1 => new Wizard(user, position, 12000, 100),
            UserLevel.Lvl2 => new Wizard(user, position, 13000, 120),
            UserLevel.Lvl3 => new Wizard(user, position, 14000, 140),
            UserLevel.Lvl4 => new Wizard(user, position, 15000, 160),
            UserLevel.Lvl5 => new Wizard(user, position, 16000, 180),
            _ => new Wizard(user, position, 12000, 100)
        };
    }
}