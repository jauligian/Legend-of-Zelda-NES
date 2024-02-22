using CSE3902.AbstractClasses;
using CSE3902.Shared;
using Microsoft.Xna.Framework;

namespace CSE3902.Environment;

public class BombableWallDoorBlock : AbstractDoorBlock
{
    public bool Bombed { get; private set; } = false;
    public BombableWallDoorBlock(Vector2 position) : base(position)
    {
        TextureAtlasSprite = SpritesheetFactory.Instance.CreateDoorSprite(typeof(BombableWallDoorBlock));
        Bombed = false;
    }

    public void BeBombed()
    {
        TextureAtlasSprite.SetFrame(TextureAtlasSprite.Row, 2);
        Bombed = true;
    }
}