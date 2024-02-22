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
    public class WoodenSwordRightProjectile : AbstractSwordProjectile
    {
        public WoodenSwordRightProjectile(Vector2 position)
        {
            TextureAtlasSprite = SpritesheetFactory.Instance.CreateLargeProjectileSprite();
            TextureAtlasSprite.SetFrame(4, 1);
            CurrentLocation = position;
            CurrentLocation.X += InitialOffset;
            RemainingTimeOfFlight = MaxTimeOfFlight;
            Active = true;
            MovingDirection = Direction.Right;
            Width = 16 * Globals.GlobalSizeMult;
            SoundFactory.Instance.PlaySwordProjectileFire();
        }
        public override void Update()
        {
            CurrentLocation.X += StepSize;
            RemainingTimeOfFlight--;
            if (RemainingTimeOfFlight % 2 == 0) TextureAtlasSprite.SetFrame(TextureAtlasSprite.Row, RemainingTimeOfFlight % 3 + 1);
            if (!StrikeRegistered && StruckSomething)
            {
                RemainingTimeOfFlight = 0;
                StrikeRegistered = true;
            }
            if (RemainingTimeOfFlight == 0)
            {
                Explosion = new WoodenSwordProjectileExplosion(CurrentLocation);
            }
            else if (RemainingTimeOfFlight < 0)
            {
                Explosion.Update();
                if (!Explosion.Active) Active = false;
            }
            UpdateHitbox();
        }
    }
}
