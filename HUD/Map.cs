using CSE3902.Interfaces;
using CSE3902.LevelLoading;
using CSE3902.Shared;
using Microsoft.Xna.Framework;

namespace CSE3902.HUD;

public class Map
{
    private readonly bool[,] _roomMap;


    private readonly ITextureAtlasSprite _mapRoom = SpritesheetFactory.Instance.CreateMapRoom();

    private readonly ITextureAtlasSprite _horizontalRoomConnector =
        SpritesheetFactory.Instance.CreateHorizontalRoomConnector();

    private readonly ITextureAtlasSprite _verticalRoomConnector =
        SpritesheetFactory.Instance.CreateVerticalRoomConnector();

    private Dungeon _dungeon;

    public Map(Dungeon dungeon)
    {
        _dungeon = dungeon;
        _roomMap = new bool[_dungeon.DungeonRows, _dungeon.DungeonColumns];
        InitializeMap(_roomMap, _dungeon.DungeonRows, _dungeon.DungeonColumns);
    }

    public void Draw()
    {
        DrawRooms();
    }

    public void Update()
    {
        int currentRow = _dungeon.ActiveRoomRelativePosition.roomRowIndex;
        int currentCol = _dungeon.ActiveRoomRelativePosition.roomColumnIndex;
        _roomMap[currentRow, currentCol] = true;
        _roomMap[0, 0] = false;
        _roomMap[0, 1] = false;
    }

    public void InitializeMap(bool[,] map, int rows, int cols)
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                map[i, j] = false;
            }
        }
    }

    public void DrawRooms()
    {
        for (int i = 0; i < _dungeon.DungeonRows; i++)
        {
            for (int j = 0; j < _dungeon.DungeonColumns; j++)
            {
                if (_roomMap[i, j])
                {
                    _mapRoom.Draw(new Vector2((137 + 8 * i) * Globals.GlobalSizeMult,
                        (112 + 8 * j) * Globals.GlobalSizeMult));

                    if (i < _dungeon.DungeonRows - 1 && _roomMap[i + 1, j])
                    {
                        _horizontalRoomConnector.Draw(new Vector2((143 + 8 * i) * Globals.GlobalSizeMult,
                            (114 + 8 * j) * Globals.GlobalSizeMult));
                        _horizontalRoomConnector.Draw(new Vector2((144 + 8 * i) * Globals.GlobalSizeMult,
                            (114 + 8 * j) * Globals.GlobalSizeMult));
                    }

                    if (j < _dungeon.DungeonColumns - 1 && _roomMap[i, j + 1])
                    {
                        _verticalRoomConnector.Draw(new Vector2((139 + 8 * i) * Globals.GlobalSizeMult,
                            (118 + 8 * j) * Globals.GlobalSizeMult));
                        _verticalRoomConnector.Draw(new Vector2((139 + 8 * i) * Globals.GlobalSizeMult,
                            (119 + 8 * j) * Globals.GlobalSizeMult));
                    }
                }
            }
        }
    }
}