namespace RocketdanGamesProject.Battle
{
    // 공격 받을 수 있음
    public interface ITakeDamageable
    {
        public void TakeDamage(float damage);

        public void Dead();
    }
}