using CSE3902.Interfaces;

namespace CSE3902.Commands;

public class PauseGameCommand : ICommand
{
    private Game1 _game;

    public PauseGameCommand(Game1 game)
    {
        _game = game;
    }

    public void Execute()
    {
        if (!_game.GameWin && !_game.SelectingCharacter && !_game.GameLose)
        {
            _game.Paused = (_game.Paused + 1) % 2;
            _game.PauseHud.UpdateObtainedItems();
            _game.PauseHud.HighlightedItem = _game.PauseHud.GetCurrentItemPos();
        }
    }
}