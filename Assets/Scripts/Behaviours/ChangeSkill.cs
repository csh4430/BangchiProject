using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Bullets;
using UnityEngine;
using UnityEngine.Serialization;

namespace Behaviours
{
    public class ChangeSkill : CharacterSkill
    {
        [FormerlySerializedAs("characterAttack")] [SerializeField] private PlayerAttack playerAttack;
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
            playerAttack.bullet = !isSkillActive ? bullets[0] : bullets[1];
        }

        public override void Update()
        {
            if (isSkillActive)
            {
                if(characterStat.currentMana < bullets[1].manaCost)
                    Skill();
            }
            else
            {
                if(characterStat.currentMana > bullets[1].manaCost * 2)
                    Skill();
            }
            base.Update();
        }
    }
}