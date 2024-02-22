using CSE3902.Collisions;
using CSE3902.Interfaces;
using CSE3902.Inventory;
using CSE3902.Shared;
using CSE3902.Shared.Managers;
using Microsoft.Xna.Framework;

namespace CSE3902.Players;

public class Navi : IGameObject
{
    private Game1 _game;
    public ITextureAtlasSprite CurrentSprite { get; set; }
    public Rectangle Hitbox { get; private set; }
    public PlayerInventory Inventory { get; set; }
    public Vector2 Position { get; set; }
    public Direction MovingDirection { get; set; }
    public bool Active { get; set; }

    private const int Height = 16 * Globals.GlobalSizeMult;
    private const int Width = 16 * Globals.GlobalSizeMult;
    public const int StepSize = (int)(1.5 * Globals.GlobalSizeMult);

    private int _spriteTimer;
    private int _spriteState;

    private IEnemy _controlledEnemy;

    public Navi(Game1 game)
    {
        _game = game;
        Position = new Vector2(_game.Player.XPosition, _game.Player.YPosition - Globals.HudOffset + Globals.HudOffset);
        _spriteTimer = 0;
        _spriteState = 1;
        CurrentSprite = SpritesheetFactory.Instance.CreateNaviSprite();
        Active = true;
        _controlledEnemy = null;
    }

    public void ControlEnemy()
    {
        _controlledEnemy =
            NaviCollideWithEnemies.Instance.CheckForCollision(this, GameObjectManagers.EnemyManager.ActiveGameObjects);
    }

    public void ReleaseEnemy()
    {
        if (_controlledEnemy != null)
        {
            _controlledEnemy.Controlled = false;
            _controlledEnemy = null;
        }
    }

    public void Draw()
    {
        if (_controlledEnemy == null) CurrentSprite.DrawFromCenter(Position);
    }

    public void MoveLeft()
    {
        if (_controlledEnemy != null)
        {
            _controlledEnemy.SetDirection(Direction.Left);
            _controlledEnemy.Position = new Vector2(Position.X - _controlledEnemy.StepSize, Position.Y);
        }
        else
        {
            Position = new Vector2(Position.X - StepSize, Position.Y);
            MovingDirection = Direction.Left;
        }
    }

    public void MoveRight()
    {
        if (_controlledEnemy != null)
        {
            _controlledEnemy.SetDirection(Direction.Right);
            _controlledEnemy.Position = new Vector2(Position.X + _controlledEnemy.StepSize, Position.Y);
        }
        else
        {
            Position = new Vector2(Position.X + StepSize, Position.Y);
            MovingDirection = Direction.Right;
        }
    }

    public void MoveUp()
    {
        if (_controlledEnemy != null)
        {
            _controlledEnemy.SetDirection(Direction.Up);
            _controlledEnemy.Position = new Vector2(Position.X, Position.Y - _controlledEnemy.StepSize);
        }
        else
        {
            Position = new Vector2(Position.X, Position.Y - StepSize);
            MovingDirection = Direction.Up;
        }
    }

    public void MoveDown()
    {
        if (_controlledEnemy != null)
        {
            _controlledEnemy.SetDirection(Direction.Down);
            _controlledEnemy.Position = new Vector2(Position.X, Position.Y + _controlledEnemy.StepSize);
        }
        else
        {
            Position = new Vector2(Position.X, Position.Y + StepSize);
            MovingDirection = Direction.Down;
        }
    }

    public void InitializeGlobalPosition(int horizontalOffset, int verticalOffset)
    {
        Position = new Vector2(Position.X + horizontalOffset,
            Position.Y + verticalOffset);
        UpdateHitbox();
    }

    public void UpdateHitbox()
    {
        Hitbox = new Rectangle((int)Position.X - Width / 2 - 8, (int)Position.Y - Height / 2 - 8, Height, Width);
    }

    public void Update()
    {
        if (_controlledEnemy != null)
        {
            Position = _controlledEnemy.Position;
            if (_controlledEnemy != null && _controlledEnemy.Active == false)
            {
                _controlledEnemy = null;
            }
        }
        else
        {
            _spriteTimer--;
            if (_spriteTimer <= 0)
            {
                if (_spriteState == 1) _spriteState = 2;
                else if (_spriteState == 2) _spriteState = 1;
                _spriteTimer = 8;
                CurrentSprite.SetFrame(1, _spriteState);
            }
        }

        UpdateHitbox();
    }
}