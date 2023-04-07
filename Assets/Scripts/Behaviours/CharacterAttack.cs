using System;
using Characters;

namespace Behaviours
{
    public class CharacterAttack : CharacterBehaviour
    {
        protected CharacterStat _characterStat;
        protected TargetFinder _targetFinder;
        protected Character _target;
        public Action<float> OnAttack;

        protected override void Awake()
        {
            base.Awake();
            _targetFinder = GetComponent<TargetFinder>();
            _characterStat = GetComponent<CharacterStat>();
            ThisCharacter.OnAttack += Attack;
        }

        protected virtual void Attack()
        {
            
        }

        public virtual void Fire()
        {
            
        }
    }
}