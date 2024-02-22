using CSE3902.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CSE3902.Shared
{
    public class TextureAtlasSprite : ITextureAtlasSprite
    {
        protected Texture2D Spritesheet;
        protected int Height;
        protected int Width;

        protected const int SizeMult = Globals.GlobalSizeMult;

        public int Row { get; set; }
        public int Column { get; set; }
        public Color Color { get; set; }



        public TextureAtlasSprite(Texture2D spritesheet, int width, int height)
        {
            Row = 1;
            Column = 1;
            Width = width;
            Height = height;
            Spritesheet = spritesheet;
            Color = Color.White;
        }

        public void DrawFromCenter(Vector2 location)
        {
            Rectangle destRect = new((int)location.X - Width / 2, (int)location.Y - Height / 2, Width * SizeMult, Height * SizeMult);
            Rectangle sourceRect = new(Width * (Column - 1), Height * (Row - 1), Width, Height);
            ITextureAtlasSprite.SpriteBatch.Draw(Spritesheet, destRect, sourceRect, Color);
        }

        public void DrawColoredFromCenter(Vector2 location, Color color)
        {
            Rectangle destRect = new((int)location.X - Width / 2, (int)location.Y - Height / 2, Width * SizeMult, Height * SizeMult);
            Rectangle sourceRect = new(Width * (Column - 1), Height * (Row - 1), Width, Height);
            ITextureAtlasSprite.SpriteBatch.Draw(Spritesheet, destRect, sourceRect, color);
        }

        public void Draw(Vector2 location)
        {
            Rectangle destRect = new((int)location.X, (int)location.Y, Width * SizeMult, Height * SizeMult);
            Rectangle sourceRect = new(Width * (Column - 1), Height * (Row - 1), Width, Height);
            ITextureAtlasSprite.SpriteBatch.Draw(Spritesheet, destRect, sourceRect, Color);
        }

        public void DrawColored(Vector2 location, Color color)
        {
            Rectangle destRect = new((int)location.X, (int)location.Y, Width * SizeMult, Height * SizeMult);
            Rectangle sourceRect = new(Width * (Column - 1), Height * (Row - 1), Width, Height);
            ITextureAtlasSprite.SpriteBatch.Draw(Spritesheet, destRect, sourceRect, color);
        }

        public void SetFrame(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}