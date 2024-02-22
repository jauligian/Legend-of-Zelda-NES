using CSE3902.Interfaces;
using CSE3902.Items;
using CSE3902.Players;
using CSE3902.Shared;
using Microsoft.Xna.Framework;

namespace CSE3902.HUD;

public class CharacteSelect
{
    private readonly ITextureAtlasSprite[] _sprites = { SpritesheetFactory.Instance.CreatePlayerSprite(), SpritesheetFactory.Instance.CreateStalfosSprite(), SpritesheetFactory.Instance.CreateGoriyaSprite() };
    private readonly ITextureAtlasSprite _background = SpritesheetFactory.Instance.CreateCharacterSelectScreen();
    private readonly Game1 _game;

    private int _characterPos = 0;
    public CharacteSelect(Game1 game)
    {
        _game = game;
    }

    public void Update()
    {
        IPlayer savedPlayer = _game.Player;
        if (_characterPos == 0) 
        {
            _game.Player = new Link(_game); 
        }
        else if (_characterPos == 1)
        {
            _game.Player = new StalfosPlayer(_game);
            _game.Player.CurrentItemType = typeof(BombItem);
        }
        else if (_characterPos == 2)
        {
            _game.Player = new GoriyaPlayer(_game);
            _game.Player.CurrentItemType = typeof(BoomerangItem);
        }
        _game.Player.Inventory = savedPlayer.Inventory;
        _game.Player.MaxPlayerHealth = savedPlayer.MaxPlayerHealth;
        _game.Player.PlayerHealth = savedPlayer.MaxPlayerHealth;
        _game.Player.XPosition = savedPlayer.XPosition;
        _game.Player.YPosition = savedPlayer.YPosition;
        if (_characterPos == 2) _game.Player.Inventory.HasBoomerang = true;
        else _game.Player.Inventory.HasBoomerang = _game.Player.Inventory.PickedUpBoomerang;
        }

    public void Draw()
    {
        _background.Draw(new Vector2(0, Globals.HudOffset));
        _sprites[_characterPos].DrawFromCenter(new Vector2(Globals.GlobalSizeMult * 123, Globals.GlobalSizeMult * 131));
    }
    public void NextOrPrevCharacter(int direction)
    {
        _characterPos = (_characterPos + direction + 3) % 3;
    }
}