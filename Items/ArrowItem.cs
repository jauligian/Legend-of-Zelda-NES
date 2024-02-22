using CSE3902.AbstractClasses;
using CSE3902.Shared;
using Microsoft.Xna.Framework;

namespace CSE3902.Items;

public class ArrowItem : AbstractItem
{
    private const int ItemId = 20;

    public ArrowItem(Vector2 position)
    {
        Sprite = SpritesheetFactory.Instance.CreateTallItemSprite();
        Sprite.SetFrame(SpriteLocationRow, ItemId);
        Position = position;
    }
}