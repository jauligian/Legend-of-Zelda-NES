using CSE3902.AbstractClasses;
using CSE3902.Shared;
using Microsoft.Xna.Framework;

namespace CSE3902.Environment;

public class PushableBlock : AbstractBlock
{
    private Direction _direction = Direction.None;
    private bool _canPush = true;
    private bool _secretFound = false;
    public bool Pushing = false;
    public int PushTime = 0;

    private const int
        StepSize = 3; //TODO: Refactor this so its no longer a magic number. This is copied from the player step size.

    private Vector2? _startingLocation = null;

    public PushableBlock(Vector2 position) : base(position)
    {
        TextureAtlasSprite = SpritesheetFactory.Instance.CreateBlockSprite(typeof(PushableBlock));
        PhysicalPassThrough = false;
        MagicalPassThrough = true;
    }

    public override void InitializeGlobalPosition(int horizontalOffset, int verticalOffset)
    {
        Position = new Vector2(Position.X + horizontalOffset,
            Position.Y + verticalOffset);
        _startingLocation = Position;
    }

    public void Push(Direction direction) //Note that this is the direction the block is being pushed in.
    {
        if (PushTime >= 8)
        {
            _direction = direction;
        }
        else PushTime++;
    }

    public override void Update()
    {
        if (!Pushing)
        {
            PushTime = 0;
        }

        UpdateHitbox();
        if (!_canPush || _startingLocation == null) return;
        Vector2 updatedPosition = Position;

        //Note: Globals.GlobalSizeMult is to correct for what appears to be it being off by one pixel after finishing moving.
        switch (_direction)
        {
            case Direction.None:
                return;
            case Direction.Left:
                updatedPosition.X -= StepSize;
                if (Position.X <= _startingLocation?.X - Width + Globals.GlobalSizeMult)
                {
                    _canPush = false;
                }

                break;
            case Direction.Right:
                updatedPosition.X += StepSize;
                if (Position.X >= _startingLocation?.X + Width - Globals.GlobalSizeMult)
                {
                    _canPush = false;
                }

                break;
            case Direction.Up:
                updatedPosition.Y -= StepSize;
                if (Position.Y <= _startingLocation?.Y - Height + Globals.GlobalSizeMult)
                {
                    _canPush = false;
                }

                break;
            case Direction.Down:
                updatedPosition.Y += StepSize;
                if (Position.Y >= _startingLocation?.Y + Height - Globals.GlobalSizeMult)
                {
                    _canPush = false;
                }

                break;
        }

        Position = updatedPosition;

        if (!_secretFound && _direction != Direction.Down)
        {
            SoundFactory.Instance.PlaySecretFound();
            _secretFound = true;
        }
    }

    public override void UpdateHitbox()
    {
        Hitbox = new Rectangle((int)Position.X - Width / 2, (int)Position.Y - Height / 2, Height, Width);
    }
}