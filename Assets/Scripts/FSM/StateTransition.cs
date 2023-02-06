using System;

namespace FSM
{
	[Serializable]
	public struct StateTransition
	{
		public bool OnlyEnded;
		public State State;
		public StateTransitionCondition Condition;
	}
}
