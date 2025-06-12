using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using RocketdanGamesProject.Core.ObjectPool;
using TMPro;
using UnityEngine;

namespace RocketdanGamesProject.UI
{
    // 전투 텍스트 UI 클래스
    public class BattleText : MonoBehaviour, IPoolable
    {
        [SerializeField] private float movePosY = 300f;
        [SerializeField] private float moveDuration = 0.5f;
        
        private TextMeshProUGUI _text;

        public void SetText(string outputText)
        {
            _text.text = outputText;
            _text.transform.DOLocalMoveY(this.transform.localPosition.y + movePosY, moveDuration).OnComplete(() =>
            {
                Destroy(gameObject);
            });
        }

        public void Release()
        {
            var poolObject = GetComponent<PoolObject>();
            ObjectPoolManager.Instance.Release(poolObject.poolName, poolObject);
        }

        public void OnCreate()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        public void OnGet()
        {
        }

        public void OnRelease()
        {
            _text.text = "";
        }
    }
}
