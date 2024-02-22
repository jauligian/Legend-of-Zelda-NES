using Microsoft.Xna.Framework;
namespace CSE3902.Interfaces
{
    public interface IItem : IGameObject, ICollidable
    {
        public void HoldItem (Vector2 location) { }
    }
}