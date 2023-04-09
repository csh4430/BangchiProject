using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class SlidingPanel : MonoBehaviour
    {
        [SerializeField] private int openPosition;
        [SerializeField] private Button toggleButton;
        private RectTransform _rectTransform => transform as RectTransform;
        private int _isOpen = 0;
        
        private void Start()
        {
            toggleButton.onClick.AddListener(Toggle);
        }
        
        public void Toggle()
        {
            if(_isOpen == 0) Open();
            else if (_isOpen == 2) Close();
        }
        
        public void Open() 
        {
            _isOpen = 1;
            var seq = DOTween.Sequence();
            seq.Append(_rectTransform.DOPivotX(1 ^ openPosition, 0.3f));
            seq.AppendCallback(() =>
            {
                _isOpen = 2;
                seq.Kill();
            });
        }
        
        public void Close()
        {
            var seq = DOTween.Sequence();
            seq.Append(_rectTransform.DOPivotX(0 ^ openPosition, 0.3f));   
            seq.AppendCallback(() =>
            {
                _isOpen = 0;
                seq.Kill();
            });
        }
    }
}