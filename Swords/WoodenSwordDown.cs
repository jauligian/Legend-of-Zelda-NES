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
    public class WoodenSwordDown : AbstractSword
    {
        public WoodenSwordDown(IPlayer player)
        {
            TextureAtlasSprite = SpritesheetFactory.Instance.CreateTallProjectileSprite();
            TextureAtlasSprite.SetFrame(1, 1);
            Location = new Vector2(player.XPosition + OffsetSize, player.YPosition + OffsetSize);
            Location.Y += 3 * OffsetSize;
            UpdateHitbox();
            SoundFactory.Instance.PlaySwordSlash();
        }
    }
}
