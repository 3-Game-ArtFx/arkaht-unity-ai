using UnityEngine;

namespace FSM
{
	public class StateMachine : MonoBehaviour
	{
		public Status Status { get; private set; } = Status.Stopped;
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
			if ( State != null )
			{
				if ( State.Status == Status.Running )
					State.End( Status.Stopped );

				State.OnSwitch();
			}

			//  start next state
			State = state;
			State.StateMachine = this;
			Status = Status.Running;
			State.Begin();
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
			State.DoUpdate( dt );
		}

		void OnDrawGizmosSelected()
		{
			if ( State == null || State.Task == null ) return;

			State.Task.OnGizmos();
		}
	}
}
