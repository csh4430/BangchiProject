using Characters;
using Managers;
using UnityEngine;

namespace Behaviours
{
    public class GrenadeSkill : CharacterSkill
    {
        [SerializeField] private GameObject grenadePrefab;

        public override void Skill()
        {
            if (isSkillActive) return;
            if (!canSkillActive) return;
            base.Skill();
            
            PoolManager.Instance.Pool(grenadePrefab, transform.position, transform.rotation);
            ThisCharacter.RemoveState(CharacterState.Skill);
            ExitSkill?.Invoke();
            isSkillActive = false;
        }
    }
}