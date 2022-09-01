using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask definionPlayer, definitionGround;

    //add times he saw the player.

    //Patrol
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //States
    public float sightRange;
    public bool playerInSightRange;

    private void Awake() 
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();    
    }
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, definionPlayer); //Checks sight

        if(!playerInSightRange)
        {
            Patroling();
        }
        else if(playerInSightRange)
        {
            Chase();
        }
    }

    private void Patroling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }
        if(walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }
        Vector3 DistanceToPoint = transform.position - walkPoint;
        
        if(DistanceToPoint.magnitude < 4.5f)
        {
            walkPointSet = false;
            PoliceScoreManager.instance.AddPassed();
        }
    }
    void SearchWalkPoint()
    {
        float randX = Random.Range(-walkPointRange, walkPointRange);
        float randZ = Random.Range(-walkPointRange, walkPointRange);
           
        walkPoint = new Vector3(transform.position.x + randX, transform.position.y, transform.position.z + randZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, definitionGround))
        {
            walkPointSet = true;
        }
    }

    private void Chase()
    {
         agent.SetDestination(player.position);
    }

}
