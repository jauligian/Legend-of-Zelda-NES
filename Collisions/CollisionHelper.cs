using CSE3902.Shared;
using Microsoft.Xna.Framework;

namespace CSE3902.Collisions;

public class CollisionHelper
{
    public static CollisionHelper Instance = new();

    private CollisionHelper()
    {
    }

    public Direction InvertDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.Left:
                direction = Direction.Right;
                break;
            case Direction.Right:
                direction = Direction.Left;
                break;
            case Direction.Up:
                direction = Direction.Down;
                break;
            case Direction.Down:
                direction = Direction.Up;
                break;
            default:
                direction = Direction.None;
                break;
        }

        return direction;
    }

    public Direction SquareCollidedFrom(Rectangle intersect, Vector2 primaryObjectPosition)
    {
        Direction hitFrom = Direction.None;
        if (intersect.Width > intersect.Height)
        {
            if (primaryObjectPosition.Y < intersect.Top)
            {
                hitFrom = Direction.Down;
            }
            else
            {
                hitFrom = Direction.Up;
            }
        }
        else
        {
            if (primaryObjectPosition.X < intersect.Left)
            {
                hitFrom = Direction.Right;
            }
            else
            {
                hitFrom = Direction.Left;
            }
        }

        return hitFrom;
    }
}