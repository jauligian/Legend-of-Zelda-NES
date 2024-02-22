using CSE3902.Enemies;
using CSE3902.Interfaces;
using CSE3902.Items;
using CSE3902.Shared;
using CSE3902.Shared.Managers;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Text;

namespace CSE3902.AbstractClasses;

public abstract class AbstractEnemy : IEnemy
{
    protected ITextureAtlasSprite Sprite;
    public Vector2 Position { get; set; }
    protected int Width;
    protected int Height;
    protected int TimeCounter;

    protected int
        CurrentDirection; //0 is down, 1 is up, 2 is left, 3 is right, 4 is down-left, 5 is down-right, 6 is up-left, 7 is up-right

    protected static System.Random Rand = new();
    public Rectangle Hitbox { get; set; }
    public int DamageAmount { get; set; }
    public Direction MovingDirection { get; set; }
    public bool Active { get; set; }
    public int Health { get; set; }
    public int InvulnerableTime { get; set; }
    public Direction DamagedFrom { get; set; }
    private int _frozenTime;
    public bool Frozen { get; private set; }
    public bool Controlled { get; set; }
    public int StepSize { get; set; }

    public AbstractEnemy()
    {
        Controlled = false;
    }

    public virtual void InitializeGlobalPosition(int horizontalOffset, int verticalOffset)
    {
        Position = new Vector2(Position.X + horizontalOffset,
            Position.Y + verticalOffset);
    }

    public virtual void TakeDamage(int damageAmount, Direction damageDirection)
    {
        if (!(InvulnerableTime > 0))
        {
            Health -= damageAmount;
            if (Health <= 0) Die();
            else SoundFactory.Instance.PlayEnemyHit();
            InvulnerableTime = 15;
            DamagedFrom = damageDirection;
        }
    }

    public virtual void HitEnemy(IEnemy enemy)
    {
        if (Controlled)
        {
            enemy.TakeDamage(1, MovingDirection);
            TakeDamage(1, enemy.MovingDirection);
        }
    }

    public virtual void HitPlayer(IPlayer player)
    {
    }

    public virtual void HitBlock(IBlock block)
    {
    }

    public virtual void HitDoor(IDoor door)
    {
    }

    public abstract void UpdateSprite();

    public virtual void Move()
    {
    }

    public void Freeze(int ticks)
    {
        Frozen = true;
        if (_frozenTime < ticks) _frozenTime = ticks;
    }

    public void Unfreeze()
    {
        Frozen = false;
    }

    public virtual List<Rectangle> GetTripwires()
    {
        return null;
    }

    public virtual void PlayerActivateTripwire(Rectangle r)
    {
    }

    public virtual void Die()
    {
        GameObjectManagers.DeathManager.Spawn(new DeadObject(Position));
        IItem ItemDrop = ItemGenerator.GenerateDrop(this);
        if(ItemDrop != null) GameObjectManagers.ItemManager.Spawn(ItemDrop);
        SoundFactory.Instance.PlayEnemyDie();
        Active = false;
    }

    public virtual void UpdateHitbox()
    {
        Hitbox = new Rectangle((int)Position.X - Width / 2, (int)Position.Y - Height / 2, Width, Height);
    }

    public virtual void Draw()
    {
        if (InvulnerableTime % 5 == 0)
        {
            if (Controlled) Sprite.DrawColored(Position, Color.Blue);
            else Sprite.Draw(Position);
        }
    }

    public virtual void Update()
    {
        if (_frozenTime <= 0)
        {
            Unfreeze();
        }
        else
        {
            _frozenTime--;
        }
    }

    public virtual void TakeKnockback()
    {
        switch (DamagedFrom)
        {
            case Direction.Left:
                Position = new Vector2(Position.X + (int)(InvulnerableTime * .75), Position.Y);
                MovingDirection = Direction.Right;
                break;
            case Direction.Right:
                Position = new Vector2(Position.X - (int)(InvulnerableTime * .75), Position.Y);
                MovingDirection = Direction.Left;
                break;
            case Direction.Up:
                Position = new Vector2(Position.X, Position.Y + (int)(InvulnerableTime * .75));
                MovingDirection = Direction.Down;
                break;
            case Direction.Down:
                Position = new Vector2(Position.X, Position.Y - (int)(InvulnerableTime * .75));
                MovingDirection = Direction.Up;
                break;
            default:
                break;
        }

        UpdateHitbox();
    }

    public virtual void SetDirection(Direction direction)
    {
        MovingDirection = direction;
        switch (direction)
        {
            case Direction.Down:
                CurrentDirection = 0;
                break;
            case Direction.Up:
                CurrentDirection = 1;
                break;
            case Direction.Left:
                CurrentDirection = 2;
                break;
            case Direction.Right:
                CurrentDirection = 3;
                break;
        }
    }
}