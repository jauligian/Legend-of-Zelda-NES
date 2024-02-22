using CSE3902.Interfaces;

namespace CSE3902.Commands;

public class QuitCommand : ICommand
{
    private readonly Game1 _game;

    public QuitCommand(Game1 game)
    {
        _game = game;
    }

    public void Execute()
    {
        _game.Exit();
    }
}