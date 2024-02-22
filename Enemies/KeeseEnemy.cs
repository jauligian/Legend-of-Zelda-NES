using CSE3902.AbstractClasses;
using CSE3902.Shared;
using Microsoft.Xna.Framework;

namespace CSE3902.Enemies;

public class KeeseEnemy : AbstractEnemy
{
    public KeeseEnemy(Vector2 position) : base()
    {
        Height = 16 * Globals.GlobalSizeMult;
        Width = 16 * Globals.GlobalSizeMult;
        Sprite = SpritesheetFactory.Instance.CreateKeeseSprite();
        Position = position;
        TimeCounter = 0;
        //_rand = new System.Random();
        CurrentDirection = Rand.Next(8);
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
        int randomInt = Rand.Next(250);
        if (randomInt <= 7 && !Controlled)
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
            case 4:
                Position = new Vector2(Position.X - StepSize / 2, Position.Y - StepSize / 2);
                MovingDirection = Direction.UpLeft;
                break;
            case 5:
                Position = new Vector2(Position.X + StepSize / 2, Position.Y - StepSize / 2);
                MovingDirection = Direction.UpRight;
                break;
            case 6:
                Position = new Vector2(Position.X - StepSize / 2, Position.Y + StepSize / 2);
                MovingDirection = Direction.DownLeft;
                break;
            case 7:
                Position = new Vector2(Position.X + StepSize / 2, Position.Y + StepSize / 2);
                MovingDirection = Direction.DownRight;
                break;
        }

        UpdateHitbox();
    }

    public override void UpdateSprite()
    {
        if (TimeCounter >= 4)
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