using CSE3902.Enemies;
using CSE3902.Environment;
using CSE3902.Environment.Doors;
using CSE3902.Interfaces;
using CSE3902.Items;
using CSE3902.LevelLoading;
using CSE3902.Shared.Managers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace CSE3902.Shared;

public readonly struct ObjectLoadingInformation
{
    public ObjectLoadingInformation(string objType, (int X, int Y) pos)
    {
        ObjectType = objType;
        Position = pos;
    }

    public readonly string ObjectType;
    public readonly (int X, int Y) Position;
}

public readonly struct ObjectsLoadingInformationPlusMetaData
{
    public ObjectsLoadingInformationPlusMetaData(List<ObjectLoadingInformation> objLoadingInfo, int width, int height)
    {
        ObjectLoadingInformationList = objLoadingInfo;
        Width = width;
        Height = height;
    }

    public readonly List<ObjectLoadingInformation> ObjectLoadingInformationList;
    public readonly int Width;
    public readonly int Height;
}

public readonly struct RoomStateInformation
{
    public RoomStateInformation(GameObjectManager<IBlock> blockManager, GameObjectManager<IDoor> doorManager,
        GameObjectManager<IEnemy> enemyManager,
        GameObjectManager<IItem> itemManager, int roomWidthInBlocks, int roomHeightInBlocks)
    {
        BlockManager = blockManager;
        DoorManager = doorManager;
        EnemyManager = enemyManager;
        ItemManager = itemManager;
        EnemyProjectileManager = new GameObjectManager<IEnemyProjectile>();
        RoomWidthInBlocks = roomWidthInBlocks;
        RoomHeightInBlocks = roomHeightInBlocks;
    }

    public RoomStateInformation(GameObjectManager<IBlock> blockManager, GameObjectManager<IDoor> doorManager,
        GameObjectManager<IEnemy> enemyManager,
        GameObjectManager<IItem> itemManager, GameObjectManager<IEnemyProjectile> enemyProjectileManager,
        int roomWidthInBlocks,
        int roomHeightInBlocks)
    {
        BlockManager = blockManager;
        DoorManager = doorManager;
        EnemyManager = enemyManager;
        ItemManager = itemManager;
        EnemyProjectileManager = enemyProjectileManager;
        RoomWidthInBlocks = roomWidthInBlocks;
        RoomHeightInBlocks = roomHeightInBlocks;
    }

    public readonly GameObjectManager<IBlock> BlockManager;
    public readonly GameObjectManager<IEnemy> EnemyManager;
    public readonly GameObjectManager<IItem> ItemManager;
    public readonly GameObjectManager<IEnemyProjectile> EnemyProjectileManager;
    public readonly GameObjectManager<IDoor> DoorManager;
    public readonly int RoomWidthInBlocks;
    public readonly int RoomHeightInBlocks;
    public readonly ITextureAtlasSprite DefaultBackground = SpritesheetFactory.Instance.CreateRoomBackgroundSprite();
}

public readonly struct DungeonStateInformation
{
    public DungeonStateInformation(Room[][] roomMatrix, Dictionary<string, Room> roomDictionary,
        Room activeRoom)
    {
        RoomMatrix = roomMatrix;
        RoomDictionary = roomDictionary;
        ActiveRoom = activeRoom;
    }


    /*
     * TODO: Refine exactly what data types I want to be a part of DungeonStateInformation.
     */

    public readonly Room[][] RoomMatrix;
    public readonly Dictionary<string, Room> RoomDictionary;
    public readonly Room ActiveRoom;
}

