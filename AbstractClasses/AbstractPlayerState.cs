using CSE3902.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSE3902.States;
using Microsoft.Xna.Framework;
using CSE3902.Shared;

namespace CSE3902.AbstractClasses
{
    public abstract class AbstractPlayerState : IPlayerState
    {
        public IPlayer MyPlayer { get; set; }
        public const int StepSize = (int)(1.5 * Globals.GlobalSizeMult);
        public virtual void MoveDown()
        {
            //In later sprints, collision checking could be done here?
            MyPlayer.YPosition += StepSize;
            MyPlayer.State = new DownIdleState(MyPlayer);
            MyPlayer.MovingDirection = Shared.Direction.Down;
        }

        public virtual void MoveLeft()
        {
            MyPlayer.XPosition -= StepSize;
            MyPlayer.State = new LeftIdleState(MyPlayer);
            MyPlayer.MovingDirection = Shared.Direction.Left;
        }

        public virtual void MoveRight()
        {
            MyPlayer.XPosition += StepSize;
            MyPlayer.State = new RightIdleState(MyPlayer);
            MyPlayer.MovingDirection = Shared.Direction.Right;
        }

        public virtual void MoveUp()
        {
            MyPlayer.YPosition -= StepSize;
            MyPlayer.State = new UpIdleState(MyPlayer);
            MyPlayer.MovingDirection = Shared.Direction.Up;
        }

        public virtual void TakeDamage(int damageAmount, Game1 game)
        {
            MyPlayer.PlayerHealth -= damageAmount;
            if (MyPlayer.PlayerHealth == 0) MyPlayer.State = new LinkDyingState(MyPlayer, game);
            else SoundFactory.Instance.PlayLinkHurt();
        }
        public virtual void Draw()
        {
            MyPlayer.CurrentSprite.DrawFromCenter(new Vector2(MyPlayer.XPosition, MyPlayer.YPosition));
        }
        public virtual void Update() { }
        public virtual void UseItem(Type itemType, Game1 game) { }
        public abstract void UseSword(Game1 game);
        public virtual void PickupItem(IItem item)
        {
            IPlayerState state = new ItemPickupState(MyPlayer);
            state.PickupItem(item);
            MyPlayer.State = state;
        }
        public void WalkBetweenRooms(Direction directionToMove)
        {
            MyPlayer.State = new TransitionRoomsState(MyPlayer, directionToMove);
        }
    }
}
