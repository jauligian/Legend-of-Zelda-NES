using CSE3902.AbstractClasses;
using CSE3902.Enemies;
using CSE3902.Environment;
using CSE3902.Interfaces;
using CSE3902.Players;
using CSE3902.Shared;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace CSE3902.Collisions;

public class EnemyCollideWithDoors
{
    public static EnemyCollideWithDoors Instance = new();

    private EnemyCollideWithDoors()
    {
    }

    public void HandleCollisions(IEnemy enemy, List<IDoor> doors)
    {
        foreach (IDoor door in doors)
        {
            Rectangle intersect = Rectangle.Intersect(enemy.Hitbox, door.Hitbox);
            if (enemy is WallmasterEnemy) continue;

            if (!intersect.IsEmpty)
            {
                //handle collisions
                Direction dir = enemy.MovingDirection;
                switch (dir)
                {
                    case Direction.UpLeft:
                    case Direction.UpRight:
                    case Direction.DownLeft:
                    case Direction.DownRight:
                        dir = CollisionHelper.Instance.SquareCollidedFrom(intersect, ((IGameObject)enemy).Position);
                        break;
                }

                switch (dir)
                {
                    case Direction.Left:
                        enemy.Position = new Vector2(enemy.Position.X + intersect.Width,
                           enemy.Position.Y);
                        break;
                    case Direction.Right:
                        enemy.Position = new Vector2(enemy.Position.X - intersect.Width,
                            enemy.Position.Y);
                        break;
                    case Direction.Up:
                        enemy.Position = new Vector2(enemy.Position.X,
                            enemy.Position.Y + intersect.Height);
                        break;
                    case Direction.Down:
                        enemy.Position = new Vector2(enemy.Position.X,
                            enemy.Position.Y - intersect.Height);
                        break;
                    case Direction.UpLeft:
                    case Direction.UpRight:
                    case Direction.DownLeft:
                    case Direction.DownRight:
                        break;
                    default:
                        enemy.Position = new Vector2(enemy.Position.X + intersect.Width,
                            enemy.Position.Y - intersect.Height);
                        break;
                }

                enemy.UpdateHitbox();
                enemy.HitDoor(door);
            }
        }
    }
}