using System;
using System.Collections;
using Behaviours;
using Characters;
using UnityEngine;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        public Transform valueTrm;
        public Transform backgroundTrm;
        
        public CharacterStat stat;
        
        private void Awake()
        {
            stat.OnDamage += OnDamage;
        }
        
        private void OnDamage()
        {
            if(gameObject.activeInHierarchy)
                StartCoroutine(DamageCoroutine());
        }

        private IEnumerator DamageCoroutine()
        {
            var value = (stat.currentHealth) / stat.maxHealth;
            valueTrm.localScale = new Vector3(value, 1, 1);
            yield return new WaitForSeconds(0.2f);
            backgroundTrm.localScale = new Vector3(value, 1, 1);
        }
    }
}