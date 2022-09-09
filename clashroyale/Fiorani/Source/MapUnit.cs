using System.Drawing;
using System.Numerics;

namespace Fiorani.Source;
public class MapUnit
{
    int HEIGHT = 15;
    int WIDTH = 19;
    Vector2 coords;
    Rectangle rect;
    Type type;

    private enum Type {
    
        TERRAIN,
    
        TOWER,
    
        OBSTACLE;
    }

    MapUnit(Vector2 coords, Vector2 pos,Type type)
    {
        coords = coords;
        rect = new Rectangle(pos.x, pos.y, WIDTH, HEIGHT);
        type = type;

        Vector2 getCoordinates() {
            return coords;
        }
    
        Rectangle getUnitRectangle() {
            return rect;
        }
    
        Vector2 getPosition() {
            return new Vector2(rect.x, rect.y);
        }
    
        Type getType() {
            return type;
        }
    
        Vector2 getCenter() {
            return new Vector2(rect.x + WIDTH, rect.y + HEIGHT);
        }
    }

}