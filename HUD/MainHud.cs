using CSE3902.Interfaces;
using CSE3902.Items;
using CSE3902.Shared;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace CSE3902.HUD;

public class MainHud
{
    private readonly ITextureAtlasSprite _fullHeart = SpritesheetFactory.Instance.CreateSmallItemSprite();
    private readonly ITextureAtlasSprite _halfHeart = SpritesheetFactory.Instance.CreateSmallItemSprite();
    private readonly ITextureAtlasSprite _emptyHeart = SpritesheetFactory.Instance.CreateSmallItemSprite();

    private readonly ITextureAtlasSprite _swordSprite = SpritesheetFactory.Instance.CreateTallItemSprite();
    private readonly ITextureAtlasSprite _secondaryItemSprite = SpritesheetFactory.Instance.CreateTallItemSprite();

    private readonly ITextureAtlasSprite _dungeonMap1 = SpritesheetFactory.Instance.CreateDungeonMapSprite();
    private readonly ITextureAtlasSprite _dungeonMap2 = SpritesheetFactory.Instance.CreateDungeonMap2Sprite();
    private readonly ITextureAtlasSprite _triforceIcon = SpritesheetFactory.Instance.CreateTriforceIconSprite();
    private readonly ITextureAtlasSprite _linkMapDot = SpritesheetFactory.Instance.CreateLinkMapDotSprite();

    private readonly Game1 _game;
    private readonly SpriteFont _font;

    private int _spriteSwapCount = 0;

    public MainHud(Game1 game, SpriteFont font)
    {
        _game = game;
        _font = font;

        _halfHeart.Column = 2;
        _emptyHeart.Row = 2;
        _emptyHeart.Column = 2;

        _swordSprite.Column = 14;
    }

    /*
     * Lots of seemingly random magic numbers used for positioning of drawing things on the background.
     * As far as I could tell there wasn't really any reasoning to the layout of the hud this game has and
     * everything was kind of arbitrarily placed so the use of the magic numbers were to try and match the layout of the hud.
     */


    public void Draw(Vector2 location)
    {
        ITextureAtlasSprite.SpriteBatch.DrawString(_font, "X" + _game.Player.Inventory.Bombs,
            new Vector2(97 * Globals.GlobalSizeMult, 39 * Globals.GlobalSizeMult + location.Y), Color.White);
        ITextureAtlasSprite.SpriteBatch.DrawString(_font, "X" + _game.Player.Inventory.Keys,
            new Vector2(97 * Globals.GlobalSizeMult, 30 * Globals.GlobalSizeMult + location.Y), Color.White);
        ITextureAtlasSprite.SpriteBatch.DrawString(_font, "" + _game.Player.Inventory.Rupees,
            new Vector2(97 * Globals.GlobalSizeMult, 15 * Globals.GlobalSizeMult + location.Y), Color.White);
        if (_game.Player.Inventory.HasMap)
        {
            if (_game.CurrentDungeon == 0)
                _dungeonMap1.Draw(new Vector2(16 * Globals.GlobalSizeMult, 15 * Globals.GlobalSizeMult + location.Y));
            if (_game.CurrentDungeon == 1)
                _dungeonMap2.Draw(new Vector2(16 * Globals.GlobalSizeMult, 15 * Globals.GlobalSizeMult + location.Y));
        }

        if (_game.Player.Inventory.HasCompass)
        {
            if (_game.CurrentDungeon == 0)
                _triforceIcon.Draw(new Vector2(66 * Globals.GlobalSizeMult, 27 * Globals.GlobalSizeMult + location.Y));
            if (_game.CurrentDungeon == 1)
                _triforceIcon.Draw(new Vector2(42 * Globals.GlobalSizeMult, 15 * Globals.GlobalSizeMult + location.Y));
        }

        DrawPlayerDot(location);
        DrawLife(location);
        if (location.Y == 0) DrawItems(location);
    }

    public void Update()
    {
        _spriteSwapCount++;

        if (_spriteSwapCount == 16) 
        {
            if (_triforceIcon.Column == 1) _triforceIcon.Column = 2;
            else _triforceIcon.Column = 1;
            _spriteSwapCount = 0;
        }
    }

    public void DrawPlayerDot(Vector2 location)
    {
        int currentRow = _game.Dungeons[_game.CurrentDungeon].ActiveRoomRelativePosition.roomColumnIndex;//+1
        int currentCol = _game.Dungeons[_game.CurrentDungeon].ActiveRoomRelativePosition.roomRowIndex;//-2
        int topLeftX = 26;
        if (_game.CurrentDungeon == 1) topLeftX = 34;
        int topLeftY = 23;
        if (_game.CurrentDungeon == 1) topLeftY = 15;
        _linkMapDot.Draw(new Vector2((topLeftX + currentRow * 8) * Globals.GlobalSizeMult,
            location.Y + (topLeftY + currentCol * 4) * Globals.GlobalSizeMult));
    }

    public void DrawLife(Vector2 location)
    {
        int seperator = 8 * Globals.GlobalSizeMult;
        Vector2 heartPos;

        for (int i = 0; i < _game.Player.MaxPlayerHealth / 2; i++)
        {
            heartPos = new Vector2(176 * Globals.GlobalSizeMult + i * seperator,
                36 * Globals.GlobalSizeMult + location.Y);
            if (_game.Player.PlayerHealth > 1 + 2 * i) _fullHeart.Draw(heartPos);
            else if (_game.Player.PlayerHealth > 0 + 2 * i) _halfHeart.Draw(heartPos);
            else _emptyHeart.Draw(heartPos);
        }
    }

    public void DrawItems(Vector2 location)
    {
        Type itemType = _game.Player.CurrentItemType;
        _secondaryItemSprite.Row = 1;
        if (itemType == typeof(ArrowItem)) _secondaryItemSprite.Column = 20;
        else if (itemType == typeof(BoomerangItem) || itemType == typeof(BlueBoomerangItem))
        {
            _secondaryItemSprite.Column = 17;
            if (_game.Player.Inventory.HasMagicBoomerang) _secondaryItemSprite.Row = 2;
        }
        else if (itemType == typeof(BombItem)) _secondaryItemSprite.Column = 18;

        _swordSprite.Draw(new Vector2(152 * Globals.GlobalSizeMult, 24 * Globals.GlobalSizeMult + location.Y));
        _game.PauseHud.UpdateObtainedItems();
        if (_game.PauseHud.HasCurrentItem(_secondaryItemSprite.Column))
            _secondaryItemSprite.Draw(new Vector2(128 * Globals.GlobalSizeMult,
                24 * Globals.GlobalSizeMult + location.Y));
    }
}