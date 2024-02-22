using CSE3902.AbstractClasses;
using CSE3902.Shared;
using Microsoft.Xna.Framework;

namespace CSE3902.Environment;

public class WhiteWalkingStairsBlock : AbstractBlock
{
    public WhiteWalkingStairsBlock(Vector2 position) : base(position)
    {
        TextureAtlasSprite = SpritesheetFactory.Instance.CreateBlockSprite(typeof(WhiteStairsBlock));
        PhysicalPassThrough = true;
        MagicalPassThrough = true;
    }
}