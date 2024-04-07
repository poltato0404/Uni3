using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ai : MonoBehaviour
{
    GameObject player;
    NavMeshAgent agent;
    

    [SerializeField] LayerMask groundLayer, playerLayer;

    Vector3 destPoint;
    bool walkPoint;
    [SerializeField] float range;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        if (!walkPoint){SearchForDest();}
        if(walkPoint)
        {agent.SetDestination(destPoint);}
        if(Vector3.Distance(transform.position, destPoint)<10){ walkPoint =false;}
       
    }

    void SearchForDest()
    {
        float z = Random.Range(-range,range);
        float x = Random.Range(-range, range);
        destPoint = new Vector3(transform.position.x +x, transform.position.y, transform.position.z+z);
        if(Physics.Raycast(destPoint, Vector3.down, groundLayer))
        {
                walkPoint = true;
        }
    }

    
}
