using Tribhuvan_AI;
using UnityEngine;

public class EnemyBrain_Stupid : MonoBehaviour
{
    public Transform target;

    private EnemyReferences enemyReferences;

    private float pathUpdateDeadline;

    private float shootingDistance;

    private void Awake()
    {
        enemyReferences = GetComponent<EnemyReferences>();
    }

    void Start()
    {
        shootingDistance = enemyReferences.navMeshAgent.stoppingDistance;
    }

    void Update()
    {
        if (target != null)
        {
            bool inRange = Vector3.Distance(transform.position, target.position) <= shootingDistance;

            if (inRange)
            {
                LookAtTarget();
            }
            else
            {
                UpdatePath();
            }
            enemyReferences.animator.SetBool("shooting", inRange);
        }
        enemyReferences.animator.SetFloat("speed", enemyReferences.navMeshAgent.desiredVelocity.sqrMagnitude);
    }

    private void LookAtTarget()
    {
        Vector3 lookPos = target.position - transform.position;
        lookPos.y = 0; // Keep only horizontal rotation

        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.2f * Time.deltaTime);
    }


    private void UpdatePath()
    {
        if (Time.time >= pathUpdateDeadline)
        {
            pathUpdateDeadline = Time.time + 0.2f; // update every 0.2 seconds

            if (enemyReferences != null && enemyReferences.navMeshAgent != null && target != null)
            {
                enemyReferences.navMeshAgent.SetDestination(target.position);
            }
        }
    }

}
