using CSE3902.AbstractClasses;
using CSE3902.Shared;
using Microsoft.Xna.Framework;

namespace CSE3902.Environment;

public class BlackBlock : AbstractBlock
{
    public BlackBlock(Vector2 position) : base(position)
    {
        TextureAtlasSprite = SpritesheetFactory.Instance.CreateBlockSprite(typeof(BlackBlock));
        Hitbox = new Rectangle(0, 0, 0, 0);
        PhysicalPassThrough = true;
        MagicalPassThrough = true;
    }
}