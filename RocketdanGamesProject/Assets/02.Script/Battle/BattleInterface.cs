namespace RocketdanGamesProject.Battle
{
    // 공격 할 수 있음
    public interface IHitDamageable
    {
        
    }
    
    // 공격 받을 수 있음
    public interface ITakeDamageable
    {
        public void TakeDamage(float damage);
    }
}