using CSE3902.Shared;
using Microsoft.Xna.Framework;

namespace CSE3902.Interfaces
{
    public interface ICollidable
    {
        public Rectangle Hitbox { get; }
        public Direction MovingDirection { get; set; }
        public void UpdateHitbox();
    }
}