using System;
using DG.Tweening;
using Managers;
using TMPro;
using UnityEngine;

namespace UI
{
    public class PopUpText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private void OnEnable()
        {
            Sequence seq;
            seq = DOTween.Sequence();
            var textTrm = _text.transform;
            var posY = textTrm.localPosition.y + 1.5f;
            var localScale = textTrm.localScale;
            seq.Append(textTrm.DOScale(localScale * 2f, 0.1f));
            seq.Append(textTrm.DOScale(localScale / 1.5f, 0.3f));
            seq.Append(textTrm.DOLocalMoveY(posY, 0.3f));
            seq.Join(_text.DOFade(0, 0.3f));
            seq.AppendCallback(() =>
            {
                textTrm.localPosition = Vector3.zero;
                textTrm.localScale = Vector3.one;
                _text.color = Color.white;
                PoolManager.Instance.DePool(gameObject);
            });
        }

        public void SetText(string text, Color color)
        {
            _text.text = text;
            _text.color = color;
        }
    }
}