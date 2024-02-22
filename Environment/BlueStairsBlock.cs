using CSE3902.AbstractClasses;
using CSE3902.Shared;
using Microsoft.Xna.Framework;

namespace CSE3902.Environment;

public class BlueStairsBlock : AbstractBlock
{
    public BlueStairsBlock(Vector2 position) : base(position)
    {
        TextureAtlasSprite = SpritesheetFactory.Instance.CreateBlockSprite(typeof(BlueStairsBlock));
        PhysicalPassThrough = true;
        MagicalPassThrough = true;
    }
}