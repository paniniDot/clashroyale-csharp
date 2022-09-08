using Panni.Source.Model.Users;
using Panni.Source.Utilities;

namespace Panni.Source.Model;

public interface IAttackable
{
    /// <summary>
    /// </summary>
    /// <returns>The current position of this IAttackable.</returns>
    Vector2 GetPosition();

    /// <summary>
    /// Set a new position for this IAttackable.
    /// </summary>
    /// <param name="newPos">The new Position.</param>
    void SetPosition(Vector2 newPos);

    /// <summary>
    /// </summary>
    /// <returns>The range of action of the entity.</returns>
    double GetRange();

    /// <summary>
    /// By default all classes are Nullable in C#, No need to implement an Optional class for it.
    /// </summary>
    /// <returns>the current target, if any, of the entity that implements this interface.</returns>
    IAttackable GetCurrentTarget();

    /// <summary>
    /// Update the current target of this IAttackable.
    /// </summary>
    /// <param name="target">The new target.</param>
    void SetCurrentTarget(IAttackable target);

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
    /// Hit one time the current target, if any.
    /// </summary>
    void AttackCurrentTarget();
    
    /// <summary>
    /// </summary>
    /// <returns>Whether the entity is dead or not.</returns>
    bool IsDead();
    
    /// <summary>
    /// </summary>
    /// <returns>The remaining Health Point of this IAttackable.</returns>
    double GetCurrentHp();

    /// <summary>
    /// </summary>
    /// <returns>The owner of this IAttackable.</returns>
    User GetOwner();
}