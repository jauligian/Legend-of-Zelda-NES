using CSE3902.Collisions;
using CSE3902.Environment;
using CSE3902.Environment.Doors;
using CSE3902.Interfaces;
using CSE3902.Shared;
using CSE3902.Shared.Managers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSE3902.LevelLoading;

public class Dungeon
{
    private readonly DungeonStateInformation _dungeonState;

    private readonly List<Room> _rooms; //TEMPORARY VARIABLE TO SUPPORT EASY ROOM CYCLING.
    private int _roomIndex = 0; //TEMPORARY VARIABLE TO SUPPORT EASY ROOM CYCLING.
    private int _bladeTrapRoomCounter = 0;
    public bool TransitioningRooms { get; private set; } = false;
    private Vector2 _transitioningRoomsCounter = Vector2.Zero;
    private string _newRoomId = string.Empty;

    //TODO Should this  be public or private???
    public string ActiveRoomId { get; private set; }

    //TODO this should be refactored out, the whole point of dungeon is to abstract away from this matrix, maybe dungeon should have a Map?
    public (int roomRowIndex, int roomColumnIndex) ActiveRoomRelativePosition { get; private set; }

    public int DungeonColumns => _dungeonState.RoomMatrix[0].Length;

    public int DungeonRows => _dungeonState.RoomMatrix.Length;

    public Vector2 CurrentRoomPosition { get; private set; }

    public Dungeon(string filepath)
    {
        _dungeonState = DungeonObjectLoader.GenerateRooms(filepath);
        InitializeDungeonInformation();

        //TEMPORARY CODE TO SUPPORT ROOM CYCLING.
        _rooms = _dungeonState.RoomDictionary.Select(pair => pair.Value).ToList();

        //Set the first room active.
        ActiveRoomId = _rooms[0].RoomId;
        _rooms[0].SetActive();
    }

    public void Update()
    {
        if (!ActiveRoomId.Equals("zelda rooms - B1.csv"))
        {
            GameObjectManagers.UpdateAllGameObjects(this);
        }
        else if (_bladeTrapRoomCounter > 100)
        {
            GameObjectManagers.UpdateAllGameObjects(this);
        }
        else
        {
            _bladeTrapRoomCounter++;
        }
        CheckValidDoors(this);
    }

    public void Draw()
    {
        if (TransitioningRooms)
        {
            Room currentRoom = _dungeonState.RoomDictionary[ActiveRoomId];
            Room newRoom = _dungeonState.RoomDictionary[_newRoomId];


            //noting that draw order is important here, background needs drawn first.
            currentRoom.RoomState.DefaultBackground.Draw(currentRoom.GlobalRoomPosition);
            currentRoom.RoomState.BlockManager.Draw();
            currentRoom.RoomState.DoorManager.Draw();

            newRoom.RoomState.DefaultBackground.Draw(newRoom.GlobalRoomPosition);
            newRoom.RoomState.BlockManager.Draw();
            newRoom.RoomState.DoorManager.Draw();
        }
        else
        {
            Room current = _dungeonState.RoomDictionary[ActiveRoomId];
            current.RoomState.DefaultBackground.Draw(current.GlobalRoomPosition);
            GameObjectManagers.DrawAllGameObjects();
        }
    }

    private void InitializeDungeonInformation()
    {
        const int roomWidth = 16 * Globals.BlockSize;
        const int roomHeight = 11 * Globals.BlockSize;

        for (int rowIndex = 0; rowIndex < _dungeonState.RoomMatrix.Length; rowIndex++)
        {
            for (int colIndex = 0; colIndex < _dungeonState.RoomMatrix[rowIndex].Length; colIndex++)
            {
                Room currentRoom = _dungeonState.RoomMatrix[rowIndex][colIndex];

                if (currentRoom == null)
                {
                    continue;
                }

                /*
                 * NOTE THE ORDER OF THESE TWO CALLS ARE IMPORTANT.
                 */
                SetDoorAdjacencyInformation(currentRoom.RoomState.DoorManager,
                    rowIndex, colIndex);

                currentRoom.ScaleGameObjectPositions(colIndex * roomWidth,
                    rowIndex * roomHeight);
            }
        }
    }

