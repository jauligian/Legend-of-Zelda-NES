//Referenced code by Matt Boggus

using CSE3902.Enemies;
using CSE3902.Environment;
using CSE3902.Environment.Doors;
using CSE3902.Interfaces;
using CSE3902.Players;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace CSE3902.Shared;

public class SpritesheetFactory
{
    private Dictionary<string, Texture2D> _spritesheets;

    public static SpritesheetFactory Instance { get; } = new();

    private SpritesheetFactory()
    {
    }

    public void LoadAllTextures(ContentManager content)
    {
        _spritesheets = new Dictionary<string, Texture2D>
        {
            //Block Spritesheets
            { nameof(BasicBlueBlock), content.Load<Texture2D>("BasicBlueBlockSpritesheet") },
            { nameof(BlackBlock), content.Load<Texture2D>("BlackBlockSpritesheet") },
            { nameof(BlackBlockC), content.Load<Texture2D>("BlackBlockSpritesheet") },
            { nameof(BlueStairsBlock), content.Load<Texture2D>("BlueStairsBlockSpritesheet") },
            { nameof(JewelCutBlueBlock), content.Load<Texture2D>("JewelCutBlueBlockSpritesheet") },
            { nameof(SandyBlueBlock), content.Load<Texture2D>("SandyBlueBlockSpritesheet") },
            { nameof(LeftFacingStatueBlock), content.Load<Texture2D>("LeftFacingStatueBlockSpritesheet") },
            { nameof(RightFacingStatueBlock), content.Load<Texture2D>("RightFacingStatueBlockSpritesheet") },
            { nameof(WaterBlock), content.Load<Texture2D>("WaterBlockSpritesheet") },
            { nameof(WhiteBrickBlock), content.Load<Texture2D>("WhiteBrickBlockSpritesheet") },
            { nameof(WhiteStairsBlock), content.Load<Texture2D>("WhiteStairsBlockSpritesheet") },
            { nameof(PushableBlock), content.Load<Texture2D>("JewelCutBlueBlockSpritesheet") },
            { nameof(FireBlock), content.Load<Texture2D>("FireBlockSpritesheet") },
            { nameof(OldManBlock), content.Load<Texture2D>("OldManBlockSpritesheet") },

            //Backgrounds
            { "RoomBackground", content.Load<Texture2D>("RoomBackgroundSpritesheet") },
            { "PauseMenuBackground", content.Load<Texture2D>("PauseMenuBackgroundSpritesheet") },
            { "MapRoom", content.Load<Texture2D>("MapRoomSquareSpritesheet") },
            { "HorizontalRoomConnector", content.Load<Texture2D>("HorizontalRoomConnectorSpritesheet") },
            { "VerticalRoomConnector", content.Load<Texture2D>("VerticalRoomConnectorSpritesheet") },
            { "BlackBackground", content.Load<Texture2D>("BlackScreenSpritesheet") },
            { "CharacterSelect", content.Load<Texture2D>("CharacterSelectSpritesheet") },

            //Hud
            { "MainHud", content.Load<Texture2D>("MainHudSpritesheet") },
            { "ItemOutline", content.Load<Texture2D>("ItemOutlineSpritesheet") },
            { "DungeonMap", content.Load<Texture2D>("DungeonMapSpritesheet") },
            { "DungeonMap2", content.Load<Texture2D>("DungeonMap2Spritesheet") },
            { "TriforceIcon", content.Load<Texture2D>("TriforceMapIconsSpritesheet") },
            { "GameOverScreen", content.Load<Texture2D>("GameOverSpritesheet") },
            { "GameOverOptions", content.Load<Texture2D>("GameOverOptionsSpritesheet") },
            { "LinkMapDot", content.Load<Texture2D>("LinkMapDotSpritesheet") },

            { nameof(OpenDoorBlock), content.Load<Texture2D>("OpenDoorSpritesheet") },
            { nameof(LockedDoorBlock), content.Load<Texture2D>("LockedDoorSpritesheet") },
            { nameof(DiamondDoorBlock), content.Load<Texture2D>("DiamondDoorSpritesheet") },
            { nameof(BombableWallDoorBlock), content.Load<Texture2D>("BombableWallSpritesheet") },

            { nameof(Navi), content.Load<Texture2D>("NaviSpritesheet") },
            { nameof(Link), content.Load<Texture2D>("LinkSpritesheet") },
            { nameof(GelEnemy), content.Load<Texture2D>("GelSpritesheet") },
            { nameof(KeeseEnemy), content.Load<Texture2D>("KeeseSpritesheet") },
            { nameof(GoriyaEnemy), content.Load<Texture2D>("GoriyaSpritesheet") },
            { nameof(WallmasterEnemy), content.Load<Texture2D>("WallmasterSpritesheet") },
            { nameof(StalfosEnemy), content.Load<Texture2D>("StalfosSpritesheet") },
            { nameof(BladeTrapEnemy), content.Load<Texture2D>("BladeTrapSpritesheet") },
            { nameof(AquamentusEnemy), content.Load<Texture2D>("AquamentusSpritesheet") },
            { nameof(RopeEnemy), content.Load<Texture2D>("RopeSpritesheet") },
            { "DodongoNarrowEnemy", content.Load<Texture2D>("DodongoNarrowSpritesheet") },
            { "DodongoWideEnemy", content.Load<Texture2D>("DodongoWideSpritesheet") },
            { nameof(DeadObject), content.Load<Texture2D>("DeathSpritesheet") },

            /*
             * TODO: Refactor Item and Projects so every projectile has its own spritesheet.
             *
             * Every Creatable Object in my opinion should have its own spritesheet.Otherwise magic
             * numbers are gonna have to be used to create every texture.
             */
            { "Projectile", content.Load<Texture2D>("ProjectileSpritesheet") },
            { "Item", content.Load<Texture2D>("ItemSpritesheet") },
            { "Pickup", content.Load<Texture2D>("PickupSpritesheet") },
            { "Hadoken", content.Load<Texture2D>("HadokenSpritesheet") }
        };
    }

