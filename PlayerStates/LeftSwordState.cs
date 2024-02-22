using CSE3902.AbstractClasses;
using CSE3902.Interfaces;
using CSE3902.Items;
using CSE3902.Players;
using CSE3902.Projectiles;
using CSE3902.Shared.Managers;
using Microsoft.Xna.Framework;
using System;

namespace CSE3902.States;

public class LeftSwordState : AbstractPlayerState
{
    private int _timeToMove;
    private readonly int _spriteColumn;

    public LeftSwordState(IPlayer player)
    {
        MyPlayer = player;
        _spriteColumn = 3;
        MyPlayer.CurrentSprite.SetFrame(MyPlayer.CurrentSprite.Row, _spriteColumn);
        _timeToMove = 10;
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
        MyPlayer.Sword.Update();
        if (_timeToMove == 0)
        {
            //Logic to change where sword is drawn here?
            MyPlayer.State = new LeftIdleState(MyPlayer);
        }
    }

    public override void Draw()
    {
        if (MyPlayer is not DamagedPlayer) MyPlayer.Sword.Draw();
        base.Draw();
    }

    public override void UseItem(Type itemType, Game1 game)
    {
    } //do nothing

    public override void UseSword(Game1 game)
    {
        MyPlayer.Sword = new WoodenSwordLeft(MyPlayer);
        if (MyPlayer.PlayerHealth == MyPlayer.MaxPlayerHealth &&
            PlayerProjectilesManager.Instance.TrySpawnProjectile(typeof(WoodenSwordProjectile)))
        {
            PlayerProjectilesManager.Instance.SpawnProjectile(
                new WoodenSwordLeftProjectile(new Vector2(MyPlayer.XPosition, MyPlayer.YPosition)));
        }
    }
}