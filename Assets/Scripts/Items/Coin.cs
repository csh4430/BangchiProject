using System;
using DG.Tweening;
using Managers;
using Tools;
using UnityEngine;

namespace Items
{
    public class Coin : DropItem
    {
        private Rigidbody2D _rigidbody2D;
        private bool _isAutoCollect = false;
        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            Invoke(nameof(AutoCollect), 2f);
        }

        private void Update()
        {
            if(_isAutoCollect)
                _rigidbody2D.velocity = (Define.Player.transform.position - transform.position).normalized * 10;
        }

        private void AutoCollect()
        {
            _isAutoCollect = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _isAutoCollect = false;
                ResourceManager.Instance.AddCoin(Mathf.RoundToInt(Count * StatManager.Instance.statList.stats[3].multiplier));
                PoolManager.Instance.DePool(gameObject);
            }
        }
    }
}