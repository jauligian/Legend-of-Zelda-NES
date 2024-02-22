using CSE3902.Interfaces;

namespace CSE3902.Commands;

public class OpenCharacterSelect : ICommand
{
    private Game1 _game;

    public OpenCharacterSelect(Game1 game)
    {
        _game = game;
    }

    public void Execute()
    {
        if (!_game.GameWin && !_game.GameLose && _game.Paused == 0)
        {
            _game.SelectingCharacter = !_game.SelectingCharacter;
        }
    }
}