using System.Numerics;

namespace Bollini.Source.Model.Deck
{

public class BasicDeck
{
    private static readonly int _HEIGHTCARD = 100;
    private static readonly int _POSCARD1 = 200;
    private static readonly int _POSCARD2 = 300;
    private static readonly int _POSCARD3 = 400;
    private static readonly int _POSCARD4 = 500;

    private readonly Set<Vector2> _positionFree;

    /**
   * Constructor for position free, protected in order to cannot instantiate it from outside its package. 
   */
    protected BasicDeck() 
    {
        positionFree = new HashSet<>(Arrays.asList(new Vector2(POSCARD1, HEIGHTCARD), new Vector2(POSCARD2, HEIGHTCARD), new Vector2(POSCARD3, HEIGHTCARD), new Vector2(POSCARD4, HEIGHTCARD)));
    }

    /**
   * 
   * @return positionFree
   */
    public Set<Vector2> GetPositionFree() => positionFree;

    /**
   * 
   * @return the first free position and deletes it from those available
   */
    public Vector2 NewPositionFree() 
    {
        readonly Iterator<Vector2> i = getPositionFree().iterator();
        readonly Vector2 tmp  =  i.next();
        getPositionFree().remove(tmp);
        return tmp;
    }
}
}