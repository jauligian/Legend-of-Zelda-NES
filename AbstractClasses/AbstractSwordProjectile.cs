using CSE3902.Interfaces;
using CSE3902.Players;
using CSE3902.Projectiles;
using CSE3902.Shared;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSE3902.Items
{
    public abstract class AbstractSwordProjectile : IPlayerProjectile, ICollidable
    {
        protected ITextureAtlasSprite TextureAtlasSprite;
        protected Vector2 CurrentLocation;
        protected int RemainingTimeOfFlight;
        protected int Width = 8 * Globals.GlobalSizeMult;
        protected int Height = 8 * Globals.GlobalSizeMult;
        protected bool StrikeRegistered = false;
        protected const int InitialOffset = 40;
        protected const int MaxTimeOfFlight = 67;
        protected const int StepSize = 8;
        protected WoodenSwordProjectileExplosion Explosion;
        public bool Active { get; set; }
        public Rectangle Hitbox { get; set; }
        public bool StruckSomething { get; set; }
        public int DamageAmount { get; set; } = 1;
        public Direction MovingDirection { get; set; }

        public void Draw()
        {
            if (RemainingTimeOfFlight > 0)
            {
                TextureAtlasSprite.DrawFromCenter(CurrentLocation);
            }
            else
            {
                Explosion.Draw();
            }
        }
        public abstract void Update();
        public void Use() { }

        public void UpdateHitbox()
        {
            if (!StruckSomething) Hitbox = new Rectangle((int)CurrentLocation.X - Width / 2, (int)CurrentLocation.Y - Height / 2, Width, Height);
            else Hitbox = Rectangle.Empty; 
        }
    }
}
