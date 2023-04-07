using System;
using System.Collections;
using Behaviours;
using Characters;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ManaBar : Bar
    {
        public Transform backgroundTrm;
        
        private void Awake()
        {
            stat.OnCost += OnChange;
        }
        
        protected override void OnChange()
        {
            if(gameObject.activeInHierarchy)
                StartCoroutine(CostCoroutine());
        }

        private IEnumerator CostCoroutine()
        {
            var value = (stat.currentMana) / stat.maxMana;
            valueTrm.localScale = new Vector3(value, 1, 1);
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)valueTrm.parent);
            yield return new WaitForSeconds(0.2f);
            backgroundTrm.localScale = new Vector3(value, 1, 1);
        }
    }
}