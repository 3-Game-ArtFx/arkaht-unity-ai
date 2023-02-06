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
		public StateStatus Status { get; private set; } = StateStatus.Finished;

		[Header( "State" )]
		public string Name = "State";

		public StateTransition[] Transitions;

		public State GetNextState()
		{
			foreach ( StateTransition transition in Transitions )
			{
				if ( Status == StateStatus.Running && transition.OnlyEnded ) continue;
				//if ( transition.Condition( StateMachine ) )
				return transition.State;
			}

			return null;
		}

		public void Begin()
		{
			Status = StateStatus.Running;
			OnBegin();
		}
		public void End(StateStatus status = StateStatus.Finished)
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
