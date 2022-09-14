using System.Numerics;
using Panni.Source.Model;
using Panni.Source.Model.Cards;
using Panni.Source.Model.Towers;
using Panni.Source.Model.Users;
using Panni.Source.Utilities;

namespace Fiorani.Source.Model;
/// <summary>
/// An implementation of GameController in which the user plays against a bot.
/// </summary>
public class BotGameModel : GameModel
{
    public List<Card> BotCards { get; }
    public List<Card> BotCardQueue { get; }
    public List<Card> BotDeployedCards { get; }
    public List<Card> BotChoosableCards { get; }
    public List<Tower> BotActiveTowers { get; }
    /// <param name="playerCards">the player deck.</param>
    /// <param name="botCards">{@inheritDoc}.</param>
    public BotGameModel(List<Card> playerCards, List<Card> botCards, User player, Bot bot) : base(playerCards, player)
    {
        this.BotCards = botCards.ToList();
        this.BotCardQueue = botCards.ToList();
        this.BotDeployedCards = new List<Card>();
        this.BotChoosableCards = new List<Card>();
        Enumerable.Range(0, ChoosableCards).ToList()
            .ForEach(i =>
            {
                this.BotChoosableCards.Add(BotCardQueue[0]);
                BotCardQueue.RemoveAt(0);
            });
        this.BotActiveTowers = this.GetBotTowers(bot);
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
    /// <summary>
    /// Deploys a card of the bot.
    /// </summary>
    /// <param name="card">the card to be deployed.</param>
    public void DeployBotCard(Card card)
    {
        if (this.BotChoosableCards.Contains(card))
        {
            this.BotChoosableCards.Remove(card);
            this.BotCardQueue.Add(card);
            this.BotDeployedCards.Add(card);
        }
    }
    /// <param name="origin">the start position of the new card.</param>
    /// <returns>an {@link Optional} of the first card entered in the queue.</returns>
    public Optional<Card> GetBotNextQueuedCard(Vector2 origin)
    {
        if (this.BotCardQueue.Count == 0)
        {
            return Optional<Card>.Empty();
        }

        var nextCard = this.BotCardQueue[0].CreateAnother(origin);
        this.BotCardQueue.RemoveAt(0);
        this.BotChoosableCards.Add(nextCard);
        return Optional<Card>.Of(nextCard);
    }
    /// <summary>
    /// Removes a card from the map.
    /// </summary>
    /// <param name="card">the card to be removed.</param>
    public void RemoveBotCardFromMap(Card card)
    {
        if (this.BotDeployedCards.Contains(card))
        {
            this.BotDeployedCards.Remove(card);
        }
    }
    /// <summary>
    /// If not already, destroys a bot tower.
    /// </summary>
    /// <param name="tower">the tower to be destroyed.</param>
    public void DestroyBotTower(Tower tower)
    {
        if (this.BotActiveTowers.Contains(tower))
        {
            this.BotActiveTowers.Remove(tower);
        }
    }
    /// <returns>a list of attackable elements of the bot.</returns>
    public List<IAttackable> GetBotAttackable()
    {
        return new List<IAttackable>(this.BotDeployedCards.Union<IAttackable>(BotActiveTowers));
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
        return (Vector2.Distance(selfAttackable.Position, enemyAttackable.Position) <= selfAttackable.Range);
    }

    public override void FindAttackableTargets()
    {
        this.FindTargets(GetPlayerAttackable(), this.GetBotAttackable());
        this.FindTargets(this.GetBotAttackable(), GetPlayerAttackable());
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