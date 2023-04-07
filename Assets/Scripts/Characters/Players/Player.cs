using System;
using System.Collections;
using Behaviours;
using UnityEngine;
using UnityEngine.Serialization;

namespace Characters.Players
{
    public class Player : Character
    {
        [SerializeField] private float sleepDelay = 1f;

        protected override void Awake()
        {
            base.Awake();
            OnStop += Sleep;
        }

        

        private void Update()
        {
            if (state.HasFlag(CharacterState.Skill)) return;
            Chase();
            Attack();
        }

        private void Sleep()
        {
            if (state.HasFlag(CharacterState.Sleep)) return;
            state &= ~CharacterState.Moving;
            StartCoroutine(SleepCoroutine(sleepDelay));
        }
        private IEnumerator SleepCoroutine(float time)
        {
            yield return new WaitForSeconds(time);
            if(state.HasFlag(CharacterState.Moving)) yield break;
            state |= CharacterState.Sleep;
        }
    }
}