using CSE3902.Interfaces;
using CSE3902.Players;
using CSE3902.Shared;
using CSE3902.Items;
using CSE3902.Projectiles;
using CSE3902.Collisions;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace CSE3902.Collisions;

public class SwordCollideWithEnemies
{
    public static SwordCollideWithEnemies Instance = new();

    private SwordCollideWithEnemies()
    {
    }

    public void HandleCollisions(List<IEnemy> enemies, ISword sword)
    {
        bool swordIsActive = sword != null;
        if (swordIsActive && !sword.Hitbox.IsEmpty)
        {
            foreach (IEnemy enemy in enemies)
                if (!Rectangle.Intersect(enemy.Hitbox, sword.Hitbox).IsEmpty)
                {
                    Rectangle intersect = Rectangle.Intersect(enemy.Hitbox, sword.Hitbox);
                    enemy.TakeDamage(sword.DamageAmount,
                        CollisionHelper.Instance.SquareCollidedFrom(intersect, ((IGameObject)enemy).Position));
                }
        }
    }
}