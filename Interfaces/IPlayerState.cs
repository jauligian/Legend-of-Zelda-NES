using CSE3902.Shared;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSE3902.Interfaces
{
    public interface IPlayerState
    {
        public void MoveUp();
        public void MoveDown();
        public void MoveLeft();
        public void MoveRight();
        public void TakeDamage(int damageAmount, Game1 game);
        public void Update();
        public void Draw();
        public void UseItem(Type itemType, Game1 game);
        public void UseSword(Game1 game);
        public void PickupItem(IItem item);
        public void WalkBetweenRooms(Direction directitonToMove);
    }
}
