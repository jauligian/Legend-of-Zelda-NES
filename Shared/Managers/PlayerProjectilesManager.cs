using CSE3902.Interfaces;
using CSE3902.Items;
using CSE3902.Projectiles;
using System;
using System.Collections.Generic;

namespace CSE3902.Shared.Managers
{
    public class PlayerProjectilesManager
    {
        public static PlayerProjectilesManager Instance { get; } = new();
        private PlayerProjectilesManager() { }
        private readonly List<Type> _activeProjectilesTypes = new();
        public List<IPlayerProjectile> ActiveProjectiles { get; } = new();

        public bool TrySpawnProjectile(Type type)
        {
            bool canSpawnProjectile = !_activeProjectilesTypes.Contains(type);
            if (canSpawnProjectile && type != typeof(EmptyItem))
            {
                _activeProjectilesTypes.Add(type);
            }
            return canSpawnProjectile;
        }
        public void SpawnProjectile(IPlayerProjectile projectile)
        {
            ActiveProjectiles.Add(projectile);
            projectile.Use();
        }

        public void UpdateProjectiles()
        {
            foreach (IPlayerProjectile activeProjectile in ActiveProjectiles)
            {
                if (!activeProjectile.Active)
                {
                    _activeProjectilesTypes.Remove(TypeOfProjectile(activeProjectile));//This seems really weird.
                }
            }
            ActiveProjectiles.RemoveAll(p => !p.Active);
            ActiveProjectiles.ForEach(p => p.Update());
        }

        public void DrawProjectiles()
        {
            ActiveProjectiles.ForEach(p => p.Draw());
        }

        private Type TypeOfProjectile(IPlayerProjectile projectile)
        {
            Type typeOfProjectile = typeof(EmptyItem);

            switch (projectile)
            {
                case IArrow:
                    typeOfProjectile = typeof(IArrow);
                    break;
                case IBoomerang:
                    typeOfProjectile = typeof(IBoomerang);
                    break;
                case IBomb:
                    typeOfProjectile = typeof(IBomb);
                    break;
                case WoodenSwordDownProjectile:
                case WoodenSwordUpProjectile:
                case WoodenSwordLeftProjectile:
                case WoodenSwordRightProjectile:
                    typeOfProjectile = typeof(WoodenSwordProjectile);
                    break;
                case Hadoken:
                    typeOfProjectile = typeof(Hadoken);
                    break;
            }

            return typeOfProjectile;
        }

        public void RemoveAll()
        {
            _activeProjectilesTypes.RemoveAll(t => true);
            ActiveProjectiles.RemoveAll(t => true);
        }
    }
}
