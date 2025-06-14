using System;
using RocketdanGamesProject.Core;
using RocketdanGamesProject.Core.ObjectPool;
using UnityEngine;

namespace RocketdanGamesProject.UI
{
    // UI 매니저 클래스
    public class UIManager : SingletonMono<UIManager>
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private PoolObject battleTextPrefab;

        private Func<BattleText> _getBattleText;
        
        private const string BattleTextPoolName = "BattleText"; 

        protected override void Initialize()
        {
            base.Initialize();
            ObjectPoolManager.Instance.CreatePool(BattleTextPoolName, battleTextPrefab, canvas.transform);
            _getBattleText = () => ObjectPoolManager.Instance.Get<BattleText>(BattleTextPoolName);
        }

        public void ShowBattleText(string text, Vector3 spawnPosition)
        {
            var battleText = _getBattleText?.Invoke();

            if (battleText)
            {
                var textPosition = Camera.main.WorldToScreenPoint(spawnPosition);
                battleText.transform.position = textPosition;
                battleText.SetText(text);
            }
        }
    }
}