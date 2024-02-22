using CSE3902.AbstractClasses;
using CSE3902.Shared;
using CSE3902.Shared.Managers;
using Microsoft.Xna.Framework;

namespace CSE3902.Items;

public class BlueBoomerangItem : AbstractItem
{
    private const int ItemId = 17;
    private bool _hasPlayedSound = false;

    public BlueBoomerangItem(Vector2 position)
    {
        Sprite = SpritesheetFactory.Instance.CreateTallItemSprite();
        Sprite.SetFrame(SpriteLocationRow + 1, ItemId);
        Position = position;
    }

    public override void Draw()
    {
        if (GameObjectManagers.EnemyManager.ActiveGameObjects.Count == 0) Sprite.Draw(Position);
    }

    public override void UpdateHitbox()
    {
        if (GameObjectManagers.EnemyManager.ActiveGameObjects.Count == 0)
        {
            Hitbox = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
            if (!_hasPlayedSound)
            {
                SoundFactory.Instance.PlayKeyAppear();
                _hasPlayedSound = true;
            }
        }
    }
}