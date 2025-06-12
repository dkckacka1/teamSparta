using RocketdanGamesProject.Battle;
using RocketdanGamesProject.Core;
using RocketdanGamesProject.Core.ObjectPool;
using UnityEngine;

namespace RocketdanGamesProject.Player
{
    // 총알 기본 클래스
    public class Bullet : MonoBehaviour, IPoolable
    {
        [HideInInspector] public float bulletDamage;
        public float bulletSpeed = 5f;

        private Vector3 _bulletDir;

        private void FixedUpdate()
        {
            transform.Translate(_bulletDir * (bulletSpeed * Time.deltaTime));
        }

        public void SetTarget(Vector3 targetPosition, float damage)
        {
            bulletDamage = damage;
            _bulletDir = (targetPosition - transform.position).normalized;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(BattleManager.MonsterTag))
            {
                var takeDamageAble = other.GetComponent<ITakeDamageable>();

                if (takeDamageAble is not null)
                {
                    takeDamageAble.TakeDamage(bulletDamage);
                    Release();
                }
            }

            if (other.CompareTag(BattleManager.ReleaseTag))
            {
                Release();
            }
        }

        private void Release()
        {
            var poolObject = GetComponent<PoolObject>();
            ObjectPoolManager.Instance.Release(poolObject.poolName, poolObject);
        }

        public void OnCreate()
        {
            
        }

        public void OnGet()
        {
        }

        public void OnRelease()
        {
        }
    }
}