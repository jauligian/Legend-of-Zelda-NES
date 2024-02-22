using CSE3902.Interfaces;
using CSE3902.Projectiles;

namespace CSE3902.Commands;

public class PlayerUseHadokenCommand : ICommand
{
    private readonly Game1 _game;

    public PlayerUseHadokenCommand(Game1 game)
    {
        _game = game;
    }

    public void Execute()
    {
        if (_game.Paused == 0) _game.Player.UseItem(typeof(Hadoken));
    }
}