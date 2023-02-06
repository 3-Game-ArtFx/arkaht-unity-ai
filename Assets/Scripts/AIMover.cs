using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class AIMover : MonoBehaviour
{
	public Transform SeeParent => seeParent;
	public int TeamID => currentTeamID;

	public UnityEvent OnTeamChanged = new(); 

	[SerializeField]
	private Transform destination;
	[SerializeField]
	private float stoppingDistance = 0.5f;

	[Header( "Patrol" )]
	[SerializeField]
	private Transform[] waypoints;
	[SerializeField]
	private float patrolWaitTime = 1.0f;

	[Header( "Detection" )]
	[SerializeField]
	private Transform seeParent;
	[SerializeField]
	private LayerMask seeLayerMask;
    [SerializeField]
	private float seeFOV = 90.0f;
	[SerializeField]
	private float seeRange = 5.0f;
	[SerializeField]
	private new SphereCollider collider;

	[SerializeField]
	private Material[] teamMaterials;
	[SerializeField]
	private int currentTeamID = 0;
	[SerializeField]
	private new SkinnedMeshRenderer renderer;

	private int currentWaypointID = 0;
	private float currentWaitTime = 0.0f; 

	private NavMeshAgent agent;

	void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
		agent.stoppingDistance = stoppingDistance;

		collider.radius = seeRange;
    }

	void Start()
	{
		SetTeam( currentTeamID );
		waypoints = WaypointManager.Instance.GetRandomWaypoints( 4 );
	}

	void OnValidate()
	{
		SetTeam( currentTeamID );
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
		}

		UpdateDestination();
	}

	public void SetTeam( int team_id )
	{
		currentTeamID = team_id;
		renderer.material = teamMaterials[team_id];

		OnTeamChanged.Invoke();
    }

	void UpdateDestination()
	{
		if ( !agent.isOnNavMesh ) return;
		if ( agent.destination == destination.position ) return;

		agent.SetDestination( destination.position );
	}

	bool CanDetect( Transform target )
	{
		Vector3 dir = ( target.position - seeParent.position ).normalized;
		//print(Vector3.Dot(seeParent.forward, dir) + " >= " +  Mathf.Cos(seeFOV / 2.0f));
		if ( Vector3.Dot( seeParent.forward, dir ) < Mathf.Cos( seeFOV / 2.0f ) ) return false;

		//  check for obstacles
		if ( Physics.Raycast( seeParent.position, seeParent.forward, out RaycastHit hit, seeRange, seeLayerMask, QueryTriggerInteraction.Ignore ) )
		{
			if ( hit.collider.transform != target ) 
				return false;
		}

		return true;
	}

	void OnTriggerStay( Collider other )
	{
		if ( !other.TryGetComponent( out AIMover mover ) ) return;
		if ( mover.TeamID == currentTeamID ) return;

		if ( !CanDetect( mover.SeeParent ) ) return;

		mover.SetTeam( currentTeamID );
    }

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere( destination.position, stoppingDistance);
		Gizmos.DrawLine( transform.position, destination.position );

		Gizmos.color = Color.magenta;
		Gizmos.matrix = ( seeParent == null ? transform : seeParent ).localToWorldMatrix;
		Gizmos.DrawFrustum( Vector3.zero, seeFOV, seeRange, 0.0f, 1.0f );

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
