using Panni.Source.Model;
using Panni.Source.Model.Cards;
using Panni.Source.Model.Towers;
using Panni.Source.Model.Users;
using Panni.Source.Utilities;
using Vector2 = System.Numerics.Vector2;

namespace Fiorani.Source;

public abstract class GameModel
{
    /**
   * the number of cards that can be chosen in every moment.
   */
    protected const int ChoosableCards = 4;

    private List<Card> _playerCards;
    private List<Card> _playerCardQueue;
    private List<Card> _playerDeployedCards;
    private List<Card> _playerChoosableCards;
    private List<Tower> _playerActiveTowers;

    /**
   * 
   * @param playerCards
   *              the player deck.
   * @param user
   *              the user who is playing.
   */
    public GameModel(List<Card> playerCards, User user)
    {
        _playerCards = playerCards.ToList();
        _playerCardQueue = playerCards.ToList();
        _playerDeployedCards = new List<Card>();
        _playerChoosableCards = new List<Card>();
        Enumerable.Range(0, ChoosableCards).ToList()
            .ForEach(i =>
            {
                this._playerChoosableCards.Add(_playerCardQueue[0]);
                _playerCardQueue.RemoveAt(0);
            });
        this._playerActiveTowers = this.GetPlayerTowers(user);
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


    /**
   * 
   * @return a list of every card used from the player during the match.
   */
    public List<Card> GetPlayerDeck()
    {
        return this._playerCards;
    }

    /**
   * 
   * @return the queued cards of the player.
   */
    public List<Card> GetPlayerCardQueue()
    {
        return this._playerCardQueue;
    }

    /**
   * 
   * @return a list of user currently deployed cards.
   */
    public List<Card> GetPlayerDeployedCards()
    {
        return this._playerDeployedCards;
    }

    /**
   * 
   * @return a list of user currently choosable cards.
   */
    public List<Card> GetPlayerChoosableCards()
    {
        return this._playerChoosableCards;
    }

    /**
   * Deploys a card of the player.
   * @param card
   *           the card to be deployed.
   */
    public void DeployPlayerCard(Card card)
    {
        if (this._playerChoosableCards.Contains(card))
        {
            this._playerChoosableCards.Remove(card);
            this._playerCardQueue.Add(card);
            this._playerDeployedCards.Add(card);
        }
    }

    /**
   * @param origin 
                  the start position of the new card.
   * @return an {@link Optional} of the first card entered in the queue.
   * 
   */
    public Optional<Card> GetPlayerNextQueuedCard(Vector2 origin)
    {
        if (this._playerCardQueue.Count == 0)
        {
            return Optional<Card>.Empty();
        }
        var nextCard = this._playerCardQueue[0].CreateAnother(origin);
        this._playerCardQueue.RemoveAt(0);
        this._playerChoosableCards.Add(nextCard);
        return Optional<Card>.Of(nextCard);
    }

    /**
   * Removes a card from the map.
   * @param card
   *           the card to be removed.
   */
    public void RemoveUserCardFromMap(Card card)
    {
        if (this._playerDeployedCards.Contains(card))
        {
            this._playerDeployedCards.Remove(card);
        }
    }

    /**
   * 
   * @return the currently active towers of the user.
   */
    public List<Tower> GetPlayerActiveTowers()
    {
        return this._playerActiveTowers;
    }

    /**
   * If not already, destroys a user tower.
   * 
   * @param tower
   *            the tower to be destroyed.
   */
    public void DestroyUserTower(Tower tower)
    {
        if (this._playerActiveTowers.Contains(tower))
        {
            this._playerActiveTowers.Remove(tower);
        }
    }

    /**
   * Remove a player attackable from the arena, whether is a tower or a card.
   * 
   * @param target
   *              the attackable to be removed.
   */
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

    /**
   * 
   * @param target
   *              the attackable to find if is tower or not.
   * @return
   *              whether the target is a tower or not.
   */
    protected bool IsTower(IAttackable target)
    {
        return target is QueenTower or KingTower;
    }

    /**
   * 
   * @param target
   *               the attackable to find if the user is the owner.
   * @return
   *               whether the user is the owner of this target or not.
   */
    protected bool IsUserTheOwner(IAttackable target)
    {
        return true;
        //return target.Owner().equals(GlobalData.USER);
    }

    /**
   * 
   * @return a list of attackable elements of the player.
   */
    public List<IAttackable> GetPlayerAttackable()
    {
        return new List<IAttackable>(this._playerDeployedCards.Union<IAttackable>(_playerActiveTowers));
    }

    /**
   * Find targets, if any, for a user attackables looking for them in the enemy attackables (whether is a bot or real player).
   */
    public abstract void FindAttackableTargets();

    /**
   * Handle the attack functionality of both user and enemy attackables (whether is a bot or real player).
   */
    public abstract void HandleAttackTargets();
}