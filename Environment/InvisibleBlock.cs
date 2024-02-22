using CSE3902.AbstractClasses;
using Microsoft.Xna.Framework;

namespace CSE3902.Environment;

public class InvisibleBlock : AbstractBlock
{
    public InvisibleBlock(Vector2 position) : base(position)
    {
        PhysicalPassThrough = false;
        MagicalPassThrough = false;
    }

    public override void Draw()
    {
    }
}