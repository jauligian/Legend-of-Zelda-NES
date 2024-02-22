using CSE3902.AbstractClasses;
using CSE3902.Projectiles;
using CSE3902.Shared;
using CSE3902.Shared.Managers;
using Microsoft.Xna.Framework;

namespace CSE3902.Enemies;

public class GoriyaEnemy : AbstractEnemy
{
    private int _spriteTimeCounter;
    private GoriyaBoomerang _boomerang;

    public GoriyaEnemy(Vector2 position) : base()
    {
        Height = 16 * Globals.GlobalSizeMult;
        Width = 16 * Globals.GlobalSizeMult;
        Sprite = SpritesheetFactory.Instance.CreateGoriyaSprite();
        Position = position;
        TimeCounter = 0;
        _spriteTimeCounter = 0;
        //_rand = new System.Random();
        CurrentDirection = Rand.Next(4);
        _boomerang = new GoriyaBoomerang(Position, CurrentDirection);
        _boomerang.Active = false;
        DamageAmount = 1;
        MovingDirection = Direction.None;
        UpdateHitbox();
        Active = true;
        Health = 3;
        StepSize = 1;
    }

    public override void Update()
    {
        base.Update();
        if (InvulnerableTime > 0) InvulnerableTime--;
        else UpdateSprite();
        if (Frozen) return;
        if (!_boomerang.Active)
        {
            int randomInt = Rand.Next(200);
            if (randomInt <= 3 && !Controlled)
            {
                CurrentDirection = randomInt;
            }

            if (!(InvulnerableTime > 0)) Move();
            else TakeKnockback();
            if (TimeCounter >= 60 * 4 && !Controlled)
            {
                _boomerang = new GoriyaBoomerang(Position, CurrentDirection);
                GameObjectManagers.EnemyProjectileManager.Spawn(_boomerang);
            }
        }
        else
        {
            // if (InvulnerableTime <= 0) TakeKnockback(); ADD IF KNOCKBACK WHILE BOOMERANGING IS NEEDED
            TimeCounter = 0;
        }

        TimeCounter++;
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
        if (_spriteTimeCounter >= 8)
        {
            int nextColumn = Sprite.Column == 1 ? 2 : 1;
            Sprite.SetFrame(CurrentDirection + 1, nextColumn);
            _spriteTimeCounter = 0;
        }

        _spriteTimeCounter++;
    }
}