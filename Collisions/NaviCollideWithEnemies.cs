using CSE3902.Enemies;
using CSE3902.Environment;
using CSE3902.Interfaces;
using CSE3902.Players;
using CSE3902.Shared;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace CSE3902.Collisions;

public class NaviCollideWithEnemies
{
    public static readonly NaviCollideWithEnemies Instance = new();

    private NaviCollideWithEnemies()
    {
    }

    public IEnemy CheckForCollision(Navi naviEntity, List<IEnemy> enemies)
    {
        foreach (IEnemy enemy in enemies)
        {
            Rectangle intersect = Rectangle.Intersect(naviEntity.Hitbox, enemy.Hitbox);

            if (!intersect.IsEmpty && enemy is not AquamentusEnemy && enemy is not DodongoEnemy)
            {
                enemy.Controlled = true;
                return enemy;
            }
        }
        return null;
    }
}