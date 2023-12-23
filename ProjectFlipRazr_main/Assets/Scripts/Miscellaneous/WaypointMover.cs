using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMover : MonoBehaviour
{
    //Stires a reference to the waypoint system this object will use
    [SerializeField] private Waypoints waypoints;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float distanceThreshold;

    private Transform currentWaypoint;
    public bool lockToFirst; // this will make the object automatically start at the first waypoint of the target waypoint system

    // Start is called before the first frame update
    void Start()
    {
        if(lockToFirst)
        {
            //Set initial position to the first waypoint
            currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
            transform.position = currentWaypoint.position;
        }

        //Set the next waypoint target
        currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
        transform.LookAt(currentWaypoint);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, currentWaypoint.position) < distanceThreshold)
        {
            currentWaypoint = waypoints.GetNextWaypoint(currentWaypoint);
            transform.LookAt(currentWaypoint);
        }
    }
}
