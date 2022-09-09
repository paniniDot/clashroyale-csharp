using Panni.Source.Utilities.Vector2;

namespace Salvato.Source
{
    class Barbarian : Troop
    {
        private const int ELIXIR_COST = 4;
        private const int RANGE = 60;

        private Barbarian(User owner, Vector2 position, double maxHP, double damage)
            : base(Barbarian.ELIXIR_COST, position, owner, maxHP, damage, Speeds.MEDIUM, Barbarian.RANGE)
        { 
        
        }
    }
}
