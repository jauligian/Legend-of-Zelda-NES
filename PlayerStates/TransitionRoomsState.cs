using CSE3902.AbstractClasses;
using CSE3902.Interfaces;
using CSE3902.Shared;
using CSE3902.Players;
using CSE3902.Shared.Managers;
using Microsoft.Xna.Framework;
using System;

namespace CSE3902.States;

public class TransitionRoomsState : AbstractPlayerState
{
    private int _timeToMove;
    private const int Height = 0;
    private const int Width = 0;

    public TransitionRoomsState(IPlayer player, Direction direction)
    {
        MyPlayer = player;
        ChangePosition(direction);
    }

    public override void MoveDown() { }
    public override void MoveLeft() { }
    public override void MoveRight() { }
    public override void MoveUp() { }
    public override void Update()
    {
        _timeToMove--;
        if (_timeToMove == 0)
        {
            MyPlayer.State = new UpIdleState(MyPlayer);
            if (MyPlayer.GetType() == typeof(GoriyaPlayer)) MyPlayer.State = new GoriyaMainState(MyPlayer);
            if (MyPlayer.GetType() == typeof(StalfosPlayer)) MyPlayer.State = new StalfosMainState(MyPlayer);
        }
    }
    public override void Draw()
    {
        base.Draw();
    }
    public override void UseItem(Type itemType, Game1 game) { }
    public override void UseSword(Game1 game) { }
    public override void PickupItem(IItem item) { }
    private void ChangePosition(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                MyPlayer.YPosition -= (int)(4.5 * Globals.BlockSize);
                _timeToMove = 200;
                break;
            case Direction.Down:
                MyPlayer.YPosition += (int)(4.5 * Globals.BlockSize);
                _timeToMove = 200;
                break;
            case Direction.Left:
                MyPlayer.XPosition -= (int)(4.5 * Globals.BlockSize);
                _timeToMove = 300;
                break;
            case Direction.Right:
                MyPlayer.XPosition += (int)(4.5 * Globals.BlockSize);
                _timeToMove = 300;
                break;
        }
    }
}