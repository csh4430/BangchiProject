using System;
using Cinemachine;
using UnityEngine;

namespace Behaviours
{
    public class PlayerCamera : CharacterBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        CinemachineBasicMultiChannelPerlin _perlin;
        private float shakeTimer = 0;
        
        protected override void Awake()
        {
            base.Awake();
            _perlin = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            GetComponent<CharacterAttack>().OnFire += () => Shake(3f, .1f);
        }
        
        public void Shake(float intensity, float time)
        {
            _perlin.m_AmplitudeGain = intensity;
            shakeTimer = time;
        }

        private void Update()
        {
            if (shakeTimer > 0)
            {
                shakeTimer -= Time.deltaTime;
                if (shakeTimer <= 0)
                {
                    _perlin.m_AmplitudeGain = 0;
                }
            }
        }
    }
}