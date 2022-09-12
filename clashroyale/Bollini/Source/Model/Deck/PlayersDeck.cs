namespace Bollini.Source.Model.Deck;

public final class PlayersDeck extends BasicDeck {

  private static final PlayersDeck DECK = new PlayersDeck();
  private final Map<String, Card> deckMap;
  private final Map<String, Card> cardsMap;

  /**
   * initialize basic cards and decks.
   */
  private PlayersDeck() {
    this.deckMap = new HashMap<>();
    this.deckMap.put("Barbarian", Barbarian.create(GlobalData.USER, newPositionFree()));
    this.deckMap.put("Giant", Giant.create(GlobalData.USER, newPositionFree()));
    this.deckMap.put("InfernoTower", InfernoTower.create(GlobalData.USER, newPositionFree()));
    this.deckMap.put("Wizard", Wizard.create(GlobalData.USER, newPositionFree())); 
    this.cardsMap = new HashMap<>();
    this.cardsMap.put("Archer", Archer.create(GlobalData.USER, new Vector2(0, 0)));
    this.cardsMap.put("MiniPekka", MiniPekka.create(GlobalData.USER, new Vector2(0, 0)));
    this.cardsMap.put("Valkrie", Valkyrie.create(GlobalData.USER, new Vector2(0, 0)));

  }

  /**
   * 
   * @return getDeckMap
   */
  public Map<String, Card> getDeck() {
    return deckMap;
  }

  /**
   * 
   * @return getCardsMap
   */
  public Map<String, Card> getCards() {
    return cardsMap;
  }

  /**
   * 
   * @param select
   * @return deckMap
   */
  public Map<String, Card> addDeck(final String select) {
    deckMap.put(select, cardsMap.get(select));
    return deckMap;
  }
  /**
   * 
   * @param select
   * @return cardsMap
   */
  public Map<String, Card> addCard(final String select) {
    cardsMap.put(select, deckMap.get(select));
    return cardsMap;
  }
  /**
   * 
   * @param select
   * @return cardsMap
   */
  public Map<String, Card> removeCard(final String select) {
    cardsMap.remove(select);
    return cardsMap;
  }

  /**
   * 
   * @param select
   * @return deckMap
   */
  public Map<String, Card> removeDeckCard(final String select) {
    deckMap.remove(select);
    return deckMap;
  }

  /**
   * 
   * @return List<Card> from map
   */
  public List<Card> cardList() {
    return deckMap.entrySet().stream()
        .map(e -> e.getValue())
        .collect(Collectors.toList());
  }
  /**
   * 
   * @return List<String> from key deckmap
   */
  public List<String> namesCardsDeck() {
    return deckMap.entrySet().stream()
        .map(e -> e.getKey())
        .collect(Collectors.toList());
  }
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
  public static PlayersDeck getInstance() {
    return DECK;
  }
}