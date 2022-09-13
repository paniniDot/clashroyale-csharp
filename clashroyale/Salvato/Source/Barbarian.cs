using Panni.Source.Model.Cards.Troops;
using Panni.Source.Model.User.User;
using System;
using System.IO;
using System.Numerics;

namespace Salvato.Source
{
    class Barbarian : Troop
    {
        private const string BarbarianWord = "barbarian";

        private const string SelfWalk = BarbarianWord + Path.DirectorySeparatorChar + "self" + Path.DirectorySeparatorChar + "walking" + Path.DirectorySeparatorChar;
        private const string SelfAtt = BarbarianWord + Path.DirectorySeparatorChar + "self" + Path.DirectorySeparatorChar + "attacking" + Path.DirectorySeparatorChar;
        private const string BotWalk = BarbarianWord + Path.DirectorySeparatorChar + "bot" + Path.DirectorySeparatorChar + "walking" + Path.DirectorySeparatorChar;
        private const string BotAtt = BarbarianWord + Path.DirectorySeparatorChar + "bot" + Path.DirectorySeparatorChar + "attacking" + Path.DirectorySeparatorChar;

        private const int ElixirCost = 4;
        private const int Range = 60;

        private Barbarian(User owner, Vector2 position, double maxHP, double damage)
            : base(Barbarian.ElixirCost, position, owner, maxHP, damage, Barbarian.Range)
        {

        }

        public static Troop Create(User user, Vector2 position) => user.CurrentLevel switch
        {
            UserLevel.Lvl1 => new Barbarian(user, position, 400*60, 200),
            UserLevel.Lvl2 => new Barbarian(user, position, 450*60, 220),
            UserLevel.Lvl3 => new Barbarian(user, position, 500*60, 240),
            UserLevel.Lvl4 => new Barbarian(user, position, 550*60, 260),
            UserLevel.Lvl5 => new Barbarian(user, position, 600*60, 280),
            _ => new Barbarian(user, position, 24000, 200)
        };

        public Map<String, List<String>> GetAnimationFiles() => Map.of(
            "SELF_MOVING", List.of(Barbarian.SelfWalk + "0.png", Barbarian.SelfWalk + "1.png", Barbarian.SelfWalk + "2.png", Barbarian.SelfWalk + "3.png",
            Barbarian.SelfWalk + "4.png", Barbarian.SelfWalk + "5.png", Barbarian.SelfWalk + "6.png"),
            "SELF_FIGHTING", List.of(Barbarian.SelfAtt + "0.png", Barbarian.SelfAtt + "1.png", Barbarian.SelfAtt + "2.png", Barbarian.SelfAtt + "3.png"),
            "ENEMY_MOVING", List.of(Barbarian.BotWalk + "0.png", Barbarian.BotWalk + "1.png"),
            "ENEMY_FIGHTING", List.of(Barbarian.BotAtt + "0.png", Barbarian.BotAtt + "1.png", Barbarian.BotAtt + "2.png", Barbarian.BotAtt + "3.png"),
            "AS_CARD", List.of("cards" + Path.DirectorySeparatorChar + "BarbariansCard.png"));
    }
}
