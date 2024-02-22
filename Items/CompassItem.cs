using CSE3902.AbstractClasses;
using CSE3902.Shared;
using Microsoft.Xna.Framework;

namespace CSE3902.Items;

public class CompassItem : AbstractItem
{
    private const int ItemId = 17;

    public CompassItem(Vector2 position)
    {
        Sprite = SpritesheetFactory.Instance.CreateLargeItemSprite();
        Sprite.SetFrame(SpriteLocationRow, ItemId);
        Position = position;
        Width = 16 * Globals.GlobalSizeMult;
    }
}