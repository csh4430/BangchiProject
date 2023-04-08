using System;
using Characters;
using Managers;
using UnityEngine;

namespace Bullets
{
    public class FireArea : MonoBehaviour
    {
        [SerializeField] private float fireDamage;
        [SerializeField] private float fireRange;
        [SerializeField] private float fireTime;
        [SerializeField] private float fireInterval;
        private float _timer = 0;
        private float _intervalTimer = 0;
        
        private void Awake()
        {
            transform.localScale *= fireRange;
        }

        private void Update()
        {
            _intervalTimer += Time.deltaTime;
            _timer += Time.deltaTime;
            if (_intervalTimer >= fireInterval)
            {
                GiveFireDamage();
                _intervalTimer = 0;
            }
            if(_timer >= fireTime)
                PoolManager.Instance.DePool(gameObject);
        }

        public void GiveFireDamage()
        {
            var colliders = Physics2D.OverlapCircleAll(transform.position, fireRange, LayerMask.GetMask("Enemy"));
            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out Character character))
                {
                    character.OnDamage?.Invoke(fireDamage * StatManager.Instance.statList.stats[0].multiplier);
                    character.OnFire?.Invoke();
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, fireRange);
        }
    }
}