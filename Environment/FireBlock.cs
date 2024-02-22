using CSE3902.AbstractClasses;
using CSE3902.Shared;
using Microsoft.Xna.Framework;

namespace CSE3902.Environment;

public class FireBlock : AbstractBlock
{
    private int _spriteState = 1;
    private int _fireTimer = 8;

    public FireBlock(Vector2 position) : base(position)
    {
        TextureAtlasSprite = SpritesheetFactory.Instance.CreateBlockSprite(typeof(FireBlock));
        PhysicalPassThrough = true;
        MagicalPassThrough = true;
    }

    public override void Update()
    {
        _fireTimer--;
        if (_fireTimer == 0)
        {
            if (_spriteState == 1) _spriteState = 2;
            else if (_spriteState == 2) _spriteState = 1;
            _fireTimer = 8;
            TextureAtlasSprite.SetFrame(1, _spriteState);
        }
    }
}