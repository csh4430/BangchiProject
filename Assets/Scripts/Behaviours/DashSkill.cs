using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Characters;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Behaviours
{
    public class DashSkill : CharacterSkill
    {

        [SerializeField] private float windDis;
        [SerializeField] private float windHeight;
        [SerializeField] private float windDamage;
        [SerializeField] private Transform anchorPoint;
        public override void Skill()
        {
            base.Skill();
            
        }

        public void DashAttack()
        {
            var dir = anchorPoint.localScale.x;
            var pos = transform.position;
            pos.x += (windDis / 2) * dir;
            Collider2D[] enemies = new Collider2D[10];
            var size = Physics2D.OverlapBoxNonAlloc(pos, new Vector2(windDis, windHeight), 0f, enemies, LayerMask.GetMask("Enemy"));
            Debug.Log(size);
            foreach (var enemy in enemies)
            {
                if (enemy == null) continue;
                if (!enemy.TryGetComponent(out Character character)) continue;
                character.OnKnockBack?.Invoke(Vector2.right * dir);
                character.OnDamage?.Invoke(windDamage);
            }
        }
        public void DashEnd()
        {
            ThisCharacter.RemoveState(CharacterState.Skill);
            ExitSkill?.Invoke();
            isSkillActive = false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            var dir = anchorPoint.localScale.x;
            var pos = transform.position;
            pos.x += (windDis / 2) * dir;
            Gizmos.DrawWireCube(pos, new Vector2(windDis, windHeight));
        }
    }
}