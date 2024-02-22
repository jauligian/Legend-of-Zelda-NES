using CSE3902.AbstractClasses;
using CSE3902.Environment.Doors;
using CSE3902.Interfaces;
using CSE3902.Shared;
using Microsoft.Xna.Framework;

namespace CSE3902.Environment;

public class OpenDoorBlock : AbstractDoorBlock
{
    public OpenDoorBlock(Vector2 position) : base(position)
    {
        TextureAtlasSprite = SpritesheetFactory.Instance.CreateDoorSprite(typeof(OpenDoorBlock));
    }

    public OpenDoorBlock(IDoor block, Vector2 position) : base(position)
    {
        TextureAtlasSprite = SpritesheetFactory.Instance.CreateDoorSprite(typeof(OpenDoorBlock));
        AdjacentRoomId = block.AdjacentRoomId;
        FacingDirection = block.FacingDirection;
        SetCorrectSprite();
    }
}