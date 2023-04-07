using System;
using Characters;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Behaviours
{
    public class CharacterSkill : CharacterBehaviour
    {
        public int skillIdx;
        public float skillCoolTime;
        public float manaCost;
        public float Timer { get; protected set; } = 0;
        public bool isSkillActive;
        public bool canSkillActive;
        public UnityEvent OnSkill;
        public UnityEvent ExitSkill;
        public Action ExitSkillInCoolDown;
        public Action OnSkillInCoolDown;
        protected override void Awake()
        {
            base.Awake();
            ThisCharacter.AddSkill(skillIdx, Skill);
            ThisCharacter.Skills.Add(skillIdx, this);
        }

        public virtual void Skill()
        {
            if (isSkillActive) return;
            if (!canSkillActive) return;
            var characterStat = ThisCharacter.GetComponent<CharacterStat>();
            if(characterStat.currentMana < manaCost) return;
            characterStat.Cost(manaCost);
            ThisCharacter.state |= (CharacterState.Skill);
            ThisCharacter.RemoveState(CharacterState.Moving);
            ThisCharacter.RemoveState(CharacterState.Attacking);
            OnSkill?.Invoke();
            isSkillActive = true;
            canSkillActive = false;
        }
        
        public virtual void Update()
        {
            if (!canSkillActive)
            {
                Timer += Time.deltaTime;
                OnSkillInCoolDown?.Invoke();
                if (Timer >= skillCoolTime)
                {
                    canSkillActive = true;
                    ExitSkillInCoolDown?.Invoke();
                    Timer = 0;
                }
            }
        }
    }
}