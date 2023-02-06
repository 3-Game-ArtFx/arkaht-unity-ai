using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FSM
{
	public abstract class State : MonoBehaviour
	{
		public bool IsRunning => StateMachine != null && StateMachine.State == this;

		public StateMachine StateMachine { get; set; }
		public Status Status { get; private set; } = Status.Finished;

		[Header( "State" )]
		public string Name = "State";

		public Transition[] Transitions;

		public State GetNextState()
		{
			foreach ( Transition transition in Transitions )
			{
				if ( Status == Status.Running && transition.OnlyEnded ) continue;

				//  evaluate transition
				State next_state = transition.GetNextState( StateMachine );
				if ( next_state == null ) continue;

				return next_state;
			}

			return null;
		}

		public void Begin()
		{
			Status = Status.Running;
			OnBegin();
		}
		public void End(Status status = Status.Finished)
		{
			Status = status;
			OnEnd();
		}

		public virtual void OnBegin() {}
		public virtual void OnUpdate( float dt ) {}
		public virtual void OnEnd() {}

		public override string ToString()
		{
			return GetType().FullName + ":" + Name;
		}
	}
}
