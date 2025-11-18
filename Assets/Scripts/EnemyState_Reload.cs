using System.Collections;
using System.Collections.Generic;
using Tribhuvan_AI;
using UnityEngine;

namespace Tribhuvan_AI
{
    public class EnemyState_Reload : IState
    {
        private EnemyReferences enemyReferences;

        public EnemyState_Reload(EnemyReferences enemyReferences)
        {
            this.enemyReferences = enemyReferences;
        }

        public void OnEnter()
        {
            Debug.Log("Start Reload");
            enemyReferences.animator.SetFloat("cover", 1);
            enemyReferences.animator.SetTrigger("reload");
        }

        public void OnExit()
        {
            Debug.Log("Stop Reload");
            enemyReferences.animator.SetFloat("cover", 0);
        }

        public void Tick()
        {
            // Reload animation or timing can be handled here
        }

        public Color GizmoColor()
        {
            return Color.gray;
        }
    }
}
