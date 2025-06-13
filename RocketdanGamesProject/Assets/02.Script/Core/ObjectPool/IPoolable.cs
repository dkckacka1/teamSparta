namespace RocketdanGamesProject.Core.ObjectPool
{
    // 풀링 전용 인터페이스
    public interface IPoolable
    {
        public void OnCreate();
        public void OnGet();
        public void OnRelease();
    }
}