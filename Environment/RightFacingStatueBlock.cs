using CSE3902.AbstractClasses;
using CSE3902.Shared;
using Microsoft.Xna.Framework;

namespace CSE3902.Environment;

public class RightFacingStatueBlock : AbstractBlock
{
    public RightFacingStatueBlock(Vector2 position) : base(position)
    {
        TextureAtlasSprite = SpritesheetFactory.Instance.CreateBlockSprite(typeof(RightFacingStatueBlock));
        PhysicalPassThrough = false;
        MagicalPassThrough = true;
    }
}