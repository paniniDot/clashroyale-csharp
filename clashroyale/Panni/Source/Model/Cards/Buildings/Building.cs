using Panni.Source.Model.Users;
using Panni.Source.Utilities;

namespace Panni.Source.Model.Cards.Buildings;

public abstract class Building : Card
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="cost">Elixir needed to deploy the troop.</param>
    /// <param name="position">x,y coordinates where the troop has to be deployed.</param>
    /// <param name="owner">Who deployed the troop.</param>
    /// <param name="maxHp">Maximum health of the troop.</param>
    /// <param name="damage">Hp per hit taken by this troop.</param>
    /// <param name="range">The maximum distance between this troop and other entities to being targeted by it.</param>
    protected Building(int cost, Vector2 position, User owner, double maxHp, double damage, double range) : base(cost, position, owner, maxHp, damage, range)
    {
    }
}