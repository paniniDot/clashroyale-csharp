using System.Drawing;
using System.Numerics;

namespace Fiorani.Source;
public class MapUnit
{
    private const int Height = 15;
    private const int Width = 19;
    private Vector2 _coords;
    private Rectangle _rect;
    private Type _type;

    public enum Type {
        Terrain,
        Tower,
        Obstacle
    }

    public MapUnit(Vector2 coords, Vector2 pos, Type type)
    {
        _coords = coords;
        _rect = new Rectangle((int) pos.X, (int) pos.Y, Width, Height);
        _type = type;

        Vector2 GetCoordinates() {
            return _coords;
        }
    
        Rectangle GetUnitRectangle() {
            return _rect;
        }
    
        Vector2 GetPosition() {
            return new Vector2(_rect.X, _rect.Y);
        }
    
        Type GetType() {
            return _type;
        }
    
        Vector2 GetCenter() {
            return new Vector2(_rect.X + Width, _rect.Y + Height);
        }
    }

}