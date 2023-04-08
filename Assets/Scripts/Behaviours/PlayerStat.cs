using Managers;
using UnityEngine;

namespace Behaviours
{
    public class PlayerStat : CharacterStat
    {
        [SerializeField] private float healthChargeMultiplier = 1f;
        private float defaultMaxHealth;

        protected override void Awake()
        {
            base.Awake();
            defaultMaxHealth = maxHealth;
        }

        protected override void Update()
        {
            base.Update();
            maxHealth = defaultMaxHealth * StatManager.Instance.statList.stats[1].multiplier;
            if(currentHealth >= maxHealth) currentHealth = maxHealth;
            else Damage(-Time.deltaTime * healthChargeMultiplier * StatManager.Instance.statList.stats[2].multiplier);
        }

        public override void Die()
        {
            OnDie?.Invoke();
        }
    }
}