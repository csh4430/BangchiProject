using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class FadeManager : MonoBehaviour
    {
        public static FadeManager Instance { get; private set; }
        Sequence _sequence;
        private void Awake()
        {
            Instance = this;
        }
        
        [SerializeField] private Image fadeImage;

        public void InitFade()
        {
            fadeImage.DOFade(1, 0);
        }
        
        public void FadeIn(float time, Action callback)
        {
            _sequence = DOTween.Sequence();
            _sequence.Append(fadeImage.DOFade(1, time));
            _sequence.AppendCallback(() =>
            {
                callback?.Invoke();
                _sequence.Kill();
            });
        }
        
        public void FadeOut(float time, Action callback)
        {
            _sequence = DOTween.Sequence();
            _sequence.Append(fadeImage.DOFade(0, time));
            _sequence.AppendCallback(() =>
            {
                callback?.Invoke();
                _sequence.Kill();
            });
        }
    }
}