    public ITextureAtlasSprite CreateBlockSprite(Type T)
    {
        if (!_spritesheets.ContainsKey(T.Name)) throw new Exception("Invalid Type of SpriteSheet Requested.");
        return new TextureAtlasSprite(_spritesheets[T.Name], 16, 16);
    }

    public ITextureAtlasSprite CreatePlayerSprite()
    {
        return new TextureAtlasSprite(_spritesheets[nameof(Link)], 16, 16);
    }

    public ITextureAtlasSprite CreateNaviSprite()
    {
        return new TextureAtlasSprite(_spritesheets[nameof(Navi)], 16, 16);
    }

    public ITextureAtlasSprite CreateGelSprite()
    {
        return new TextureAtlasSprite(_spritesheets[nameof(GelEnemy)], 8, 16);
    }

    public ITextureAtlasSprite CreateKeeseSprite()
    {
        return new TextureAtlasSprite(_spritesheets[nameof(KeeseEnemy)], 16, 16);
    }

    public ITextureAtlasSprite CreateGoriyaSprite()
    {
        return new TextureAtlasSprite(_spritesheets[nameof(GoriyaEnemy)], 16, 16);
    }

    public ITextureAtlasSprite CreateWallmasterSprite()
    {
        return new TextureAtlasSprite(_spritesheets[nameof(WallmasterEnemy)], 16, 16);
    }

    public ITextureAtlasSprite CreateStalfosSprite()
    {
        return new TextureAtlasSprite(_spritesheets[nameof(StalfosEnemy)], 16, 16);
    }

    public ITextureAtlasSprite CreateBladeTrapSprite()
    {
        return new TextureAtlasSprite(_spritesheets[nameof(BladeTrapEnemy)], 16, 16);
    }

    public ITextureAtlasSprite CreateAquamentusSprite()
    {
        return new TextureAtlasSprite(_spritesheets[nameof(AquamentusEnemy)], 24, 32);
    }

    public ITextureAtlasSprite CreateRopeSprite()
    {
        return new TextureAtlasSprite(_spritesheets[nameof(RopeEnemy)], 16, 16);
    }

    public ITextureAtlasSprite CreateDeathSprite()
    {
        return new TextureAtlasSprite(_spritesheets[nameof(DeadObject)], 16, 16);
    }

    public ITextureAtlasSprite CreateTallProjectileSprite()
    {
        return new TextureAtlasSprite(_spritesheets["Projectile"], 8, 16);
    }

