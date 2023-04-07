using System;
using System.Linq;
using System.Security.Cryptography;
using Characters;
using Characters.Enemies;
using Managers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Behaviours
{
    public class TargetFinder : MonoBehaviour
    {
         
        public Character ChaseTarget;
        public Character AttackTarget; 
        private Character thisCharacter;
        public float attackRange = 0;
        public float delay;
        private float _time;
        private int _detectedCount = 0;
        public int DetectedCount => _detectedCount;

        protected virtual void Awake()
        {
            thisCharacter = GetComponent<Character>();
        }

        protected virtual void Update()
        {
            
            _time += Time.deltaTime;
            if (_time >= delay)
            {
                _time = 0;
                FindTarget();
            }

            if (ChaseTarget != null)
            {
                if(Vector2.Distance(transform.position, ChaseTarget.transform.position) <= attackRange)
                    AttackTarget = ChaseTarget;
                else
                    AttackTarget = null;
            }
        }

        private void FindTarget()
        {
            var enemies = GameManager.Instance.characters.FindAll(c => c != thisCharacter).OrderBy(c => Vector2.Distance(transform.position, c.transform.position));
            var activeEnemies = enemies.Where(e => e.gameObject.activeSelf);
            var characters = activeEnemies.ToList();
            _detectedCount = characters.Count();
            var closest = characters.FirstOrDefault();
            if (closest == ChaseTarget) return;
            ChaseTarget = closest;
        }
    }
}