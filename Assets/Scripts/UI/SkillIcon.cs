using System;
using SO.SkillIcon;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SkillIcon : MonoBehaviour
    {
        [SerializeField] private SkillIconSO _skillIconSo;
        private Image _image;
        private bool isActive = false;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public void ToggleIcon()
        {
            isActive = !isActive;
            _image.sprite = isActive ? _skillIconSo.activeIcon : _skillIconSo.defaultIcon;
        }

        public void SetIcon(bool value)
        {
            isActive = value;
            _image.sprite = isActive ? _skillIconSo.activeIcon : _skillIconSo.defaultIcon;
        }
    }
}