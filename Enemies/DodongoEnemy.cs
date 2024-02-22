using CSE3902.AbstractClasses;
using CSE3902.Interfaces;
using CSE3902.Shared;
using Microsoft.Xna.Framework;

namespace CSE3902.Enemies;

public class DodongoEnemy : AbstractEnemy
{
    private ITextureAtlasSprite _spriteNarrow;
    private ITextureAtlasSprite _spriteWide;
    private bool _wide;
    private int _stunnedTime;

    public DodongoEnemy(Vector2 position) : base()
    {
        Height = 16 * Globals.GlobalSizeMult;
        Width = 16 * Globals.GlobalSizeMult;
        _spriteNarrow = SpritesheetFactory.Instance.CreateDodongoNarrowSprite();
        _spriteWide = SpritesheetFactory.Instance.CreateDodongoWideSprite();
        Position = position;
        TimeCounter = 0;
        //_rand = new System.Random();
        CurrentDirection = 0;
        DamageAmount = 1;
        MovingDirection = Direction.None;
        UpdateHitbox();
        Active = true;
        Health = 1;
        StepSize = 1;
        _wide = false;
        _stunnedTime = 0;
    }

    public override void Update()
    {
        if (_stunnedTime > 0)
        {
            _stunnedTime--;
            return;
        }

        int randomInt = Rand.Next(400);
        if (randomInt <= 3 && !Controlled)
        {
            CurrentDirection = randomInt;
            switch (CurrentDirection)
            {
                case 0:
                case 1:
                    _wide = false;
                    Width = 16 * Globals.GlobalSizeMult;
                    _spriteNarrow.SetFrame(CurrentDirection + 1, _spriteNarrow.Column);
                    _spriteWide.SetFrame(CurrentDirection - 1, _spriteWide.Column);
                    break;
                case 2:
                case 3:
                    _wide = true;
                    Width = 32 * Globals.GlobalSizeMult;
                    _spriteNarrow.SetFrame(CurrentDirection + 1, _spriteNarrow.Column);
                    _spriteWide.SetFrame(CurrentDirection - 1, _spriteWide.Column);
                    break;
            }
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

    public void Stun()
    {
        _stunnedTime = 120;
    }

    public override void UpdateSprite()
    {
        if (TimeCounter >= 8)
        {
            if (_spriteNarrow.Column == 1)
            {
                _spriteNarrow.SetFrame(CurrentDirection + 1, 2);
                _spriteWide.SetFrame(CurrentDirection - 1, 2);
            }
            else
            {
                _spriteNarrow.SetFrame(CurrentDirection + 1, 1);
                _spriteWide.SetFrame(CurrentDirection - 1, 1);
            }

            TimeCounter = 0;
        }

        TimeCounter++;
    }

    public override void TakeKnockback()
    {
    }

    public override void TakeDamage(int damageAmount, Direction damageDirection)
    {
        if (!(InvulnerableTime > 0) && _stunnedTime > 0)
        {
            Health -= damageAmount;
            if (Health <= 0) Die();
            else SoundFactory.Instance.PlayEnemyHit();
            InvulnerableTime = 15;
            DamagedFrom = damageDirection;
        }
    }

    public override void Draw()
    {
        if (InvulnerableTime % 5 == 0)
        {
            if (!_wide) _spriteNarrow.DrawFromCenter(Position);
            else _spriteWide.DrawFromCenter(Position);
        }
    }
}