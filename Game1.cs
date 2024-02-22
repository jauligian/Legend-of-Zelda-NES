using CSE3902.Collisions;
using CSE3902.Commands;
using CSE3902.Controllers;
using CSE3902.HUD;
using CSE3902.Interfaces;
using CSE3902.LevelLoading;
using CSE3902.Players;
using CSE3902.Shared;
using CSE3902.Shared.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace CSE3902;

public class Game1 : Game
{
    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private IKeyboardController _controller;
    private MouseController _mouse;
    private IKeyboardController _menuController;
    private IGamePadController _gamePadController;
    private IGamePadController _gamePadMenuController;

    public Dungeon[] Dungeons;
    public int CurrentDungeon { get; set; } = 0;

    public IPlayer Player { get; set; }

    public Navi NaviEntity { get; set; }

    private SpriteFont _hudFont;
    private MainHud _mainHud;
    public PauseHud PauseHud;
    public GameOverScreens GameOverScreen;
    public WinScreen WinScreen;
    public CharacteSelect CharacterSelect;

    public int Paused;
    public bool SelectingCharacter;
    public bool GameLose;
    public bool GameWin;

    private ICollisionManager _playerCollisionManager;
    private ICollisionManager _enemyCollisionManager;
    private ICollisionManager _projectileCollisionManager;
    private ICollisionManager _detectorCollisionManager;
    private ICollisionManager _naviCollisionManager;

    private ITextureAtlasSprite _pauseMenuBackground;
    private ITextureAtlasSprite _mainHudBackground;

    private Map _visitedMap;

