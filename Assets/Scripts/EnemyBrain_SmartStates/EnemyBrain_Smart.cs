using System;
using Tribhuvan_AI;
using UnityEngine;

namespace Tribhuvan_AI
{
    public class EnemyBrain_Smart : MonoBehaviour
    {
        private EnemyReferences enemyReferences;
        private StateMachine stateMachine;

        void Start()
        {
            enemyReferences = GetComponent<EnemyReferences>();
            stateMachine = new StateMachine();

            // In this demo, we have only one cover area
            CoverArea coverArea = FindFirstObjectByType<CoverArea>();

            // STATES
            var runToCover = new EnemyState_RunToCover(enemyReferences, coverArea);
            var delayAfterRun = new EnemyState_Delay(2f);
            var cover = new EnemyState_Cover(enemyReferences);

            //TRANSITIONS
            At(runToCover, delayAfterRun, () => runToCover.HasArrivedAtDestination());
            At(delayAfterRun, cover, () => delayAfterRun.IsDone());


            // START STATE
            stateMachine.SetState(runToCover);

            // FUNCTIONS & CONDITIONS for easier transition setup
            void At(IState from, IState to, Func<bool> condition) =>
                stateMachine.AddTransition(from, to, condition);

            void Any(IState to, Func<bool> condition) =>
                stateMachine.AddAnyTransition(to, condition);
        }

        void Update()
        {
            // Update the active state every frame
            stateMachine.Tick();
        }
        private void OnDrawGizmos()
        {
            if (stateMachine != null)
            {
                Gizmos.color = stateMachine.GetGizmoColor();
                Gizmos.DrawSphere(transform.position + Vector3.up * 3, 0.4f);
            }
        }
    }
}
