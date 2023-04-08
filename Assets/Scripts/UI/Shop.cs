using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private Button _toggleButton;
        private RectTransform _rectTransform => transform as RectTransform;
        private int _isOpen = 0;
        
        private void Start()
        {
            _toggleButton.onClick.AddListener(Toggle);
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
            seq.Append(_rectTransform.DOPivotX(0, 0.3f));
            seq.AppendCallback(() =>
            {
                _isOpen = 2;
                seq.Kill();
            });
        }
        
        public void Close()
        {
            var seq = DOTween.Sequence();
            seq.Append(_rectTransform.DOPivotX(1, 0.3f));   
            seq.AppendCallback(() =>
            {
                _isOpen = 0;
                seq.Kill();
            });
        }
    }
}