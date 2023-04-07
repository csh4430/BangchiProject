using System;
using Behaviours;
using Characters;
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
            targetFinder.AttackTarget.OnDamage?.Invoke(damage);
        }
        
        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}