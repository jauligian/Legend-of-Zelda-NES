using CSE3902.Interfaces;
using CSE3902.Shared.Managers;
using System.Collections.Generic;

namespace CSE3902.Collisions;

public class TripwireCollisionManager : ICollisionManager
{
    private readonly List<IEnemy> _enemies = GameObjectManagers.EnemyManager.ActiveGameObjects;
    private readonly Game1 _game;
    public TripwireCollisionManager(Game1 game)
    {
        _game = game;
    }

    public void HandleCollisions()
    {
        PlayerCollideWithTripwire.Instance.HandleCollisions(_game.Player, _enemies);
    }
}