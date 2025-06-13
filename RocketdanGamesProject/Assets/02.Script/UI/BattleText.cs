using DG.Tweening;
using RocketdanGamesProject.Core.ObjectPool;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RocketdanGamesProject.UI
{
    // 전투 텍스트 UI 클래스
    public class BattleText : MonoBehaviour, IPoolable
    {
        [SerializeField] private float maxJumpPosY = 100f;
        [SerializeField] private float minJumpPosY = 50f;
        [SerializeField] private float maxJumpPosX = 50f;
        [SerializeField] private float tweenDuration = 0.5f;
        
        private TextMeshProUGUI _text;

        private Sequence _textSequence;

        public void SetText(string outputText)
        {
            _text.text = outputText;

            var randomPosition = new Vector3(Random.Range(-maxJumpPosX, maxJumpPosX),
                Random.Range(minJumpPosY, maxJumpPosY), 0);

            _textSequence = DOTween.Sequence();
            _textSequence.Append(_text.DOFade(0f, tweenDuration).SetEase(Ease.InQuart));
            _textSequence.Insert(0f, _text.transform.DOJump(transform.position + randomPosition, 50f, 1, tweenDuration).SetEase(Ease.OutCirc));
            _textSequence.OnComplete(Release);
        }

        private void Release()
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
            _text.color = Color.white;
            
            if (_textSequence.IsPlaying())
            {
                _textSequence.Kill();
                _textSequence = null;
            }
        }
    }
}
