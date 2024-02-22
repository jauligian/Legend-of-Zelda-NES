using CSE3902.AbstractClasses;
using CSE3902.Shared;
using Microsoft.Xna.Framework;

namespace CSE3902.Enemies;

public class GelEnemy : AbstractEnemy
{
    private int _moveCounter;
    private int _currentStopTime;
    private int _totalStopTime;
    private int _spriteTimeCounter;
    private bool _waitingState;

    public GelEnemy(Vector2 position) : base()
    {
        Height = 16 * Globals.GlobalSizeMult;
        Width = 16 * Globals.GlobalSizeMult;
        Sprite = SpritesheetFactory.Instance.CreateGelSprite();
        Position = position;
        _moveCounter = 0;
        _currentStopTime = 0;
        _totalStopTime = 60;
        _spriteTimeCounter = 0;
        //_rand = new System.Random();
        CurrentDirection = Rand.Next(4);
        _waitingState = false;
        DamageAmount = 1;
        MovingDirection = Direction.None;
        UpdateHitbox();
        Active = true;
        Health = 1;
        StepSize = 2;
    }

    public override void Update()
    {
        base.Update();
        if (InvulnerableTime > 0) InvulnerableTime--;
        else UpdateSprite();
        if (!_waitingState)
        {
            if (InvulnerableTime <= 0) Move();
            else TakeKnockback();
            _moveCounter++;
        }

        if (_moveCounter >= 24)
        {
            if (InvulnerableTime > 0) TakeKnockback();
            _waitingState = true;
            if (_currentStopTime >= _totalStopTime)
            {
                _currentStopTime = 0;
                _totalStopTime = Rand.Next(60);
                CurrentDirection = Rand.Next(4);
                _waitingState = false;
                _moveCounter = 0;
            }

            _currentStopTime++;
        }
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

    public override void UpdateSprite()
    {
        if (_spriteTimeCounter >= 3)
        {
            if (Sprite.Column == 1)
            {
                Sprite.SetFrame(1, 2);
                _spriteTimeCounter = 0;
            }
            else
            {
                Sprite.SetFrame(1, 1);
                _spriteTimeCounter = 0;
            }
        }

        _spriteTimeCounter++;
    }
}