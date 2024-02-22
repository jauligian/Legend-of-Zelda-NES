using CSE3902.Enemies;
using CSE3902.Interfaces;
using CSE3902.Items;
using CSE3902.Projectiles;
using CSE3902.Shared.Managers;
using Microsoft.Xna.Framework;

namespace CSE3902.Collisions;

public class EnemyCollideWithProjectiles
{
    public static EnemyCollideWithProjectiles Instance = new();

    private EnemyCollideWithProjectiles()
    {
    }

    public void HandleCollisions(IEnemy enemy)
    {
        foreach (IPlayerProjectile projectile in PlayerProjectilesManager.Instance.ActiveProjectiles)
        {
            Rectangle intersect = Rectangle.Intersect(enemy.Hitbox, projectile.Hitbox);

            if (!intersect.IsEmpty /*&& enemy.GetType() != typeof(DamagedEnemy)*/)
            {
                //handle collisions
                //enemy.TakeDamage(1, global.InvertDirection(projectile.MovingDirection
                projectile.StruckSomething = true;

                if (projectile is BoomerangDown || projectile is BoomerangUp || projectile is BoomerangRight ||
                    projectile is BoomerangLeft)
                {
                    enemy.Freeze(120);
                    if (enemy is KeeseEnemy || enemy is GelEnemy || projectile.DamageAmount > 0)
                    {
                        enemy.TakeDamage(1, CollisionHelper.Instance.SquareCollidedFrom(intersect, enemy.Position));
                    }
                }

                else if (projectile is IBomb && enemy is DodongoEnemy)
                {
                    DodongoEnemy dodongo = (DodongoEnemy)enemy;
                    dodongo.Stun();
                }

                else
                {
                    enemy.TakeDamage(projectile.DamageAmount,
                        CollisionHelper.Instance.SquareCollidedFrom(intersect, enemy.Position));
                }
            }
        }
    }
}