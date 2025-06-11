using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using RocketdanGamesProject.Core.Creator;
using RocketdanGamesProject.Enemy;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RocketdanGamesProject.Core
{
    // 게임 매니저 클래스
    public class BattleManager : SingletonMono<BattleManager>
    {
        public const string HeroTag = "Hero";
        public const string MonsterTag = "Monster";
        public const string GroundTag = "Ground";
        public const string ReleaseTag = "Release";
        
        public readonly List<Monster> MonsterList = new();

        private MonsterCreator monsterCreator;

        [SerializeField] private float spawnDelay = 1f;

        protected override void Initialize()
        {
            base.Initialize();
            monsterCreator = GetComponent<MonsterCreator>();
        }

        private void Start()
        {
            SpawnStart().Forget();    
        }

        private async UniTaskVoid SpawnStart()
        {
            while (true)
            {
                var randomPos = Random.Range(0, 3);
                monsterCreator.CreateMonster((MonsterCreator.SpawnType)randomPos);
                await UniTask.WaitForSeconds(spawnDelay);
            }
        }
        

        public void AddMonster(Monster monster)
        {
            MonsterList.Add(monster);
        }

        public void RemoveMonster(Monster monster)
        {
            MonsterList.Remove(monster);
        }
    }
}