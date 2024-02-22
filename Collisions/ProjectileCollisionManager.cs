using CSE3902.Interfaces;
using CSE3902.Shared.Managers;
using System.Collections.Generic;

namespace CSE3902.Collisions;

public class ProjectileCollisionManager : ICollisionManager
{
    private readonly List<IPlayerProjectile> _projectiles = PlayerProjectilesManager.Instance.ActiveProjectiles;

    private readonly List<IEnemyProjectile> _enemyProjectiles =
        GameObjectManagers.EnemyProjectileManager.ActiveGameObjects;

    private readonly List<IBlock> _blocks = GameObjectManagers.BlockManager.ActiveGameObjects;
    private readonly List<IDoor> _doors = GameObjectManagers.DoorManager.ActiveGameObjects;

    public void HandleCollisions()
    {
        foreach (IPlayerProjectile projectile in _projectiles)
        {
            ProjectileCollideWithBlocks.Instance.HandleCollisions(projectile, _blocks);
            if (projectile is IBomb) BombCollideWithDoors.Instance.HandleCollisions(projectile, _doors);
        }

        foreach (IEnemyProjectile projectile in _enemyProjectiles)
        {
            ProjectileCollideWithBlocks.Instance.HandleCollisions(projectile, _blocks);
            //ProjectileCollideWithWalls.Instance.HandleCollisions(projectile, _walls?);
        }
    }
}