using CSE3902.Shared;
using CSE3902.Inventory;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSE3902.Interfaces
{
    public interface IPlayer : ICollidable
    {
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public int PlayerHealth { get; set; }
        public int MaxPlayerHealth { get; set; }
        public IPlayerState State { set; }
        public ITextureAtlasSprite CurrentSprite { get; set; }
        public ISword Sword { get; set; }
        public Type CurrentItemType { get; set; }
        public PlayerInventory Inventory { get; set; }
        public void MoveUp();
        public void MoveDown();
        public void MoveLeft();
        public void MoveRight();
        public void TakeDamage(int damageAmount, Direction damagedFrom);
        public void Update();
        public void Draw();
        public void UseItem(Type itemType); 
        public void UseSword();
        public void PickupItem(IItem item);
        public void WalkBetweenRooms(Direction directitonToMove);
    }
}
