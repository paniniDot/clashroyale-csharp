using Panni.Source.Model.Users;
using Panni.Source.Utilities;

namespace Panni.Source.Model.Cards;

public abstract class Card : IAttackable
{
    public int Cost { get; }
    public Vector2 Position { get; set; }
    public double Range { get; }
    public double CurrentHp { get; private set; }
    public User Owner { get; }
    public double Damage { get; }
    public Optional<IAttackable> CurrentTarget { get; private set; }

    protected Card(int cost, Vector2 position, User owner, double maxHp, double damage, double range)
    {
        this.Cost = cost;
        this.Owner = owner;
        this.Position = position;
        this.CurrentHp = maxHp;
        this.Damage = damage;
        this.Range = range;
        this.CurrentTarget = Optional<IAttackable>.Empty();
    }

    public void SetCurrentTarget(IAttackable target)
    {
        this.CurrentTarget = Optional<IAttackable>.Of(target);
    }

    public void ResetCurrentTarget()
    {
        this.CurrentTarget = Optional<IAttackable>.Empty();
    }

    public void ReduceHpBy(double damage)
    {
        this.CurrentHp = this.CurrentHp < damage ? 0 : this.CurrentHp - damage;
    }

    public void AttackCurrentTarget()
    {
        if (this.CurrentTarget.IsPresent)
            this.CurrentTarget.Get().ReduceHpBy(this.Damage);
    }

    public bool IsDead()
    {
        return this.CurrentHp <= 0;
    }

    private bool Equals(Card other)
    {
        return Cost == other.Cost 
               && Position.Equals(other.Position) 
               && Range.Equals(other.Range) 
               && CurrentHp.Equals(other.CurrentHp) 
               && Owner.Equals(other.Owner) 
               && Damage.Equals(other.Damage) 
               && CurrentTarget.Equals(other.CurrentTarget);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return (obj.GetType() == this.GetType()) && Equals((Card) obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Cost, Position, Range, CurrentHp, Owner, Damage, CurrentTarget);
    }
}