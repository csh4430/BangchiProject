using UnityEngine;

namespace Behaviours
{
    public class PlayerStat : CharacterStat
    {
        [SerializeField] private float healthChargeMultiplier = 1f;
        protected override void Update()
        {
            base.Update();
            if(currentHealth >= maxHealth) currentHealth = maxHealth;
            else Damage(-Time.deltaTime * healthChargeMultiplier);
        }
    }
}