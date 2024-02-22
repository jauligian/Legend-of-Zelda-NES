using CSE3902.AbstractClasses;
using CSE3902.Shared;
using Microsoft.Xna.Framework;

namespace CSE3902.Environment;

public class WhiteStairsBlock : AbstractBlock
{
    public WhiteStairsBlock(Vector2 position) : base(position)
    {
        TextureAtlasSprite = SpritesheetFactory.Instance.CreateBlockSprite(typeof(WhiteStairsBlock));
        Hitbox = new Rectangle(0, 0, 0, 0);
        PhysicalPassThrough = true;
        MagicalPassThrough = true;
    }
}