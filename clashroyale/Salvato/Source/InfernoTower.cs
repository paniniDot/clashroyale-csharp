using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Panni.Source.Model.Cards.Buildings;
using Panni.source.Model.Cards;
using Panni.Source.Model.User;

namespace Salvato.Source
{
    class InfernoTower : Building
    {
        private const string InfernoTowerWord = "infernoTower";

        private const string SelfWalk = InfernoTowerWord + Path.DirectorySeparatorChar + "self" + Path.DirectorySeparatorChar + "walking" + Path.DirectorySeparatorChar;
        private const string SelfAtt = InfernoTowerWord + Path.DirectorySeparatorChar + "self" + Path.DirectorySeparatorChar + "attacking" + Path.DirectorySeparatorChar;
        private const string BotWalk = InfernoTowerWord + Path.DirectorySeparatorChar + "bot" + Path.DirectorySeparatorChar + "walking" + Path.DirectorySeparatorChar;
        private const string BotAtt = InfernoTowerWord + Path.DirectorySeparatorChar + "bot" + Path.DirectorySeparatorChar + "attacking" + Path.DirectorySeparatorChar;

        private const int ElixirCost = 3;
        private const int Range = 30;

        private readonly List<IAttackable> targets;

        private InfernoTower(User owner, Vector2 position, double maxHP, double damage)
            : base(InfernoTower.ElixirCost, position, owner, maxHP, damage, InfernoTower.Range) => targets = new List<IAttackable>();

        public void Attack()
        {
            foreach (IAttackable enemy in targets)
            {
                enemy.ReduceHPBy(this.GetDamage());
            }
        }

        public void AddTarget(IAttackable enemy)
        {
            if (!targets.Contains(enemy))
            {
                targets.Add(enemy);
            }
        }

        public static Building Create(User user, Vector2 position) => user.CurrentLevel switch
            {
                UserLevel.Lvl1 => new InfernoTower(user, position, 200 * 60, 100),
                UserLevel.Lvl2 => new InfernoTower(user, position, 225 * 60, 120),
                UserLevel.Lvl3 => new InfernoTower(user, position, 250 * 60, 140),
                UserLevel.Lvl4 => new InfernoTower(user, position, 280 * 60, 160),
                UserLevel.Lvl5 => new InfernoTower(user, position, 300 * 60, 180),
                _ => new InfernoTower(user, position, 200 * 60, 200)
            };

        public Dictionary<string, List<string>> GetAnimationFiles() => new Dictionary<string, List<string>>()
            {
                ["SELF_MOVING"] = new List<string> { InfernoTower.SelfWalk + "0.png", InfernoTower.SelfWalk + "1.png", InfernoTower.SelfWalk + "2.png", InfernoTower.SelfWalk + "3.png" },
                ["SELF_FIGHTING"] = new List<string> { InfernoTower.SelfAtt + "0.png", InfernoTower.SelfAtt + "1.png", InfernoTower.SelfAtt + "2.png", InfernoTower.SelfAtt + "3.png" },
                ["ENEMY_MOVING"] = new List<string> { InfernoTower.BotWalk + "0.png", InfernoTower.BotWalk + "1.png", InfernoTower.BotWalk + "2.png", InfernoTower.BotWalk + "3.png" },
                ["ENEMY_FIGHTING"] = new List<string> { InfernoTower.BotAtt + "0.png", InfernoTower.BotAtt + "1.png", InfernoTower.BotAtt + "2.png", InfernoTower.BotAtt + "3.png" },
                ["AS_CARD"] = new List<string> { "cards" + Path.DirectorySeparatorChar + "InfernoTowerCard.png" }
            };

        public override Card CreateAnother(Vector2 position) => Create(
            this.Owner,
            position);
    }
}
