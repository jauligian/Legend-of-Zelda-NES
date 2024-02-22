using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CSE3902.Interfaces
{
    public interface ITextureAtlasSprite
    {
        public static SpriteBatch SpriteBatch;

        public int Row { get; set; }
        public int Column { get; set; }
        public Color Color { get; set; }
        public void Draw(Vector2 location);
        public void DrawColored(Vector2 location, Color color);
        public void DrawFromCenter(Vector2 location);
        public void DrawColoredFromCenter(Vector2 location, Color color);
        public void SetFrame(int row, int column);
    }
}
