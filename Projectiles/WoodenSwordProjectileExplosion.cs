using CSE3902.Interfaces;
using CSE3902.Shared;
using Microsoft.Xna.Framework;

namespace CSE3902.Projectiles;

public class WoodenSwordProjectileExplosion : IPlayerProjectile
{
    private readonly ITextureAtlasSprite _topLeftSprite;
    private readonly ITextureAtlasSprite _topRightSprite;
    private readonly ITextureAtlasSprite _bottomLeftSprite;
    private readonly ITextureAtlasSprite _bottomRightSprite;
    private readonly Vector2 _initialLocation;
    private Vector2 _bottomRightSpriteLocation;
    private Vector2 _bottomLeftSpriteLocation;
    private Vector2 _topRightSpriteLocation;
    private Vector2 _topLeftSpriteLocation;
    private int _remainingTimeOfFlight;
    private const int StepSize = 4;
    public bool Active { get; set; }
    public Rectangle Hitbox { get; }
    public bool StruckSomething { get; set; }
    public int DamageAmount { get; set; }

    public WoodenSwordProjectileExplosion(Vector2 position)
    {
        _topLeftSprite = SpritesheetFactory.Instance.CreateTallProjectileSprite();
        _topLeftSprite.SetFrame(5, 1);
        _topRightSprite = SpritesheetFactory.Instance.CreateTallProjectileSprite();
        _topRightSprite.SetFrame(6, 1);
        _bottomLeftSprite = SpritesheetFactory.Instance.CreateTallProjectileSprite();
        _bottomLeftSprite.SetFrame(7, 1);
        _bottomRightSprite = SpritesheetFactory.Instance.CreateTallProjectileSprite();
        _bottomRightSprite.SetFrame(8, 1);
        _initialLocation = position;
        _bottomRightSpriteLocation = _initialLocation;
        _bottomLeftSpriteLocation = _initialLocation;
        _topRightSpriteLocation = _initialLocation;
        _topLeftSpriteLocation = _initialLocation;
        _remainingTimeOfFlight = 10;
        Active = true;
        DamageAmount = 0;
    }

    public void Draw()
    {
        if (_remainingTimeOfFlight > 0)
        {
            SelectSprites();
            _bottomLeftSprite.DrawFromCenter(_bottomLeftSpriteLocation);
            _bottomRightSprite.DrawFromCenter(_bottomRightSpriteLocation);
            _topLeftSprite.DrawFromCenter(_topLeftSpriteLocation);
            _topRightSprite.DrawFromCenter(_topRightSpriteLocation);
        }
        else
        {
            Active = false;
        }
    }

    public void Update()
    {
        _bottomLeftSpriteLocation.X -= StepSize;
        _bottomLeftSpriteLocation.Y += StepSize;
        _topRightSpriteLocation.X += StepSize;
        _topRightSpriteLocation.Y -= StepSize;
        _topLeftSpriteLocation.X -= StepSize;
        _topLeftSpriteLocation.Y -= StepSize;
        _bottomRightSpriteLocation.X += StepSize;
        _bottomRightSpriteLocation.Y += StepSize;
        _remainingTimeOfFlight--;
    }

    public void Use()
    {
    }

    private void SelectSprites()
    {
        if (_remainingTimeOfFlight % 2 == 0)
        {
            int frame = _remainingTimeOfFlight / 2 % 3 + 1;
            _bottomLeftSprite.SetFrame(_bottomLeftSprite.Row, frame);
            _bottomRightSprite.SetFrame(_bottomRightSprite.Row, frame);
            _topLeftSprite.SetFrame(_topLeftSprite.Row, frame);
            _topRightSprite.SetFrame(_topRightSprite.Row, frame);
        }
    }
}