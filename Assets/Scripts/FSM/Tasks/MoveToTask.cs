using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace FSM.Tasks
{
	public class MoveToTask : Task
	{
		public NavMeshAgent Agent;
		public Vector3 Position;
		public float UpdateDist = 1.0f, ReachDist = 1.0f;

		public Animator Animator;

		public override void OnBegin()
		{
			Agent.SetDestination( Position );

            if ( Animator != null )
                Animator.SetBool( "IsWalking", true );
        }

		public override void OnUpdate( float dt )
		{
			//  update destination
			if ( ( Position - Agent.destination ).sqrMagnitude >= UpdateDist * UpdateDist ) 
			{ 
				Agent.SetDestination( Position );
			}

			//  reach dist
			if ( ( Agent.destination - Agent.transform.position ).sqrMagnitude <= ReachDist * ReachDist )
			{
				End();
			}
		}

		public override void OnEnd()
		{
			if ( Animator != null )
				Animator.SetBool( "IsWalking", false );
		}

		public override void OnGizmos()
		{
			if ( Agent == null ) return;

			Gizmos.color = Color.green;
			Gizmos.DrawLine( Agent.transform.position, Position );
			Gizmos.DrawWireSphere( Agent.destination, ReachDist );

			//  draw destination difference
			if ( Agent.destination != Position )
			{
				Gizmos.color = Color.blue;
				Gizmos.DrawLine( Agent.destination, Position );
				Gizmos.DrawWireSphere( Position, UpdateDist );
			}
		}
	}
}