using CSE3902.Interfaces;
using CSE3902.Items;
using CSE3902.Players;
using CSE3902.Shared;
using Microsoft.Xna.Framework;
using System;

namespace CSE3902.Projectiles;

public class BoomerangLeft : IBoomerang
{
    private readonly ITextureAtlasSprite _sprite;
    private Vector2 _location;
    private bool _inUse;
    private int _distMoved;
    private readonly int _stepDist;
    private readonly int _turnDist;
    private int _frameCounter;
    public bool StruckSomething { get; set; }
    private bool _returned;
    private readonly IPlayer _player;
    public bool Active { get; set; }
    public Rectangle Hitbox { get; set; }
    private const int Width = 8 * Globals.GlobalSizeMult;
    private const int Height = 8 * Globals.GlobalSizeMult;
    public int DamageAmount { get; set; }
    public Direction MovingDirection { get; set; }

    public BoomerangLeft(IPlayer player)
    {
        _player = player;
        StruckSomething = false;
        _returned = false;
        _stepDist = 8;
        _turnDist = 300;
        _distMoved = 0;
        _frameCounter = 0;
        _inUse = false;
        _sprite = SpritesheetFactory.Instance.CreateTallProjectileSprite();
        _sprite.SetFrame(14, 1);
        Active = true;
        DamageAmount = 0;
        if (player.GetType() == typeof(GoriyaPlayer)) DamageAmount = 1;
        MovingDirection = Direction.Left;

        _location = new Vector2(_player.XPosition, _player.YPosition);
        SoundFactory.Instance.PlayBoomerangFly();
    }

    public void Draw()
    {
        if (_inUse && !_returned) _sprite.Draw(_location);
    }

    public void Update()
    {
        _frameCounter++;
        if (!StruckSomething && _distMoved >= _turnDist) StruckSomething = true;
        if (_inUse && !StruckSomething)
        {
            _distMoved += _stepDist;
            _location.X -= _stepDist;
            if (_frameCounter % 2 == 0) _sprite.SetFrame(14, _frameCounter / 2);
            if (_frameCounter == 14) _frameCounter = 0;
        }
        else if (_inUse && StruckSomething && !_returned)
        {
            if (_frameCounter % 2 == 0) _sprite.SetFrame(14, _frameCounter / 2);
            if (_frameCounter == 14) _frameCounter = 0;

            int deltaX = (int)(_player.XPosition - _location.X);
            int deltaY = (int)(_player.YPosition - _location.Y);

            double magnitude = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);

            double unitVectorX = deltaX / magnitude;
            double unitVectorY = deltaY / magnitude;

            _location.X = (int)(_location.X + _stepDist * unitVectorX);
            _location.Y = (int)(_location.Y + _stepDist * unitVectorY);

            if (magnitude < _stepDist)
            {
                //change player state to catch
                _returned = true;
                Active = false;
                _player.UseItem(typeof(EmptyItem));
                SoundFactory.Instance.StopBoomerangFly();
            }
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