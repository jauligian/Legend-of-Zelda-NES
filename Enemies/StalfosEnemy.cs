using CSE3902.AbstractClasses;
using CSE3902.Shared;
using Microsoft.Xna.Framework;

namespace CSE3902.Enemies;

public class StalfosEnemy : AbstractEnemy
{
    public StalfosEnemy(Vector2 position) : base()
    {
        Height = 16 * Globals.GlobalSizeMult;
        Width = 16 * Globals.GlobalSizeMult;
        Sprite = SpritesheetFactory.Instance.CreateStalfosSprite();
        Position = position;
        TimeCounter = 0;
        //_rand = new System.Random();
        CurrentDirection = Rand.Next(4);
        DamageAmount = 1;
        MovingDirection = Direction.None;
        UpdateHitbox();
        Active = true;
        Health = 2;
        StepSize = 1;
    }

    public override void Update()
    {
        base.Update();
        int randomInt = Rand.Next(200);
        if (randomInt <= 3 && !Controlled)
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
        if (TimeCounter >= 8)
        {
            if (Sprite.Column == 1)
            {
                Sprite.SetFrame(1, 2);
                TimeCounter = 0;
            }
            else
            {
                Sprite.SetFrame(1, 1);
                TimeCounter = 0;
            }
        }

        TimeCounter++;
    }
}