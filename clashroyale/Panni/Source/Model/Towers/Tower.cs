using System.Numerics;
using Panni.Source.Model.Users;
using Panni.Source.Utilities;

namespace Panni.Source.Model.Towers;

public class Tower : IAttackable
{
    public Vector2 Position { get; }
    public double Range { get; }
    public double Damage { get; }
    public double CurrentHp { get; private set; }
    public User Owner { get; }
    public Optional<IAttackable> CurrentTarget { get; private set; }
    public bool IsActive { get; private set; }

    protected Tower(Vector2 position, double range, double damage, double maxHp, User owner, bool isActive)
    {
        this.Position = position;
        this.Range = range;
        this.Damage = damage;
        this.CurrentHp = maxHp;
        this.Owner = owner;
        this.IsActive = isActive;
        this.CurrentTarget = Optional<IAttackable>.Empty();
    }

    public void SetCurrentTarget(IAttackable target)
    {
        this.CurrentTarget = Optional<IAttackable>.Of(target);
    }

    public void AttackCurrentTarget()
    {
        if (this.CurrentTarget.IsPresent)
        {
            this.CurrentTarget.Get().ReduceHpBy(this.Damage);
        }
    }

    public void ResetCurrentTarget()
    {
        this.CurrentTarget = Optional<IAttackable>.Empty();
    }

    public void ReduceHpBy(double damage)
    {
        this.CurrentHp = this.CurrentHp < damage ? 0 : this.CurrentHp - damage;
    }

    public bool IsDead()
    {
        return this.CurrentHp <= 0;
    }

    public void SetActive()
    {
        this.IsActive = true;
    }

    private bool Equals(Tower other)
    {
        return Position.Equals(other.Position) 
               && Range.Equals(other.Range) 
               && Damage.Equals(other.Damage) 
               && CurrentHp.Equals(other.CurrentHp) 
               && Owner.Equals(other.Owner) 
               && CurrentTarget.Equals(other.CurrentTarget) 
               && IsActive == other.IsActive;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return (obj.GetType() == this.GetType()) && Equals((Tower) obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Position, Range, Damage, CurrentHp, Owner, CurrentTarget, IsActive);
    }
}