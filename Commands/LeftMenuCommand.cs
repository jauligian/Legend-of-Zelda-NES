using CSE3902.Interfaces;

namespace CSE3902.Commands;

public class LeftMenuCommand : ICommand
{
    private readonly Game1 _game;

    public LeftMenuCommand(Game1 game)
    {
        _game = game;
    }

    public void Execute()
    {
        if (_game.SelectingCharacter) _game.CharacterSelect.NextOrPrevCharacter(-1);
        else if (_game.Paused == 1) _game.PauseHud.FindNextOrPrevItem(-1);
    }
}