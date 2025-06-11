using RocketdanGamesProject.Core;
using UnityEngine;

namespace RocketdanGamesProject.UI
{
    // UI 매니저 클래스
    public class UIManager : SingletonMono<UIManager>
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private BattleText battleTextPrefab;

        public void ShowBattleText(string text, Vector3 spawnPosition)
        {
            var battleText = Instantiate(battleTextPrefab, canvas.transform);

            var textPosition = Camera.main.WorldToScreenPoint(spawnPosition);
            battleText.transform.position = textPosition;
            battleText.SetText(text);
        }
    }
}