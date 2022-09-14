using System.Collections;
using System.Collections.Immutable;
using System.Numerics;
using Panni.Source.Model.Cards;
using Panni.Source.Model.Cards.Troops;

namespace Bollini.Source.Model.Deck
{
public sealed class PlayersDeck : BasicDeck {

  private static readonly PlayersDeck _DECK = new PlayersDeck();
  public IDictionary<string, Card> DeckMap { get; }
  public IDictionary<string, Card> CardsMap { get; }

  /// <summary>
  /// initialize basic cards and decks.
  /// </summary>
  private PlayersDeck() {
      DeckMap = new Dictionary<string, Card>();
      DeckMap.Add("Barbarian", Wizard.Create(GlobalData.User, NewPositionFree()));
      DeckMap.Add("Giant", Wizard.Create(GlobalData.User, NewPositionFree()));
      DeckMap.Add("InfernoTower", Wizard.Create(GlobalData.User, NewPositionFree()));
      DeckMap.Add("Wizard", Wizard.Create(GlobalData.User, NewPositionFree()));
      CardsMap = new Dictionary<string, Card>();
      CardsMap.Add("Archer", Wizard.Create(GlobalData.User, new Vector2(0, 0)));
      CardsMap.Add("MiniPekka", Wizard.Create(GlobalData.User, new Vector2(0, 0)));
      CardsMap.Add("Valkrie", Wizard.Create(GlobalData.User, new Vector2(0, 0)));


  }

  /// 
  /// <param name="select"> </param>
  /// <returns> deckMap </returns>
  public IDictionary<string, Card> AddDeck(in string select) {
    DeckMap.Add(select, CardsMap[select]);
    return DeckMap;
  }
  /// 
  /// <param name="select"> </param>
  /// <returns> cardsMap </returns>
  public IDictionary<string, Card> AddCard(in string select) {
    CardsMap.Add(select, DeckMap[select]);
    return CardsMap;
  }
  /// 
  /// <param name="select"> </param>
  /// <returns> cardsMap </returns>
  public IDictionary<string, Card> RemoveCard(in string select) {
    CardsMap.Remove(select);
    return CardsMap;
  }

  /// 
  /// <param name="select"> </param>
  /// <returns> deckMap </returns>

  public IDictionary<string, Card> RemoveDeckCard(in string select) {
    DeckMap.Remove(select);
    return DeckMap;
  }

  /// 
  /// <returns> List<Card> from map </returns>
  public List<Card> CardList()
  {
    var c = new List<Card>();
    foreach (var e in DeckMap.Values)
    {
      c.Add(e);
    }
    return c;
  }

  /// 
  /// <returns> List<String> from key deckmap </returns>
  public IList<string> NamesCardsDeck()
{
  var c = new List<string>();
  foreach (var e in DeckMap.Keys)
  {
    c.Add(e);
  }
  return c;
}

  /// 
  /// <returns> List<string> from key cardsmap </returns>
  public IList<string> NamesCardsCard() {
    var c = new List<string>();
    foreach (var e in CardsMap.Keys)
    {
      c.Add(e);
    }

    return c;
  }

  /// 
  /// 
  /// <returns> the only DECK </returns>
  public static PlayersDeck Instance
  {
    get
    {
      return _DECK;
    }
  }
}
}