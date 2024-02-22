using CSE3902.AbstractClasses;
using CSE3902.Environment;
using CSE3902.Shared;
using CSE3902.Shared.Managers;
using Microsoft.Xna.Framework;
using System;
using System.Runtime.CompilerServices;

namespace CSE3902.Items;

public class BowItem : AbstractItem
{
    private const int ItemId = 19;

    public BowItem(Vector2 position)
    {
        Sprite = SpritesheetFactory.Instance.CreateTallItemSprite();
        Sprite.SetFrame(SpriteLocationRow, ItemId);
        Position = position;
    }

    public override void HoldItem(Vector2 location)
    {
        GameObjectManagers.BlockManager.Spawn(new BlackBlock(this.Position));
        base.HoldItem(location);
    }
}