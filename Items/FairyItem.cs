using CSE3902.AbstractClasses;
using CSE3902.Shared;
using Microsoft.Xna.Framework;
using System;

namespace CSE3902.Items;

public class FairyItem : AbstractItem
{
    private int _frameCounter;
    private int _currentDirection;
    private readonly Random _rand;
    private const int ItemId = 5;
    private const int RandMax = 250; //Used for determining the frequency of direction changes.
    private const int RandTrigger = 7;

    public FairyItem(Vector2 position)
    {
        _frameCounter = 0;
        Sprite = SpritesheetFactory.Instance.CreateTallItemSprite();
        Sprite.SetFrame(SpriteLocationRow, ItemId);
        Position = position;
        _currentDirection = 0;
        _rand = new Random();
    }

    public override void Update()
    {
        base.Update();
        _frameCounter++;
        int randomInt = _rand.Next(RandMax);
        if (randomInt <= RandTrigger) _currentDirection = randomInt; //Only switch directions occasionally.
        Move();
        if (_frameCounter != 4) return;

        _frameCounter = 0;
        if (Sprite.Column == ItemId)
        {
            Sprite.SetFrame(SpriteLocationRow, ItemId + 1);
        }
        else
        {
            Sprite.SetFrame(SpriteLocationRow, ItemId);
        }
    }

    private void Move()
    {
        switch (_currentDirection)
        {
            case 0:
                Position = new Vector2(Position.X, Position.Y + 2);
                MovingDirection = Direction.Down;
                break;
            case 1:
                Position = new Vector2(Position.X, Position.Y - 2);
                MovingDirection = Direction.Up;
                break;
            case 2:
                Position = new Vector2(Position.X - 2, Position.Y);
                MovingDirection = Direction.Left;
                break;
            case 3:
                Position = new Vector2(Position.X + 2, Position.Y);
                MovingDirection = Direction.Right;
                break;
            case 4:
                Position = new Vector2(Position.X - 1, Position.Y - 1);
                MovingDirection = Direction.UpLeft;
                break;
            case 5:
                Position = new Vector2(Position.X + 1, Position.Y - 1);
                MovingDirection = Direction.UpRight;
                break;
            case 6:
                Position = new Vector2(Position.X - 1, Position.Y + 1);
                MovingDirection = Direction.DownLeft;
                break;
            case 7:
                Position = new Vector2(Position.X + 1, Position.Y + 1);
                MovingDirection = Direction.DownRight;
                break;
        }
    }
}