using CSE3902.AbstractClasses;
using CSE3902.Interfaces;
using CSE3902.Items;
using CSE3902.Projectiles;
using CSE3902.Shared;
using CSE3902.Shared.Managers;
using Microsoft.Xna.Framework;
using System;

namespace CSE3902.States;
public class StalfosMainState : AbstractPlayerState
{
    private int _movingCounter;
    private int _lastMovingCounter;
    private readonly int _spriteFrameOneRow;
    private readonly int _spriteFrameTwoRow;
    private readonly int _spriteFrameOneColumn;
    private readonly int _spriteFrameTwoColumn;
    private Direction _lastDirection;
    private int _timeToMove;

    private const int Height = 16 * Globals.GlobalSizeMult;
    private const int Width = 16 * Globals.GlobalSizeMult;
    public StalfosMainState(IPlayer player)
    {
        MyPlayer = player;
        _movingCounter = 0;
        _spriteFrameOneRow = 1;
        _spriteFrameTwoRow = 1;
        _spriteFrameOneColumn = 1;
        _spriteFrameTwoColumn = 2;
        MyPlayer.CurrentSprite = SpritesheetFactory.Instance.CreateStalfosSprite();
        MyPlayer.CurrentSprite.SetFrame(_spriteFrameOneRow, _spriteFrameOneColumn);
        _lastDirection = Direction.Down;
        _timeToMove = 0;
    }
    public override void MoveDown()
    {
        if (_timeToMove != 0) return;
        MyPlayer.MovingDirection = Direction.Down;
        _lastDirection = Direction.Down;
        MyPlayer.YPosition += StepSize;
        _movingCounter++;
    }
    public override void MoveUp()
    {
        if (_timeToMove != 0) return;
        MyPlayer.MovingDirection = Direction.Up;
        _lastDirection = Direction.Up;
        MyPlayer.YPosition -= StepSize;
        _movingCounter++;
    }
    public override void MoveLeft()
    {
        if (_timeToMove != 0) return;
        MyPlayer.MovingDirection = Direction.Left;
        _lastDirection = Direction.Left;
        MyPlayer.XPosition -= StepSize;
        _movingCounter++;
    }
    public override void MoveRight()
    {
        if (_timeToMove != 0) return;
        MyPlayer.MovingDirection = Direction.Right;
        _lastDirection = Direction.Right;
        MyPlayer.XPosition += StepSize;
        _movingCounter++;
    }
    public override void Update()
    {
        if (_timeToMove > 0) _timeToMove--;
        if (_movingCounter == _lastMovingCounter) MyPlayer.MovingDirection = Direction.None;
        if (_movingCounter == 10)
        {
            _movingCounter = 0;
            if (MyPlayer.CurrentSprite.Column == _spriteFrameOneColumn) MyPlayer.CurrentSprite.SetFrame(_spriteFrameTwoRow, _spriteFrameTwoColumn);
            else MyPlayer.CurrentSprite.SetFrame(_spriteFrameOneRow, _spriteFrameOneColumn);
        }
        _lastMovingCounter = _movingCounter;
    }

    public override void UseItem(Type itemType, Game1 game)
    {
        if (MyPlayer.Inventory.CanUseItem(itemType) && PlayerProjectilesManager.Instance.TrySpawnProjectile(itemType))
        {
            _timeToMove = 10;
            if (itemType == typeof(IBomb)) MyPlayer.Inventory.Bombs--;
            switch (_lastDirection)
            {
                case Direction.Left:
                    if (itemType == typeof(IBomb)) PlayerProjectilesManager.Instance.SpawnProjectile(new BombLeft(MyPlayer));
                    break;
                case Direction.Right:
                    if (itemType == typeof(IBomb)) PlayerProjectilesManager.Instance.SpawnProjectile(new BombRight(MyPlayer));
                    break;
                case Direction.Up:
                    if (itemType == typeof(IBomb)) PlayerProjectilesManager.Instance.SpawnProjectile(new BombUp(MyPlayer));
                    break;
                case Direction.Down:
                    if (itemType == typeof(IBomb)) PlayerProjectilesManager.Instance.SpawnProjectile(new BombDown(MyPlayer));
                    break;
            }
        }
    }
    public override void UseSword(Game1 game)
    {
        if (PlayerProjectilesManager.Instance.TrySpawnProjectile(typeof(WoodenSwordProjectile)))
        {
            _timeToMove = 10;
            switch (_lastDirection)
            {
                case Direction.Left:
                    PlayerProjectilesManager.Instance.SpawnProjectile(
                new WoodenSwordLeftProjectile(new Vector2(MyPlayer.XPosition, MyPlayer.YPosition)));
                    PlayerProjectilesManager.Instance.SpawnProjectile(
                new WoodenSwordLeftProjectile(new Vector2(MyPlayer.XPosition, MyPlayer.YPosition - Height / 2)));
                    PlayerProjectilesManager.Instance.SpawnProjectile(
                new WoodenSwordLeftProjectile(new Vector2(MyPlayer.XPosition, MyPlayer.YPosition + Height / 2)));

                    break; 
                case Direction.Right:
                    PlayerProjectilesManager.Instance.SpawnProjectile(
                new WoodenSwordRightProjectile(new Vector2(MyPlayer.XPosition, MyPlayer.YPosition)));
                    PlayerProjectilesManager.Instance.SpawnProjectile(
                new WoodenSwordRightProjectile(new Vector2(MyPlayer.XPosition, MyPlayer.YPosition - Height/2)));
                    PlayerProjectilesManager.Instance.SpawnProjectile(
                new WoodenSwordRightProjectile(new Vector2(MyPlayer.XPosition, MyPlayer.YPosition + Height/2)));
                    break;
                case Direction.Up:
                    PlayerProjectilesManager.Instance.SpawnProjectile(
                new WoodenSwordUpProjectile(new Vector2(MyPlayer.XPosition, MyPlayer.YPosition)));
                    PlayerProjectilesManager.Instance.SpawnProjectile(
                new WoodenSwordUpProjectile(new Vector2(MyPlayer.XPosition - Width/2, MyPlayer.YPosition)));
                    PlayerProjectilesManager.Instance.SpawnProjectile(
                new WoodenSwordUpProjectile(new Vector2(MyPlayer.XPosition + Width/2, MyPlayer.YPosition)));
                    break;
                case Direction.Down:
                    PlayerProjectilesManager.Instance.SpawnProjectile(
                new WoodenSwordDownProjectile(new Vector2(MyPlayer.XPosition, MyPlayer.YPosition)));
                    PlayerProjectilesManager.Instance.SpawnProjectile(
                new WoodenSwordDownProjectile(new Vector2(MyPlayer.XPosition - Width / 2, MyPlayer.YPosition)));
                    PlayerProjectilesManager.Instance.SpawnProjectile(
                new WoodenSwordDownProjectile(new Vector2(MyPlayer.XPosition + Width / 2, MyPlayer.YPosition)));
                    break;
            } 
        }
    }
    public override void PickupItem(IItem item)
    {
        _timeToMove = 10;
        item.HoldItem(new Vector2(MyPlayer.Hitbox.Left + Width / 2, MyPlayer.Hitbox.Top + Height / 2));
        if (item is RupeeItem) SoundFactory.Instance.PlayGetRupee();
        else if (item is TriforcePieceItem or HeartContainerItem or BowItem or BoomerangItem)
            SoundFactory.Instance.PlayGetKeyItem();
        else if (item is HeartItem or KeyItem) SoundFactory.Instance.PlayGetHeart();
        else SoundFactory.Instance.PlayGetMinorItem();
    }
}