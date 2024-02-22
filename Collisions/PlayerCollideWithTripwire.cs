using CSE3902.Interfaces;
using CSE3902.Players;
using CSE3902.Shared;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace CSE3902.Collisions
{
    public class PlayerCollideWithTripwire
    {
        public static PlayerCollideWithTripwire Instance = new();
        private PlayerCollideWithTripwire() { }
        public void HandleCollisions(IPlayer player, List<IEnemy> enemies)
        {
            foreach (IEnemy enemy in enemies)
            {
                List<Rectangle> detectors = enemy.GetTripwires();
                if (detectors != null)
                {
                    foreach (Rectangle rect in detectors)
                    {
                        Rectangle intersect = Rectangle.Intersect(player.Hitbox, rect);

                        if (!intersect.IsEmpty)
                        {
                            //handle collisions
                            enemy.PlayerActivateTripwire(rect);
                        }
                    }
                }
            }
        }
    }
}