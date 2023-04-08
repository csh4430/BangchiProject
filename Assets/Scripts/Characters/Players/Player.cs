using System;
using System.Collections;
using Behaviours;
using Managers;
using Tools;
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
            Define.Player = this;
            OnStop += Sleep;
            FadeManager.Instance.InitFade();
            FadeManager.Instance.FadeOut(3f, null);
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