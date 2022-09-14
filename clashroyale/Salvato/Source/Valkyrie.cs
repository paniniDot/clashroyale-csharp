using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Panni.Source.Model.Cards.Troops;
using Panni.source.Model.Cards;
using Panni.Source.Model.User;

namespace Salvato.Source
{
    class Valkyrie : Troop
    {
        private const string ValkyrieWord = "valkyrie";

        private const string SelfWalk = ValkyrieWord + Path.DirectorySeparatorChar + "self" + Path.DirectorySeparatorChar + "walking" + Path.DirectorySeparatorChar;
        private const string SelfAtt = ValkyrieWord + Path.DirectorySeparatorChar + "self" + Path.DirectorySeparatorChar + "attacking" + Path.DirectorySeparatorChar;
        private const string BotWalk = ValkyrieWord + Path.DirectorySeparatorChar + "bot" + Path.DirectorySeparatorChar + "walking" + Path.DirectorySeparatorChar;
        private const string BotAtt = ValkyrieWord + Path.DirectorySeparatorChar + "bot" + Path.DirectorySeparatorChar + "attacking" + Path.DirectorySeparatorChar;

        private const int ElixirCost = 4;
        private const int Range = 60;

        private Valkyrie(User owner, Vector2 position, double maxHP, double damage)
            : base(Valkyrie.ElixirCost, position, owner, maxHP, damage, Valkyrie.Range)
        {

        }

        public static Troop Create(User user, Vector2 position) => user.CurrentLevel switch
            {
                UserLevel.Lvl1 => new Valkyrie(user, position, 600 * 60, 300),
                UserLevel.Lvl2 => new Valkyrie(user, position, 650 * 60, 320),
                UserLevel.Lvl3 => new Valkyrie(user, position, 700 * 60, 340),
                UserLevel.Lvl4 => new Valkyrie(user, position, 750 * 60, 360),
                UserLevel.Lvl5 => new Valkyrie(user, position, 800 * 60, 380),
                _ => new Valkyrie(user, position, 600 * 60, 300)
            };

        public Dictionary<string, List<string>> GetAnimationFiles() => new Dictionary<string, List<string>>()
            {
                ["SELF_MOVING"] = new List<string> { Valkyrie.SelfWalk + "0.png", Valkyrie.SelfWalk + "1.png" },
                ["SELF_FIGHTING"] = new List<string> { Valkyrie.SelfAtt + "0.png", Valkyrie.SelfAtt + "1.png", Valkyrie.SelfAtt + "2.png", Valkyrie.SelfAtt + "3.png",
                    Valkyrie.SelfAtt + "4.png", Valkyrie.SelfAtt + "5.png" },
                ["ENEMY_MOVING"] = new List<string> { Valkyrie.BotWalk + "0.png", Valkyrie.BotWalk + "1.png" },
                ["ENEMY_FIGHTING"] = new List<string> { Valkyrie.BotAtt + "0.png", Valkyrie.BotAtt + "1.png", Valkyrie.BotAtt + "2.png", Valkyrie.BotAtt + "3.png", 
                    Valkyrie.BotAtt + "4.png", Valkyrie.BotAtt + "5.png", Valkyrie.BotAtt + "6.png" },
                ["AS_CARD"] = new List<string> { "cards" + Path.DirectorySeparatorChar + "ValkyriesCard.png" }
            };

        public override Card CreateAnother(Vector2 position) => Create(
            this.Owner,
            position);
    }
}
