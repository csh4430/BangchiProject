using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class WaveManager : MonoBehaviour
    {
        public static WaveManager Instance;
        
        [SerializeField] private int _enemyPerWave = 5;
        [SerializeField] private Vector2 _spawnRange = new Vector2(5, 5);
        [SerializeField] private List<GameObject> _enemies = new List<GameObject>();
        private bool _isSpawning = false;
        
        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            if (GameManager.Instance.enemies.Count == 0)
                SpawnWave();
        }

        public void SpawnWave()
        {
            if(_isSpawning) return;
            _isSpawning = true;
            StartCoroutine(WaveCoroutine());
        }

        private IEnumerator WaveCoroutine()
        {
            yield return new WaitForSeconds(2f);
            for (int i = 0; i < _enemyPerWave; i++)
            {
                var enemy = PoolManager.Instance.Pool(_enemies[0], transform.position + new Vector3(Random.Range(-_spawnRange.x, _spawnRange.x), Random.Range(-_spawnRange.y, _spawnRange.y)), Quaternion.identity);
                yield return new WaitForSeconds(0.3f);
            }
            _isSpawning = false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(transform.position, _spawnRange * 2);
        }
    }
}