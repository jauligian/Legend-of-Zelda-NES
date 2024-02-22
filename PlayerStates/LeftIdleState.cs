using CSE3902.AbstractClasses;
using CSE3902.Interfaces;
using CSE3902.Shared;
using CSE3902.Shared.Managers;
using Microsoft.Xna.Framework;
using System;

namespace CSE3902.States;

public class LeftIdleState : AbstractPlayerState
{
    private int _movingCounter;
    private int _lastMovingCounter;
    private readonly int _spriteFrameOneRow;
    private readonly int _spriteFrameTwoRow;
    private readonly int _spriteFrameOneColumn;
    private readonly int _spriteFrameTwoColumn;

    public LeftIdleState(IPlayer player)
    {
        MyPlayer = player;
        _movingCounter = 0;
        _spriteFrameOneRow = 3;
        _spriteFrameTwoRow = 3;
        _spriteFrameOneColumn = 1;
        _spriteFrameTwoColumn = 2;
        MyPlayer.CurrentSprite = SpritesheetFactory.Instance.CreatePlayerSprite();
        MyPlayer.CurrentSprite.SetFrame(_spriteFrameOneRow, _spriteFrameOneColumn);
    }

    public override void MoveLeft()
    {
        MyPlayer.XPosition -= StepSize;
        _movingCounter++;
    }

    public override void Update()
    {
        if (_movingCounter != _lastMovingCounter)
        {
            MyPlayer.MovingDirection = Direction.Left;
        }
        else
        {
            MyPlayer.MovingDirection = Direction.None;
        }

        if (_movingCounter == 10)
        {
            _movingCounter = 0;
            if (MyPlayer.CurrentSprite.Column == _spriteFrameOneColumn)
            {
                MyPlayer.CurrentSprite.SetFrame(_spriteFrameTwoRow, _spriteFrameTwoColumn);
            }
            else
            {
                MyPlayer.CurrentSprite.SetFrame(_spriteFrameOneRow, _spriteFrameOneColumn);
            }
        }

        _lastMovingCounter = _movingCounter;
    }

    public override void UseItem(Type itemType, Game1 game)
    {
        if (MyPlayer.Inventory.CanUseItem(itemType) && PlayerProjectilesManager.Instance.TrySpawnProjectile(itemType))
        {
            IPlayerState nextState = new LeftItemState(MyPlayer);
            nextState.UseItem(itemType, game);
            MyPlayer.State = nextState;
        }
    }

    public override void UseSword(Game1 game)
    {
        IPlayerState nextState = new LeftSwordState(MyPlayer);
        nextState.UseSword(game);
        MyPlayer.State = nextState;
    }
}