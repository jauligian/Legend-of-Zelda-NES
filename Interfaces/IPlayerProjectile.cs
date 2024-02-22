using Microsoft.Xna.Framework;

namespace CSE3902.Interfaces
{
    public interface IPlayerProjectile
    {
        public bool Active { get; set; }
        public void Draw();
        public void Update();
        public void Use();
        public Rectangle Hitbox { get; }
        public bool StruckSomething { get; set; } //Only used for boomerang but here for use in collisions
        public int DamageAmount { get; set; }
    }
}