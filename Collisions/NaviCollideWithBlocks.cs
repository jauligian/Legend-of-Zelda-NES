using CSE3902.Environment;
using CSE3902.Interfaces;
using CSE3902.Players;
using CSE3902.Shared;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace CSE3902.Collisions;

public class NaviCollideWithBlocks
{
    public static readonly NaviCollideWithBlocks Instance = new();

    private NaviCollideWithBlocks()
    {
    }

    public void HandleCollisions(Navi naviEntity, List<IBlock> blocks)
    {
        foreach (IBlock block in blocks)
            {
                Rectangle intersect;
                if (block is InvisibleBlock || block is BlackBlockC || block is IDoor) intersect = Rectangle.Intersect(naviEntity.Hitbox, block.Hitbox);
                else continue;

                if (!intersect.IsEmpty)
                {
                    MoveNavi(naviEntity, intersect);

                    naviEntity.UpdateHitbox();

                }
            }
    }

    private void MoveNavi(Navi naviEntity, Rectangle intersect)
    {
        switch (naviEntity.MovingDirection)
        {
            case Direction.Left:
                naviEntity.Position = new Vector2(naviEntity.Position.X + intersect.Width, naviEntity.Position.Y);
                break;
            case Direction.Right:
                naviEntity.Position = new Vector2(naviEntity.Position.X - intersect.Width, naviEntity.Position.Y);
                break;
            case Direction.Up:
                naviEntity.Position = new Vector2(naviEntity.Position.X, naviEntity.Position.Y + intersect.Height);
                break;
            case Direction.Down:
                naviEntity.Position = new Vector2(naviEntity.Position.X, naviEntity.Position.Y - intersect.Height);
                break;
        }
    }
}