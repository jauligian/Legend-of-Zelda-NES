using CSE3902.AbstractClasses;
using CSE3902.Shared;
using Microsoft.Xna.Framework;

namespace CSE3902.Environment;

public class BasicBlueBlock : AbstractBlock
{
    public BasicBlueBlock(Vector2 position) : base(position)
    {
        TextureAtlasSprite = SpritesheetFactory.Instance.CreateBlockSprite(typeof(BasicBlueBlock));
        PhysicalPassThrough = false;
        MagicalPassThrough = true;
    }
}