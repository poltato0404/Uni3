using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ai : MonoBehaviour, IDataPersistence
{
    public GameObject player;
    NavMeshAgent agent;
    

    [SerializeField] LayerMask groundLayer, playerLayer;

    Vector3 destPoint;
    bool walkPoint;
    [SerializeField] float range, attackRange;
    List<Vector3> possiblePatrol;
    [SerializeField] float gravity = 9.81f; // Acceleration due to gravity (m/s^2)
    private Vector3 velocity = Vector3.zero;

    [SerializeField] float sightRange;
    bool playerInSight;
    bool playerInAttackRange;
    Vector3 guardPos;
    public float distanceChase;
    private Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        possiblePatrol = new List<Vector3>();
        agent = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        guardPos = transform.position;
        RaycastHit hit;
    if (Physics.Raycast(transform.position, (player.transform.position - transform.position).normalized, out hit, sightRange, playerLayer))
    {
        if (hit.collider.CompareTag("Player"))
        {
            playerInSight = true;
            // If player is in sight, chase the player
            chase();
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange,playerLayer);
            if(playerInAttackRange){Attack();}
            if(!playerInAttackRange){animator.SetTrigger("notAttacking");}
            return; // Exit early to prioritize chasing the player
        }
    }

    // If player is not in sight, continue patrolling
    playerInSight = false;
    Patrol();


    // Make the guard face the direction it's traveling
    if (agent.velocity.magnitude > 0.1f) // Check if the guard is moving
    {
        Vector3 lookDirection = agent.velocity.normalized;
        Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * agent.angularSpeed);
    }

    applyGravity();

    //end of update
    }

    void chase()
    {
        
    Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
    Vector3 chasePosition = player.transform.position - directionToPlayer * distanceChase;; // Adjust 2.0f to control the distance behind the player
    animator.SetTrigger("Chasing");
    agent.SetDestination(chasePosition);
    }

void Patrol()
{
    animator.SetTrigger("Patrolling");
    if (!walkPoint)
    {
        SearchForDest();
    }
    else
    {
        agent.SetDestination(destPoint);
        if (agent.remainingDistance < 1f) // Check if agent is close to the destination
        {
            walkPoint = false;
        }
    }
}

void SearchForDest()
{
    NavMeshAgent agent = GetComponent<NavMeshAgent>();
    int i = Random.Range(0, possiblePatrol.Count);
    destPoint = possiblePatrol[i];
    
    NavMeshHit hit;
    if (NavMesh.SamplePosition(destPoint, out hit, 10.0f, NavMesh.AllAreas))
    {
        destPoint = hit.position;
        agent.SetDestination(destPoint);
        walkPoint = true;
    }
}


    public void SaveData(ref GameData data)
    {
        Vector3 guardV3 = guardPos;
        data.guard1Pos = guardV3;
    }
     public void LoadData(GameData data)
    {
        transform.position = data.guard1Pos;
        data.guard1Pos.y = 1.5f;
        possiblePatrol = data.slotPosition;

        if (!data.loadedLevel1)
         {
            int i = Random.Range(0, data.slotPosition.Count);
            transform.position = data.slotPosition[i];
            data.loadedLevel1 = true;
        }
        
        
        
    }
    private void Attack()
    {
        animator.SetTrigger("Attacking");
    }

    private void applyGravity()
    {
        velocity.y -= gravity * Time.deltaTime; // Update velocity based on gravity

        // Apply the velocity to the position
        transform.position += velocity * Time.deltaTime;

        
    }
   
}
