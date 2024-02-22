using CSE3902.Environment;
using CSE3902.Environment.Doors;
using CSE3902.Interfaces;
using CSE3902.Items;
using CSE3902.LevelLoading;
using CSE3902.Players;
using CSE3902.Shared;
using CSE3902.Shared.Managers;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace CSE3902.Collisions;

public class PlayerCollideWithDoors
{
    public static readonly PlayerCollideWithDoors Instance = new();

    private PlayerCollideWithDoors()
    {
    }

    public void HandleCollisions(IPlayer player, List<IDoor> doors, Dungeon dungeon)
    {
        IDoor doorToReplace = null;
        foreach (IDoor door in doors)
            if (!dungeon.TransitioningRooms && player is not DamagedPlayer)
            {
                Rectangle intersect = Rectangle.Intersect(player.Hitbox, door.Hitbox);

                if (CanPassThroughDoor(door) && intersect.Height > player.Hitbox.Height / 2 &&
                    intersect.Width > player.Hitbox.Width / 2)
                {
                    dungeon.StartTransitionToNewRoom(door.AdjacentRoomId);
                    player.WalkBetweenRooms(CollisionHelper.Instance.InvertDirection(door.FacingDirection));
                    break;
                }
                else if (!intersect.IsEmpty && !CanPassThroughDoor(door))
                {
                    if (door is LockedDoorBlock && player.Inventory.Keys > 0)
                    {
                        player.Inventory.Keys--;
                        door.Active = false;
                        doorToReplace = door;
                        SoundFactory.Instance.PlayUnlockDoor();
                    }

                    PlayerCollideWithBlocks.Instance.MovePlayer(player, intersect);
                }
            }

        if (doorToReplace != null) dungeon.SpawnOpenDoor(doorToReplace);
    }

    private static bool CanPassThroughDoor(IDoor door)
    {
        return door is OpenDoorBlock || (door is LockedDoorBlock && !door.Active) ||
               (door is BombableWallDoorBlock && (door as BombableWallDoorBlock).Bombed);
    }

    public void BombAdjacentDoor(IPlayer player)
    {
        int width = 16 * Globals.GlobalSizeMult;
        int height = 16 * Globals.GlobalSizeMult;
        foreach (IDoor door in GameObjectManagers.DoorManager.ActiveGameObjects)
        {
            Rectangle largerHitbox = new(player.XPosition - width / 2 - 16, player.YPosition - height / 2 - 16,
                width + 16, height + 16);
            Rectangle intersect = Rectangle.Intersect(largerHitbox, door.Hitbox);
            if (!intersect.IsEmpty && door is BombableWallDoorBlock)
            {
                (door as BombableWallDoorBlock).BeBombed();
            }
        }
    }
}