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

        private void Awake()
        {
            thisCharacter = GetComponent<Character>();
        }

        private void Update()
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
            var enemys = GameManager.Instance.characters.FindAll(c => c != thisCharacter).OrderBy(c => Vector2.Distance(transform.position, c.transform.position));
            var closest = enemys.FirstOrDefault();
            if (closest == ChaseTarget) return;
            ChaseTarget = closest;
        }
    }
}