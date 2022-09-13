using System.Numerics;
using Panni.Source.Model;
using Panni.Source.Model.Cards;
using Panni.Source.Model.Towers;
using Panni.Source.Model.Users;
using Panni.Source.Utilities;

namespace Fiorani.Source;

public class BotGameModel : GameModel
{
    private List<Card> _botCards;
    private List<Card> _botCardQueue;
    private List<Card> _botDeployedCards;
    private List<Card> _botChoosableCards;
    private List<Tower> _botActiveTowers;

    public BotGameModel(List<Card> playerCards, List<Card> botCards, User player, Bot bot) : base(playerCards, player)
    {
        this._botCards = botCards.ToList();
        this._botCardQueue = botCards.ToList();
        this._botDeployedCards = new List<Card>();
        this._botChoosableCards = new List<Card>();
        Enumerable.Range(0, ChoosableCards).ToList()
            .ForEach(i =>
            {
                this._botChoosableCards.Add(_botCardQueue[0]);
                _botCardQueue.RemoveAt(0);
            });
        this._botActiveTowers = this.GetBotTowers(bot);
    }

    private List<Tower> GetBotTowers(Bot bot)
    {
        List<Tower> towers = new List<Tower>();
        var leftTowerPosition = new Vector2(238, 657);
        var rightTowerPosition = new Vector2(448, 657);
        var centralTowerPosition = new Vector2(344, 706);
        towers.Add(QueenTower.Create(bot, leftTowerPosition));
        towers.Add(QueenTower.Create(bot, rightTowerPosition));
        towers.Add(KingTower.Create(bot, centralTowerPosition));
        return towers;
    }

    public List<Card> GetBotDeck()
    {
        return this._botCards;
    }

    public List<Card> GetPBotCardQueue()
    {
        return this._botCardQueue;
    }

    public List<Card> GetBotDeployedCards()
    {
        return this._botDeployedCards;
    }

    public List<Card> GetBotChoosableCards()
    {
        return this._botChoosableCards;
    }

    public void DeployBotCard(Card card)
    {
        if (this._botChoosableCards.Contains(card))
        {
            this._botChoosableCards.Remove(card);
            this._botCardQueue.Add(card);
            this._botDeployedCards.Add(card);
        }
    }

    public Optional<Card> GetBotNextQueuedCard(Vector2 origin)
    {
        if (this._botCardQueue.Count == 0)
        {
            return Optional<Card>.Empty();
        }
        var nextCard = this._botCardQueue[0].CreateAnother(origin);
        this._botCardQueue.RemoveAt(0);
        this._botChoosableCards.Add(nextCard);
        return Optional<Card>.Of(nextCard);
    }

    public void RemoveBotCardFromMap(Card card)
    {
        if (this._botDeployedCards.Contains(card))
        {
            this._botDeployedCards.Remove(card);
        }
    }

    public List<Tower> GetBotActiveTowers()
    {
        return this._botActiveTowers;
    }

    public void DestroyBotTower(Tower tower)
    {
        if (this._botActiveTowers.Contains(tower))
        {
            this._botActiveTowers.Remove(tower);
        }
    }

    public List<IAttackable> GetBotAttackable()
    {
        return new List<IAttackable>(this._botDeployedCards.Union<IAttackable>(_botActiveTowers));
    }

    private void FindTargets(List<IAttackable> selfAttackables, List<IAttackable> enemyAttackables)
    {
        selfAttackables.ForEach(sattackable =>
        {
            if (sattackable.CurrentTarget.IsPresent)
            {
                enemyAttackables.ForEach(eattackable =>
                {
                    if (this.IsInRange(sattackable, eattackable))
                    {
                        sattackable.SetCurrentTarget(eattackable);
                    }
                }); 
            }
        });
    }

    private bool IsInRange(IAttackable selfAttackable, IAttackable enemyAttackable)
    {   
        return (Vector2.Distance(selfAttackable.Position,enemyAttackable.Position) <= selfAttackable.Range);
    }

    public override void FindAttackableTargets()
    {
        this.FindTargets(GetPlayerAttackable(), this.GetBotAttackable());
        this.FindTargets(this.GetBotAttackable(),GetPlayerAttackable());
    }

    private void AttackTargets(List<IAttackable> selfAttackables)
    {
        selfAttackables.ForEach(attackable =>
        {
            attackable.AttackCurrentTarget();
            if (attackable.CurrentTarget.IsPresent)
            {
                IAttackable currentTarget = attackable.CurrentTarget.Get();
                if (currentTarget.IsDead())
                {
                    if (IsUserTheOwner(currentTarget))
                    {
                        RemoveUserAttackableFromArena(currentTarget);
                    }
                    else
                    {
                        this.RemoveBotAttackableFromArena(currentTarget);
                    }

                    attackable.ResetCurrentTarget();
                }
            }
        });
    }

    private void RemoveBotAttackableFromArena(IAttackable target)
    {
        if (IsTower(target))
        {
            this.DestroyBotTower(((Tower) (target)));
        }
        else
        {
            this.RemoveBotCardFromMap(((Card) (target)));
        }
    }


    public override void HandleAttackTargets()
    {
        this.AttackTargets(GetPlayerAttackable());
        this.AttackTargets(this.GetBotAttackable());
    }
}
