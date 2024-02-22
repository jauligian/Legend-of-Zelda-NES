using CSE3902.Interfaces;
using CSE3902.LevelLoading;
using CSE3902.Shared;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace CSE3902.Commands;

public class NextRoomCommand : ICommand
{
    private readonly Game1 _game;
    private List<Room> _rooms = new();

    public NextRoomCommand(Game1 game)
    {
        _game = game;
    }

    public void Execute()
    {
        _game.Dungeons[_game.CurrentDungeon].CycleToNextRoom();
        //Placeholder; moving-between-rooms logic will be completely different in future sprints.
        _game.Player.XPosition = (int)_game.Dungeons[_game.CurrentDungeon].CurrentRoomPosition.X + 116 * Globals.GlobalSizeMult;
        _game.Player.YPosition =
            (int)_game.Dungeons[_game.CurrentDungeon].CurrentRoomPosition.Y + 130 * Globals.GlobalSizeMult;
        if (_game.Dungeons[_game.CurrentDungeon].ActiveRoomId.Equals("zelda rooms - A2.csv"))
        {
            _game.Player.YPosition -= Globals.BlockSize;
        }
        _game.NaviEntity.Position = new Vector2(_game.Player.XPosition, _game.Player.YPosition);
        _game.NaviEntity.UpdateHitbox();
        _game.Player.UpdateHitbox();
    }
}