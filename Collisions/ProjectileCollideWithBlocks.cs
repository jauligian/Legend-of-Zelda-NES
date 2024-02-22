using CSE3902.Environment;
using CSE3902.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace CSE3902.Collisions;

public class ProjectileCollideWithBlocks
{
    public static ProjectileCollideWithBlocks Instance = new();

    private ProjectileCollideWithBlocks()
    {
    }

    public void HandleCollisions(IPlayerProjectile projectile, List<IBlock> blocks)
    {
        foreach (IBlock block in blocks)
            if (!(block is IIgnorableBlock))
            {
                Rectangle intersect = Rectangle.Intersect(projectile.Hitbox, block.Hitbox);

                if (!intersect.IsEmpty)
                {
                    //handle collisions
                    Type type = projectile.GetType();
                    if (!(type == typeof(IBoomerang)))
                    {
                        projectile.StruckSomething = true;
                    }
                    if (block is BombableWallDoorBlock && projectile is IBomb)
                    {
                        (block as BombableWallDoorBlock).BeBombed();
                    }
                }
            }
    }

    public void HandleCollisions(IEnemyProjectile projectile, List<IBlock> blocks)
    {
        foreach (IBlock block in blocks)
            if (!(block is IIgnorableBlock))
            {
                Rectangle intersect = Rectangle.Intersect(projectile.Hitbox, block.Hitbox);

                if (!intersect.IsEmpty)
                {
                    projectile.StruckSomething = true;
                }
            }
    }
}