using UnityEngine;

namespace Behaviours
{
    public class EnemyAttack : CharacterAttack
    {
        [SerializeField] private float damage = 1f;
        protected override void Attack()
        {
            OnAttack?.Invoke(1);
            _target = _targetFinder.AttackTarget;
        }
        
        public override void Fire()
        {
            if (_target != _targetFinder.AttackTarget) return;
            if(_targetFinder.AttackTarget == null) return;
            _targetFinder.AttackTarget.OnDamage?.Invoke(damage);
        }
    }
}