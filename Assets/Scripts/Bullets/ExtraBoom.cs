using System;
using Behaviours;
using Characters;
using UnityEngine;

namespace Bullets
{
    public class ExtraBoom : MonoBehaviour
    {
        [SerializeField] private float extraDamage;
        [SerializeField] private float extraRange;
        [SerializeField] private Transform parentTrm;
        public Character target;

        private void OnEnable()
        {
            target = GetComponentInParent<TargetFinder>().AttackTarget;
            transform.SetParent(null);
            transform.localScale *= extraRange * 2.2f;
        }

        public void GiveExtraDamage()
        {
            if (target == null) return;
            var colliders = Physics2D.OverlapCircleAll(target.transform.position, extraRange, LayerMask.GetMask("Enemy"));
            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out Character character))
                {
                    character.OnDamage?.Invoke(extraDamage);
                }
            }
        }

        public void Disable()
        {
            transform.SetParent(parentTrm);
            transform.localScale = Vector3.one;
            gameObject.SetActive(false);
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, extraRange);
        }
    }
}