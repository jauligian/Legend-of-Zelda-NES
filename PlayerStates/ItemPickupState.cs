using CSE3902.AbstractClasses;
using CSE3902.Interfaces;
using CSE3902.Items;
using CSE3902.Shared;
using CSE3902.Shared.Managers;
using Microsoft.Xna.Framework;
using System;

namespace CSE3902.States;

public class ItemPickupState : AbstractPlayerState
{
    private int _timeToMove;
    private readonly int _spriteColumn;
    private readonly int _spriteRow;
    private const int Height = 16 * Globals.GlobalSizeMult;
    private const int Width = 16 * Globals.GlobalSizeMult;

    public ItemPickupState(IPlayer player)
    {
        MyPlayer = player;
        _spriteRow = 5;
        _spriteColumn = 1;
        MyPlayer.CurrentSprite.SetFrame(_spriteRow, _spriteColumn);
        _timeToMove = 20;
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
        if (_timeToMove == 0) MyPlayer.State = new UpIdleState(MyPlayer);
    }

    public override void Draw()
    {
        base.Draw();
    }

    public override void UseItem(Type itemType, Game1 game)
    {
    } //do nothing

    public override void UseSword(Game1 game)
    {
    }

    public override void PickupItem(IItem item)
    {
        if (item is TriforcePieceItem) MyPlayer.CurrentSprite.SetFrame(_spriteRow, 2);
        else
        {
            MyPlayer.CurrentSprite.SetFrame(_spriteRow, 1);
        }

        //TODO: maybe create freezeall method? 
        if (item is ClockItem)
        {
            foreach (IEnemy enemy in GameObjectManagers.EnemyManager.ActiveGameObjects)
            {
                enemy.Freeze(100000);
            }
        }

        item.HoldItem(new Vector2(MyPlayer.Hitbox.Left + Width / 2, MyPlayer.Hitbox.Top + Height / 2));
        //Some sound effects are reused in the original game, this reflects that.
        if (item is RupeeItem) SoundFactory.Instance.PlayGetRupee();
        else if (item is TriforcePieceItem or HeartContainerItem or BowItem or BoomerangItem)
            SoundFactory.Instance.PlayGetKeyItem();
        else if (item is HeartItem or KeyItem) SoundFactory.Instance.PlayGetHeart();
        else SoundFactory.Instance.PlayGetMinorItem();
    }
}