using System;
using System.Collections;
using System.Linq;
using Bullets;
using Characters;
using Characters.Enemies;
using Managers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Behaviours
{
    public class PlayerAttack : CharacterAttack
    {
        [SerializeField] private float _attackDamage = 1f;
        [SerializeField] private Transform attackTrm;
        [SerializeField] public Bullet bullet;

        protected override void Attack()
        {
            if (_characterStat.currentMana < bullet.manaCost)
            {
                ThisCharacter.RemoveState(CharacterState.Attacking);
                return;
            }
            OnAttack?.Invoke(bullet.delay);
            _target = _targetFinder.AttackTarget;
        }
        
        public override void Fire()
        {
            if (_characterStat.currentMana < bullet.manaCost)
            {
                ThisCharacter.RemoveState(CharacterState.Attacking);
                return;
            }

            if (_target != _targetFinder.AttackTarget) return;
            _characterStat.Cost(bullet.manaCost);
            
            if(_targetFinder.AttackTarget == null) return;
            var dir = _targetFinder.AttackTarget.transform.position -attackTrm.position;
            var size = (dir).magnitude;
            attackTrm.localScale = new Vector3(size, 1f, 1);
            var rot = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            attackTrm.localRotation = Quaternion.Euler(0, 0, rot);
            bullet.gameObject.SetActive(true);
            bullet.targetFinder = _targetFinder;
        }
    }
}
