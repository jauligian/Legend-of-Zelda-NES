using CSE3902.AbstractClasses;
using CSE3902.Interfaces;
using CSE3902.Shared;
using Microsoft.Xna.Framework;
using System;

namespace CSE3902.States;

public class LinkDyingState : AbstractPlayerState
{
    private int _timeToFinish;
    private readonly int _spriteColumn;
    private readonly Game1 _game;

    public LinkDyingState(IPlayer player, Game1 game)
    {
        MyPlayer = player;
        _timeToFinish = 60;
        _spriteColumn = 1;
        _game = game;
        MyPlayer.CurrentSprite = SpritesheetFactory.Instance.CreatePlayerSprite();
        MyPlayer.CurrentSprite.SetFrame(1, _spriteColumn);
        SoundFactory.Instance.PlayLinkDie();
    }

    public override void MoveDown()
    {
    }

    public override void MoveUp()
    {
    }

    public override void MoveLeft()
    {
    }

    public override void MoveRight()
    {
    }

    public override void Update()
    {
        if (_timeToFinish > 0)
        {
            _timeToFinish--;
            SelectSprite();
            SoundFactory.Instance.StopBackgroundMusic();
            SoundFactory.Instance.StopBoomerangFly();
            SoundFactory.Instance.StopLowHealth();
            SoundFactory.Instance.StopTextWriting();
        }
        else
        {
            _game.GameLose = true;
        }
    }

    public override void UseItem(Type itemType, Game1 game)
    {
    }

    public override void UseSword(Game1 game)
    {
        //Do nothing?
    }

    private void SelectSprite()
    {
        if (_timeToFinish % 2 == 0)
        {
            MyPlayer.CurrentSprite.SetFrame(_timeToFinish / 2 % 4 + 1, _spriteColumn);
        }
    }
}