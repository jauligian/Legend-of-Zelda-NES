using CSE3902.Commands;
using CSE3902.Enemies;
using CSE3902.Interfaces;
using CSE3902.Items;
using Microsoft.Xna.Framework.Input;

namespace CSE3902.Shared;

public static class ItemGenerator
{
    public static IItem GenerateDrop(IEnemy enemy)
    {
        System.Random Rand = new();
        int randomInt = Rand.Next(100);
        IItem item = null;
        if(enemy is StalfosEnemy || enemy is RopeEnemy)
        {
            if(randomInt < 59 - 1) {
                randomInt = Rand.Next(10);
                if (randomInt >= 9) item = new ClockItem(enemy.Position);
                else if (randomInt >= 7) item = new BlueRupeeItem(enemy.Position);
                else if (randomInt >= 5) item = new HeartItem(enemy.Position);
                else item = new RupeeItem(enemy.Position);
            }
        }
        else if (enemy is GoriyaEnemy)
        {
            if (randomInt < 41 - 1)
            {
                randomInt = Rand.Next(10);
                if (randomInt >= 9) item = new ClockItem(enemy.Position);
                else if (randomInt >= 6) item = new BombItem(enemy.Position);
                else if (randomInt >= 3) item = new HeartItem(enemy.Position);
                else item = new RupeeItem(enemy.Position);
            }
        }
        else if (enemy is AquamentusEnemy || enemy is DodongoEnemy)
        {
            if (randomInt < 59 - 1)
            {
                randomInt = Rand.Next(10);
                if (randomInt >= 8) item = new FairyItem(enemy.Position);
                else if (randomInt >= 6) item = new RupeeItem(enemy.Position);
                else item = new HeartItem(enemy.Position);
            }
        }
        return item;
    }
}