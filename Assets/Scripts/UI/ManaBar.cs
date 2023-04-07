using System;
using System.Collections;
using Behaviours;
using Characters;
using UnityEngine;

namespace UI
{
    public class ManaBar : MonoBehaviour
    {
        public Transform valueTrm;
        public Transform backgroundTrm;
        
        public CharacterStat stat;
        
        private void Awake()
        {
            stat.OnCost += OnCost;
        }
        
        private void OnCost()
        {
            if(gameObject.activeInHierarchy)
                StartCoroutine(CostCoroutine());
        }

        private IEnumerator CostCoroutine()
        {
            var value = (stat.currentMana) / stat.maxMana;
            valueTrm.localScale = new Vector3(value, 1, 1);
            yield return new WaitForSeconds(0.2f);
            backgroundTrm.localScale = new Vector3(value, 1, 1);
        }
    }
}