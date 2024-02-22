using CSE3902.Interfaces;
using CSE3902.Shared;
using Microsoft.Xna.Framework;

namespace CSE3902.Items;

public class EmptyItem : IItem, IPlayerProjectile
{
    public Vector2 Position { get; set; } = new();
    public bool Active { get; set; }
    public EmptyItem(Vector2 location) { Active = false; }
    public void Draw() { }
    public void Update() { }
    public void Use() { }
    public void UpdateHitbox() { }
    public Rectangle Hitbox { get; }
    public bool StruckSomething { get; set; }
    public int DamageAmount { get; set; }
    public Direction MovingDirection { get; set; } = Direction.None;
    public virtual void InitializeGlobalPosition(int horizontalOffset, int verticalOffset)
    {
        Position = new Vector2(Position.X + horizontalOffset,
            Position.Y + verticalOffset);
    }
}
