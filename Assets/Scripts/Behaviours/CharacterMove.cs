using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Characters;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Serialization;

namespace Behaviours
{
    public class CharacterMove : CharacterBehaviour
    {
        private Rigidbody2D _thisRigidbody2D;
        [SerializeField] private float speed = 1f;


        protected override void Awake()
        {
            base.Awake();
            _thisRigidbody2D = GetComponent<Rigidbody2D>();
            ThisCharacter.OnMove += Translate;
            ThisCharacter.OnChase += Chase;
            ThisCharacter.OnStop = StopMove;
            ThisCharacter.OnKnockBack += KnockBack;
        }

        private void Update()
        {
            if (ThisCharacter.state.HasFlag(CharacterState.Attacking))
            {
                StopMove();
            }
            if (ThisCharacter.state.HasFlag(CharacterState.Death))
            {
                StopMove();
            }
            if (ThisCharacter.state.HasFlag(CharacterState.Skill))
            {
                StopMove();
            }
        }

        private void Translate(Vector2 direction)
        {
            Move(direction);
        }

        private void Chase(Vector2 position)
        {
            Move(position - (Vector2)transform.position);
        }

        private void Move(Vector2 direction)
        {
            if (ThisCharacter.state.HasFlag(CharacterState.KnockBack)) return;
            direction = direction.normalized;
            _thisRigidbody2D.velocity = direction * speed;
        }
        
        private void KnockBack(Vector2 direction)
        {
            direction = direction.normalized;
            _thisRigidbody2D.AddForce(direction * 150, ForceMode2D.Impulse);
            StartCoroutine(KnockBackDelay());
        }
        
        private IEnumerator KnockBackDelay()
        {
            yield return new WaitForSeconds(0.5f);
            _thisRigidbody2D.velocity = Vector2.zero;
            ThisCharacter.RemoveState(CharacterState.KnockBack);
        }

        private void StopMove()
        {
            _thisRigidbody2D.velocity = Vector2.zero;
        }
    }
}