    private void SetDoorAdjacencyInformation(GameObjectManager<IDoor> doorManager, int row, int col)
    {
        foreach (IDoor door in doorManager.ActiveGameObjects)
        {
            door.InitializeFacingDirection(_dungeonState.RoomMatrix[row][col]);

            switch (door.FacingDirection)
            {
                case Direction.Down:
                    if (row > 0 && _dungeonState.RoomMatrix[row - 1][col] != null)
                    {
                        door.InitializeAdjacencyInformation(_dungeonState.RoomMatrix[row - 1][col].RoomId);
                    }

                    break;
                case Direction.Up:
                    if (row < _dungeonState.RoomMatrix.Length - 1 && _dungeonState.RoomMatrix[row + 1][col] != null)
                    {
                        door.InitializeAdjacencyInformation(_dungeonState.RoomMatrix[row + 1][col].RoomId);
                    }

                    break;
                case Direction.Right:
                    if (row < 0 || row > _dungeonState.RoomMatrix.Length - 1)
                    {
                        throw new Exception("Invalid Row Number in Dungeon matrix");
                    }

                    if (col > 0 && _dungeonState.RoomMatrix[row][col - 1] != null)
                    {
                        door.InitializeAdjacencyInformation(_dungeonState.RoomMatrix[row][col - 1].RoomId);
                    }

                    break;
                case Direction.Left:
                    if (row < 0 || row > _dungeonState.RoomMatrix.Length - 1)
                    {
                        throw new Exception("Invalid Row Number in Dungeon matrix");
                    }

                    if (col < _dungeonState.RoomMatrix[row].Length - 1 &&
                        _dungeonState.RoomMatrix[row][col + 1] != null)
                    {
                        door.InitializeAdjacencyInformation(_dungeonState.RoomMatrix[row][col + 1].RoomId);
                    }

                    break;
                default:
                    throw new Exception($"Direction: {door.FacingDirection} is not a valid door direction.");
            }

            if (door.AdjacentRoomId == null &&
                !_dungeonState.RoomMatrix[row][col].RoomId.Equals("zelda rooms - C6.csv"))
            {
                //throw new Exception(
                //  $"Door.AdjacentRoomId is not allowed to be null after dungeon initialization.\nRoom Containing Door: {_dungeonState.RoomMatrix[row][col].RoomId} Row: {row} Column: {col}");
            }
        }
    }

    public Matrix CurrentViewportTransition(IPlayer player)
    {
        Room currentRoom = _dungeonState.RoomDictionary[ActiveRoomId];
        if (TransitioningRooms)
        {
            Room newRoom = _dungeonState.RoomDictionary[_newRoomId];
            Vector2 orgRoomPosition = currentRoom.GlobalRoomPosition;
            Vector2 newRoomPosition = newRoom.GlobalRoomPosition;
            Vector2 differenceInPosition = new(orgRoomPosition.X - newRoomPosition.X + _transitioningRoomsCounter.X,
                orgRoomPosition.Y - newRoomPosition.Y + _transitioningRoomsCounter.Y);

            if (differenceInPosition is { X: 0, Y: 0 })
            {
                TransitionToNewRoomFinished();
                PlayerCollideWithDoors.Instance.BombAdjacentDoor(player);
            }

            if (differenceInPosition.X > 0)
            {
                //TODO: Fix this size.
                _transitioningRoomsCounter.X -= Globals.GlobalSizeMult;
            }
            else if (differenceInPosition.X < 0)
            {
                _transitioningRoomsCounter.X += Globals.GlobalSizeMult;
            }

            if (differenceInPosition.Y > 0)
            {
                _transitioningRoomsCounter.Y -= Globals.GlobalSizeMult;
            }
            else if (differenceInPosition.Y < 0)
            {
                _transitioningRoomsCounter.Y += Globals.GlobalSizeMult;
            }
        }

        return Matrix.CreateTranslation(-(currentRoom.GlobalRoomPosition.X + _transitioningRoomsCounter.X),
            -(currentRoom.GlobalRoomPosition.Y + _transitioningRoomsCounter.Y) + Globals.HudOffset, 0);
    }

    public void StartTransitionToNewRoom(string nextRoomId)
    {
        if (!TransitioningRooms)
        {
            GameObjectManagers.RemoveAllGameObjects();
            TransitioningRooms = true;
            _newRoomId = nextRoomId;
        }
    }

