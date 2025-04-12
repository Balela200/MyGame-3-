using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float walkRange = 10f;
    public float chaseRange = 15f;
    public float attackRange = 2.5f;

    public float rotationSpeed = 10f;

    private Transform player;
    private NavMeshAgent agent;
    private Animator animator;

    private Vector3 walkPoint;
    private bool walkPointSet;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance > chaseRange)
        {
            Patrol();
        }
        else if (distance > attackRange)
        {
            ChasePlayer();
        }
        else
        {
            AttackPlayer();
        }
    }

    void Patrol()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
            animator.SetBool("walk", true);
            animator.SetBool("run", false);
            animator.SetBool("attack", false);
        }

        float distanceToPoint = Vector3.Distance(transform.position, walkPoint);
        if (distanceToPoint < 2f) walkPointSet = false;
    }

    void SearchWalkPoint()
    {
        float randZ = Random.Range(-walkRange, walkRange);
        float randX = Random.Range(-walkRange, walkRange);

        Vector3 potentialPoint = new Vector3(transform.position.x + randX, transform.position.y, transform.position.z + randZ);

        if (NavMesh.SamplePosition(potentialPoint, out NavMeshHit hit, 2f, NavMesh.AllAreas))
        {
            walkPoint = hit.position;
            walkPointSet = true;
        }
    }

    void ChasePlayer()
    {
        agent.SetDestination(player.position);
        animator.SetBool("walk", false);
        animator.SetBool("run", true);
        animator.SetBool("attack", false);
    }

    void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        FaceTarget();
        animator.SetBool("walk", false);
        animator.SetBool("run", false);
        animator.SetBool("attack", true);
    }

    void FaceTarget()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }
}

