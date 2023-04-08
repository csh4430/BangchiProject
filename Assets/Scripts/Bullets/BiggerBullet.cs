using System;
using Behaviours;
using Characters;
using UnityEngine;

namespace Bullets
{
    public class BiggerBullet : Bullet
    {
        [SerializeField] private Transform particleTrm;
        private Transform _anchorPoint;

        private void Awake()
        {
            _anchorPoint = transform.parent;
        }

        public override void GiveDamage()
        {
            base.GiveDamage();
            if(targetFinder.AttackTarget == null) return;
            particleTrm.position = targetFinder.AttackTarget.transform.position;
            particleTrm.gameObject.SetActive(true);
        }
    }
}