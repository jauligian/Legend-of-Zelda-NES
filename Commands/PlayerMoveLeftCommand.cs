﻿using CSE3902.Interfaces;

namespace CSE3902.Commands;

public class PlayerMoveLeftCommand : ICommand
{
    private readonly Game1 _game;

    public PlayerMoveLeftCommand(Game1 game)
    {
        _game = game;
    }

    public void Execute()
    {
        if (_game.Paused == 0 && !_game.SelectingCharacter) _game.Player.MoveLeft();
    }
}