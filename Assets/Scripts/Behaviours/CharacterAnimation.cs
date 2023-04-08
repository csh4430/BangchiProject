using System;
using System.Collections;
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
        private Material _mainMaterial;
        private TargetFinder _targetFinder;
        
        
        private static readonly int IsMove = Animator.StringToHash("IsMove");
        private static readonly int TrgAttack = Animator.StringToHash("TrgAttack");
        private static readonly int AttackSpeed = Animator.StringToHash("AttackSpeed");
        private static readonly int TrgDeath = Animator.StringToHash("TrgDeath");
        private static readonly int TrgDash = Animator.StringToHash("TrgDash");
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
            ThisCharacter.OnDamage += PlayDamageBlink;
            ThisCharacter.OnDie += PlayDeathAnimation;
            var dash = ThisCharacter.GetComponent<DashSkill>();
            if(dash != null)
                dash.OnSkill.AddListener(PlayDashAnimation);
            var stat = ThisCharacter.GetComponent<CharacterStat>();
            if(stat != null)
                stat.OnDie += ReInit;
            
            _mainMaterial = mainRenderer.material;
        }

        private void Update()
        {
            if (ThisCharacter.state.HasFlag(CharacterState.Fire))
            {
                _mainMaterial.SetInt("_IsBurn", 1);
            }
            else
            {
                _mainMaterial.SetInt("_IsBurn", 0);
            }
            mainRenderer.material = _mainMaterial;
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
        
        private void PlayDamageBlink(float value)
        {
            if(gameObject.activeSelf)
                StartCoroutine(BlinkCoroutine());
        }
        
        private IEnumerator BlinkCoroutine()
        {
            _mainMaterial.SetInt("_IsBlink", 1);
            mainRenderer.material = _mainMaterial;
            yield return new WaitForSeconds(0.2f);
            _mainMaterial.SetInt("_IsBlink", 0);
            mainRenderer.material = _mainMaterial;
        }
        private void PlayDeathAnimation()
        {
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

        private void ReInit()
        {
            _mainMaterial.SetInt("IsBlink", 0);
        }
    }
}