using CSE3902.Interfaces;
using CSE3902.Shared;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSE3902.Items
{
    public abstract class AbstractSword : ISword
    {
        protected ITextureAtlasSprite TextureAtlasSprite;
        protected const int OffsetSize = 4 * Globals.GlobalSizeMult;
        protected Vector2 Location;
        protected const int Height = 20 * Globals.GlobalSizeMult;
        protected const int Width = 12 * Globals.GlobalSizeMult;
        private int _timeToDisplay = 8;
        public Rectangle Hitbox { get; set; }
        public Direction MovingDirection { get; set; }
        public int DamageAmount { get; set; } = 1;

        public void Draw()
        {
            TextureAtlasSprite.DrawFromCenter(Location);
        }

        public void Update()
        {
            if (_timeToDisplay > 0) {
                _timeToDisplay--;
                UpdateHitbox();
            } else if (_timeToDisplay == 0) Hitbox = Rectangle.Empty;
        }

        public void UpdateHitbox()
        {
            Hitbox = new Rectangle((int) Location.X - Width / 2, (int) Location.Y - Height / 2, Width, Height);
        }
    }
}
