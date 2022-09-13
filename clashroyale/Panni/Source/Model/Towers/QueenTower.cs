using System.Numerics;
using Panni.Source.Model.Users;

namespace Panni.Source.Model.Towers;

public class QueenTower : Tower
{
    private new const double Range = 100;
    
    private QueenTower(Vector2 position, double damage, double maxHp, User owner) : base(
        position, QueenTower.Range, damage, maxHp, owner, true)
    {
    }

    public static QueenTower Create(User owner, Vector2 position)
    {
        return owner.CurrentLevel switch
        {
            UserLevel.Lvl1 => new QueenTower(position, 100, 12000, owner),
            UserLevel.Lvl2 => new QueenTower(position, 200, 12500, owner),
            UserLevel.Lvl3 => new QueenTower(position, 300, 13000, owner),
            UserLevel.Lvl4 => new QueenTower(position, 400, 13500, owner),
            UserLevel.Lvl5 => new QueenTower(position, 500, 14000, owner),
            _ => new QueenTower(position, 100, 12000, owner)
        };
    }
}