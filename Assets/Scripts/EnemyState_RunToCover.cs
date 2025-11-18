using System.Collections;
using System.Collections.Generic;
using Tribhuvan_AI;
using UnityEngine;

namespace Tribhuvan_AI
{
    public class EnemyState_RunToCover : IState
    {
        private EnemyReferences enemyReferences;
        private CoverArea coverArea;

        public EnemyState_RunToCover(EnemyReferences enemyReferences, CoverArea coverArea)
        {
            this.enemyReferences = enemyReferences;
            this.coverArea = coverArea;
        }

        public void OnEnter()
        {
            Cover nextCover = coverArea.GetRandomCover(enemyReferences.transform.position);
            enemyReferences.navMeshAgent.SetDestination(nextCover.transform.position);

            // Optional: Reset speed or play run animation
            enemyReferences.animator.SetFloat("speed", 0f);
        }

        public void OnExit()
        {
            // Optional: Stop movement or reset animations
            enemyReferences.animator.SetFloat("speed", 0f);
        }

        public void Tick()
        {
            // Update animation based on movement speed
            enemyReferences.animator.SetFloat("speed", enemyReferences.navMeshAgent.desiredVelocity.sqrMagnitude);
        }
        public bool HasArrivedAtDestination()
        {
            return enemyReferences.navMeshAgent.remainingDistance < 0.1f;
        }
        public Color GizmoColor()
        {
            return Color.blue;
        }
    }
}
