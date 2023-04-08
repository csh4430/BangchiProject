using System;
using Items;
using Managers;
using Tools;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Characters.Enemies
{
    public class Enemy : Character
    {
        [SerializeField] private DropItem dropItem;
        [SerializeField] private int maxDropCount = 1;
        private int dropCount;
        protected override void Awake()
        {
            base.Awake();
            dropCount = Random.Range(1, maxDropCount + 1);
            OnDie += () =>
            {
                var obj = PoolManager.Instance.Pool(dropItem.gameObject, transform.position, quaternion.identity);
                obj.GetComponent<DropItem>().Count = dropCount;
            };
            OnDamage += (x) =>
            {
                UIManager.Instance.CreateDamageText(x.ToString(), new Color(230, 0,0), transform.position + 0.5f * Vector3.up);
            };
        }

        private void Update()
        {
            Chase();
            Attack();
        }
    }
}