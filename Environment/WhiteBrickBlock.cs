using CSE3902.AbstractClasses;
using CSE3902.Shared;
using Microsoft.Xna.Framework;

namespace CSE3902.Environment;

public class WhiteBrickBlock : AbstractBlock
{
    public WhiteBrickBlock(Vector2 position) : base(position)
    {
        TextureAtlasSprite = SpritesheetFactory.Instance.CreateBlockSprite(typeof(WhiteBrickBlock));
        PhysicalPassThrough = true;
        MagicalPassThrough = true;
    }
}