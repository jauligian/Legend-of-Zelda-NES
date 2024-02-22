using CSE3902.Interfaces;
using CSE3902.LevelLoading;
using CSE3902.Players;
using CSE3902.Shared.Managers;
using System.Collections.Generic;

namespace CSE3902.Collisions;

public class NaviCollisionManager : ICollisionManager
{
    private readonly Navi _naviEntity;
    private readonly List<IBlock> _blocks = GameObjectManagers.BlockManager.ActiveGameObjects;

    public NaviCollisionManager(Navi n)
    {
        _naviEntity = n;
    }

    public void HandleCollisions()
    {
        NaviCollideWithBlocks.Instance.HandleCollisions(_naviEntity, _blocks);
    }
}