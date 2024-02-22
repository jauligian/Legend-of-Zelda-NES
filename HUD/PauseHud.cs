using CSE3902.Interfaces;
using CSE3902.Items;
using CSE3902.Shared;
using Microsoft.Xna.Framework;
using System;

namespace CSE3902.HUD;

public class PauseHud
{
    private readonly ITextureAtlasSprite _itemOutline = SpritesheetFactory.Instance.CreateItemOutlineSprite();
    private readonly ITextureAtlasSprite _boomerangSprite = SpritesheetFactory.Instance.CreateTallItemSprite();
    private readonly ITextureAtlasSprite _bombSprite = SpritesheetFactory.Instance.CreateTallItemSprite();
    private readonly ITextureAtlasSprite _bowSprite = SpritesheetFactory.Instance.CreateTallItemSprite();
    private readonly ITextureAtlasSprite _arrowSprite = SpritesheetFactory.Instance.CreateTallItemSprite();
    private readonly ITextureAtlasSprite _mapSprite = SpritesheetFactory.Instance.CreateTallItemSprite();
    private readonly ITextureAtlasSprite _compassSprite = SpritesheetFactory.Instance.CreateLargeItemSprite();

    private readonly Game1 _game;

    private int _spriteSwapCount = 0;
    public int HighlightedItem = -1;

    private bool[] _obtainedItems = { false, false, false };
    private readonly Type[] _types = { typeof(BoomerangItem), typeof(BombItem), typeof(ArrowItem) };

    public PauseHud(Game1 game)
    {
        _game = game;

        _boomerangSprite.Column = 17;
        _bombSprite.Column = 18;
        _bowSprite.Column = 19;
        _arrowSprite.Column = 20;
        _mapSprite.Column = 12;
        _compassSprite.Column = 17;
    }

    public void Draw()
    {
        DrawObtainedItems();
        if (_obtainedItems[0] || _obtainedItems[1] || _obtainedItems[2])
            _itemOutline.Draw(new Vector2(
                133 * Globals.GlobalSizeMult + 20 * Globals.GlobalSizeMult * HighlightedItem,
                54 * Globals.GlobalSizeMult));
        DrawCurrentItem();
        if (_game.Player.Inventory.HasMap)
            _mapSprite.DrawFromCenter(new Vector2(48 * Globals.GlobalSizeMult, 120 * Globals.GlobalSizeMult));
        if (_game.Player.Inventory.HasCompass)
            _compassSprite.DrawFromCenter(new Vector2(46 * Globals.GlobalSizeMult, 160 * Globals.GlobalSizeMult));
    }

    public void Update()
    {
        _spriteSwapCount++;
        if (_spriteSwapCount % 8 == 0)
        {
            _itemOutline.Column = _itemOutline.Column % 2 + 1;
            _spriteSwapCount = 0;
        }

        _game.Player.CurrentItemType = HighlightedItem >= 0 ? _types[HighlightedItem] : null;
    }

    public void DrawObtainedItems()
    {
        if (_obtainedItems[0])
            _boomerangSprite.Draw(new Vector2(137 * Globals.GlobalSizeMult, 54 * Globals.GlobalSizeMult));
        if (_obtainedItems[1])
            _bombSprite.Draw(new Vector2(157 * Globals.GlobalSizeMult, 54 * Globals.GlobalSizeMult));
        if (_obtainedItems[2])
        {
            _bowSprite.Draw(new Vector2(181 * Globals.GlobalSizeMult, 54 * Globals.GlobalSizeMult));
            _arrowSprite.Draw(new Vector2(173 * Globals.GlobalSizeMult, 54 * Globals.GlobalSizeMult));
        }
    }

    public void UpdateObtainedItems()
    {
        _obtainedItems[0] = _game.Player.Inventory.HasBoomerang;
        _obtainedItems[1] = _game.Player.Inventory.Bombs > 0;
        _obtainedItems[2] = _game.Player.Inventory.HasBow && _game.Player.Inventory.HasArrow && _game.Player.Inventory.Rupees > 0;
        if (_game.Player.Inventory.HasMagicBoomerang) _boomerangSprite.Row = 2;
    }

    public void DrawCurrentItem()
    {
        if (HighlightedItem == 0)
            _boomerangSprite.Draw(new Vector2(68 * Globals.GlobalSizeMult, 56 * Globals.GlobalSizeMult));
        else if (HighlightedItem == 1)
            _bombSprite.Draw(new Vector2(68 * Globals.GlobalSizeMult, 56 * Globals.GlobalSizeMult));
        else if (HighlightedItem == 2)
            _arrowSprite.Draw(new Vector2(68 * Globals.GlobalSizeMult, 56 * Globals.GlobalSizeMult));
    }

    public int GetFirstItem()
    {
        if (_obtainedItems[0]) return 0;
        else if (_obtainedItems[1]) return 1;
        else if (_obtainedItems[2]) return 2;
        return -1;
    }

    public void FindNextOrPrevItem(int direction)
    {
        if (HighlightedItem == -1)
        {
            HighlightedItem = GetFirstItem();
            return;
        }

        int currentIndex = (HighlightedItem + direction + _obtainedItems.Length) % _obtainedItems.Length;

        do
        {
            if (_obtainedItems[currentIndex % _obtainedItems.Length])
            {
                HighlightedItem = currentIndex;
                return;
            }

            currentIndex = (currentIndex + direction + _obtainedItems.Length) % _obtainedItems.Length;
        } while (currentIndex != HighlightedItem);

        HighlightedItem = GetFirstItem();
    }

    public int GetCurrentItemPos()
    {
        if (_game.Player.CurrentItemType == typeof(BoomerangItem) && _obtainedItems[0]) return 0;
        else if (_game.Player.CurrentItemType == typeof(BombItem) && _obtainedItems[1]) return 1;
        else if (_game.Player.CurrentItemType == typeof(ArrowItem) && _obtainedItems[2]) return 2;
        else return GetFirstItem();
    }

    public bool HasCurrentItem(int col)
    {
        if (col == 17) return _obtainedItems[0];
        if (col == 18) return _obtainedItems[1];
        if (col == 20) return _obtainedItems[2];
        return false;
    }
}