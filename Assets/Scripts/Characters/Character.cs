using System;
using System.Collections.Generic;
using Behaviours;
using Managers;
using UnityEngine;

namespace Characters
{
    [Flags]
    public enum CharacterState
    {
        None = 0,
        Sleep = 1 << 0,
        Moving = 1 << 1,
        KnockBack = 1 << 2,
        Attacking = 1 << 3,
        Skill = 1 << 4,
        Fire = 1 << 5,
        Death = 1 << 6
    }
    public class Character : MonoBehaviour
    {
        public CharacterState state = CharacterState.Sleep;
        public Action<Vector2> OnMove;
        public Action OnStop;
        public Action<Vector2> OnChase;
        public Action OnAttack;
        public Action<float> OnDamage;
        public Action OnDie;
        public Action OnFire;
        public Action<Vector2> OnKnockBack;
        public Dictionary<int, Action> OnSkill;
        public Dictionary<int, CharacterSkill> Skills = new(); 
        protected TargetFinder _targetFinder;
        [SerializeField] protected float chaseRange = 1f;
        [SerializeField] private float attackRange = 1f;


        protected virtual void Awake()
        {
            GameManager.Instance.characters.Add(this);
            OnMove += (v) => DoState(CharacterState.Moving);
            OnMove += (v) => RemoveState(CharacterState.Attacking);
            OnDie += () => DoState(CharacterState.Death);
            OnAttack += () => DoState(CharacterState.Attacking);
            OnAttack += () => DoState(CharacterState.Sleep);
            OnKnockBack += (v) => DoState(CharacterState.KnockBack);
            OnFire += () => AddState(CharacterState.Fire);
            OnDamage += f => RemoveState(CharacterState.Attacking); 
            _targetFinder = GetComponent<TargetFinder>();
            _targetFinder.attackRange = attackRange;
        }
        protected void Chase()
        {
            if (state.HasFlag(CharacterState.Sleep))
            {
                if(_targetFinder.ChaseTarget == null) return;
                var dir = _targetFinder.ChaseTarget.transform.position;
                
                if (Vector2.Distance(transform.position, dir) <= chaseRange)
                {
                    OnStop?.Invoke();
                    return;
                }
                OnChase?.Invoke(dir);
            }
        }
        protected virtual void Attack()
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
        public void RemoveState(CharacterState value)
        {
            state &= ~value;
        }
        public void AddState(CharacterState value)
        {
            state |= value;
        }
        public void DoState(CharacterState value)
        {
            state &= ~CharacterState.Sleep;
            state |= value;
        }
        public void AddSkill(int skillIdx, Action skill)
        {
            if (OnSkill == null)
                OnSkill = new Dictionary<int, Action>();
            if(OnSkill.ContainsKey(skillIdx))
                OnSkill[skillIdx] += skill;
            else
                OnSkill.Add(skillIdx, skill);
        }

        public void ActSkill(int skillIdx)
        {
            if (OnSkill == null)
                return;
            if (OnSkill.ContainsKey(skillIdx))
            {
                OnSkill[skillIdx]?.Invoke();

            }
        }
        
        protected void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, chaseRange);
        }
    }
}