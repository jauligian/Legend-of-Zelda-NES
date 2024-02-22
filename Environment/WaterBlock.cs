using CSE3902.AbstractClasses;
using CSE3902.Interfaces;
using CSE3902.Shared;
using Microsoft.Xna.Framework;

namespace CSE3902.Environment;

public class WaterBlock : AbstractBlock, IIgnorableBlock
{
    public WaterBlock(Vector2 position) : base(position)
    {
        TextureAtlasSprite = SpritesheetFactory.Instance.CreateBlockSprite(typeof(WaterBlock));
        PhysicalPassThrough = true;
        MagicalPassThrough = true;
    }
}