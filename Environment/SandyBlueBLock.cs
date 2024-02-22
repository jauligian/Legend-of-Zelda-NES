using CSE3902.AbstractClasses;
using CSE3902.Shared;
using Microsoft.Xna.Framework;

namespace CSE3902.Environment;

public class SandyBlueBlock : AbstractBlock
{
    public SandyBlueBlock(Vector2 position) : base(position)
    {
        TextureAtlasSprite = SpritesheetFactory.Instance.CreateBlockSprite(typeof(SandyBlueBlock));
        Hitbox = Rectangle.Empty;
        PhysicalPassThrough = true;
        MagicalPassThrough = true;
    }

    public override void Update()
    {
        Hitbox = Rectangle.Empty;
        base.Update();
    }
}