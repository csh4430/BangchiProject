﻿using System;
using Characters;
using Managers;
using UnityEngine;

namespace Behaviours
{
    public class CharacterStat : CharacterBehaviour
    {
        public float maxHealth = 100;
        public float currentHealth;
        public float maxMana = 100;
        public float currentMana;
        public float manaChargeMultiplier = 1;
        
        public Action OnDamage;
        public Action OnCost;
        public Action OnFire;
        public float fireTime = 1f;
        public float fireDamage = 1;
        public float fireInterval = 0.2f;
        private float _fireContainTimer = 0;
        private float _fireTimer = 0;
        protected override void Awake()
        {
            base.Awake();
            ThisCharacter.OnDamage += Damage;
            ThisCharacter.OnFire += () => _fireContainTimer = 0;
            currentHealth = maxHealth;
            currentMana = maxMana;
        }

        private void Update()
        {
            if(currentMana >= maxMana) currentMana = maxMana;
            else Cost(-Time.deltaTime * manaChargeMultiplier);
            if (ThisCharacter.state.HasFlag(CharacterState.Fire))
            {
                if(ThisCharacter.state.HasFlag(CharacterState.Death)) return;
                _fireContainTimer += Time.deltaTime;
                _fireTimer += Time.deltaTime;
                if (_fireContainTimer >= fireTime)
                {
                    ThisCharacter.RemoveState(CharacterState.Fire);
                    _fireContainTimer = 0;
                }
                if (_fireTimer >= fireInterval)
                {
                    _fireTimer = 0;
                    ThisCharacter.OnDamage?.Invoke(fireDamage);
                    OnFire?.Invoke();
                }
            }
        }
        
        private void Damage(float value)
        {
            currentHealth -= value;
            if(currentHealth < 0) currentHealth = 0;
            OnDamage?.Invoke();
            if (currentHealth == 0)
            {
                ThisCharacter.OnDie?.Invoke();
            }
        }

        public void Cost(float value)
        {
            currentMana -= value;
            if(currentMana < 0) currentMana = 0;
            OnCost?.Invoke();
        }

        public void Die()
        {
            gameObject.SetActive(false);
        }
        
        public void Remove()
        {
            GameManager.Instance.characters.Remove(ThisCharacter);
        }
    }
}