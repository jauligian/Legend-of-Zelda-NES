using CSE3902.Interfaces;
using CSE3902.Shared;
using Microsoft.Xna.Framework;

namespace CSE3902.AbstractClasses;

public class AbstractItem : IItem
{
    private int _timeToDestroy = -1;

    public Vector2 Position
    {
        get => _position;
        set => _position = value;
    }

    /*
     * I added an explict backing variable for this property due to funkyness
     * where the getter was return a value copy of the Vector struct and not a reference.
     * This doesn't seem to happen in other classes, don't know whats going on.
     */
    private Vector2 _position;
    protected int Width = 8 * Globals.GlobalSizeMult;
    protected int Height = 16 * Globals.GlobalSizeMult;

    protected const int
        SpriteLocationRow = 1; //Allows each item to locate its sprite within the generic items spritesheet.

    protected ITextureAtlasSprite Sprite;
    public bool Active { get; set; } = true;
    public Rectangle Hitbox { get; protected set; }
    public Direction MovingDirection { get; set; } = Direction.None;

    public virtual void InitializeGlobalPosition(int horizontalOffset, int verticalOffset)
    {
        Position = new Vector2(Position.X + horizontalOffset,
            Position.Y + verticalOffset);
    }

    public virtual void Draw()
    {
        Sprite.Draw(Position);
    }

    public virtual void Update()
    {
        if (_timeToDestroy > 0) _timeToDestroy--;
        else if (_timeToDestroy == 0) Active = false;
        else UpdateHitbox();
    }

    public virtual void UpdateHitbox()
    {
        Hitbox = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
    }

    public virtual void HoldItem(Vector2 location)
    {
        Position = location;
        _position.Y -= Height;
        _timeToDestroy = 20;
        Hitbox = Rectangle.Empty;
    }
}