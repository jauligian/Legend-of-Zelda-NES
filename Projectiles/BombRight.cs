using CSE3902.Interfaces;
using CSE3902.Shared;
using Microsoft.Xna.Framework;

namespace CSE3902.Projectiles;

public class BombRight : IBomb
{
    private const int Width = 16 * Globals.GlobalSizeMult;
    private const int Height = 16 * Globals.GlobalSizeMult;
    private readonly ITextureAtlasSprite _sprite;
    private readonly ITextureAtlasSprite _explosion;
    private Vector2 _location;
    private bool _inUse;
    private int _time;
    private readonly int _maxTime;
    private bool _exploded;
    private readonly int _offset;
    private readonly int _verticalOffset;
    private readonly IPlayer _player;
    public bool Active { get; set; }
    public Rectangle Hitbox { get; private set; }
    public bool StruckSomething { get; set; } //Not used
    public int DamageAmount { get; set; }
    public Direction MovingDirection { get; set; }

    public BombRight(IPlayer player)
    {
        _verticalOffset = 8;
        _player = player;
        _offset = 48;
        _exploded = false;
        _time = 0;
        _maxTime = 60;
        _inUse = false;
        _sprite = SpritesheetFactory.Instance.CreateTallProjectileSprite();
        _sprite.SetFrame(15, 1);
        _explosion = SpritesheetFactory.Instance.CreateLargeProjectileSprite();
        _explosion.SetFrame(16, 1);
        Active = true;
        DamageAmount = 2;
        MovingDirection = Direction.None;
        Hitbox = Rectangle.Empty;

        _location = new Vector2(_player.XPosition, _player.YPosition);
        SoundFactory.Instance.PlayBombDrop();
    }

    public void Draw()
    {
        if (_inUse && !_exploded) _sprite.Draw(_location);
        else if (_exploded)
        {
            _explosion.Draw(_location);
            _explosion.Draw(new Vector2(_location.X, _location.Y));
            _explosion.Draw(new Vector2(_location.X + Width, _location.Y));
            _explosion.Draw(new Vector2(_location.X - Width, _location.Y));
            _explosion.Draw(new Vector2(_location.X - Width / 2, _location.Y - Height));
            _explosion.Draw(new Vector2(_location.X - Width / 2, _location.Y + Height));
            _explosion.Draw(new Vector2(_location.X + Width / 2, _location.Y - Height));
            _explosion.Draw(new Vector2(_location.X + Width / 2, _location.Y + Height));
        }
    }

    public void Update()
    {
        _time++;
        if (!_exploded && _time >= _maxTime)
        {
            _exploded = true;
            UpdateHitbox();
            _inUse = false;
            _location.X -= 12;
            SoundFactory.Instance.PlayBombExplode();
        }
        else
        {
            if (_time % 4 == 0)
            {
                if (_sprite.Column == 1) _sprite.SetFrame(15, 2);
                else _sprite.SetFrame(15, 1);
            }
        }

        if (_exploded) DrawExplosion();
    }

    public void DrawExplosion()
    {
        if (_time == 68) _explosion.SetFrame(16, 2);
        else if (_time == 76) _explosion.SetFrame(16, 3);
        else if (_time >= 84) Active = false;
    }

    public void Use()
    {
        if (!_inUse)
        {
            _inUse = true;
            _location = new Vector2(_player.XPosition + _offset, _player.YPosition - _verticalOffset);
        }
    }

    public void UpdateHitbox()
    {
        Hitbox = new Rectangle((int)_location.X - Width / 2, (int)_location.Y - Height / 2, Width * 2, Height * 2);
    }
}