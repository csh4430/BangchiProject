using System;
using Resources;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

namespace Managers
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance;
        [SerializeField] private AudioMixer _audioMixer;
        public SoundList soundList;

        private void Awake()
        {
            Instance = this;
        }
        
        
        public void ChangeVolume(string type, float value)
        {
            soundList.GetSound(type).value = value;
            _audioMixer.SetFloat(type, soundList.GetSound(type).Volume);
        }
    }
}