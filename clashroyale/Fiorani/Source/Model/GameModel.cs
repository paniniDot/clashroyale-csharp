using Panni.Source.Model;
using Panni.Source.Model.Cards;
using Panni.Source.Model.Towers;
using Panni.Source.Model.Users;
using Panni.Source.Utilities;
using Vector2 = System.Numerics.Vector2;

namespace Fiorani.Source.Model;

/// <summary>
/// Defines the logic to be used inside the game.
/// </summary>
public abstract class GameModel
{
    /// <summary>
    /// the number of cards that can be chosen in every moment.
    /// </summary>
    protected const int ChoosableCards = 4;

    public List<Card> PlayerCards { get; }
    public List<Card> PlayerCardQueue { get; }
    public List<Card> PlayerDeployedCards { get; }
    public List<Card> PlayerChoosableCards { get; }
    public List<Tower> PlayerActiveTowers { get; }

    /// <param name="playerCards">the player deck.</param>
    /// <param name="user">the user who is playing.</param>
    public GameModel(List<Card> playerCards, User user)
    {
        PlayerCards = playerCards.ToList();
        PlayerCardQueue = playerCards.ToList();
        PlayerDeployedCards = new List<Card>();
        PlayerChoosableCards = new List<Card>();
        Enumerable.Range(0, ChoosableCards).ToList()
            .ForEach(i =>
            {
                this.PlayerChoosableCards.Add(PlayerCardQueue[0]);
                PlayerCardQueue.RemoveAt(0);
            });
        this.PlayerActiveTowers = this.GetPlayerTowers(user);
    }

    private List<Tower> GetPlayerTowers(User user)
    {
        List<Tower> towers = new List<Tower>();
        var leftTowerPosition = new Vector2(238, 356);
        var rightTowerPosition = new Vector2(448, 356);
        var centralTowerPosition = new Vector2(344, 310);
        towers.Add(QueenTower.Create(user, leftTowerPosition));
        towers.Add(QueenTower.Create(user, rightTowerPosition));
        towers.Add(KingTower.Create(user, centralTowerPosition));
        return towers;
    }

    /// <summary>
    /// Deploys a card of the player.
    /// </summary>
    /// <param name="card">the card to be deployed.</param>
    public void DeployPlayerCard(Card card)
    {
        if (this.PlayerChoosableCards.Contains(card))
        {
            this.PlayerChoosableCards.Remove(card);
            this.PlayerCardQueue.Add(card);
            this.PlayerDeployedCards.Add(card);
        }
    }

    /// <param name="origin">the start position of the new card.</param>
    /// <returns>an {@link Optional} of the first card entered in the queue.</returns>
    public Optional<Card> GetPlayerNextQueuedCard(Vector2 origin)
    {
        if (this.PlayerCardQueue.Count == 0)
        {
            return Optional<Card>.Empty();
        }

        var nextCard = this.PlayerCardQueue[0].CreateAnother(origin);
        this.PlayerCardQueue.RemoveAt(0);
        this.PlayerChoosableCards.Add(nextCard);
        return Optional<Card>.Of(nextCard);
    }

    /// <summary>
    /// Removes a card from the map.
    /// </summary>
    /// <param name="card">the card to be removed.</param>
    public void RemoveUserCardFromMap(Card card)
    {
        if (this.PlayerDeployedCards.Contains(card))
        {
            this.PlayerDeployedCards.Remove(card);
        }
    }


    /// <summary>
    /// If not already, destroys a user tower.
    /// </summary>
    /// <param name="tower">the tower to be destroyed.</param>
    public void DestroyUserTower(Tower tower)
    {
        if (this.PlayerActiveTowers.Contains(tower))
        {
            this.PlayerActiveTowers.Remove(tower);
        }
    }

    /// <summary>
    /// Remove a player attackable from the arena, whether is a tower or a card.
    /// </summary>
    /// <param name="target">the attackable to be removed.</param>
    protected void RemoveUserAttackableFromArena(IAttackable target)
    {
        if (IsTower(target))
        {
            this.DestroyUserTower((Tower) target);
        }
        else
        {
            this.RemoveUserCardFromMap((Card) target);
        }
    }

    /// <param name="target">the attackable to find if is tower or not.</param>
    /// <returns>whether the target is a tower or not.</returns>
    protected bool IsTower(IAttackable target)
    {
        return target is QueenTower or KingTower;
    }

    /// <param name="target">the attackable to find if the user is the owner.</param>
    /// <returns>whether the user is the owner of this target or not.</returns>
    protected bool IsUserTheOwner(IAttackable target)
    {
        return true;
        //return target.Owner().equals(GlobalData.USER);
    }

    /// <summary>
    /// a list of attackable elements of the player.
    /// </summary>
    public List<IAttackable> GetPlayerAttackable()
    {
        return new List<IAttackable>(this.PlayerDeployedCards.Union<IAttackable>(PlayerActiveTowers));
    }

    /// <summary>
    /// Find targets, if any, for a user attackables looking for them in the enemy attackables (whether is a bot or real player).
    /// </summary>
    public abstract void FindAttackableTargets();

    /// <summary>
    /// Handle the attack functionality of both user and enemy attackables (whether is a bot or real player).
    /// </summary>
    public abstract void HandleAttackTargets();
}