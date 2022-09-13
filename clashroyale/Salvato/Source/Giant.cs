using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Panni.Source.Model.Cards.Troops;
using Panni.Source.Model.User.User;

namespace Salvato.Source
{
    class Giant : Troop
    {
        private const string GiantWord = "giant";

        private const string SelfWalk = GiantWord + Path.DirectorySeparatorChar + "self" + Path.DirectorySeparatorChar + "walking" + Path.DirectorySeparatorChar;
        private const string SelfAtt = GiantWord + Path.DirectorySeparatorChar + "self" + Path.DirectorySeparatorChar + "attacking" + Path.DirectorySeparatorChar;
        private const string BotWalk = GiantWord + Path.DirectorySeparatorChar + "bot" + Path.DirectorySeparatorChar + "walking" + Path.DirectorySeparatorChar;
        private const string BotAtt = GiantWord + Path.DirectorySeparatorChar + "bot" + Path.DirectorySeparatorChar + "attacking" + Path.DirectorySeparatorChar;

        private const int ElixirCost = 5;
        private const int Range = 30;

        private Giant(User owner, Vector2 position, double maxHP, double damage)
            : base(Giant.ElixirCost, position, owner, maxHP, damage, Giant.Range)
        {

        }

        public static Troop Create(User user, Vector2 position) => user.CurrentLevel switch
            {
                UserLevel.Lvl1 => new Giant(user, position, 600 * 60, 300),
                UserLevel.Lvl2 => new Giant(user, position, 650 * 60, 320),
                UserLevel.Lvl3 => new Giant(user, position, 700 * 60, 340),
                UserLevel.Lvl4 => new Giant(user, position, 750 * 60, 360),
                UserLevel.Lvl5 => new Giant(user, position, 800 * 60, 380),
                _ => new Giant(user, position, 600 * 60, 300)
            };

        public Dictionary<string, List<string>> GetAnimationFiles() => new Dictionary<string, List<string>>()
            {
                ["SELF_MOVING"] = new List<string> { Giant.SelfWalk + "0.png", Giant.SelfWalk + "1.png" },
                ["SELF_FIGHTING"] = new List<string> { Giant.SelfAtt + "0.png", Giant.SelfAtt + "1.png", Giant.SelfAtt + "2.png" },
                ["ENEMY_MOVING"] = new List<string> { Giant.BotWalk + "0.png", Giant.BotWalk + "1.png" },
                ["ENEMY_FIGHTING"] = new List<string> { Giant.BotAtt + "0.png", Giant.BotAtt + "1.png", Giant.BotAtt + "2.png" },
                ["AS_CARD"] = new List<string> { "cards" + Path.DirectorySeparatorChar + "GiantsCard.png" }
            };

    }
}
