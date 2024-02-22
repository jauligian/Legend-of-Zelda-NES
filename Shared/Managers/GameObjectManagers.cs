using CSE3902.Enemies;
using CSE3902.Environment;
using CSE3902.Environment.Doors;
using CSE3902.Interfaces;
using CSE3902.LevelLoading;
using System;

namespace CSE3902.Shared.Managers;

public static class GameObjectManagers
{
    /*
     * TODO: Do I want to replace all of these with a room state information struct? I don't think so because it would add another layer of dots.
     */
    public static GameObjectManager<IEnemy> EnemyManager { get; } = new();
    public static GameObjectManager<IEnemyProjectile> EnemyProjectileManager { get; } = new();
    public static GameObjectManager<IBlock> BlockManager { get; } = new();
    public static GameObjectManager<IDoor> DoorManager { get; } = new();
    public static GameObjectManager<IItem> ItemManager { get; } = new();
    public static GameObjectManager<DeadObject> DeathManager { get; } = new();

    public static PlayerProjectilesManager PlayerProjectilesManager => PlayerProjectilesManager.Instance;

    public static void RemoveAllGameObjects()
    {
        //This could be improved by having all of these belong to a private List of game object managers.
        EnemyManager.RemoveAll();
        EnemyProjectileManager.RemoveAll();
        BlockManager.RemoveAll();
        DoorManager.RemoveAll();
        ItemManager.RemoveAll();
        DeathManager.RemoveAll();
        PlayerProjectilesManager.RemoveAll();
    }

    public static void UpdateAllGameObjects(Dungeon dungeon)
    {
        BlockManager.Update();
        DoorManager.Update();
        EnemyProjectileManager.Update();
        PlayerProjectilesManager.UpdateProjectiles();
        EnemyManager.Update();
        ItemManager.Update();
        DeathManager.Update();
    }

    public static void DrawAllGameObjects()
    {
        BlockManager.Draw();
        DoorManager.Draw();
        EnemyProjectileManager.Draw();
        PlayerProjectilesManager.DrawProjectiles();
        EnemyManager.Draw();
        ItemManager.Draw();
        DeathManager.Draw();
    }

    public static void SetNewRoomState(RoomStateInformation roomStateInformation)
    {
        RemoveAllGameObjects();
        EnemyManager.Spawn(roomStateInformation.EnemyManager.ActiveGameObjects);
        BlockManager.Spawn(roomStateInformation.BlockManager.ActiveGameObjects);
        DoorManager.Spawn(roomStateInformation.DoorManager.ActiveGameObjects);
        ItemManager.Spawn(roomStateInformation.ItemManager.ActiveGameObjects);
        EnemyProjectileManager.Spawn(roomStateInformation.EnemyProjectileManager.ActiveGameObjects);
    }

    
}