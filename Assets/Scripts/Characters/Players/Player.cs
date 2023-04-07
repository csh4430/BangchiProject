using System;
using System.Collections;
using Behaviours;
using UnityEngine;
using UnityEngine.Serialization;

namespace Characters.Players
{
    public class Player : Character
    {
        [SerializeField] private float sleepDelay = 1f;
        [SerializeField] private float attackRange = 1f;

        protected override void Awake()
        {
            base.Awake();
            _targetFinder.attackRange = attackRange;
            OnStop += Sleep;
        }

        

        private void Update()
        {
            if (state.HasFlag(CharacterState.Skill)) return;
            Chase();
            Attack();
        }
        

        private void Attack()
        {
            if (!state.HasFlag(CharacterState.Moving))
            {
                if(_targetFinder.AttackTarget == null) return;
                var dir = _targetFinder.AttackTarget.transform.position;
                
                if (Vector2.Distance(transform.position, dir) <= attackRange)
                {
                    if(state.HasFlag(CharacterState.Attacking)) return;
                    if(_targetFinder.AttackTarget.state.HasFlag(CharacterState.Death)) return;
                    OnAttack?.Invoke();
                }
            }
        }
        
        
        private void Sleep()
        {
            if (state.HasFlag(CharacterState.Sleep)) return;
            state &= ~CharacterState.Moving;
            StartCoroutine(SleepCoroutine(sleepDelay));
        }
        private IEnumerator SleepCoroutine(float time)
        {
            yield return new WaitForSeconds(time);
            if(state.HasFlag(CharacterState.Moving)) yield break;
            state |= CharacterState.Sleep;
        }

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}