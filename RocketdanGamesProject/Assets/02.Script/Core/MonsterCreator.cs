using System;
using RocketdanGamesProject.Core.ObjectPool;
using RocketdanGamesProject.Enemy;
using UnityEngine;

namespace RocketdanGamesProject.Core.Creator
{
    // 몬스터 생성 클래스
    public class MonsterCreator : MonoBehaviour
    {
        public enum SpawnType
        {
            Top = 0,
            Middle,
            Bottom
        }

        [SerializeField] private PoolObject monsterPoolObject;

        [SerializeField] private Transform monsterSpawnParent;

        [SerializeField] private Transform topSpawnPoint;
        [SerializeField] private Transform middleSpawnPoint;
        [SerializeField] private Transform bottomSpawnPoint;

        private Func<Monster> _getMonster = null;

        private const string MonsterPoolName = "monster";

        private void Awake()
        {
            ObjectPoolManager.Instance.CreatePool(MonsterPoolName, monsterPoolObject, monsterSpawnParent);
            _getMonster = () => ObjectPoolManager.Instance.Get<Monster>(MonsterPoolName);
        }

        public void CreateMonster(SpawnType spawnType)
        {
            var monster = _getMonster?.Invoke();
            if (monster)
            {
                monster.transform.position = GetSpawnPoint(spawnType).position;

                BattleManager.Instance.AddMonster(monster);
            }
        }

        private Transform GetSpawnPoint(SpawnType spawnType)
        {
            return spawnType switch
            {
                SpawnType.Top => topSpawnPoint,
                SpawnType.Middle => middleSpawnPoint,
                SpawnType.Bottom => bottomSpawnPoint,
                _ => throw new ArgumentOutOfRangeException(nameof(spawnType), spawnType, null)
            };
        }
    }
}