using CSE3902.Interfaces;
using CSE3902.Shared;
using Microsoft.Xna.Framework;

namespace CSE3902.Projectiles;

public class ArrowDown : IArrow
{
    private readonly ITextureAtlasSprite _sprite;
    private Vector2 _location;
    private bool _inUse;
    private bool _endOfPath;
    private int _distMoved;
    private readonly int _maxDistance;
    private readonly int _stepDist;
    private readonly IPlayer _player;
    private readonly ITextureAtlasSprite _explosion;
    private int _explosionCounter;
    public bool Active { get; set; }
    public Rectangle Hitbox { get; set; }
    private const int Width = 8 * Globals.GlobalSizeMult;
    private const int Height = 16 * Globals.GlobalSizeMult;
    public bool StruckSomething { get; set; }
    public int DamageAmount { get; set; }
    public Direction MovingDirection { get; set; }

    public ArrowDown(IPlayer player)
    {
        _player = player;
        _stepDist = 10;
        _maxDistance = 300;
        _distMoved = 0;
        _inUse = false;
        _endOfPath = false;
        _sprite = SpritesheetFactory.Instance.CreateTallProjectileSprite();
        _sprite.SetFrame(9, 1);
        _explosion = SpritesheetFactory.Instance.CreateTallProjectileSprite();
        _explosion.SetFrame(13, 1);
        _explosionCounter = 0;
        Active = true;
        DamageAmount = 1;
        MovingDirection = Direction.Down;

        _location = new Vector2(_player.XPosition, _player.YPosition);
        SoundFactory.Instance.PlayArrowFire();
    }

    public void Draw()
    {
        if (_inUse && !_endOfPath)
        {
            _sprite.Draw(_location);
        }
        else if (_endOfPath && _explosionCounter <= 4)
        {
            _explosionCounter++;
            _explosion.Draw(_location);
        }
        else
        {
            Active = false;
        }
    }

    public void Update()
    {
        if ((_inUse && _distMoved >= _maxDistance) || StruckSomething)
        {
            _endOfPath = true;
        }

        if (_inUse && !_endOfPath)
        {
            _distMoved += _stepDist;
            _location.Y = _location.Y + _stepDist;
        }

        UpdateHitbox();
    }

    public void Use()
    {
        if (!_inUse)
        {
            _inUse = true;
            _location = new Vector2(_player.XPosition, _player.YPosition);
        }
    }

    public void UpdateHitbox()
    {
        Hitbox = new Rectangle((int)_location.X - Width / 2, (int)_location.Y - Height / 2, Width, Height);
    }
}