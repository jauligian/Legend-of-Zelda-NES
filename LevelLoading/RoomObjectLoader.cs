using CSE3902.Interfaces;
using CSE3902.Shared;
using CSE3902.Shared.Managers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace CSE3902.LevelLoading;

public static class RoomObjectLoader
{
    /*
     * This is the only place that position should be changed and only exists due to special cases. Therefore,
     * it was decided to keep the struct readonly, even though it requires some extra memory.
     */
    private static List<ObjectLoadingInformation> UpdateObjectInformation(List<ObjectLoadingInformation> objectsToLoad)
    {
        List<ObjectLoadingInformation> objsWithUpdatedPositions = new();
        foreach (ObjectLoadingInformation info in objectsToLoad)
        {
            string objectType = info.ObjectType;
            if (objectType.Contains("[EMPTY]")) continue;
            (int X, int Y) updatedPosition = new()
            {
                X = info.Position.X * Globals.BlockSize,
                Y = info.Position.Y * Globals.BlockSize
            };
            //TODO: Review if these should be floats or integers? I think they should probably be integers, position is pixel related right?

            /*
             * SPECIAL CASES FOR BLOCKS NOT CENTERED ON THE GRID:
             *
             * Format Specifier is [OFFSET X] where X is the direction offset desired {
             *  L: left
             *  R: right
             *  U: up
             *  D: down
             * }
             *
             * Read as GameObject OFFSET to the Left (shifted left).
             *
             * TriforcePiece, Doors on horizontal walls.
             */
            if (info.ObjectType.Contains("[OFFSET") && info.ObjectType.Contains("]"))
            {
                string controlCode = objectType.Substring(objectType.IndexOf(' ') + 1, 1);
                switch (controlCode)
                {
                    case "L":
                        updatedPosition.X -= Globals.BlockSize / 2;
                        break;
                    case "R":
                        updatedPosition.X += Globals.BlockSize / 2;
                        break;
                    case "U":
                        updatedPosition.Y -= Globals.BlockSize / 2;
                        break;
                    case "D":
                        updatedPosition.Y += Globals.BlockSize / 2;
                        break;
                    default:
                        throw new Exception("Unrecognized controlCode in []\n\n" + objectType);
                }

                objectType = objectType.Substring(0, objectType.IndexOf("[", StringComparison.Ordinal));
            }
            else if (info.ObjectType.Contains("[HOLDS") && info.ObjectType.Contains("]"))
            {
            }

            objsWithUpdatedPositions.Add(new ObjectLoadingInformation(objectType, updatedPosition));
        }

        return objsWithUpdatedPositions;
    }

    public static RoomStateInformation GenerateObjects(string filepath)
    {
        /*
         * Parse CSV into a string matrix then into a GameObjectLoadingInformation List
         * and a height and width of the room in blocks.
         */
        ObjectsLoadingInformationPlusMetaData roomObjectsLoadingInformationPlusMetaData =
            ObjectLoadingInformationCsvMatrixParser.GenerateObjectsLoadingInformationPlusMetaData(
                CsvParser.GenerateCsvMatrix(filepath));

        List<ObjectLoadingInformation> updatedObjectInformation = UpdateObjectInformation(
            roomObjectsLoadingInformationPlusMetaData.ObjectLoadingInformationList
        );

        GameObjectManager<IBlock> blockManager = new();
        GameObjectManager<IEnemy> enemyManager = new();
        GameObjectManager<IItem> itemManager = new();
        GameObjectManager<IDoor> doorManager = new();
        foreach (ObjectLoadingInformation info in updatedObjectInformation)
        {
            Vector2 position = new(info.Position.X, info.Position.Y);

            if (!SharedUtilDefinitions.GameObjectCreationDictionary.ContainsKey(info.ObjectType))
            {
                throw new Exception("Invalid type in loading file.\n\n" + info.ObjectType);
            }

            if (info.ObjectType.Contains("Block"))
            {
                if (info.ObjectType.Contains("Door"))
                {
                    doorManager.Spawn(
                        (IDoor)SharedUtilDefinitions.GameObjectCreationDictionary[info.ObjectType](position));
                }
                else
                {
                    blockManager.Spawn(
                        (IBlock)SharedUtilDefinitions.GameObjectCreationDictionary[info.ObjectType](
                            position));
                }
            }
            else if (info.ObjectType.Contains("Enemy"))
            {
                enemyManager.Spawn(
                    (IEnemy)SharedUtilDefinitions.GameObjectCreationDictionary[info.ObjectType](
                        position));
            }
            else if (info.ObjectType.Contains("Item"))
            {
                itemManager.Spawn(
                    (IItem)SharedUtilDefinitions.GameObjectCreationDictionary[info.ObjectType](
                        position));
            }
            else
            {
                throw new Exception("Invalid type in loading file.\n\n" + info);
            }
        }

        return new RoomStateInformation(blockManager, doorManager, enemyManager, itemManager,
            roomObjectsLoadingInformationPlusMetaData.Width,
            roomObjectsLoadingInformationPlusMetaData.Height);
    }
}