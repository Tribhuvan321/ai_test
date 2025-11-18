using UnityEngine;
using UnityEngine.AI;

namespace Tribhuvan_AI
{
    [DisallowMultipleComponent]
    public class EnemyReferences : MonoBehaviour
    {
        [HideInInspector] public NavMeshAgent navMeshAgent;
        [HideInInspector] public Animator animator;
        public EnemyShooter shooter;

        [Header("Stats")]
        public float pathUpdateDelay = 0.2f;

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            shooter = GetComponent<EnemyShooter>();
        }
    }
}