using CSE3902.Interfaces;
using CSE3902.Shared;
using Microsoft.Xna.Framework;

namespace CSE3902.AbstractClasses;

public abstract class AbstractBlock : IBlock
{
    protected ITextureAtlasSprite TextureAtlasSprite;

    public Vector2 Position { get; set; }

    protected int Height = 16 * Globals.GlobalSizeMult;
    protected int Width = 16 * Globals.GlobalSizeMult;

    public virtual bool Active { get; set; } = true;
    public Rectangle Hitbox { get; set; }
    public virtual Direction MovingDirection { get; set; } = Direction.None;
    protected bool MagicalPassThrough;
    protected bool PhysicalPassThrough;

    protected AbstractBlock(Vector2 position)
    {
        Position = position;
        Hitbox = new Rectangle((int)position.X - Width / 2, (int)position.Y - Height / 2, Height, Width);
    }

    public virtual void InitializeGlobalPosition(int horizontalOffset, int verticalOffset)
    {
        Position = new Vector2(Position.X + horizontalOffset,
            Position.Y + verticalOffset);
        UpdateHitbox();
    }

    public virtual void Draw()
    {
        TextureAtlasSprite?.Draw(Position);
    }


    public virtual void Update()
    {
    }

    public virtual void UpdateHitbox()
    {
        Hitbox = new Rectangle((int)Position.X - Width / 2, (int)Position.Y - Height / 2, Height, Width);
    }
}