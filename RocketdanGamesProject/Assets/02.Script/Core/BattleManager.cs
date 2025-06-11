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

        public readonly List<Monster> _monsterList = new();

        private MonsterCreator monsterCreator;
        
        
        public const string HeroTag = "Hero";
        public const string MonsterTag = "Monster";

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
                await UniTask.WaitForSeconds(1);
            }
        }
        

        public void AddMonster(Monster monster)
        {
            _monsterList.Add(monster);
        }

        public void RemoveMonster(Monster monster)
        {
            _monsterList.Remove(monster);
        }

        public Monster GetMonster(Func<Monster, bool> monsterFunc)
        {
            return _monsterList.Where(monsterFunc).Single();
        }
    }
}