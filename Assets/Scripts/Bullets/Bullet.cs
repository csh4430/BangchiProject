using System;
using Behaviours;
using Characters;
using Managers;
using UnityEngine;

namespace Bullets
{
    public class Bullet : MonoBehaviour
    {
        public float delay;
        public float damage;
        public TargetFinder targetFinder;
        public float manaCost;

        public virtual void GiveDamage()
        {
            if(targetFinder.AttackTarget == null) return;
            targetFinder.AttackTarget.OnDamage?.Invoke(damage * StatManager.Instance.statList.stats[0].multiplier);
        }
        
        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}