    public ITextureAtlasSprite CreateLargeProjectileSprite()
    {
        return new TextureAtlasSprite(_spritesheets["Projectile"], 16, 16);
    }

    public ITextureAtlasSprite CreateSmallItemSprite()
    {
        return new TextureAtlasSprite(_spritesheets["Item"], 8, 8);
    }

    public ITextureAtlasSprite CreateTallItemSprite()
    {
        return new TextureAtlasSprite(_spritesheets["Item"], 8, 16);
    }

    public ITextureAtlasSprite CreateLargeItemSprite()
    {
        return new TextureAtlasSprite(_spritesheets["Item"], 16, 16);
    }

    public ITextureAtlasSprite CreatePickupSprite()
    {
        return new TextureAtlasSprite(_spritesheets["Pickup"], 16, 16);
    }

    public ITextureAtlasSprite CreateRoomBackgroundSprite()
    {
        return new TextureAtlasSprite(_spritesheets["RoomBackground"], 256, 176);
    }

    public ITextureAtlasSprite CreatePauseMenuBackgroundSprite()
    {
        return new TextureAtlasSprite(_spritesheets["PauseMenuBackground"], 256, 176);
    }

    public ITextureAtlasSprite CreateMainHudSprite()
    {
        return new TextureAtlasSprite(_spritesheets["MainHud"], 256, 48);
    }

    public ITextureAtlasSprite CreateGameOverScreen()
    {
        return new TextureAtlasSprite(_spritesheets["GameOverScreen"], 256, 176);
    }

    public ITextureAtlasSprite CreateGameOverOptions()
    {
        return new TextureAtlasSprite(_spritesheets["GameOverOptions"], 256, 224);
    }

    public ITextureAtlasSprite CreateBlackScreen()
    {
        return new TextureAtlasSprite(_spritesheets["BlackBackground"], 256, 224);
    }

    public ITextureAtlasSprite CreateCharacterSelectScreen()
    {
        return new TextureAtlasSprite(_spritesheets["CharacterSelect"], 256, 224);
    }

    public ITextureAtlasSprite CreateItemOutlineSprite()
    {
        return new TextureAtlasSprite(_spritesheets["ItemOutline"], 16, 16);
    }

    public ITextureAtlasSprite CreateDungeonMapSprite()
    {
        return new TextureAtlasSprite(_spritesheets["DungeonMap"], 64, 32);
    }
    public ITextureAtlasSprite CreateDungeonMap2Sprite()
    {
        return new TextureAtlasSprite(_spritesheets["DungeonMap2"], 64, 32);
    }

    public ITextureAtlasSprite CreateTriforceIconSprite()
    {
        return new TextureAtlasSprite(_spritesheets["TriforceIcon"], 3, 3);
    }

    public ITextureAtlasSprite CreateLinkMapDotSprite()
    {
        return new TextureAtlasSprite(_spritesheets["LinkMapDot"], 3, 3);
    }

    public ITextureAtlasSprite CreateDoorSprite(Type T)
    {
        if (!_spritesheets.ContainsKey(T.Name)) throw new Exception("Invalid Type of SpriteSheet Requested.");
        return new TextureAtlasSprite(_spritesheets[T.Name], 32, 32);
    }

    public ITextureAtlasSprite CreateMapRoom()
    {
        return new TextureAtlasSprite(_spritesheets["MapRoom"], 6, 6);
    }

    public ITextureAtlasSprite CreateHorizontalRoomConnector()
    {
        return new TextureAtlasSprite(_spritesheets["HorizontalRoomConnector"], 1, 2);
    }

    public ITextureAtlasSprite CreateVerticalRoomConnector()
    {
        return new TextureAtlasSprite(_spritesheets["VerticalRoomConnector"], 2, 1);
    }

    public ITextureAtlasSprite CreateHadokenSprite()
    {
        return new TextureAtlasSprite(_spritesheets["Hadoken"], 16, 16);
    }

    public ITextureAtlasSprite CreateDodongoNarrowSprite()
    {
        return new TextureAtlasSprite(_spritesheets["DodongoNarrowEnemy"], 16, 16);
    }

    public ITextureAtlasSprite CreateDodongoWideSprite()
    {
        return new TextureAtlasSprite(_spritesheets["DodongoWideEnemy"], 32, 16);
    }
}