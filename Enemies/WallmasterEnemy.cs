using CSE3902.AbstractClasses;
using CSE3902.Shared;
using CSE3902.Shared.Managers;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace CSE3902.Enemies;

public class WallmasterEnemy : AbstractEnemy
{
    private Rectangle _upTripwire;
    private Rectangle _downTripwire;
    private Rectangle _leftTripwire;
    private Rectangle _rightTripwire;
    private List<Rectangle> _tripwireList;
    private const int TripwireLength = 256 * Globals.GlobalSizeMult;
    private const int TripwireWidth = 1 * Globals.GlobalSizeMult;
    private const int AttackMoveSpeed = (int)(1.5 * Globals.GlobalSizeMult);
    private int _moveAmount = 0;
    private MovementState _mState;
    private int _totalMoved = 0;
    private int _deathCooldown = 100;
    private Vector2 _positionStorage;

    public WallmasterEnemy(Vector2 position)
    {
        Height = 16 * Globals.GlobalSizeMult;
        Width = 16 * Globals.GlobalSizeMult;
        Sprite = SpritesheetFactory.Instance.CreateWallmasterSprite();
        Position = position;
        TimeCounter = 0;
        //_rand = new System.Random();
        CurrentDirection = Rand.Next(4);
        DamageAmount = 1;
        MovingDirection = Direction.None;
        UpdateHitbox();
        _mState = MovementState.Idle;
        Active = true;
        Health = 1;
    }

    public override void Die()
    {
        GameObjectManagers.DeathManager.Spawn(new DeadObject(Position));
        SoundFactory.Instance.PlayEnemyDie();
        MovingDirection = Direction.None;
        _mState = MovementState.Idle;
        _totalMoved = 0;
        Position = _positionStorage;
        _deathCooldown = 100;
    }

    public override void InitializeGlobalPosition(int horizontalOffset, int verticalOffset)
    {
        base.InitializeGlobalPosition(horizontalOffset, verticalOffset);
        _tripwireList = new List<Rectangle>();
        _upTripwire = new Rectangle((int)Position.X, (int)Position.Y - TripwireLength, TripwireWidth, TripwireLength);
        _tripwireList.Add(_upTripwire);
        _downTripwire = new Rectangle((int)Position.X, (int)Position.Y, TripwireWidth, TripwireLength);
        _tripwireList.Add(_downTripwire);
        _leftTripwire = new Rectangle((int)Position.X - TripwireLength, (int)Position.Y, TripwireLength, TripwireWidth);
        _tripwireList.Add(_leftTripwire);
        _rightTripwire = new Rectangle((int)Position.X, (int)Position.Y, TripwireLength, TripwireWidth);
        _tripwireList.Add(_rightTripwire);
    }

    public override void Update()
    {
        base.Update();
        int randomInt = Rand.Next(200);
        if (randomInt <= 3)
        {
            CurrentDirection = randomInt;
        }

        if (InvulnerableTime <= 0)
        {
            UpdateSprite();
            Move();
        }
        else
        {
            InvulnerableTime--;
            TakeKnockback();
        }

        if (_deathCooldown > 0) _deathCooldown--;
    }

    public override void UpdateHitbox()
    {
        base.UpdateHitbox();
        if (_mState == MovementState.Idle) Hitbox = new Rectangle(0, 0, 0, 0);
    }

    public override void Move()
    {
        if (_mState == MovementState.Attacking) _moveAmount = AttackMoveSpeed;
        else _moveAmount = 0;
        switch (MovingDirection)
        {
            case Direction.Left:
                Position = new Vector2(Position.X - _moveAmount, Position.Y);
                break;
            case Direction.Right:
                Position = new Vector2(Position.X - _moveAmount, Position.Y);
                break;
            case Direction.Up:
                Position = new Vector2(Position.X, Position.Y - _moveAmount);
                break;
            case Direction.Down:
                Position = new Vector2(Position.X, Position.Y - _moveAmount);
                break;
        }

        _totalMoved += _moveAmount;
        int holder = 0;
        if (MovingDirection == Direction.Up || MovingDirection == Direction.Down) holder = Globals.FloorHeight;
        else if (MovingDirection == Direction.Left || MovingDirection == Direction.Right) holder = Globals.FloorWidth;
        if (_totalMoved >= holder + 2 * Globals.BlockSize)
        {
            //must be adjusted by room width
            MovingDirection = Direction.None;
            _mState = MovementState.Idle;
            _totalMoved = 0;
            Position = _positionStorage;
        }

        UpdateHitbox();
    }

    public override void UpdateSprite()
    {
        if (TimeCounter >= 24)
        {
            if (Sprite.Column == 1)
            {
                Sprite.SetFrame(Sprite.Row, 2);
                TimeCounter = 0;
            }
            else
            {
                Sprite.SetFrame(Sprite.Row, 1);
                TimeCounter = 0;
            }
        }

        TimeCounter++;
    }

    public override void Draw()
    {
        if (_mState == MovementState.Attacking)
            base.Draw();
    }

    public override List<Rectangle> GetTripwires()
    {
        return _tripwireList;
    }

    public override void PlayerActivateTripwire(Rectangle r)
    {
        if (_mState == MovementState.Idle && _deathCooldown <= 0)
        {
            _positionStorage = new Vector2(Position.X, Position.Y);
            if (r.Equals(_leftTripwire))
            {
                Position = new Vector2(Position.X + Globals.BlockSize, Position.Y);
                MovingDirection = Direction.Left;
                _mState = MovementState.Attacking;
                Sprite.SetFrame(3, 2);
            }
            else if (r.Equals(_rightTripwire))
            {
                Position = new Vector2(Position.X + Globals.BlockSize + Globals.FloorWidth, Position.Y);
                MovingDirection = Direction.Right;
                _mState = MovementState.Attacking;
                Sprite.SetFrame(2, 2);
            }
            else if (r.Equals(_upTripwire))
            {
                Position = new Vector2(Position.X, Position.Y + Globals.BlockSize);
                MovingDirection = Direction.Up;
                _mState = MovementState.Attacking;
                Sprite.SetFrame(3, 2);
            }
            else if (r.Equals(_downTripwire))
            {
                Position = new Vector2(Position.X, Position.Y + Globals.BlockSize + Globals.FloorHeight);
                MovingDirection = Direction.Down;
                _mState = MovementState.Attacking;
                Sprite.SetFrame(1, 2);
            }
        }
    }

    public override void TakeKnockback()
    {
    }
}