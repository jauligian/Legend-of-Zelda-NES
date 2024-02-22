using CSE3902.Interfaces;
using CSE3902.Items;
using CSE3902.Shared;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace CSE3902.Projectiles
{
    public class CirclingBoomerang : IBoomerang
    {
        private readonly ITextureAtlasSprite _sprite;
        private Vector2 _location;
        private bool _inUse;
        private int _frameCounter;
        public bool StruckSomething { get; set; }
        private readonly IPlayer _player;
        public bool Active { get; set; }
        public Rectangle Hitbox { get; set; }
        private const int Width = 8 * Globals.GlobalSizeMult;
        private const int Height = 8 * Globals.GlobalSizeMult;
        private int _angle;
        private double _x;
        private double _y;
        private int _distFromPlayer;
        private int _changeDistFactor;
        public int DamageAmount { get; set; }
        public Direction MovingDirection { get; set; }
        public CirclingBoomerang(IPlayer player)
        {
            _player = player;
            StruckSomething = false;
            _frameCounter = 0;
            _inUse = false;
            _sprite = SpritesheetFactory.Instance.CreateTallProjectileSprite();
            _sprite.SetFrame(14, 1);
            Active = true;
            DamageAmount = 0;
            _angle = 0;
            _x = 0;
            _y = 0;
            _location = new Vector2(_player.XPosition, _player.YPosition);
            SoundFactory.Instance.PlayBoomerangFly();
            _distFromPlayer = 5;
            _changeDistFactor = 1;
        }
        public void Draw()
        {
            if (_inUse) _sprite.Draw(_location);
        }

        public void Update()
        {
            if (_inUse)
            {
                _frameCounter++;
                if (_frameCounter % 4 == 0) _distFromPlayer += _changeDistFactor;
                if (_frameCounter % 2 == 0) _sprite.SetFrame(14, _frameCounter / 2);
                _frameCounter %= 16;
                _x = _distFromPlayer * Math.Cos(_angle * Math.PI/180) * Globals.GlobalSizeMult;
                _y = _distFromPlayer * Math.Sin(_angle * Math.PI / 180) * Globals.GlobalSizeMult;
                _location = new Vector2((float)_x + _player.XPosition, (float)_y + _player.YPosition);
                _angle += 12 - (_distFromPlayer / 5);
                if (_distFromPlayer == 20) _changeDistFactor = -1;
                if (_distFromPlayer == 5 && _changeDistFactor == -1)
                {
                    Active = false;
                    _player.UseItem(typeof(EmptyItem));
                }
                UpdateHitbox();
            }
        }

        public void Use()
        {
            _inUse = true;
        }

        public void UpdateHitbox()
        {
            Hitbox = new Rectangle((int)_location.X - Width / 2, (int)_location.Y - Height / 2, Width, Height);
        }
    }
}
