namespace CSE3902.Shared;

public enum Direction
{
    Left,
    Right,
    Up,
    Down,
    None,
    UpLeft,
    UpRight,
    DownLeft,
    DownRight
};

public class Globals
{
    public const int GlobalSizeMult = 3;
    public const int BlockSize = 16 * GlobalSizeMult;
    public const int FloorWidth = 192 * GlobalSizeMult;
    public const int FloorHeight = 112 * GlobalSizeMult;
    public const int HudOffset = 48 * GlobalSizeMult;
}
