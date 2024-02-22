using CSE3902.LevelLoading;
using CSE3902.Shared;

namespace CSE3902.Interfaces;

public interface IDoor : IBlock
{
    public string AdjacentRoomId { get; set;  }
    public Direction FacingDirection { get; set; }
    public void InitializeAdjacencyInformation(string roomId);

    public void InitializeFacingDirection(Room room);
}