using UnityEngine;

namespace RocketdanGamesProject.Core
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        public static GameManager Instance
        {
            get
            {
                if (_instance is null)
                {
                    var obj = new GameObject
                    {
                        name = nameof(GameManager)
                    };
                    var ins = obj.AddComponent<GameManager>();
                    _instance = ins; 
                }

                return _instance;
            }
        }

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
    }
}