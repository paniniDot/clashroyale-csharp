using System.Numerics;

namespace Bollini.Source.Model.Deck
{
public sealed class PlayersDeck : BasicDeck {

  private static readonly PlayersDeck _DECK = new PlayersDeck();
  public Map<String, Card> DeckMap { get; }
  public Map<String, Card> CardsMap { get; }

  /**
   * initialize basic cards and decks.
   */
  private PlayersDeck() {
    deckMap = new HashMap<>();
    deckMap.put("Barbarian", Barbarian.create(GlobalData.USER, newPositionFree()));
    deckMap.put("Giant", Giant.create(GlobalData.USER, newPositionFree()));
    deckMap.put("InfernoTower", InfernoTower.create(GlobalData.USER, newPositionFree()));
    deckMap.put("Wizard", Wizard.create(GlobalData.USER, newPositionFree())); 
    cardsMap = new HashMap<>();
    cardsMap.put("Archer", Archer.create(GlobalData.USER, new Vector2(0, 0)));
    cardsMap.put("MiniPekka", MiniPekka.create(GlobalData.USER, new Vector2(0, 0)));
    cardsMap.put("Valkrie", Valkyrie.create(GlobalData.USER, new Vector2(0, 0)));

  }

/**
   * 
   * @param select
   * @return deckMap
   */
  public Map<String, Card> AddDeck(readonly String select) {
    DeckMap.put(select, cardsMap.get(select));
    return DeckMap;
  }
  /**
   * 
   * @param select
   * @return cardsMap
   */
  public Map<String, Card> AddCard(readonly String select) {
    CardsMap.put(select, deckMap.get(select));
    return CardsMap;
  }
  /**
   * 
   * @param select
   * @return cardsMap
   */
  public Map<String, Card> RemoveCard(readonly String select) {
    CardsMap.remove(select);
    return CardsMap;
  }

  /**
   * 
   * @param select
   * @return deckMap
   */
  public Map<String, Card> RemoveDeckCard(readonly String select) {
    DeckMap.remove(select);
    return DeckMap;
  }

  /**
   * 
   * @return List<Card> from map
   */
  public List<Card> CardList() => DeckMap.entrySet().stream()
        .map(e -> e.getValue())
        .collect(Collectors.toList());

/**
   * 
   * @return List<String> from key deckmap
   */
public List<String> namesCardsDeck()
{
    return DeckMap.entrySet().stream()
        .map(e->e.getKey())
        .collect(Collectors.toList());
}

/*  
 *
   * 
   * @return List<String> from key cardsmap
   */
  public List<String> namesCardsCard() {
    return CardsMap.entrySet().stream()
        .map(e -> e.getKey())
        .collect(Collectors.toList());
  }

  /**
   * 
   * 
   * @return the only DECK
   */
  public static PlayersDeck getInstance() => DECK;
}
}