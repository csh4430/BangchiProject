using System;
using Managers;
using Resources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SoundSlider : MonoBehaviour
    {
        public string type;
        [SerializeField] private TMP_Text text;
        [SerializeField] private Slider slider;

        private void Awake()
        {
            slider.onValueChanged.AddListener(ChangeVolume);
        }

        public void ChangeVolume(float value)
        {
            SoundManager.Instance.ChangeVolume(type, value);
        }
        
        public void SetData(Sound sound)
        {
            type = sound.type;
            text.text = sound.type;
            slider.value = sound.value;
            
            ChangeVolume(sound.value);
        }
    }
}