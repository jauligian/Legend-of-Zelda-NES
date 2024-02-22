using CSE3902.Shared;
using CSE3902.Shared.Managers;
using Microsoft.Xna.Framework;
using System;

namespace CSE3902.LevelLoading;

public class Room
{
    public bool Active { get; set; } //noting this isn't currently used

    public RoomStateInformation RoomState { get; }

    public readonly string RoomId;
    public Vector2 GlobalRoomPosition;

    public Room(string file)
    {
        Active = false;
        RoomState = RoomObjectLoader.GenerateObjects(file);
        RoomId = file.Substring(file.IndexOf("/", StringComparison.Ordinal) + 1);
    }

    public void SetActive()
    {
        Active = true;
        GameObjectManagers.SetNewRoomState(RoomState);
    }

    public void ScaleGameObjectPositions(int horizontalOffset, int verticalOffset)
    {
        /*
         * TODO: Noting that rooms should have a List of GameObjectManagers but C# was being annoying and not letting me
         * do what I wanted :(
         * This would also be applicable to fixing the GameObjectManagers Static class to look better.
         */
        GlobalRoomPosition = new Vector2(horizontalOffset, verticalOffset);
        RoomState.EnemyManager.ScaleGameObjectPositions(horizontalOffset, verticalOffset);
        RoomState.EnemyProjectileManager.ScaleGameObjectPositions(horizontalOffset, verticalOffset);
        RoomState.DoorManager.ScaleGameObjectPositions(horizontalOffset, verticalOffset);
        RoomState.BlockManager.ScaleGameObjectPositions(horizontalOffset, verticalOffset);
        RoomState.ItemManager.ScaleGameObjectPositions(horizontalOffset, verticalOffset);
    }
}