using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using RocketdanGamesProject.Battle;
using RocketdanGamesProject.Core;
using RocketdanGamesProject.Enemy;
using UnityEngine;
using UnityEngine.Serialization;

namespace RocketdanGamesProject.Player
{
    // 플레이어 영웅 클래스
    public class Hero : MonoBehaviour,ITakeDamageable
    {
        [SerializeField] private Transform gun;
        [SerializeField] private Transform gunShotPoint;
        [SerializeField] private float shotAngle = 30f;
        [SerializeField] private Bullet bulletPrefab;

        public float damage;
        public float shotDelay = 0.5f;
        
        private Transform projectileParentTransform;
        private Monster nearlyMonster;

        private const float GunRotationOffset = 30f;

        private void Awake()
        {
            projectileParentTransform = GameObject.Find("Projectile").transform;
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
            var bullet = Instantiate(bulletPrefab, projectileParentTransform);
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