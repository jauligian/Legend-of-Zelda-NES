using CSE3902.AbstractClasses;
using CSE3902.Collisions;
using CSE3902.Interfaces;
using CSE3902.Shared;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace CSE3902.Enemies;

internal enum MovementState
{
    Idle,
    Attacking,
    Returning
}

public class BladeTrapEnemy : AbstractEnemy
{
    private Rectangle _upTripwire;
    private Rectangle _downTripwire;
    private Rectangle _leftTripwire;
    private Rectangle _rightTripwire;
    private List<Rectangle> _tripwireList;
    private const int TripwireLength = 256 * Globals.GlobalSizeMult;
    private const int TripwireWidth = 1 * Globals.GlobalSizeMult;
    private const int AttackMoveSpeed = (int)(1.2 * Globals.GlobalSizeMult);
    private const int ReturnMoveSpeed = (int)(0.8 * Globals.GlobalSizeMult);
    private int _moveAmount = 0;
    private MovementState _mState; //0 for idle, 1 for attacking, 2 for returning

    public BladeTrapEnemy(Vector2 location) : base()
    {
        Height = 16 * Globals.GlobalSizeMult;
        Width = 16 * Globals.GlobalSizeMult;
        Sprite = SpritesheetFactory.Instance.CreateBladeTrapSprite();
        Position = location;
        DamageAmount = 1;
        MovingDirection = Direction.None;
        UpdateHitbox();
        _mState = MovementState.Idle;
        Active = true;
        Health = 0;
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

    public override void TakeDamage(int damageAmount, Direction damageDirection)
    {
        //dont?
    }

    public override void Update()
    {
        Move();
    }

    public override void UpdateSprite()
    {
    }

    public override void TakeKnockback()
    {
    }

    public override List<Rectangle> GetTripwires()
    {
        return _tripwireList;
    }

    public override void HitBlock(IBlock block)
    {
        if (_mState == MovementState.Attacking)
        {
            _mState = MovementState.Returning;
            MovingDirection = CollisionHelper.Instance.InvertDirection(MovingDirection);
        }
        else if (_mState == MovementState.Returning)
        {
            _mState = MovementState.Idle;
            MovingDirection = Direction.None;
        }
    }

    public override void HitDoor(IDoor door)
    {
        if (_mState == MovementState.Attacking)
        {
            _mState = MovementState.Returning;
            MovingDirection = CollisionHelper.Instance.InvertDirection(MovingDirection);
        }
        else if (_mState == MovementState.Returning)
        {
            _mState = MovementState.Idle;
            MovingDirection = Direction.None;
        }
    }

    public override void HitEnemy(IEnemy enemy)
    {
        if (_mState == MovementState.Attacking)
        {
            _mState = MovementState.Returning;
            MovingDirection = CollisionHelper.Instance.InvertDirection(MovingDirection);
        }
    }

    public override void HitPlayer(IPlayer player)
    {
        if (_mState == MovementState.Attacking)
        {
            _mState = MovementState.Returning;
            MovingDirection = CollisionHelper.Instance.InvertDirection(MovingDirection);
        }
    }

    public override void PlayerActivateTripwire(Rectangle r)
    {
        if (_mState == MovementState.Idle)
        {
            if (r.Equals(_leftTripwire))
            {
                MovingDirection = Direction.Left;
                _mState = MovementState.Attacking;
            }
            else if (r.Equals(_rightTripwire))
            {
                MovingDirection = Direction.Right;
                _mState = MovementState.Attacking;
            }
            else if (r.Equals(_upTripwire))
            {
                MovingDirection = Direction.Up;
                _mState = MovementState.Attacking;
            }
            else if (r.Equals(_downTripwire))
            {
                MovingDirection = Direction.Down;
                _mState = MovementState.Attacking;
            }
        }
    }

    public override void Move()
    {
        if (_mState == MovementState.Attacking) _moveAmount = AttackMoveSpeed;
        else if (_mState == MovementState.Returning) _moveAmount = ReturnMoveSpeed;
        else _moveAmount = 0;
        switch (MovingDirection)
        {
            case Direction.Left:
                Position = new Vector2(Position.X - _moveAmount, Position.Y);
                break;
            case Direction.Right:
                Position = new Vector2(Position.X + _moveAmount, Position.Y);
                break;
            case Direction.Up:
                Position = new Vector2(Position.X, Position.Y - _moveAmount);
                break;
            case Direction.Down:
                Position = new Vector2(Position.X, Position.Y + _moveAmount);
                break;
        }

        UpdateHitbox();
    }
}