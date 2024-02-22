namespace CSE3902.Interfaces
{
    public interface IEnemyProjectile : ICollidable, IGameObject
    {
        public bool StruckSomething { get; set; }
        public int DamageAmount { get; set; }
    }
}