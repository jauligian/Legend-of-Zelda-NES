using CSE3902.Interfaces;
using CSE3902.Items;
using CSE3902.Shared;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using System.Xml.Linq;

namespace CSE3902.HUD
{
    public class WinScreen
    {
        private ITextureAtlasSprite _leftScreen = SpritesheetFactory.Instance.CreateBlackScreen();
        private ITextureAtlasSprite _rightScreen = SpritesheetFactory.Instance.CreateBlackScreen();
        private IItem _triforcePiece;
        private int _time = 0;
        private int _step = 0;
        private int _triforceHeight = 16 * Globals.GlobalSizeMult;
        private int _centeringOffset = 3 * Globals.GlobalSizeMult;
        private Game1 _game;

        public WinScreen(Game1 game)
        {
            _game = game;
        }

        public void Update()
        {
            const int roomWidth = 16 * Globals.BlockSize;
            const int roomHeight = 11 * Globals.BlockSize;
            if (_time == 0)
            {
                _game.Player.XPosition = _game.Player.XPosition % roomWidth;
                _game.Player.YPosition = _game.Player.YPosition % roomHeight + Globals.HudOffset;
                _triforcePiece = new TriforcePieceItem(new Vector2(_game.Player.XPosition - _centeringOffset, _game.Player.YPosition - _triforceHeight));
            }
            _triforcePiece.Update();
            _time++;
            if (_step < 8)
            {
                if (_time % 8 == 0) _step++;
            }
            if (_time == 500)
            {
                _game.GameWin = false;
                _game.ContinueGame = true;
                _game.Player.Inventory.HasMap = false;
                _game.Player.Inventory.HasCompass = false;
                _game.CurrentDungeon++;
                _game.Reset();
            }
        }

        public void Draw()
        {
            _leftScreen.Draw(new Vector2((-256 + _step * 16) * Globals.GlobalSizeMult, 0));
            _rightScreen.Draw(new Vector2((256 - _step * 16) * Globals.GlobalSizeMult,0));
            
            _game.Player.Draw();
            _triforcePiece.Draw();
        }
    }
}
