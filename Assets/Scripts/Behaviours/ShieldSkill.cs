using System;
using System.Collections;
using Characters;
using UnityEngine;

namespace Behaviours
{
    public class ShieldSkill : CharacterSkill
    {
        [SerializeField] private float shieldTime = 3f;
        [SerializeField] private ParticleSystem shieldParticle;
        private CharacterStat _stat;

        protected override void Awake()
        {
            base.Awake();
            _stat = GetComponent<CharacterStat>();
        }

        public override void Update()
        {
            base.Update();
            if (canSkillActive)
            {
                if(_stat.currentMana < manaCost) return;
                if(_stat.currentHealth < _stat.maxHealth * 0.7f)
                    Skill();
            }
        }
        
        public override void Skill()
        {
            if (isSkillActive) return;
            if (!canSkillActive) return;
            base.Skill();
            shieldParticle.Play();
            _stat.ArmedShield(300f);
            ThisCharacter.RemoveState(CharacterState.Skill);
            StartCoroutine(ShieldCoroutine());
        }
        
        private IEnumerator ShieldCoroutine()
        {
            yield return new WaitForSeconds(shieldTime);
            _stat.ArmedShield(0f);
            ExitSkill?.Invoke();
            isSkillActive = false;
        }
    }
}