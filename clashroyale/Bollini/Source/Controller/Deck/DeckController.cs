using Bollini.Source.Model.Deck;

namespace Bollini.Source.Controller.Deck
{
  /**
 ///Controller implementation for the game screen.
 */
public class DeckController : Controller
{
  private static readonly int _DECK_SIZE = 4;

  private readonly List<String> _cards;
  private readonly List<String> _decklist;
  private readonly JFrame _frame;

  /**
   * Constructor.
   */
  public DeckController() 
  {
    base(new AudioDeckController());
    base.registerModel(new Model());
    readonly var skin = new Skin(Gdx.files.internal("buttons/menuSkin.json"), new TextureAtlas("buttons/atlas.pack"));
    decklist = new List<>(skin);
    cards = new List<>(skin);
    frame = new JFrame();
  }

  

  /**
   * Istanciate a new MenuController which takes control of the application.
   */
  public void TriggerMenu() 
  {
    new MenuController().setCurrentActiveScreen();
  }
  public override void SetCurrentActiveScreen() 
  {
    ClashRoyale.setActiveScreen(new DeckScreen(this));
  }

  /**
   * setCard in DeckScreen.
   * 
   * @return the List of cards.
   */
  public List<String> ListGDXCard() 
  {
    cards.setItems(PlayersDeck.getInstance().namesCardsCard().stream().toArray(String[]::new));
    return cards;
  }

  /**
   * setDeck in DeckScreen.
   * 
   * @return the List of deck.
   */
  public List<String> ListGDXDeck() 
  {
    decklist.setItems(PlayersDeck.getInstance().namesCardsDeck().stream().toArray(String[]::new));
    return decklist;
  }

  /**
   * Add card in DeckScreen and remove from CardList.
   * 
   * @param select the card to move
   * 
   * @return List of deck. 
   */
  public List<String> AddDeck(readonly String select) 
  {
    PlayersDeck.getInstance().addDeck(select);
    PlayersDeck.getInstance().getDeck().get(select).setPosition(PlayersDeck.getInstance().newPositionFree());
    return listGDXDeck();
  }

  /**
   * Add card in DeckScreen and remove in CardList.
   * 
   * @param select the card to move.
   * 
   * @return the list of cards updated.
   */
  public List<String> AddCard(readonly String select) 
  {
    PlayersDeck.getInstance().addCard(select);
    return listGDXCard();
  }

  /**
   * Remove card in DeckScreen and add in CardList.
   * 
   * @param card the card to remove.
   * 
   * @return the list of cards updated. 
   */
  public List<String> RemoveCard(readonly String card) 
  {
    PlayersDeck.getInstance().removeCard(card);
    return listGDXCard();
  }

  /**
   * Remove card in DeckScreen and add in CardList.
   * Adds the position of the card removed from the deck between the free positions
   * @param card the card to remove.
   * 
   * @return the deckList updated.
   */
  public List<String> RemoveDeckCard(readonly String card) 
  {
    PlayersDeck.getInstance().getPositionFree().add(PlayersDeck.getInstance().getDeck().get(card).getPosition());
    PlayersDeck.getInstance().removeDeckCard(card);
    return listGDXDeck();
  }

  /**
   * Check dim deck < 4 for insert new card.
   * 
   * @return true if the deck has 4 cards.
   */
  public boolean Full() 
  {
    if (PlayersDeck.getInstance().getDeck().size() < DECK_SIZE) 
    {
      return true;
    }
    JOptionPane.showMessageDialog(frame, "DECK PIENO(MAX 4 CARTE), RIMUOVERE PRIMA UNA CARTA");
    return false;
  }

  /**
   *  Check if the deck has no cards inside.
   *
   *   @return true if the deck is empty.
   */
  public bool Empty() 
  {
    if (!PlayersDeck.getInstance().getDeck().isEmpty()) 
    {
      return true;
    }
    JOptionPane.showMessageDialog(frame, "DECK VUOTO");
    return false;
  }

  /**
   * Check if the deck has 4 cards, than save the deck and return to the menu. 
   */
  public void ReturnButton() {
    if (PlayersDeck.getInstance().getDeck().size() == DECK_SIZE) {

      triggerMenu();
    } else {
      JOptionPane.showMessageDialog(frame, "INSERIRE 4 CARTE NEL DECK PER POTER GIOCARE");
    }
  }
}
