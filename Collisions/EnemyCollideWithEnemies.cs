using CSE3902.Interfaces;
using CSE3902.Players;
using CSE3902.Shared;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace CSE3902.Collisions
{
    public class EnemyCollideWithEnemies
    {
        public static EnemyCollideWithEnemies Instance = new();
        private EnemyCollideWithEnemies() { }
        public void HandleCollisions(IEnemy enemy, List<IEnemy> enemies)
        {
            foreach (IEnemy enemy2 in enemies)
            {
                if (enemy2 != enemy)
                {
                    Rectangle intersect = Rectangle.Intersect(enemy.Hitbox, enemy2.Hitbox);

                    if (!intersect.IsEmpty)
                    {
                        //handle collisions
                        enemy.HitEnemy(enemy2);
                        enemy2.HitEnemy(enemy);
                    }
                }
            }
        }
    }
}