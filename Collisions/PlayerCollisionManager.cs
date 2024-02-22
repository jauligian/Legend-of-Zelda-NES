using CSE3902.Interfaces;
using CSE3902.LevelLoading;
using CSE3902.Shared.Managers;
using System.Collections.Generic;

namespace CSE3902.Collisions;

public class PlayerCollisionManager : ICollisionManager
{
    private readonly Game1 _game;
    private readonly Dungeon _dungeon;

    private readonly List<IEnemyProjectile> _enemyProjectiles =
        GameObjectManagers.EnemyProjectileManager.ActiveGameObjects;

    private readonly List<IBlock> _blocks = GameObjectManagers.BlockManager.ActiveGameObjects;
    private readonly List<IEnemy> _enemies = GameObjectManagers.EnemyManager.ActiveGameObjects;
    private readonly List<IItem> _items = GameObjectManagers.ItemManager.ActiveGameObjects;
    private readonly List<IDoor> _doors = GameObjectManagers.DoorManager.ActiveGameObjects;

    public PlayerCollisionManager(Game1 game, Dungeon dungeon)
    {
        _game = game;
        _dungeon = dungeon;
    }

    public void HandleCollisions()
    {
        SwordCollideWithEnemies.Instance.HandleCollisions(_enemies, _game.Player.Sword);
        PlayerCollideWithEnemies.Instance.HandleCollisions(_game.Player, _enemies);
        PlayerCollideWithProjectiles.Instance.HandleCollisions(_game.Player, _enemyProjectiles);
        PlayerCollideWithBlocks.Instance.HandleCollisions(_game.Player, _blocks, _dungeon);
        PlayerCollideWithDoors.Instance.HandleCollisions(_game.Player, _doors, _dungeon);
        PlayerCollideWithItems.Instance.HandleCollisions(_game.Player, _items);
    }
}