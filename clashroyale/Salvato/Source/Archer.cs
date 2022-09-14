using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Panni.Source.Model.Cards.Troops;
using Panni.source.Model.Cards;
using Panni.Source.Model.User;

namespace Salvato.Source
{
    class Archer : Troop
    {
        private const string ArcherWord = "archer";

        private const string SelfWalk = ArcherWord + Path.DirectorySeparatorChar + "self" + Path.DirectorySeparatorChar + "walking" + Path.DirectorySeparatorChar;
        private const string SelfAtt = ArcherWord + Path.DirectorySeparatorChar + "self" + Path.DirectorySeparatorChar + "attacking" + Path.DirectorySeparatorChar;
        private const string BotWalk = ArcherWord + Path.DirectorySeparatorChar + "bot" + Path.DirectorySeparatorChar + "walking" + Path.DirectorySeparatorChar;
        private const string BotAtt = ArcherWord + Path.DirectorySeparatorChar + "bot" + Path.DirectorySeparatorChar + "attacking" + Path.DirectorySeparatorChar;

        private const int ElixirCost = 3;
        private const int Range = 100;

        private Archer(User owner, Vector2 position, double maxHP, double damage)
            : base(Archer.ElixirCost, position, owner, maxHP, damage, Archer.Range)
        {

        }

        public static Troop Create(User user, Vector2 position) => user.CurrentLevel switch
            {
                UserLevel.Lvl1 => new Archer(user, position, 200 * 60, 200),
                UserLevel.Lvl2 => new Archer(user, position, 250 * 60, 220),
                UserLevel.Lvl3 => new Archer(user, position, 300 * 60, 240),
                UserLevel.Lvl4 => new Archer(user, position, 350 * 60, 260),
                UserLevel.Lvl5 => new Archer(user, position, 400 * 60, 280),
                _ => new Archer(user, position, 200 * 60, 200)
            };

        public Dictionary<string, List<string>> GetAnimationFiles() => new Dictionary<string, List<string>>()
            {
                ["SELF_MOVING"] = new List<string> { Archer.SelfWalk + "0.png", Archer.SelfWalk + "1.png" },
                ["SELF_FIGHTING"] = new List<string> { Archer.SelfAtt + "0.png", Archer.SelfAtt + "1.png", Archer.SelfAtt + "2.png", Archer.SelfAtt + "3.png", 
                    Archer.SelfAtt + "4.png", Archer.SelfAtt + "5.png" },
                ["ENEMY_MOVING"] = new List<string> { Archer.BotWalk + "0.png", Archer.BotWalk + "1.png", Archer.BotWalk + "2.png", Archer.BotWalk + "3.png" },
                ["ENEMY_FIGHTING"] = new List<string> { Archer.BotAtt + "0.png", Archer.BotAtt + "1.png", Archer.BotAtt + "2.png", Archer.BotAtt + "3.png", Archer.BotAtt + "4.png" },
                ["AS_CARD"] = new List<string> { "cards" + Path.DirectorySeparatorChar + "ArchersCard.png" }
            };

        public override Card CreateAnother(Vector2 position) => Create(
            this.Owner,
            position);
    }
}
