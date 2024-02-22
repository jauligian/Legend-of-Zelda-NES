using CSE3902.Interfaces;
using CSE3902.Inventory;
using CSE3902.Shared;
using Microsoft.Xna.Framework;
using System;

namespace CSE3902.Players;

public class DamagedPlayer : IPlayer
{
    private readonly Game1 _game;
    private readonly IPlayer _decoratedPlayer;
    private int _invincibleTimeLeft;
    private readonly Direction _damagedFrom;

    public DamagedPlayer(IPlayer player, Game1 game, Direction damagedFrom)
    {
        _game = game;
        _decoratedPlayer = player;
        _invincibleTimeLeft = 15;
        _damagedFrom = damagedFrom;
    }

    public int XPosition
    {
        get => _decoratedPlayer.XPosition;
        set => _decoratedPlayer.XPosition = value;
    }

    public int YPosition
    {
        get => _decoratedPlayer.YPosition;
        set => _decoratedPlayer.YPosition = value;
    }

    public IPlayerState State
    {
        set => _decoratedPlayer.State = value;
    }

    public int PlayerHealth
    {
        get => _decoratedPlayer.PlayerHealth;
        set => _decoratedPlayer.PlayerHealth = value;
    }

    public int MaxPlayerHealth
    {
        get => _decoratedPlayer.MaxPlayerHealth;
        set => _decoratedPlayer.MaxPlayerHealth = value;
    }

    public ITextureAtlasSprite CurrentSprite
    {
        get => _decoratedPlayer.CurrentSprite;
        set => _decoratedPlayer.CurrentSprite = value;
    }

    public ISword Sword
    {
        get => _decoratedPlayer.Sword;
        set => _decoratedPlayer.Sword = value;
    }

    public Rectangle Hitbox => _decoratedPlayer.Hitbox;

    public Direction MovingDirection
    {
        get => _decoratedPlayer.MovingDirection;
        set => _decoratedPlayer.MovingDirection = value;
    }

    public Type CurrentItemType
    {
        get => _decoratedPlayer.CurrentItemType;
        set => _decoratedPlayer.CurrentItemType = value;
    }

    public PlayerInventory Inventory
    {
        get => _decoratedPlayer.Inventory;
        set => _decoratedPlayer.Inventory = value;
    }

    public void MoveDown()
    {
    }

    public void MoveLeft()
    {
    }

    public void MoveRight()
    {
    }

    public void MoveUp()
    {
    }

    public void TakeDamage(int damageAmount, Direction damagedFrom)
    {
    }

    public void Update()
    {
        _decoratedPlayer.Update();
        _invincibleTimeLeft--;
        TakeKnockback();

        if (_invincibleTimeLeft == 0)
        {
            CurrentSprite.Color = Color.White;
            _game.Player = _decoratedPlayer;
        }
    }

    public void UseItem(Type itemType)
    {
        _decoratedPlayer.UseItem(itemType);
    }


    public void UseSword()
    {
        _decoratedPlayer.UseSword();
    }

    public void Draw()
    {
        //Only switch colors every two frames
        CurrentSprite.Color = (_invincibleTimeLeft % 8) switch
        {
            0 => Color.White,
            2 => Color.Red,
            4 => Color.Yellow,
            6 => Color.Green,
            _ => CurrentSprite.Color
        };

        _decoratedPlayer.Draw();
    }

    private void TakeKnockback()
    {
        switch (_damagedFrom)
        {
            case Direction.Left:
                XPosition += (int)(_invincibleTimeLeft * .75);
                MovingDirection = Direction.Right;
                break;
            case Direction.Right:
                XPosition -= (int)(_invincibleTimeLeft * .75);
                MovingDirection = Direction.Left;
                break;
            case Direction.Up:
                YPosition += (int)(_invincibleTimeLeft * .75);
                MovingDirection = Direction.Down;
                break;
            case Direction.Down:
                YPosition -= (int)(_invincibleTimeLeft * .75);
                MovingDirection = Direction.Up;
                break;
        }

        UpdateHitbox();
    }

    public void UpdateHitbox()
    {
        _decoratedPlayer.UpdateHitbox();
    }

    public void PickupItem(IItem item)
    {
    }
    public void WalkBetweenRooms(Direction direction) { }
}