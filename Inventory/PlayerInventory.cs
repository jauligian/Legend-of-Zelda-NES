using CSE3902.Interfaces;
using CSE3902.Items;
using System;
using CSE3902.Shared.Managers;
using CSE3902.Shared;
using CSE3902.Projectiles;

namespace CSE3902.Inventory;

public class PlayerInventory
{
    public bool HasBow { get; set; } = true;
    public bool HasBoomerang { get; set; } = false;
    public bool PickedUpBoomerang { get; set; } = false;
    public bool HasMagicBoomerang { get; set; } = false;
    public bool HasArrow { get; set; } = true;
    public bool HasMap { get; set; } = false;
    public bool HasCompass { get; set; } = false;
    public int Bombs { get; set; } = 2;
    public int Rupees { get; set; } = 3;
    public int Keys { get; set; } = 0;

    private readonly Game1 _game;

    public PlayerInventory(Game1 game)
    {
        _game = game;
    }

    public void AddItem(IItem item)
    {
        switch (item)
        {
            case ArrowItem _:
                HasArrow = true; 
                break;
            case BowItem _:
                HasBow = true;
                break;
            case BoomerangItem _:
                PickedUpBoomerang = true;
                HasBoomerang = true;
                break;
            case BlueBoomerangItem _:
                HasMagicBoomerang = true;
                HasBoomerang = true;
                break;
            case BombItem _:
                Bombs++;
                break;
            case RupeeItem _:
                Rupees++; 
                break;
            case BlueRupeeItem _:
                Rupees += 5;
                break;
            case KeyItem _:
                Keys++;
                break;
            case TriforcePieceItem _:
                _game.GameWin = true;
                SoundFactory.Instance.StopBackgroundMusic();
                SoundFactory.Instance.PlayTriforcePickup();
                break;
            case ClockItem _:
                foreach (IEnemy enemy in GameObjectManagers.EnemyManager.ActiveGameObjects)
                {
                    enemy.Freeze(100000);
                }
                break;
            case CompassItem _:
                HasCompass = true;
                break;
            case FairyItem _:
                _game.Player.PlayerHealth += 3;
                SoundFactory.Instance.StopLowHealth();
                break;
            case HeartContainerItem _:
                _game.Player.MaxPlayerHealth += 2;
                _game.Player.PlayerHealth = _game.Player.MaxPlayerHealth;
                SoundFactory.Instance.StopLowHealth();
                break;
            case HeartContainerRewardItem _:    
                _game.Player.MaxPlayerHealth += 2;
                _game.Player.PlayerHealth = _game.Player.MaxPlayerHealth;
                SoundFactory.Instance.StopLowHealth();
                break;
            case HeartItem _:
                _game.Player.PlayerHealth += 2;
                if (_game.Player.PlayerHealth > _game.Player.MaxPlayerHealth) _game.Player.PlayerHealth = _game.Player.MaxPlayerHealth;
                SoundFactory.Instance.StopLowHealth();
                break;
            case MapItem _:
                HasMap = true;
                break;
            default:
                break;
        }
    }

    public bool CanUseItem(Type type)
    {
        Type currentItem = _game.Player.CurrentItemType;
        if (type == typeof(IBoomerang) && currentItem == typeof(BoomerangItem)) return true;
        if (type == typeof(IBomb) && Bombs > 0 && currentItem == typeof(BombItem)) return true;
        if (type == typeof(IArrow) && Rupees > 0 && currentItem == typeof(ArrowItem)) return true;
        if (type == typeof(Hadoken)) return true;
        return false;
    }
}