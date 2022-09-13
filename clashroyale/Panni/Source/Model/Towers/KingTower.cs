using System.Numerics;
using Panni.Source.Model.Users;

namespace Panni.Source.Model.Towers;

public class KingTower : Tower
{

    private new const double Range = 100;
    
    private KingTower(Vector2 position, double damage, double maxHp, User owner) : base(
        position, KingTower.Range, damage, maxHp, owner, false)
    {
    }

    public static KingTower Create(User owner, Vector2 position)
    {
        return owner.CurrentLevel switch
        {
            UserLevel.Lvl1 => new KingTower(position, 150, 15000, owner),
            UserLevel.Lvl2 => new KingTower(position, 200, 16000, owner),
            UserLevel.Lvl3 => new KingTower(position, 250, 17000, owner),
            UserLevel.Lvl4 => new KingTower(position, 300, 18000, owner),
            UserLevel.Lvl5 => new KingTower(position, 350, 19000, owner),
            _ => new KingTower(position, 150, 15000, owner)
        };
    }
    
}