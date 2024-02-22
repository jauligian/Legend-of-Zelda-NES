using CSE3902.AbstractClasses;
using CSE3902.Shared;
using Microsoft.Xna.Framework;

namespace CSE3902.Items;

public class RupeeItem : AbstractItem
{
    private int _frameCounter;
    private const int ItemId = 10;

    public RupeeItem(Vector2 position)
    {
        _frameCounter = 0;
        Sprite = SpritesheetFactory.Instance.CreateTallItemSprite();
        Sprite.SetFrame(SpriteLocationRow, ItemId);
        Position = position;
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