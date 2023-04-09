using System;
using System.Collections;
using System.Collections.Generic;
using Resources;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;

namespace Managers
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
        [SerializeField] private List<AudioSource> _audioSources;
        [SerializeField] private List<AudioClip> _audioClips;

        public enum ClipType
        {
            Fire,
            Shoot,
            UIClick,
            WalkSound,
        }
        private Dictionary<ClipType, AudioClip> _audioClipDictionary = new();

        private Dictionary<Transform, AudioSource> _followAudios = new();

        public enum AudioType
        {
            SFX,
            UI,
        }

        private void Awake()
        {
            Instance = this;
            foreach (var audioClip in _audioClips)
            {
                _audioClipDictionary.Add((ClipType) Enum.Parse(typeof(ClipType), audioClip.name), audioClip);
            }
        }
        
        public void PlaySound(ClipType clipType, AudioType audioType, float volume = 1f, Vector2 position = default)
        {
            switch (audioType)
            {
                case AudioType.SFX:
                    StartCoroutine(PlayVFXCoroutine(_audioClipDictionary[clipType], volume, position));
                    break;
                case AudioType.UI:
                    _audioSources[1].volume = volume;
                    _audioSources[1].PlayOneShot(_audioClipDictionary[clipType]);
                    break;
            }
        }

        public void SoundOn(ClipType clipType, Transform trm)
        {
            if(_followAudios.ContainsKey(trm))
            {
                if (_followAudios[trm].clip == _audioClipDictionary[clipType])
                    return;
            }
            var obj= PoolManager.Instance.Pool(_audioSources[2].gameObject, trm.position, Quaternion.identity);
            obj.transform.SetParent(trm);
            var follow = obj.GetComponent<AudioSource>();
            _followAudios.Add(trm, follow);
            follow.clip = _audioClipDictionary[clipType];
            follow.Play();
        }

        public void SoundOff(ClipType clipType, Transform trm)
        {
            if(_followAudios.ContainsKey(trm) == false)
                return;
            var follow = _followAudios[trm];
            follow.Stop();
            follow.clip = null;
            PoolManager.Instance.DePool(follow.gameObject);
            _followAudios.Remove(trm);
        }
        
        public void ButtonSound()
        {
            _audioSources[1].PlayOneShot(_audioClipDictionary[ClipType.UIClick]);
        }

        private IEnumerator PlayVFXCoroutine(AudioClip clip, float volume = 1f, Vector2 position = default)
        {
            var obj= PoolManager.Instance.Pool(_audioSources[0].gameObject, position, Quaternion.identity);
            var sfx = obj.GetComponent<AudioSource>();
            sfx.PlayOneShot(clip, volume);
            yield return new WaitForSeconds(clip.length);
            PoolManager.Instance.DePool(obj);
        }
    }
}