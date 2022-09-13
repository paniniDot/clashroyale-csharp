using System.Numerics;
using Panni.Source.Model.Users;

namespace Panni.Source.Model.Towers;

public class QueenTower : Tower
{
    private QueenTower(Vector2 position, double range, double damage, double maxHp, User owner, bool isActive) : base(position, range, damage, maxHp, owner, isActive)
    {
    }
}