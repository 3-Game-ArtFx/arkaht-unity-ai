using System.Collections;
using UnityEngine;

namespace FSM.Tasks
{
	public class SwitchRandomStateTask : Task
	{
		[Header( "Settings" )]
		public State[] States; 

		public override void OnBegin()
		{
			State.OverrideNextEndState = States[Random.Range( 0, States.Length )];
			End();
		}
	}
}