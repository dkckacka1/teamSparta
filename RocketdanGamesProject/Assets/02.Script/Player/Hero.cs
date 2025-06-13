using System.Linq;
using Cysharp.Threading.Tasks;
using RocketdanGamesProject.Battle;
using RocketdanGamesProject.Core;
using RocketdanGamesProject.Enemy;
using RocketdanGamesProject.Player.Weapon;
using UnityEngine;

namespace RocketdanGamesProject.Player
{
    // 플레이어 영웅 클래스
    public class Hero : MonoBehaviour,ITakeDamageable
    {
        [SerializeField] private Gun gun;

        private Monster _targetMonster;
        private const float GunRotationOffset = 30f;

        private void Start()
        {
            ShotStart().Forget();
        }

        private void FixedUpdate()
        {
            _targetMonster = BattleManager.Instance.MonsterList.OrderBy(monster => Vector3.Distance(this.transform.position, monster.transform.position)).FirstOrDefault();

            if (_targetMonster is not null)
            {
                GunRotate(_targetMonster);
            }
        }

        // 총 회전
        private void GunRotate(Monster monster)
        {
            var targetPos = new Vector3(monster.transform.position.x, monster.transform.position.y, 0);
            var gunPos = gun.transform.position;
                
            Vector2 dir = targetPos - gunPos;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - GunRotationOffset;
            gun.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        private async UniTaskVoid ShotStart()
        {
            while (true)
            {
                if (_targetMonster is not null)
                {
                    gun.ShotBullet(_targetMonster.transform.position);
                }

                await UniTask.WaitForSeconds(gun.shotDelay);
            }
        }
        
        public void TakeDamage(float damage)
        {
        }

        public void Dead()
        {
            
        }
    }
}