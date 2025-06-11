using System;
using System.Collections.Generic;
using System.Linq;
using RocketdanGamesProject.Enemy;
using UnityEngine;

namespace RocketdanGamesProject.Core
{
    // 게임 매니저 클래스
    public class BattleManager : MonoBehaviour
    {
        private static BattleManager _instance;
        public static BattleManager Instance
        {
            get
            {
                if (_instance is null)
                {
                    var obj = new GameObject
                    {
                        name = nameof(BattleManager)
                    };
                    var ins = obj.AddComponent<BattleManager>();
                    _instance = ins; 
                }

                return _instance;
            }
        }

        public readonly List<Monster> _monsterList = new();   
        
        
        public const string HeroTag = "Hero";
        public const string MonsterTag = "Monster"; 
        
        private void Awake()
        {
            if (_instance is not null)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
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