using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Ranges")]
    public float walkRange = 10f;
    public float chaseRange = 15f;
    public float attackRange = 2.5f;

    [Header("Attack Settings")]
    public float attackCooldown = 1.2f;
    public float rotationSpeed = 10f;

    private Transform player;
    private NavMeshAgent agent;
    private Animator animator;

    private Vector3 walkPoint;
    private bool walkPointSet;

    private float timeAttack;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        GameObject foundPlayer = GameObject.FindGameObjectWithTag("Player");
        if (foundPlayer != null)
        {
            player = foundPlayer.transform;
        }
    }

    void Update()
    {
        if (player == null)
        {
            GameObject foundPlayer = GameObject.FindGameObjectWithTag("Player");
            if (foundPlayer != null)
            {
                player = foundPlayer.transform;
            }
            return;
        }

        float distance = Vector3.Distance(player.position, transform.position);
        timeAttack += Time.deltaTime;

        if (distance > chaseRange)
        {
            Patrol();
        }
        else if (distance > attackRange)
        {
            ChasePlayer();
        }
        else if (distance <= attackRange && timeAttack >= attackCooldown)
        {
            Vector3 toPlayer = player.position - transform.position;
            toPlayer.y = 0;
            float angle = Vector3.Angle(transform.forward, toPlayer);

            if (angle < 40f)
            {
                AttackPlayer();
                timeAttack = 0f;
            }
            else
            {
                FaceTarget();
            }
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
            agent.speed = 1.5f;
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
        agent.speed = 4f;
        FaceTarget();
    }

    void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        FaceTarget();
        animator.SetBool("walk", false);
        animator.SetBool("run", false);
        animator.SetTrigger("attack");
    }

    void FaceTarget()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }
}

