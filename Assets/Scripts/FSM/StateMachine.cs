using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
	public class StateMachine : MonoBehaviour
	{
		public Status Status { get; private set; } = Status.Finished;
		public State State { get; private set; } = null;

		public State StartState = null;

		private void Start()
		{
			Begin();
		}

		public void Begin() => SetState( StartState );

		public void SetState( State state )
		{
			if ( state == null ) return;

			//  ensure previous state is ended
			if ( State != null && State.Status == Status.Running )
			{
				State.End( Status.Stopped );
			}

			//  start next state
			State = state;
			State.StateMachine = this;
			Status = Status.Running;
			State.Begin();

			print("starting " + state);
		}

		void Update()
		{
			if ( State == null ) return;

			//  check for transitions
			State next_state = State.GetNextState();
			if ( next_state != null )
			{
				SetState( next_state );
				return;
			}

			//  update current state
			float dt = Time.deltaTime;
			State.OnUpdate( dt );
		}
	}
}
