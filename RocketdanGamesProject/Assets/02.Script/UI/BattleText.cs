using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace RocketdanGamesProject.UI
{
    // 전투 텍스트 UI 클래스
    public class BattleText : MonoBehaviour
    {
        [SerializeField] private float movePosY = 300f;
        [SerializeField] private float moveDuration = 0.5f;
        
        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        public void SetText(string outputText)
        {
            _text.text = outputText;
            _text.transform.DOLocalMoveY(this.transform.localPosition.y + movePosY, moveDuration).OnComplete(() =>
            {
                Destroy(gameObject);
            });
        }
    }
}
