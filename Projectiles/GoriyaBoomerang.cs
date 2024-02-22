using CSE3902.Collisions;
using CSE3902.Interfaces;
using CSE3902.Shared;
using Microsoft.Xna.Framework;

namespace CSE3902.Projectiles;

public class GoriyaBoomerang : IEnemyProjectile
{
    private readonly ITextureAtlasSprite _sprite;
    public Vector2 Position { get; set; }
    private Vector2 _unitDirection;
    private int _timeCounter;
    private int _currentDirection; //0 is down, 1 is up, 2 is left, 3 is right
    private int _frameCounter;
    private int _maxFlightTime;
    private bool _returning;
    private bool _strikeRegistered;
    public bool Active { get; set; }
    public Rectangle Hitbox { get; set; }
    public Direction MovingDirection { get; set; }
    private const int Width = 8 * Globals.GlobalSizeMult;
    private const int Height = 8 * Globals.GlobalSizeMult;
    public bool StruckSomething { get; set; }
    public int DamageAmount { get; set; }

    public GoriyaBoomerang(Vector2 position, int direction)
    {
        _sprite = SpritesheetFactory.Instance.CreateTallProjectileSprite();
        Position = position;
        _timeCounter = 0;
        _currentDirection = direction;
        _frameCounter = 0;
        _returning = false;
        _strikeRegistered = false;
        _maxFlightTime = 60;
        Active = true;
        StruckSomething = false;
        DamageAmount = 2;

        switch (direction)
        {
            case 0:
                _unitDirection = new Vector2(0, 1);
                MovingDirection = Direction.Down;
                break;
            case 1:
                _unitDirection = new Vector2(0, -1);
                MovingDirection = Direction.Up;
                break;
            case 2:
                _unitDirection = new Vector2(-1, 0);
                MovingDirection = Direction.Left;
                break;
            case 3:
                _unitDirection = new Vector2(1, 0);
                MovingDirection = Direction.Right;
                break;
        }
        //TODO: Only activate this if Goriya is on-screen.
        //SoundFactory.Instance.PlayBoomerangFly();
    }

    public virtual void InitializeGlobalPosition(int horizontalOffset, int verticalOffset)
    {
        Position = new Vector2(Position.X + horizontalOffset,
            Position.Y + verticalOffset);
    }

    public void Draw()
    {
        _sprite.Draw(Position);
    }

    public void Update()
    {
        _frameCounter++;
        if (_frameCounter % 2 == 0)
        {
            _sprite.SetFrame(14, _frameCounter / 2);
        }

        if (_frameCounter == 14)
        {
            _frameCounter = 0;
        }

        Position += _unitDirection * 5;
        if (!_strikeRegistered && StruckSomething)
        {
            _maxFlightTime = _timeCounter;
            _strikeRegistered = true;
        }

        if (_timeCounter >= _maxFlightTime)
        {
            _timeCounter = 0;
            _unitDirection = -_unitDirection;
            MovingDirection = CollisionHelper.Instance.InvertDirection(MovingDirection);
            if (!_returning)
            {
                _returning = true;
            }
            else
            {
                Active = false;
                //SoundFactory.Instance.StopBoomerangFly();
            }
        }

        _timeCounter++;
        UpdateHitbox();
    }

    public void UpdateHitbox()
    {
        Hitbox = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
    }
}