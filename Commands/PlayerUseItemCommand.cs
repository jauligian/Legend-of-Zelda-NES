using CSE3902.Interfaces;
using CSE3902.Items;

namespace CSE3902.Commands;

public class PlayerUseItemCommand : ICommand
{
    private readonly Game1 _game;

    public PlayerUseItemCommand(Game1 game)
    {
        _game = game;
    }

    public void Execute()
    {
        if (_game.Paused == 0)
        {
            if (_game.Player.CurrentItemType == typeof(BoomerangItem)) _game.Player.UseItem(typeof(IBoomerang));
            else if (_game.Player.CurrentItemType == typeof(BombItem)) _game.Player.UseItem(typeof(IBomb));
            else if (_game.Player.CurrentItemType == typeof(ArrowItem)) _game.Player.UseItem(typeof(IArrow));
        }
    }
}