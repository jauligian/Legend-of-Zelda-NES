using CSE3902.AbstractClasses;
using CSE3902.Shared;
using Microsoft.Xna.Framework;

namespace CSE3902.Environment;

public class JewelCutBlueBlock : AbstractBlock
{
    public JewelCutBlueBlock(Vector2 position) : base(position)
    {
        TextureAtlasSprite = SpritesheetFactory.Instance.CreateBlockSprite(typeof(JewelCutBlueBlock));
        PhysicalPassThrough = false;
        MagicalPassThrough = true;
    }
}