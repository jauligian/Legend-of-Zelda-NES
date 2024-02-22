using CSE3902.AbstractClasses;
using CSE3902.Shared;
using Microsoft.Xna.Framework;

namespace CSE3902.Environment.Doors;

public class DiamondDoorBlock : AbstractDoorBlock
{
    public DiamondDoorBlock(Vector2 position) : base(position)
    {
        TextureAtlasSprite = SpritesheetFactory.Instance.CreateDoorSprite(typeof(DiamondDoorBlock));
    }
}