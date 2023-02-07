using System.Collections;
using UnityEngine;

namespace FSM.Tasks
{
	public class WaitWorkTimeTask : WaitTimeTask
	{
		public float CryTime = 1000.0f;
		public State CryState;

		public override void OnBegin()
		{
			base.OnBegin();

			if ( time >= CryTime )
			{
				State.OverrideNextEndState = CryState;
				End( Status.Failed );
			}
		}
	}
}