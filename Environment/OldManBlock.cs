using CSE3902.AbstractClasses;
using CSE3902.Shared;
using Microsoft.Xna.Framework;

namespace CSE3902.Environment;

public class OldManBlock : AbstractBlock
{
    public OldManBlock(Vector2 position) : base(position)
    {
        TextureAtlasSprite = SpritesheetFactory.Instance.CreateBlockSprite(typeof(OldManBlock));
        PhysicalPassThrough = true;
        MagicalPassThrough = true;
    }
}