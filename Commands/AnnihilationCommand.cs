using CSE3902.Interfaces;
using CSE3902.Projectiles;
using CSE3902.Shared.Managers;

namespace CSE3902.Commands;

public class AnnihilationCommand : ICommand
{
    private readonly Game1 _game;

    public AnnihilationCommand(Game1 game)
    {
        _game = game;
    }

    public void Execute()
    {
        if (_game.Paused == 0)
        {
            IBomb bomb1 = new BombDown(_game.Player);
            IBomb bomb2 = new BombLeft(_game.Player);
            IBomb bomb3 = new BombRight(_game.Player);
            IBomb bomb4 = new BombUp(_game.Player);
            PlayerProjectilesManager.Instance.SpawnProjectile(bomb1);
            PlayerProjectilesManager.Instance.SpawnProjectile(bomb2);
            PlayerProjectilesManager.Instance.SpawnProjectile(bomb3);
            PlayerProjectilesManager.Instance.SpawnProjectile(bomb4);
        }
    }
}