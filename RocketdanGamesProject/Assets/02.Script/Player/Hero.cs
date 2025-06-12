using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using RocketdanGamesProject.Battle;
using RocketdanGamesProject.Core;
using RocketdanGamesProject.Core.ObjectPool;
using RocketdanGamesProject.Enemy;
using UnityEngine;

namespace RocketdanGamesProject.Player
{
    // 플레이어 영웅 클래스
    public class Hero : MonoBehaviour,ITakeDamageable
    {
        [SerializeField] private Transform gun;
        [SerializeField] private Transform gunShotPoint;
        [SerializeField] private float shotAngle = 30f;
        [SerializeField] private PoolObject bulletPrefab;

        public float damage;
        public float shotDelay = 0.5f;
        
        private Monster nearlyMonster;

        private Func<Bullet> _getBullet = null;

        private const float GunRotationOffset = 30f;
        private const string BulletPoolName = "Bullet";

        private void Awake()
        {
            ObjectPoolManager.Instance.CreatePool(BulletPoolName, bulletPrefab, GameObject.Find("Projectile").transform);
            _getBullet = () => ObjectPoolManager.Instance.Get<Bullet>(BulletPoolName);
        }

        private void Start()
        {
            ShotStart().Forget();
        }

        private void FixedUpdate()
        {
            nearlyMonster = BattleManager.Instance.MonsterList.OrderBy(monster => Vector3.Distance(this.transform.position, monster.transform.position)).FirstOrDefault();

            if (nearlyMonster is not null)
            {
                GunRotate(nearlyMonster);
            }
        }

        // 총 회전
        private void GunRotate(Monster nearlyMonster)
        {
            var targetPos = new Vector3(nearlyMonster.transform.position.x, nearlyMonster.transform.position.y, 0);
            var gunPos = gun.transform.position;
                
            Vector2 dir = targetPos - gunPos;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - GunRotationOffset;
            gun.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        private async UniTaskVoid ShotStart()
        {
            while (true)
            {
                if (nearlyMonster is not null)
                {
                    ShotBullet(nearlyMonster.transform.position);
                }

                await UniTask.WaitForSeconds(shotDelay);
            }
        }

        private void ShotBullet(Vector3 targetPosition)
        {
            var bullet = _getBullet.Invoke();
            bullet.transform.position = gunShotPoint.transform.position;
            bullet.SetTarget(targetPosition, damage);
        }

        public void TakeDamage(float damage)
        {
        }

        public void Dead()
        {
            
        }
    }
}