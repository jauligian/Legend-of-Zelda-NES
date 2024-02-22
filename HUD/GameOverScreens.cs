using CSE3902.Interfaces;
using CSE3902.Shared;
using Microsoft.Xna.Framework;

namespace CSE3902.HUD
{
    public class GameOverScreens
    {
        private ITextureAtlasSprite _gameOverScreen = SpritesheetFactory.Instance.CreateGameOverScreen();
        private ITextureAtlasSprite _gameOverOptions = SpritesheetFactory.Instance.CreateGameOverOptions();
        private ITextureAtlasSprite _heartIcon = SpritesheetFactory.Instance.CreateSmallItemSprite();

        public int Pos = 0;
        private int _count = 0;
        public GameOverScreens() 
        {
        }

        public void Update()
        {
            _count++;
            if (_count % 8 == 0)
            {
                _heartIcon.Row = _heartIcon.Row % 2 + 1;
            }
        }

        public void Draw()
        {
            if (_count < 120)
            {
                DrawGameOver();
            }
            else
            {
                DrawOptions();
            }
        }

        public void DrawGameOver()
        {
            _gameOverScreen.Draw(new Vector2(0, Globals.HudOffset));
        }

        public void DrawOptions()
        {
            _gameOverOptions.Draw(new Vector2(0, 0));
            _heartIcon.Draw(new Vector2(70 * Globals.GlobalSizeMult, (80 + Pos * 24) * Globals.GlobalSizeMult));
        }

        public void UpMenu()
        {
            Pos = (Pos + 2) % 3;
        }

        public void DownMenu()
        {
            Pos = (Pos + 4) % 3;
        }
    }
}
