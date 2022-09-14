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

    public ISet<Vector2> PositionFree { get; }

    /// <summary>
    /// Constructor for position free, protected in order to cannot instantiate it from outside its package. 
    /// </summary>
    protected BasicDeck() 
    {
        PositionFree = new HashSet<Vector2>
        {
            new Vector2(_POSCARD1, _HEIGHTCARD),
            new Vector2(_POSCARD2, _HEIGHTCARD),
            new Vector2(_POSCARD3, _HEIGHTCARD),
            new Vector2(_POSCARD4, _HEIGHTCARD)
        };
    }
    

    /// 
    /// <returns> the first free position and deletes it from those available </returns>
    public Vector2 NewPositionFree() 
    {
        IEnumerator<Vector2> i = PositionFree.GetEnumerator();
        Vector2 tmp  =  i.Current;
        PositionFree.Remove(tmp);
        return tmp;
    }
}
}