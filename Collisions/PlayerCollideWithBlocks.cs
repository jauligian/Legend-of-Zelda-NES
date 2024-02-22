using CSE3902.Environment;
using CSE3902.Interfaces;
using CSE3902.LevelLoading;
using CSE3902.Players;
using CSE3902.Shared;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace CSE3902.Collisions;

public class PlayerCollideWithBlocks
{
    public static readonly PlayerCollideWithBlocks Instance = new();

    private PlayerCollideWithBlocks()
    {
    }

    public void HandleCollisions(IPlayer player, List<IBlock> blocks, Dungeon dungeon)
    {
        bool takeTheStairs = false;
        foreach (IBlock block in blocks) if (block is not OpenDoorBlock && block is not WhiteStairsBlock && block is not BlackBlock)
        {
            Rectangle linkBox = player.Hitbox;
            Rectangle playerFeet = new(linkBox.X + linkBox.Width / 5, linkBox.Y + linkBox.Height / 2, linkBox.Width * 3 / 5, linkBox.Height / 2);
            Rectangle intersect;
            if (block is InvisibleBlock || block is WhiteBrickBlock) intersect = Rectangle.Intersect(player.Hitbox, block.Hitbox);
            else if (block is BlackBlockC)
            {
                Rectangle smallerBlockHitbox = new(block.Hitbox.X + 3, block.Hitbox.Y, block.Hitbox.Width - 6, block.Hitbox.Height);
                intersect = Rectangle.Intersect(player.Hitbox, smallerBlockHitbox);
            }
            else intersect = Rectangle.Intersect(playerFeet, block.Hitbox);

            if (!intersect.IsEmpty && player.GetType() != typeof(DamagedPlayer))
            {
                MovePlayer(player, intersect);

                player.UpdateHitbox();

                if (block is PushableBlock)
                {
                    PushableBlock pushable = block as PushableBlock;
                    if (pushable != null)
                    {
                        pushable.Pushing = true;
                        pushable.Push(player.MovingDirection);
                    }
                }
                else if (block is BlueStairsBlock || block is WhiteWalkingStairsBlock)
                {
                    takeTheStairs = true;
                    SoundFactory.Instance.PlayWalkStairs();
                }
            }
            else if (!intersect.IsEmpty)
            {
                player.MovingDirection = CollisionHelper.Instance.InvertDirection(
                    CollisionHelper.Instance.SquareCollidedFrom(intersect,
                        new Vector2(player.XPosition, player.YPosition)));

                MovePlayer(player, intersect);

                player.UpdateHitbox();
            }
            else if(block is PushableBlock)
            {
                (block as PushableBlock).Pushing = false;
            }
        }

        if (takeTheStairs) dungeon.ChangeCrawlSpace(player);
    }

    public void MovePlayer(IPlayer player, Rectangle intersect)
    {
        switch (player.MovingDirection)
        {
            case Direction.Left:
                player.XPosition += intersect.Width;
                break;
            case Direction.Right:
                player.XPosition -= intersect.Width;
                break;
            case Direction.Up:
                player.YPosition += intersect.Height;
                break;
            case Direction.Down:
                player.YPosition -= intersect.Height;
                break;
        }
    }
}