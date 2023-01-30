using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMover : MonoBehaviour
{
	[SerializeField]
	private Transform[] waypoints;
	[SerializeField]
	private Transform destination;
	[SerializeField]
	private float stoppingDistance = 0.5f;
	[SerializeField]
	private float patrolWaitTime = 1.0f;

	private NavMeshPathStatus lastStatus;
	private int currentWaypointID = 0;
	private float currentWaitTime = 0.0f; 

	private NavMeshAgent agent;

	void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
		agent.stoppingDistance = stoppingDistance;
	}

	void Update()
	{
		if ( ( currentWaitTime -= Time.deltaTime ) > 0.0f )
		{
			agent.isStopped = true;
            return;
		}

		agent.isStopped = false;

		if ( agent.remainingDistance <= stoppingDistance )
		{
			destination = waypoints[currentWaypointID];
			currentWaypointID = ( currentWaypointID + 1 ) % waypoints.Length;

			currentWaitTime = patrolWaitTime;
			print( "next waypoint " + currentWaypointID );
		}

		UpdateDestination();
	}

	void UpdateDestination()
	{
		if ( !agent.isOnNavMesh ) return;
		if ( agent.destination == destination.position ) return;

		agent.SetDestination( destination.position );
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere( destination.position, stoppingDistance);
		Gizmos.DrawLine( transform.position, destination.position );

		//  draw path
		if ( agent != null && agent.hasPath && agent.path.corners.Length > 0 )
		{
			NavMeshPath path = agent.path;

			Gizmos.color = Color.blue;

			Vector3 last_corner = path.corners[0];
			for ( int i = 1; i < path.corners.Length; i++ )
			{
				Vector3 current_corner = path.corners[i];
				Gizmos.DrawLine( last_corner, current_corner );
				last_corner = current_corner;
			}
		}
	}
}
