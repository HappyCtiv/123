using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CarAI : MonoBehaviour
{
    public NavMeshAgent agent;
    //race
    public Transform Point1, Point2, Point3, Point4;
    public Vector3 racePoint;
    bool checkpointSet;

    public int checkpointCount = 0;
    public LayerMask RaceTrack;

    private void Awake() 
    {
        agent = GetComponent<NavMeshAgent>();    
    }


    // Update is called once per frame
    void Update()
    {
        racing();
    }


    void racing()
    {
        if(!checkpointSet)
        {
            searchCheckpoint();
        }
        if(checkpointSet)
        {
            agent.SetDestination(racePoint);
        }
        Vector3 DistanceToPoint = transform.position - racePoint;

        if(DistanceToPoint.magnitude < 0.5f)
        {
            checkpointSet = false;
            checkpointCount++;
            //PoliceScoreManager.instance.AddPassed();
        }
    }
    void searchCheckpoint() //searches for checkpoint of a race
    {
        switch (checkpointCount % 4)
        {
        case 0:
            racePoint = Point1.transform.position;
            break;
        case 1: 
            racePoint = Point2.transform.position;
            break;
        case 2: 
            racePoint = Point3.transform.position;
            break;
        default:
            racePoint = Point4.transform.position;
            break;
        }
        if (Physics.Raycast(racePoint, -transform.up, 2f, RaceTrack))
        {
            checkpointSet = true;
        }
    }
}
