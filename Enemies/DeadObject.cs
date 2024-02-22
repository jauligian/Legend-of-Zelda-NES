using CSE3902.Interfaces;
using CSE3902.Shared;
using Microsoft.Xna.Framework;

namespace CSE3902.Enemies;

public class DeadObject : IGameObject
{
    private readonly ITextureAtlasSprite _sprite;
    private int _frameNumber;
    private int _frameChangeModifier;
    private int _spriteTimeCounter;
    public Vector2 Position { get; set; }
    public bool Active { get; set; }

    public DeadObject(Vector2 position) : base()
    {
        _sprite = SpritesheetFactory.Instance.CreateDeathSprite();
        _spriteTimeCounter = 0;
        _frameNumber = 4;
        _frameChangeModifier = -1;
        Position = position;
        Active = true;
    }

    public virtual void InitializeGlobalPosition(int horizontalOffset, int verticalOffset)
    {
        Position = new Vector2(Position.X + horizontalOffset,
            Position.Y + verticalOffset);
    }

    public void Draw()
    {
        _sprite.Draw(Position);
    }

    public void Update()
    {
        if (_spriteTimeCounter >= 5)
        {
            if (_frameNumber == 1) _frameChangeModifier = 1;
            _sprite.SetFrame(1, _frameNumber);
            _frameNumber += _frameChangeModifier;
            if (_frameNumber == 5) Active = false;
            _spriteTimeCounter = 0;
        }

        _spriteTimeCounter++;
    }
}