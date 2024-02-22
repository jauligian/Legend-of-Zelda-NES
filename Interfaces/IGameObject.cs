using Microsoft.Xna.Framework;

namespace CSE3902.Interfaces;

public interface IGameObject
{
    public Vector2 Position { get; set; }
    public bool Active { get; set; }
    void Draw();

    /*
     * TODO: I don't like the way this is named relative to how it is used by GameObjectMangers and the Dungeon class.
     */
    void InitializeGlobalPosition(int horizontalOffset, int verticalOffset);
    void Update();
}