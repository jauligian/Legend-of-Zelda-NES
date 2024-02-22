using CSE3902.AbstractClasses;
using CSE3902.Shared;
using Microsoft.Xna.Framework;

namespace CSE3902.Environment;

public class BlackBlockC : AbstractBlock
{
    public BlackBlockC(Vector2 position) : base(position)
    {
        TextureAtlasSprite = SpritesheetFactory.Instance.CreateBlockSprite(typeof(BlackBlockC));
        PhysicalPassThrough = false;
        MagicalPassThrough = false;
    }
}