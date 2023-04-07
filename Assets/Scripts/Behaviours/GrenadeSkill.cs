using Characters;
using Managers;
using UnityEngine;

namespace Behaviours
{
    public class GrenadeSkill : CharacterSkill
    {
        [SerializeField] private GameObject grenadePrefab;
        private CharacterStat _stat;
        private TargetFinder _targetFinder;

        protected override void Awake()
        {
            base.Awake();
            _stat = GetComponent<CharacterStat>();
            _targetFinder = GetComponent<TargetFinder>();
        }
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

        public override void Update()
        {
            base.Update();
            if (canSkillActive)
            {
                if(_stat.currentMana < manaCost) return;
                if(_targetFinder.DetectedCount > 2)
                    Skill();
            }
        }
    }
}