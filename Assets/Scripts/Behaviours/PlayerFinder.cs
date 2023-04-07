using Tools;
using UnityEngine;

namespace Behaviours
{
    public class PlayerFinder : TargetFinder
    {
        protected override void Awake()
        {
            base.Awake();
            ChaseTarget = Define.Player;
        }

        protected override void Update()
        {
            if (ChaseTarget != null)
            {
                if(Vector2.Distance(transform.position, ChaseTarget.transform.position) <= attackRange)
                    AttackTarget = ChaseTarget;
                else
                    AttackTarget = null;
            }
        }
    }
}