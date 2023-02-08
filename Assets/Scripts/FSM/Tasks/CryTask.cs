using System.Collections;
using UnityEngine;

namespace FSM.Tasks
{
	public class CryTask : WaitAnimationTask
	{
		public State WinState, LoseState;
		public string WinTrigger, LoseTrigger;

		public Condition Condition;

		public override void OnBegin()
		{
			bool has_won = Condition.Evaluate( StateMachine );
			State.OverrideNextEndState = has_won ? WinState : LoseState;
			Animator.SetTrigger( has_won ? WinTrigger : LoseTrigger );

			base.OnBegin();
		}

		public override void OnEnd()
		{

		}
	}
}