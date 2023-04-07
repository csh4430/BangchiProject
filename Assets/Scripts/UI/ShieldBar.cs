using System.Collections;
using Behaviours;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ShieldBar : Bar
    {
        private void Awake()
        {
            stat.OnDamage += OnChange;
        }
        
        protected override void OnChange()
        {
            var value = (stat.currentShield) / stat.maxHealth;
            valueTrm.localScale = new Vector3(value, 1, 1);
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)valueTrm.parent);
        }
    }
}