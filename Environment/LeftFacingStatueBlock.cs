using CSE3902.AbstractClasses;
using CSE3902.Shared;
using Microsoft.Xna.Framework;

namespace CSE3902.Environment;

public class LeftFacingStatueBlock : AbstractBlock
{
    public LeftFacingStatueBlock(Vector2 position) : base(position)
    {
        TextureAtlasSprite = SpritesheetFactory.Instance.CreateBlockSprite(typeof(LeftFacingStatueBlock));
        PhysicalPassThrough = false;
        MagicalPassThrough = true;
    }
}