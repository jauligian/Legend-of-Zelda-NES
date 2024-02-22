using CSE3902.Interfaces;
using CSE3902.Items;
using CSE3902.Players;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace CSE3902.Collisions
{
    public class PlayerCollideWithItems 
    {
        public static PlayerCollideWithItems Instance = new();
        private PlayerCollideWithItems() { }
        public void HandleCollisions(IPlayer player, List<IItem> items)
        {
            foreach (IItem item in items)
            {
                Rectangle intersect = Rectangle.Intersect(player.Hitbox, item.Hitbox);

                if (!intersect.IsEmpty)
                {
                    player.PickupItem(item);
                }
            }
        }
    }
}