public static class SharedUtilDefinitions
{
    public static readonly Dictionary<string, Func<Vector2, IGameObject>> GameObjectCreationDictionary = new()
    {
        //Beginning of IBlock
        { nameof(BasicBlueBlock), (location) => new BasicBlueBlock(location) },
        { nameof(BlackBlock), (location) => new BlackBlock(location) },
        { nameof(BlackBlockC), (location) => new BlackBlockC(location) },
        { nameof(BlueStairsBlock), (location) => new BlueStairsBlock(location) },
        { nameof(BombableWallDoorBlock), (location) => new BombableWallDoorBlock(location) },
        { nameof(DiamondDoorBlock), (location) => new DiamondDoorBlock(location) },
        { nameof(InvisibleBlock), (location) => new InvisibleBlock(location) },
        { nameof(JewelCutBlueBlock), (location) => new JewelCutBlueBlock(location) },
        { nameof(LeftFacingStatueBlock), (location) => new LeftFacingStatueBlock(location) },
        { nameof(LockedDoorBlock), (location) => new LockedDoorBlock(location) },
        { nameof(OpenDoorBlock), (location) => new OpenDoorBlock(location) },
        { nameof(PushableBlock), (location) => new PushableBlock(location) },
        { nameof(RightFacingStatueBlock), (location) => new RightFacingStatueBlock(location) },
        { nameof(SandyBlueBlock), (location) => new SandyBlueBlock(location) },
        { nameof(WaterBlock), (location) => new WaterBlock(location) },
        { nameof(WhiteBrickBlock), (location) => new WhiteBrickBlock(location) },
        { nameof(WhiteStairsBlock), (location) => new WhiteStairsBlock(location) },
        { nameof(WhiteWalkingStairsBlock), (location) => new WhiteWalkingStairsBlock(location) },
        { nameof(FireBlock), (location) => new FireBlock(location) },
        { nameof(OldManBlock), (location) => new OldManBlock(location) },
        //End of IBlock

        //Beginning of IItem
        { nameof(ArrowItem), (location) => new ArrowItem(location) },
        { nameof(BombItem), (location) => new BombItem(location) },
        { nameof(BoomerangItem), (location) => new BoomerangItem(location) },
        { nameof(BowItem), (location) => new BowItem(location) },
        { nameof(ClockItem), (location) => new ClockItem(location) },
        { nameof(CompassItem), (location) => new CompassItem(location) },
        { nameof(EmptyItem), (location) => new EmptyItem(location) },
        { nameof(FairyItem), (location) => new FairyItem(location) },
        { nameof(HeartContainerItem), (location) => new HeartContainerItem(location) },
        { nameof(HeartItem), (location) => new HeartItem(location) },
        { nameof(KeyItem), (location) => new KeyItem(location) },
        { nameof(KeyRewardItem), (location) => new KeyRewardItem(location) },
        { nameof(MapItem), (location) => new MapItem(location) },
        { nameof(RupeeItem), (location) => new RupeeItem(location) },
        { nameof(BlueRupeeItem), (location) => new BlueRupeeItem(location) },
        { nameof(TriforcePieceItem), (location) => new TriforcePieceItem(location) },
        { nameof(BlueBoomerangItem), (location) => new BlueBoomerangItem(location) },
        { nameof(HeartContainerRewardItem), (location) => new HeartContainerRewardItem(location) },
        //End of IItem
        //Beginning of IEnemy
        { nameof(AquamentusEnemy), (location) => new AquamentusEnemy(location) },
        { nameof(BladeTrapEnemy), (location) => new BladeTrapEnemy(location) },
        { nameof(GelEnemy), (location) => new GelEnemy(location) },
        { nameof(GoriyaEnemy), (location) => new GoriyaEnemy(location) },
        { nameof(KeeseEnemy), (location) => new KeeseEnemy(location) },
        { nameof(StalfosEnemy), (location) => new StalfosEnemy(location) },
        { nameof(WallmasterEnemy), (location) => new WallmasterEnemy(location) },
        { nameof(RopeEnemy), (location) => new RopeEnemy(location) },
        { nameof(DodongoEnemy), (location) => new DodongoEnemy(location) }
        //End of IEnemy
    };
}