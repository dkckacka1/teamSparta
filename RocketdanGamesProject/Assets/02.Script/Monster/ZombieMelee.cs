namespace RocketdanGamesProject.Enemy
{
    // 근접 좀비 몬스터 클래스
    public class ZombieMelee : Monster
    {
        public override void Hit()
        {
            GetTarget?.Invoke().TakeDamage(hitDamage);
        }
    }
}