    private void TransitionToNewRoomFinished()
    {
        if (ActiveRoomId.Equals("zelda rooms - B1.csv"))
            _bladeTrapRoomCounter = 0;
        TransitioningRooms = false;
        ActiveRoomId = _newRoomId;
        _dungeonState.RoomDictionary[ActiveRoomId].SetActive();
        CurrentRoomPosition = new Vector2(_dungeonState.RoomDictionary[ActiveRoomId].GlobalRoomPosition.X,
            _dungeonState.RoomDictionary[ActiveRoomId].GlobalRoomPosition.Y);
        _transitioningRoomsCounter = Vector2.Zero;
        for (int rowIndex = 0; rowIndex < _dungeonState.RoomMatrix.Length; rowIndex++)
        {
            for (int colIndex = 0; colIndex < _dungeonState.RoomMatrix[rowIndex].Length; colIndex++)
            {
                Room currentRoom = _dungeonState.RoomMatrix[rowIndex][colIndex];

                if (currentRoom != null && currentRoom.RoomId.Equals(ActiveRoomId))
                {
                    ActiveRoomRelativePosition = (rowIndex, colIndex);
                }
            }
        }
    }

    //TEMPORARY METHODS FOR CYCLING ROOMS.
    public void CycleToNextRoom()
    {
        _roomIndex = _roomIndex == _rooms.Count - 1 ? 0 : _roomIndex + 1;
        ActiveRoomId = _rooms[_roomIndex].RoomId;
        CurrentRoomPosition =
            new Vector2(_rooms[_roomIndex].GlobalRoomPosition.X, _rooms[_roomIndex].GlobalRoomPosition.Y);
        _rooms[_roomIndex].SetActive();
    }

    public void CycleToPreviousRoom()
    {
        _roomIndex = _roomIndex == 0 ? _rooms.Count - 1 : _roomIndex - 1;
        ActiveRoomId = _rooms[_roomIndex].RoomId;
        CurrentRoomPosition =
            new Vector2(_rooms[_roomIndex].GlobalRoomPosition.X, _rooms[_roomIndex].GlobalRoomPosition.Y);
        _rooms[_roomIndex].SetActive();
    }

    public void StartActiveRoom(int dungeonIndex)
    {
        if (dungeonIndex == 0) _roomIndex = 17;
        else if (dungeonIndex == 1) _roomIndex = 16;

        ActiveRoomId = _rooms[_roomIndex].RoomId;
        CurrentRoomPosition =
            new Vector2(_rooms[_roomIndex].GlobalRoomPosition.X, _rooms[_roomIndex].GlobalRoomPosition.Y);
        _rooms[_roomIndex].SetActive();
    }

    public void SpawnOpenDoor(IDoor oldDoor)
    {
        oldDoor.Active = false;
        OpenDoorBlock newDoor = new(oldDoor, oldDoor.Position);
        _dungeonState.RoomDictionary[ActiveRoomId].RoomState.DoorManager
            .Spawn(newDoor);
        GameObjectManagers.DoorManager.Spawn(newDoor);
    }

    public void ChangeCrawlSpace(IPlayer player)
    {
        if (_roomIndex == 1) _roomIndex = 3;
        else _roomIndex = 1;
        ActiveRoomId = _rooms[_roomIndex].RoomId;
        CurrentRoomPosition =
            new Vector2(_rooms[_roomIndex].GlobalRoomPosition.X, _rooms[_roomIndex].GlobalRoomPosition.Y);
        _rooms[_roomIndex].SetActive();

        player.XPosition = (int)(CurrentRoomPosition.X + 3.2 * Globals.BlockSize);
        player.YPosition = (int)CurrentRoomPosition.Y + 4 * Globals.BlockSize;
    }

    private static void CheckValidDoors(Dungeon dungeon)
    {
        if (GameObjectManagers.EnemyManager.ActiveGameObjects.Count == 0)
        {
            IDoor doorToReplace = null;
            foreach (IDoor door in GameObjectManagers.DoorManager.ActiveGameObjects)
            {
                if (door is DiamondDoorBlock)
                {
                    doorToReplace = door;
                    door.Active = false;
                    SoundFactory.Instance.PlayUnlockDoor();
                }
            }
            if (doorToReplace != null) dungeon.SpawnOpenDoor(doorToReplace);
        }
    }
}