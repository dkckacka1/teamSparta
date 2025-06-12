using System;
using RocketdanGamesProject.Core.ObjectPool;
using UnityEngine;

namespace RocketdanGamesProject.Player.Gun
{
    // 총기의 기본 클래스
    public abstract class Gun : MonoBehaviour
    {
        [SerializeField] protected Transform gunShotPoint;
        [SerializeField] protected PoolObject bulletPrefab;
        
        public float damage;
        public float shotDelay = 0.5f;
        
        protected Func<Bullet> GetBullet = null;
        
        private const string BulletPoolName = "Bullet";

        private void Awake()
        {
            ObjectPoolManager.Instance.CreatePool(BulletPoolName, bulletPrefab, GameObject.Find("Projectile").transform);
            GetBullet = () => ObjectPoolManager.Instance.Get<Bullet>(BulletPoolName);
        }

        public abstract void ShotBullet(Vector3 targetPosition);
    }
}
