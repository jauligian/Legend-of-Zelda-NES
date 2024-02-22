﻿using CSE3902.Interfaces;

namespace CSE3902.Commands;

public class NaviMoveDownCommand : ICommand
{
    private readonly Game1 _game;

    public NaviMoveDownCommand(Game1 game)
    {
        _game = game;
    }

    public void Execute()
    {
        if (_game.Paused == 0) _game.NaviEntity.MoveDown();
    }
}