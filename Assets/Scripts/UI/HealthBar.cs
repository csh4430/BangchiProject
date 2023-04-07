using System;
using System.Collections;
using Behaviours;
using Characters;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : Bar
    {
        public Transform backgroundTrm;

        private void Awake()
        {
            stat.OnDamage += OnChange;
        }
        
        protected override void OnChange()
        {
            if(gameObject.activeInHierarchy)
                StartCoroutine(DamageCoroutine());
        }

        private IEnumerator DamageCoroutine()
        {
            var value = (stat.currentHealth) / stat.maxHealth;
            valueTrm.localScale = new Vector3(value, 1, 1);
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)valueTrm.parent);
            yield return new WaitForSeconds(0.2f);
            backgroundTrm.localScale = new Vector3(value, 1, 1);
        }
    }
}