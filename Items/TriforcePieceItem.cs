using CSE3902.AbstractClasses;
using CSE3902.Shared;
using Microsoft.Xna.Framework;

namespace CSE3902.Items;

public class TriforcePieceItem : AbstractItem
{
    private int _frameCounter;
    private const int ItemId = 18;

    public TriforcePieceItem(Vector2 position)
    {
        _frameCounter = 0;
        Sprite = SpritesheetFactory.Instance.CreateLargeItemSprite();
        Sprite.SetFrame(SpriteLocationRow, ItemId);
        Position = position;
        UpdateHitbox();
    }

    public override void Update()
    {
        base.Update();
        _frameCounter++;
        if (_frameCounter != 8) return;

        _frameCounter = 0;
        if (Sprite.Row == SpriteLocationRow)
        {
            Sprite.SetFrame(SpriteLocationRow + 1, ItemId);
        }
        else
        {
            Sprite.SetFrame(SpriteLocationRow, ItemId);
        }
    }
}