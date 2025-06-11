using System;
using System.Collections;
using System.Collections.Generic;
using RocketdanGamesProject.Battle;
using RocketdanGamesProject.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace RocketdanGamesProject.Player
{
    // 총알 기본 클래스
    public class Bullet : MonoBehaviour
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
            Destroy(this.gameObject);
        }
    }
}