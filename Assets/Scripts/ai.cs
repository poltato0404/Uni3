using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ai : MonoBehaviour, IDataPersistence
{
    public GameObject player;
    public int currentlevel;
    NavMeshAgent agent;
    [SerializeField] StaminaBar _staminaBar;

    public float raycastDistance = 0.1f;
    public bool flashed = false;

    [SerializeField] LayerMask groundLayer, playerLayer;

    Vector3 destPoint;
    bool walkPoint;
    [SerializeField] float range, attackRange;
    public List<Vector3> possiblePatrol;
    [SerializeField] float gravity = 9.81f; // Acceleration due to gravity (m/s^2)
    private Vector3 velocity = Vector3.zero;

    [SerializeField] float sightRange;
    bool playerInSight;
    bool playerInAttackRange;
    Vector3 guardPos;
    public float distanceChase;
    private FileHandler handler;
    private Animator animator;
    private GameData gameData;
    public int guardNumber;
    playerControScript playCon;

    [SerializeField] private string filename;

    public void SaveData(ref GameData data)
    {
        Vector3 guardV3 = guardPos;
        if (1 == guardNumber) { data.guard1Pos = guardV3; }
        if (2 == guardNumber) { data.guard2Pos = guardV3; }

    }


    public void LoadData(GameData data)
    {
        Debug.Log("sss:" + data.slotPosition.Count);
        possiblePatrol = data.slotPosition;

        if (1 == guardNumber)
        {
            transform.position = data.guard1Pos;
            data.guard1Pos.y = 10f;
        }

        if (2 == guardNumber)
        {
            transform.position = data.guard2Pos;
            data.guard2Pos.y = 15f;
        }

        switch (data.currentLevel)
        {

            case 2:
                if (!data.loadedLevel2)
                {
                    int i = Random.Range(0, data.slotPosition.Count);
                    transform.position = data.slotPosition[i];

                }
                break;
            case 3:
                if (!data.loadedLevel3)
                {
                    int i = Random.Range(0, data.slotPosition.Count);
                    transform.position = data.slotPosition[i];

                }
                break;
        }





    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playCon = player.GetComponent<playerControScript>();
        animator = GetComponent<Animator>();
        possiblePatrol = new List<Vector3>();
        Vector3 thisPos = transform.position;
        this.handler = new FileHandler(Application.persistentDataPath, filename);
        this.gameData = handler.Load();
        LoadData(this.gameData);
        agent = GetComponent<NavMeshAgent>();

        GameObject staminabar = GameObject.FindGameObjectWithTag("staminaBar");
        _staminaBar = staminabar.GetComponent<StaminaBar>();

    }


    // Update is called once per frame
    void Update()
    {
        if (flashed)
        {
            animator.SetTrigger("flashed"); agent.SetDestination(transform.position);
            StartCoroutine(WaitOneSecond());
            return;
        }
        animator.SetTrigger("notflashed");
        guardPos = transform.position;
        RaycastHit hit;
        // Make the guard face the direction it's traveling
        if (agent.velocity.magnitude > 0.1f) // Check if the guard is moving
        {
            Vector3 lookDirection = agent.velocity.normalized;
            Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * agent.angularSpeed);
        }
        Debug.DrawRay(transform.position, (player.transform.position - transform.position).normalized * sightRange, Color.green);
        if (Physics.Raycast(transform.position, (player.transform.position - transform.position).normalized, out hit, sightRange, playerLayer))
        {
            Debug.Log("lookaing at player");
            if (hit.collider.CompareTag("Player"))
            {
                playerInSight = true;
                Debug.Log("player in sight");
                // If player is in sight, chase the player
                chase();
                playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
                if (playerInAttackRange) { Attack(); Debug.Log("attack"); }
                if (!playerInAttackRange) { animator.SetTrigger("notAttacking"); Debug.Log("no attack"); playCon.slowed = false; }
                return; // Exit early to prioritize chasing the player
            }
        }

        // If player is not in sight, continue patrolling
        playerInSight = false;
        Patrol();
        applyGravity();

        //end of update
    }

    void chase()
    {
        Debug.Log("chasing");
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
        Vector3 chasePosition = player.transform.position - directionToPlayer * distanceChase; ; // Adjust 2.0f to control the distance behind the player

        agent.SetDestination(chasePosition);
    }

    void Patrol()
    {
        Debug.Log("patrolling");

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
            Debug.Log(" walk point false");
        }
    }
    IEnumerator WaitOneSecond()
    {

        // Wait for one second
        yield return new WaitForSeconds(2f);


        // Continue with your code after the delay
    }
    void SearchForDest()
    {

        Debug.Log("searching destination");
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        int i = Random.Range(0, possiblePatrol.Count);

        Debug.Log(possiblePatrol.Count);
        destPoint = possiblePatrol[i]; Debug.Log(destPoint);



        NavMeshHit hit;
        if (NavMesh.SamplePosition(destPoint, out hit, 10.0f, NavMesh.AllAreas))
        {
            destPoint = hit.position;
            agent.SetDestination(destPoint);
            walkPoint = true;
        }
    }


    private void Attack()
    {
        animator.SetTrigger("Attacking");
        playCon.slowed = true;
        StaminaGameManager.staminaGameManager._playertStamina.UseStamina(10f);
        _staminaBar.SetStamina(StaminaGameManager.staminaGameManager._playertStamina.Stamina);

    }

    private void applyGravity()
    {
        // Get the NavMeshAgent component
        NavMeshAgent agent = this.GetComponent<NavMeshAgent>();
        Vector3 agentPosition = agent.transform.position;
        // Assuming this code is inside an Update() method or similar loop
        if (IsGrounded() && agentPosition.y > 0)
        {
            // Update velocity based on gravity
            velocity.y -= gravity * Time.deltaTime;

            // Apply the velocity to the position
            transform.position += velocity * Time.deltaTime;

        }



        // Check if the NavMeshAgent component is valid
        if (agent != null)
        {
            // Get the position of the NavMeshAgent


            // Set the position of the objectToPosition at the same height as the NavMeshAgent
            Vector3 newPosition = new Vector3(agentPosition.x, agentPosition.y + agent.baseOffset, agentPosition.z);
            transform.position = newPosition;
        }

    }
    public bool IsGrounded()
    {
        // Create a raycast downward from the collider's position
        RaycastHit hit;
        bool isHit = Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance, groundLayer);

        // If the raycast hit something on the ground layer, return true
        return isHit;
    }

}