    public bool ContinueGame = false;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        _graphics.PreferredBackBufferWidth = 256 * Globals.GlobalSizeMult;
        _graphics.PreferredBackBufferHeight = 224 * Globals.GlobalSizeMult;
        _graphics.ApplyChanges();
    }

    public void Reset()
    {
        SoundFactory.Instance.StopBoomerangFly();
        SoundFactory.Instance.StopLowHealth();
        SoundFactory.Instance.StopTextWriting();
        SoundFactory.Instance.StopBackgroundMusic();
        GameObjectManagers.RemoveAllGameObjects();

        Initialize();
    }

    protected override void Initialize()
    {
        /*
         * Force the game to update at fixed time intervals
         * - taken from https://stackoverflow.com/questions/54076446/xna-4-0-c-sharp-limit-fps
         */
        IsFixedTimeStep = true;
        TargetElapsedTime = TimeSpan.FromSeconds(1 / 60.0f); //Set the refresh rate to 60fps

        Paused = 0;
        GameLose = false;
        GameWin = false;
        SelectingCharacter = false;

        _controller = new KeyboardController();
        _menuController = new KeyboardMenuController();
        _mouse = new MouseController();
        _gamePadController = new GamePadController();
        _gamePadMenuController = new GamePadMenuController();

        ControllerHelper.RegisterStandardCommands(_controller, this);
        ControllerHelper.RegisterHudCommands(_menuController, this);
        ControllerHelper.RegisterGamePadCommands(_gamePadController, this);
        ControllerHelper.RegisterGamePadHudCommands(_gamePadMenuController, this);

        _mouse.RegisterLeftCommand(new PreviousRoomCommand(this));
        _mouse.RegisterRightCommand(new NextRoomCommand(this));

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        ITextureAtlasSprite.SpriteBatch = _spriteBatch;
        SpritesheetFactory.Instance.LoadAllTextures(Content);
        SoundFactory.Instance.LoadAllSounds(Content);
        SoundFactory.Instance.PlayBackgroundMusic();

        Dungeons = new Dungeon[2];
        Dungeons[0] = new Dungeon("Content/ZeldaDungeon_1.csv");
        Dungeons[1] = new Dungeon("Content/ZeldaDungeon_2.csv");
        if (CurrentDungeon >= 2)
        {
            CurrentDungeon = 0;
            Reset();
        }

        Dungeons[CurrentDungeon].StartActiveRoom(CurrentDungeon);

        _pauseMenuBackground = SpritesheetFactory.Instance.CreatePauseMenuBackgroundSprite();
        _mainHudBackground = SpritesheetFactory.Instance.CreateMainHudSprite();

        IPlayer savedPlayer = Player;
        Player = new Link(this);
        if (ContinueGame)
        {
            Player.Inventory = savedPlayer.Inventory;
            Player.MaxPlayerHealth = savedPlayer.MaxPlayerHealth;
            Player.PlayerHealth = savedPlayer.MaxPlayerHealth;
        }

        Player.XPosition = (int)Dungeons[CurrentDungeon].CurrentRoomPosition.X +
                           116 * Globals.GlobalSizeMult;
        Player.YPosition =
            (int)Dungeons[CurrentDungeon].CurrentRoomPosition.Y + 130 * Globals.GlobalSizeMult;

        //CHANGE TO SOME SPECIAL ACTIVATION
        NaviEntity = new Navi(this);

        _playerCollisionManager = new PlayerCollisionManager(this, Dungeons[CurrentDungeon]);
        _enemyCollisionManager = new EnemyCollisionManager();
        _projectileCollisionManager = new ProjectileCollisionManager();
        _naviCollisionManager = new NaviCollisionManager(NaviEntity);
        _detectorCollisionManager = new TripwireCollisionManager(this);

        _hudFont = Content.Load<SpriteFont>("File");

        _visitedMap = new Map(Dungeons[CurrentDungeon]);
        _mainHud = new MainHud(this, _hudFont);
        PauseHud = new PauseHud(this);
        GameOverScreen = new GameOverScreens();
        WinScreen = new WinScreen(this);
        CharacterSelect = new CharacteSelect(this);
        ContinueGame = false;
    }

    protected override void Update(GameTime gameTime)
    {
        _controller.Update();
        _menuController.Update();
        _gamePadController.Update();
        _mouse.Update();
        _gamePadMenuController.Update();
        if (!GameLose && !GameWin)
        {
            _mainHud.Update();
            if (Paused == 0 && !SelectingCharacter)
            {
                Player.Update();
                NaviEntity.Update();
                Dungeons[CurrentDungeon].Update();

                _detectorCollisionManager.HandleCollisions();
                _playerCollisionManager.HandleCollisions();
                _enemyCollisionManager.HandleCollisions();
                _projectileCollisionManager.HandleCollisions();
                _naviCollisionManager.HandleCollisions();
                _visitedMap.Update();
            }
            else if (Paused == 1 && !SelectingCharacter) PauseHud.Update();
            else CharacterSelect.Update();
        }

        if (GameLose) GameOverScreen.Update();
        if (GameWin) WinScreen.Update();
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        if (!GameLose)
        {
            if (Paused == 0 && !SelectingCharacter)
            {
                Matrix transform = Dungeons[CurrentDungeon].CurrentViewportTransition(Player);
                _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: transform);

                Dungeons[CurrentDungeon].Draw();
                Player.Draw();
                NaviEntity.Draw();

                _spriteBatch.End();
            }
            else if (Paused == 1 && !SelectingCharacter)
            {
                _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
                _pauseMenuBackground.Draw(new Vector2(0, 0));
                PauseHud.Draw();
                _visitedMap.Draw();
                _spriteBatch.End();
            }
            else
            {
                _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
                CharacterSelect.Draw();
                _spriteBatch.End();
            }
        }

        _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
        if (GameWin) WinScreen.Draw();
        _mainHudBackground.Draw(new Vector2(0, (224 * Globals.GlobalSizeMult - Globals.HudOffset) * Paused));
        _mainHud.Draw(new Vector2(0, (224 * Globals.GlobalSizeMult - Globals.HudOffset) * Paused));
        if (GameLose) GameOverScreen.Draw();
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}