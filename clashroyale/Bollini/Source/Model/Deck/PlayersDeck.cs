using System.Numerics;

namespace Bollini.Source.Model.Deck
{
public final class PlayersDeck extends BasicDeck {

  private static readonly PlayersDeck _DECK = new PlayersDeck();
  private readonly Map<String, Card> _deckMap;
  private readonly Map<String, Card> _cardsMap;

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
   * @return getDeckMap
   */
  public Map<String, Card> GetDeck() => deckMap;

/**
   * 
   * @return getCardsMap
   */
  public Map<String, Card> GetCards() => cardsMap;

/**
   * 
   * @param select
   * @return deckMap
   */
  public Map<String, Card> AddDeck(readonly String select) {
    deckMap.put(select, cardsMap.get(select));
    return deckMap;
  }
  /**
   * 
   * @param select
   * @return cardsMap
   */
  public Map<String, Card> AddCard(readonly String select) {
    cardsMap.put(select, deckMap.get(select));
    return cardsMap;
  }
  /**
   * 
   * @param select
   * @return cardsMap
   */
  public Map<String, Card> RemoveCard(readonly String select) {
    cardsMap.remove(select);
    return cardsMap;
  }

  /**
   * 
   * @param select
   * @return deckMap
   */
  public Map<String, Card> RemoveDeckCard(readonly String select) {
    deckMap.remove(select);
    return deckMap;
  }

  /**
   * 
   * @return List<Card> from map
   */
  public List<Card> CardList() => deckMap.entrySet().stream()
        .map(e -> e.getValue())
        .collect(Collectors.toList());
  
  /**
   * 
   * @return List<String> from key deckmap
   */
  public List<String> namesCardsDeck() => deckMap.entrySet().stream()
        .map(e -> e.getKey())
        .collect(Collectors.toList());
/**
   * 
   * @return List<String> from key cardsmap
   */
  public List<String> namesCardsCard() {
    return cardsMap.entrySet().stream()
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