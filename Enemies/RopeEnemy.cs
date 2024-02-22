using CSE3902.AbstractClasses;
using CSE3902.Interfaces;
using CSE3902.Shared;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace CSE3902.Enemies;

public class RopeEnemy : AbstractEnemy
{
    private int _spriteFaceDirection;
    private bool _chargeState;
    private Rectangle _upTripwire;
    private Rectangle _downTripwire;
    private Rectangle _leftTripwire;
    private Rectangle _rightTripwire;
    private List<Rectangle> _tripwireList;
    private const int TripwireLength = 256 * Globals.GlobalSizeMult;
    private const int TripwireWidth = 1 * Globals.GlobalSizeMult;
    private const int BaseMoveSpeed = (int)(0.8 * Globals.GlobalSizeMult);
    private const int ChargeMoveSpeed = (int)(1.2 * Globals.GlobalSizeMult);

    public RopeEnemy(Vector2 position) : base()
    {
        Height = 16 * Globals.GlobalSizeMult;
        Width = 16 * Globals.GlobalSizeMult;
        Sprite = SpritesheetFactory.Instance.CreateRopeSprite();
        Position = position;
        TimeCounter = 0;
        CurrentDirection = Rand.Next(4);
        DamageAmount = 1;
        MovingDirection = Direction.None;
        UpdateHitbox();
        Active = true;
        Health = 2;
        StepSize = 2;
        _spriteFaceDirection = 1;
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

    public override List<Rectangle> GetTripwires()
    {
        return _tripwireList;
    }

    public override void PlayerActivateTripwire(Rectangle r)
    {
        if (!_chargeState)
        {
            if (r.Equals(_leftTripwire))
            {
                CurrentDirection = 2;
                _chargeState = true;
                StepSize = ChargeMoveSpeed;
                _spriteFaceDirection = 2;
            }
            else if (r.Equals(_rightTripwire))
            {
                CurrentDirection = 3;
                _chargeState = true;
                StepSize = ChargeMoveSpeed;
                _spriteFaceDirection = 1;
            }
            else if (r.Equals(_upTripwire))
            {
                CurrentDirection = 1;
                _chargeState = true;
                StepSize = ChargeMoveSpeed;
            }
            else if (r.Equals(_downTripwire))
            {
                CurrentDirection = 0;
                _chargeState = true;
                StepSize = ChargeMoveSpeed;
            }
        }
    }

    public override void Update()
    {
        base.Update();
        int randomInt = Rand.Next(200);
        if (randomInt <= 3 && !Controlled && !_chargeState)
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
    }

    public override void HitBlock(IBlock block)
    {
        _chargeState = false;
        StepSize = BaseMoveSpeed;
    }

    public override void HitDoor(IDoor door)
    {
        _chargeState = false;
        StepSize = BaseMoveSpeed;
    }

    public override void Move()
    {
        if (Frozen || Controlled)
        {
            UpdateHitbox();
            return;
        }

        switch (CurrentDirection)
        {
            case 0:
                Position = new Vector2(Position.X, Position.Y + StepSize);
                MovingDirection = Direction.Down;
                break;
            case 1:
                Position = new Vector2(Position.X, Position.Y - StepSize);
                MovingDirection = Direction.Up;
                break;
            case 2:
                Position = new Vector2(Position.X - StepSize, Position.Y);
                MovingDirection = Direction.Left;
                break;
            case 3:
                Position = new Vector2(Position.X + StepSize, Position.Y);
                MovingDirection = Direction.Right;
                break;
        }

        UpdateHitbox();
    }

    public override void UpdateHitbox()
    {
        base.UpdateHitbox();
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

    public override void UpdateSprite()
    {
        if (TimeCounter >= 8)
        {
            if (Sprite.Column == 1) Sprite.SetFrame(_spriteFaceDirection, 2);
            else Sprite.SetFrame(_spriteFaceDirection, 1);
            TimeCounter = 0;
        }

        TimeCounter++;
    }
}