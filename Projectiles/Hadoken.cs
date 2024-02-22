using CSE3902.Interfaces;
using CSE3902.Shared;
using Microsoft.Xna.Framework;

namespace CSE3902.Projectiles;

public class Hadoken : IPlayerProjectile
{
    private readonly ITextureAtlasSprite _sprite;
    private Vector2 _location;
    private bool _inUse;
    private bool _endOfPath;
    private int _distMoved;
    private readonly int _maxDistance;
    private readonly int _stepDist;
    private readonly IPlayer _player;
    public bool Active { get; set; }
    public Rectangle Hitbox { get; set; }
    private const int Width = 16 * Globals.GlobalSizeMult;
    private const int Height = 16 * Globals.GlobalSizeMult;
    public bool StruckSomething { get; set; } //Not used
    public int DamageAmount { get; set; }
    public Direction MovingDirection { get; set; }

    public Hadoken(IPlayer player)
    {
        _player = player;
        _stepDist = 5;
        _maxDistance = 300;
        _distMoved = 0;
        _inUse = false;
        _endOfPath = false;
        _sprite = SpritesheetFactory.Instance.CreateHadokenSprite();
        _sprite.SetFrame(1, 1);
        Active = true;
        DamageAmount = 1;
        MovingDirection = Direction.Right;

        _location = new Vector2(_player.XPosition, _player.YPosition);
        SoundFactory.Instance.PlayHadokenFire();
    }

    public void Draw()
    {
        _sprite.Draw(_location);
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
            _location.X = _location.X + _stepDist;
        }
        else
        {
            Active = false;
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
        Hitbox = new Rectangle((int)_location.X, (int)_location.Y, Width, Height);
    }
}