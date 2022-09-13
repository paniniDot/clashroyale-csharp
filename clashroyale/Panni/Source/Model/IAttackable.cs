using Panni.Source.Model.Users;
using Panni.Source.Utilities;
using System.Numerics;

namespace Panni.Source.Model;

public interface IAttackable
{
    /// <summary>
    /// The current position of the IAttackable.
    /// </summary>
    Vector2 Position { get; }
    
    /// <summary>
    /// The range of action of the IAttackable.
    /// </summary>
    double Range { get; }
    
    /// <summary>
    /// The damage per hit given from this IAttackable.
    /// </summary>
    double Damage { get; }
    
    /// <summary>
    /// Health points left to the IAttackable.
    /// </summary>
    double CurrentHp { get; }
    
    /// <summary>
    /// The User who owns this IAttackable.
    /// </summary>
    User Owner { get; }
    
    /// <summary>
    /// The current enemy IAttackable targeted by this one (if present).
    /// </summary>
    Optional<IAttackable> CurrentTarget { get; }

    /// <summary>
    /// Update the current target of this IAttackable.
    /// </summary>
    /// <param name="target">The new target.</param>
    void SetCurrentTarget(IAttackable target);

    /// <summary>
    /// Hit one time the current target, if any.
    /// </summary>
    void AttackCurrentTarget();

    /// <summary>
    /// Reset the current target.
    /// </summary>
    void ResetCurrentTarget();

    /// <summary>
    /// Reduce the health of this IAttackable.
    /// </summary>
    /// <param name="damage">The amount of life to be taken.</param>
    void ReduceHpBy(double damage);

    /// <summary>
    /// </summary>
    /// <returns>Whether the entity is dead or not.</returns>
    bool IsDead();
    
}