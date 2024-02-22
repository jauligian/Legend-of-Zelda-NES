using CSE3902.Shared;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace CSE3902.Interfaces;

public interface IEnemy : IGameObject
{
    public void TakeDamage(int damageAmount, Direction damageDirection);
    public void UpdateSprite();
    public void Die();
    public void UpdateHitbox();
    public void Freeze(int ticks);
    public void Unfreeze();
    public void HitEnemy(IEnemy enemy);
    public void HitPlayer(IPlayer player);
    public void HitBlock(IBlock block);
    public void HitDoor(IDoor door);
    public void SetDirection(Direction direction);
    public bool Frozen { get; }
    public List<Rectangle> GetTripwires();
    public void PlayerActivateTripwire(Rectangle r);
    public Rectangle Hitbox { get; }
    public int DamageAmount { get; }
    public Direction MovingDirection { get; }
    public int Health { get; set; }
    public int InvulnerableTime { get; set; }
    public bool Controlled { get; set; }
    public Direction DamagedFrom { get; set; }
    public int StepSize { get; set; }
}