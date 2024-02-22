using CSE3902.Interfaces;
using CSE3902.Items;
using CSE3902.Shared;
using CSE3902.States;
using CSE3902.Inventory;
using Microsoft.Xna.Framework;
using System;

namespace CSE3902.Players
{
    public class StalfosPlayer : IPlayer
    {
        public Game1 Game;
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public int PlayerHealth { get; set; }
        public int MaxPlayerHealth { get; set; }
        public IPlayerState State { get; set; }
        public ITextureAtlasSprite CurrentSprite { get; set; }
        public ISword Sword { get; set; }
        public Rectangle Hitbox { get; private set; }
        public Direction MovingDirection { get; set; }
        public Type CurrentItemType { get; set; }
        public PlayerInventory Inventory { get; set; }
        private int _invincibleTime;
        private const int Height = 16 * Globals.GlobalSizeMult;
        private const int Width = 16 * Globals.GlobalSizeMult;

        public StalfosPlayer(Game1 game)
        {
            State = new StalfosMainState(this);
            XPosition = 200;
            YPosition = 100 + Globals.HudOffset;
            Game = game;
            CurrentItemType = typeof(ArrowItem);
            _invincibleTime = 0;
            PlayerHealth = 6;
            MaxPlayerHealth = 6;
            Inventory = new PlayerInventory(game);
        }
        public void MoveDown()
        {
            State.MoveDown();
        }

        public void MoveLeft()
        {
            State.MoveLeft();
        }

        public void MoveRight()
        {
            State.MoveRight();
        }

        public void MoveUp()
        {
            State.MoveUp();
        }

        public void TakeDamage(int damageAmount, Direction damagedFrom)
        {
            if (_invincibleTime == 0)
            {
                State.TakeDamage(damageAmount, Game);
                Game.Player = new DamagedPlayer(this, Game, damagedFrom);
                _invincibleTime = 75;
                if (PlayerHealth <= MaxPlayerHealth / 4)
                {
                    SoundFactory.Instance.StopLowHealth();
                    SoundFactory.Instance.PlayLowHealth();
                }
            }
        }

        public void Update()
        {
            if (_invincibleTime > 0) _invincibleTime--;
            State.Update();
            UpdateHitbox();
        }

        public void Draw()
        {
            State.Draw();
        }

        public void UseItem(Type itemType)
        {
            State.UseItem(itemType, Game);
        }

        public void UseSword()
        {
            State.UseSword(Game);
        }

        public void UpdateHitbox()
        {
            Hitbox = new Rectangle(XPosition - Width / 2 - 8, YPosition - Height / 2 - 8, Width, Height);
        }

        public void PickupItem(IItem item)
        {
            State.PickupItem(item);
            Inventory.AddItem(item);
            Game.PauseHud.UpdateObtainedItems();
        }
        public void WalkBetweenRooms(Direction directionToMove)
        {
            State.WalkBetweenRooms(directionToMove);
            _invincibleTime = 30;
        }
    }
}
