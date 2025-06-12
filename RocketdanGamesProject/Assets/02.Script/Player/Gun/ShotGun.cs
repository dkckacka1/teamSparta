using UnityEngine;

namespace RocketdanGamesProject.Player.Gun
{
    // 샷건 형태의 총기 클래스
    public class ShotGun : Gun
    {
        [SerializeField] private float shotRadius = 1f;
        [SerializeField] private int bulletCount = 5;
        [SerializeField] private float minBulletSpeed = 4f;
        [SerializeField] private float maxBulletSpeed = 10f;

        public override void ShotBullet(Vector3 targetPosition)
        {
            for (int i = 0; i < bulletCount; ++i)
            {
                var bullet = GetBullet.Invoke();
                bullet.transform.position = gunShotPoint.transform.position;
                bullet.SetTarget(GetRandomRadiusTargetPosition(new Vector3(targetPosition.x, targetPosition.y, 0)),
                    damage, Random.Range(minBulletSpeed, maxBulletSpeed));
            }
        }

        // 원형 위치 랜덤 반환
        private Vector3 GetRandomRadiusTargetPosition(Vector3 targetPosition)
        {
            var randomRadiusPos = shotRadius * Random.insideUnitCircle;
            return targetPosition + (Vector3)randomRadiusPos;
        }
    }
}