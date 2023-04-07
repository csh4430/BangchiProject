using System;
using Characters;
using UnityEngine;
using UnityEngine.Serialization;

namespace Behaviours
{
    public class CharacterAnimation : CharacterBehaviour
    {
        public Transform anchorTransform;

        private Animator _thisAnimator;
        [SerializeField] private SpriteRenderer mainRenderer;
        private TargetFinder _targetFinder;
        
        
        private static readonly int IsMove = Animator.StringToHash("IsMove");
        private static readonly int TrgAttack = Animator.StringToHash("TrgAttack");
        private static readonly int AttackSpeed = Animator.StringToHash("AttackSpeed");
        private static readonly int TrgDamage = Animator.StringToHash("TrgDamage");
        private static readonly int TrgDeath = Animator.StringToHash("TrgDeath");
        private static readonly int TrgDash = Animator.StringToHash("TrgDash");
        private static readonly int IsDead = Animator.StringToHash("IsDead");
        protected float _lastDir = 1;

        protected override void Awake()
        {
            base.Awake();
            _thisAnimator = GetComponent<Animator>();
            _targetFinder = GetComponent<TargetFinder>();
            ThisCharacter.OnMove += PlayMoveAnimation;
            ThisCharacter.OnChase += PlayChaseAnimation;
            ThisCharacter.OnStop += StopMoveAnimation;
            var attack = ThisCharacter.GetComponent<CharacterAttack>();
            if(attack != null)
                attack.OnAttack += PlayAttackAnimation;
            ThisCharacter.OnDamage += PlayDamageAnimation;
            ThisCharacter.OnDie += PlayDeathAnimation;
            var dash = ThisCharacter.GetComponent<DashSkill>();
            if(dash != null)
                dash.OnSkill.AddListener(PlayDashAnimation);
        }

        private void Update()
        {
            if (ThisCharacter.state.HasFlag(CharacterState.Fire))
            {
                mainRenderer.color = new Color(1, 0.35f, 0.2f);
            }
            else
            {
                mainRenderer.color = Color.white;
            }
        }

        private void PlayMoveAnimation(Vector2 dir)
        {
            _thisAnimator.SetBool(IsMove, dir != Vector2.zero);
            Face(dir);
        }
        private void PlayChaseAnimation(Vector2 position)
        {
            var dir = position - (Vector2)transform.position;
            _thisAnimator.SetBool(IsMove, dir != Vector2.zero);
            Face(dir);
        }

        private void PlayAttackAnimation(float attackSpeed)
        {
            var dir = _targetFinder.AttackTarget.transform.position - transform.position;
            Face(dir);
            _thisAnimator.SetFloat(AttackSpeed, attackSpeed);
            _thisAnimator.SetTrigger(TrgAttack);
        }
        
        private void PlayDamageAnimation(float value)
        {
            _thisAnimator.SetTrigger(TrgDamage);
        }
        private void PlayDeathAnimation()
        {
            _thisAnimator.SetBool(IsDead, true);
            _thisAnimator.SetTrigger(TrgDeath);
        }
        private void StopMoveAnimation() => PlayMoveAnimation(Vector2.zero);

        private void PlayDashAnimation()
        {
            _thisAnimator.SetTrigger(TrgDash);
        }

        protected virtual void Face(Vector2 dir)
        {
            dir.y = 0;
            dir = dir.normalized;
            if(_lastDir == dir.x) return;
            if (dir.x == 0) return;
            _lastDir = dir.x;
            anchorTransform.localScale = new Vector3(_lastDir, 1, 1);
            
        }
    }
}