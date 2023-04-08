using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Behaviours
{
    public class PlayerDamageFeedback : CharacterBehaviour
    {
        [SerializeField] private Volume _volume;
        private Vignette _vignette;
        
        protected override void Awake()
        {
            base.Awake();
            _volume.profile.TryGet(out _vignette);
            ThisCharacter.OnDamage += FeedBack;
        }

        private void FeedBack(float value)
        {
            StartCoroutine(DamageCoroutine());
        }

        private IEnumerator DamageCoroutine()
        {
            for(var r = 0f; r <= 1f; r += 0.1f)
            {
                yield return new WaitForSeconds(0.01f);
                var color = new Color(r, 0, 0);
                _vignette.color.value = color;
            }
            yield return new WaitForSeconds(0.1f);
            for(var r = 0f; r <= 1f; r += 0.1f)
            {
                yield return new WaitForSeconds(0.01f);
                var color = new Color(1 - r, 0, 0);
                _vignette.color.value = color;
            }
        }
    }
}