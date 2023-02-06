using System;

namespace FSM
{
	[Serializable]
	public class Transition
	{
		public bool OnlyEnded;
		public State TrueState, FalseState;
		public Condition Condition;

		public State GetNextState( StateMachine machine )
		{
			return ( Condition == null || Condition.Evaluate( machine ) ) ? TrueState : FalseState;
		}
	}
}
