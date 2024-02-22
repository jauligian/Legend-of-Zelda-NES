using CSE3902.Enemies;
using CSE3902.Interfaces;
using CSE3902.Shared;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace CSE3902.Collisions
{
    public class PlayerCollideWithEnemies 
    {
        public static PlayerCollideWithEnemies Instance = new();
        private PlayerCollideWithEnemies() { }
        public void HandleCollisions(IPlayer player, List<IEnemy> enemies)
        {
            foreach (IEnemy enemy in enemies)
            {
                Rectangle intersect = Rectangle.Intersect(player.Hitbox, enemy.Hitbox);

                if (!intersect.IsEmpty && !enemy.Controlled)
                {
                    //handle collisions
                    if(player.MovingDirection != Direction.None) //|| enemy is BladeTrapEnemy)
                    {
                        player.TakeDamage(enemy.DamageAmount, player.MovingDirection);
                    }
                    else
                    {
                        Globals global = new Globals();
                        player.TakeDamage(enemy.DamageAmount, CollisionHelper.Instance.InvertDirection(enemy.MovingDirection));
                    }
                    enemy.HitPlayer(player);
                }
            }
        }
    }
}