using CSE3902.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CSE3902.LevelLoading;

public static class DungeonObjectLoader
{
    private readonly struct RoomLoadingInformation
    {
        public RoomLoadingInformation(string file, ObjectLoadingInformation objInfo)
        {
            FileName = file;
            ObjectInformation = objInfo;
        }

        public readonly string FileName;
        public readonly ObjectLoadingInformation ObjectInformation;
    }

    private static List<RoomLoadingInformation> UpdateObjectLoadingInformationList(List<ObjectLoadingInformation> list)
    {
        List<RoomLoadingInformation> roomLoadingInfo = new();
        foreach (ObjectLoadingInformation info in list)
        {
            if (info.ObjectType.Contains("[EMPTY]")) continue;
            string fileName = "";
            string objectType = info.ObjectType;
            if (!info.ObjectType.StartsWith("Room"))
            {
                throw new Exception("Invalid type in dungeon file.\n\n" + info.ObjectType);
            }

            if (objectType.Contains("[") && objectType.Contains("]"))
            {
                string commands = objectType.Substring(objectType.IndexOf("[", StringComparison.Ordinal));

                objectType = objectType.Substring(0, objectType.IndexOf("[", StringComparison.Ordinal));

                fileName = commands.Substring(commands.IndexOf(" ", StringComparison.Ordinal) + 1,
                    commands.IndexOf("]", StringComparison.Ordinal) - commands.IndexOf(" ", StringComparison.Ordinal) -
                    1);
            }

            roomLoadingInfo.Add(new RoomLoadingInformation(fileName,
                new ObjectLoadingInformation(objectType, info.Position)));
        }

        return roomLoadingInfo;
    }

    public static DungeonStateInformation GenerateRooms(string filepath)
    {
        ObjectsLoadingInformationPlusMetaData roomsToLoadPlusMetaData =
            ObjectLoadingInformationCsvMatrixParser.GenerateObjectsLoadingInformationPlusMetaData(
                CsvParser.GenerateCsvMatrix(filepath));
        List<RoomLoadingInformation> roomsToLoad =
            UpdateObjectLoadingInformationList(roomsToLoadPlusMetaData.ObjectLoadingInformationList);

        Dictionary<string, Room> dungeonRoomDictionary = new();

        /*
         * Create roomMatrix, this is done as a jagged array... might should've done it square but, here we are.
         */
        Room[][] roomMatrix = new Room[roomsToLoadPlusMetaData.Height][];
        for (int i = 0; i < roomsToLoadPlusMetaData.Height; i++)
            roomMatrix[i] = new Room[roomsToLoadPlusMetaData.Width];

        foreach (RoomLoadingInformation info in roomsToLoad)
        {
            string qualifiedFileName = "Content/" + info.FileName;
            if (!File.Exists(qualifiedFileName))
            {
                throw new Exception("Room File Name doesn't exist!\n\n" + qualifiedFileName);
            }

            //Add room to dungeon dictionary.
            dungeonRoomDictionary.Add(info.FileName, new Room(qualifiedFileName));

            //Add alias of room to dungeon matrix.
            roomMatrix[info.ObjectInformation.Position.Y][info.ObjectInformation.Position.X] =
                dungeonRoomDictionary[info.FileName];
        }

        //TODO: Eventually will add a flag like [Start] to the room that will be set active here. Currently this is handled in the dungeon constructor.

        return new DungeonStateInformation(roomMatrix, dungeonRoomDictionary, dungeonRoomDictionary.First().Value);
    }
}