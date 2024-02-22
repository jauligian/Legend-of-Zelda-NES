using CSE3902.Interfaces;
using CSE3902.Shared;
using Microsoft.Xna.Framework;

namespace CSE3902.Projectiles;

internal class AquamentusFireball : IEnemyProjectile
{
    private readonly ITextureAtlasSprite _sprite;
    public Vector2 Position { get; set; }
    private int _timeCounter;
    private readonly Vector2 _currentDirection;
    private int _frameCounter;
    public bool Active { get; set; }
    public Rectangle Hitbox { get; set; }
    public Direction MovingDirection { get; set; } = Direction.Left;
    private const int Width = 8 * Globals.GlobalSizeMult;
    private const int Height = 16 * Globals.GlobalSizeMult;
    public bool StruckSomething { get; set; }
    public int DamageAmount { get; set; }

    public AquamentusFireball(Vector2 position, Vector2 direction)
    {
        _sprite = SpritesheetFactory.Instance.CreateTallProjectileSprite();
        Position = position;
        _timeCounter = 0;
        _frameCounter = 0;
        Active = true;
        _currentDirection = direction;
        DamageAmount = 1;
    }

    public virtual void InitializeGlobalPosition(int horizontalOffset, int verticalOffset)
    {
        Position = new Vector2(Position.X + horizontalOffset,
            Position.Y + verticalOffset);
    }


    public void Draw()
    {
        if (Active)
        {
            _sprite.Draw(Position);
        }
    }

    public void Update()
    {
        _frameCounter++;
        if (_frameCounter % 2 == 0)
        {
            _sprite.SetFrame(17, _frameCounter / 2);
        }

        if (_frameCounter == 8)
        {
            _frameCounter = 0;
        }

        Position += _currentDirection;
        if (_timeCounter >= 120 || StruckSomething)
        {
            Active = false;
        }

        _timeCounter++;
        UpdateHitbox();
    }

    public void UpdateHitbox()
    {
        Hitbox = new Rectangle((int)Position.X - Width / 2, (int)Position.Y - Height / 2, Width, Height);
    }
}