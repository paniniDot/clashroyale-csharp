using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Panni.Source.Model.Cards.Troops;
using Panni.Source.Model.User.User;

namespace Salvato.Source
{
    class MiniPekka : Troop
    {
        private const string MiniPekkaWord = "miniPekka";

        private const string SelfWalk = MiniPekkaWord + Path.DirectorySeparatorChar + "self" + Path.DirectorySeparatorChar + "walking" + Path.DirectorySeparatorChar;
        private const string SelfAtt = MiniPekkaWord + Path.DirectorySeparatorChar + "self" + Path.DirectorySeparatorChar + "attacking" + Path.DirectorySeparatorChar;
        private const string BotWalk = MiniPekkaWord + Path.DirectorySeparatorChar + "bot" + Path.DirectorySeparatorChar + "walking" + Path.DirectorySeparatorChar;
        private const string BotAtt = MiniPekkaWord + Path.DirectorySeparatorChar + "bot" + Path.DirectorySeparatorChar + "attacking" + Path.DirectorySeparatorChar;

        private const int ElixirCost = 5;
        private const int Range = 60;

        private MiniPekka(User owner, Vector2 position, double maxHP, double damage)
            : base(MiniPekka.ElixirCost, position, owner, maxHP, damage, MiniPekka.Range)
        {

        }

        public static Troop Create(User user, Vector2 position) => user.CurrentLevel switch
        {
            UserLevel.Lvl1 => new MiniPekka(user, position, 300 * 60, 150),
            UserLevel.Lvl2 => new MiniPekka(user, position, 350 * 60, 170),
            UserLevel.Lvl3 => new MiniPekka(user, position, 400 * 60, 300),
            UserLevel.Lvl4 => new MiniPekka(user, position, 450 * 60, 325),
            UserLevel.Lvl5 => new MiniPekka(user, position, 500 * 60, 360),
            _ => new MiniPekka(user, position, 300 * 60, 150)
        };

        public Map<String, List<String>> GetAnimationFiles() => Map.of(
            "SELF_MOVING", List.of(MiniPekka.SelfWalk + "0.png", MiniPekka.SelfWalk + "1.png", MiniPekka.SelfWalk + "2.png", MiniPekka.SelfWalk + "3.png",
            MiniPekka.SelfWalk + "4.png", MiniPekka.SelfWalk + "5.png", MiniPekka.SelfWalk + "6.png"),
            "SELF_FIGHTING", List.of(MiniPekka.SelfAtt + "0.png", MiniPekka.SelfAtt + "1.png", MiniPekka.SelfAtt + "2.png", MiniPekka.SelfAtt + "3.png"),
            "ENEMY_MOVING", List.of(MiniPekka.BotWalk + "0.png", MiniPekka.BotWalk + "1.png"),
            "ENEMY_FIGHTING", List.of(MiniPekka.BotAtt + "0.png", MiniPekka.BotAtt + "1.png", MiniPekka.BotAtt + "2.png", MiniPekka.BotAtt + "3.png"),
            "AS_CARD", List.of("cards" + Path.DirectorySeparatorChar + "MiniPekkasCard.png"));
    }
}
