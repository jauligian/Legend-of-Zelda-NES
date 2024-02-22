using CSE3902.Interfaces;
using CSE3902.LevelLoading;
using CSE3902.Shared;
using Microsoft.Xna.Framework;
using System.Transactions;

namespace CSE3902.AbstractClasses;

public abstract class AbstractDoorBlock : AbstractBlock, IDoor
{
    public string AdjacentRoomId { get; set; }
    public Direction FacingDirection { get; set; } = Direction.None;

    protected AbstractDoorBlock(Vector2 pos) : base(pos)
    {
        Position = pos;
        Height = 2 * Globals.BlockSize;
        Width = 2 * Globals.BlockSize;
    }

    public void InitializeAdjacencyInformation(string roomId)
    {
        AdjacentRoomId = roomId;
    }

    public void InitializeFacingDirection(Room room)
    {
        float scaledRoomWidthPlusGlobalOffset =
            room.RoomState.RoomWidthInBlocks * Globals.BlockSize;
        float scaledRoomHeightPlusGlobalOffset =
            room.RoomState.RoomHeightInBlocks * Globals.BlockSize;

        /*
         * TODO: Note that this type of scaling will break once global positions are used.
         */
        if (Position.Y > scaledRoomHeightPlusGlobalOffset / 5.0 &&
            Position.Y < scaledRoomHeightPlusGlobalOffset * 0.8)
        {
            if (Position.X < scaledRoomWidthPlusGlobalOffset / 2.0)
            {
                //On the left side of the room not on the top or bottom of the room.
                FacingDirection = Direction.Right;
            }
            else if (Position.X > scaledRoomWidthPlusGlobalOffset / 2.0)
            {
                //On the right side of the room not on the top or bottom of the room
                FacingDirection = Direction.Left;
            }
        }
        else if (Position.Y < scaledRoomHeightPlusGlobalOffset / 2.0)
        {
            //No on the right or left side in the middle of the room, on the top of the room
            FacingDirection = Direction.Down;
        }
        else
        {
            FacingDirection = Direction.Up;
        }

        SetCorrectSprite();
    }

    protected void SetCorrectSprite()
    {
        //base.Hitbox = new Rectangle((int)Position.X - Width / 2, (int)Position.Y - Height / 2, Height, Width);
        //THIS IS TEMPORARY JUST FOR SPRINT THREE TODO: REFACTOR AND REPLACE
        //Hitbox = new Rectangle(0, 0, 0, 0);
        switch (FacingDirection)
        {
            case Direction.Down:
                TextureAtlasSprite.SetFrame(1, 1);
                break;
            case Direction.Right:
                TextureAtlasSprite.SetFrame(2, 1);
                break;
            case Direction.Left:
                TextureAtlasSprite.SetFrame(3, 1);
                break;
            case Direction.Up:
                TextureAtlasSprite.SetFrame(4, 1);
                break;
        }
    }
    public override void Update()
    {
        UpdateHitbox();
    }
    public override void UpdateHitbox()
    {
        switch (FacingDirection)
        {
            case Direction.Down:
                Hitbox = new Rectangle((int)(Position.X - Globals.BlockSize / 2), (int)(Position.Y - .5 * Globals.BlockSize), Height, Width);
                break;
            case Direction.Up:
                Hitbox = new Rectangle((int)(Position.X), (int)(Position.Y - .5 * Globals.BlockSize), Height, Width);
                break;
            case Direction.Left:
                Hitbox = new Rectangle((int)(Position.X - Globals.BlockSize / 2), (int)Position.Y, Height, Width);
                break;
            case Direction.Right:
                Hitbox = new Rectangle((int)(Position.X - Globals.BlockSize / 2), (int)Position.Y, Width, Height);
                break;
        }
    }
}