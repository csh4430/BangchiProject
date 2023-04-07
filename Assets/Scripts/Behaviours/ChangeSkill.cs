using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Bullets;
using UnityEngine;

namespace Behaviours
{
    public class ChangeSkill : CharacterSkill
    {
        [SerializeField] private CharacterAttack characterAttack;
        private CharacterStat characterStat;
        [SerializeField] private List<Bullet> bullets = new ();
        protected override void Awake()
        {
            base.Awake();
            characterStat = GetComponent<CharacterStat>();
        }

        public override void Skill()
        {
            if(canSkillActive == false) return;
            OnSkill?.Invoke();
            isSkillActive = !isSkillActive;
            canSkillActive = !canSkillActive;
            characterAttack.bullet = !isSkillActive ? bullets[0] : bullets[1];
        }

        public override void Update()
        {
            if (isSkillActive)
            {
                if(characterStat.currentMana < bullets[1].manaCost)
                    Skill();
            }
            base.Update();
        }
    }
}