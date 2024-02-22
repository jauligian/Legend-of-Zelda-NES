using CSE3902.AbstractClasses;
using CSE3902.Interfaces;
using CSE3902.Projectiles;
using CSE3902.Shared.Managers;
using Microsoft.Xna.Framework;
using System;

namespace CSE3902.States;

public class RightItemState : AbstractPlayerState
{
    private int _timeToMove;
    private bool _itemUsed;
    private readonly int _spriteColumn = 3;

    public RightItemState(IPlayer player)
    {
        MyPlayer = player;
        _spriteColumn = 3;
        MyPlayer.CurrentSprite.SetFrame(MyPlayer.CurrentSprite.Row, _spriteColumn);
        _timeToMove = 10;
        _itemUsed = false;
    }

    public override void MoveDown()
    {
    } //do nothing 

    public override void MoveLeft()
    {
    } //do nothing 

    public override void MoveRight()
    {
    } //do nothing 

    public override void MoveUp()
    {
    } //do nothing 

    public override void Update()
    {
        _timeToMove--;
        if (_timeToMove == 0)
        {
            MyPlayer.State = new RightIdleState(MyPlayer);
        }
    }

    public override void UseItem(Type itemType, Game1 game)
    {
        if (!_itemUsed)
        {
            IPlayerProjectile item;
            if (itemType == typeof(IArrow))
            {
                item = new ArrowRight(MyPlayer);
                MyPlayer.Inventory.Rupees--;
            }
            else if (itemType == typeof(IBomb))
            {
                item = new BombRight(MyPlayer);
                MyPlayer.Inventory.Bombs--;
            }
            else if (itemType == typeof(IBoomerang))
            {
                item = new BoomerangRight(MyPlayer);
            }
            else if (itemType == typeof(Hadoken))
            {
                item = new Hadoken(MyPlayer);
            }
            else
            {
                _itemUsed = true;
                return;
            }

            PlayerProjectilesManager.Instance.SpawnProjectile(item);
            _itemUsed = true;
        }
    }

    public override void UseSword(Game1 game)
    {
    } //do nothing
}