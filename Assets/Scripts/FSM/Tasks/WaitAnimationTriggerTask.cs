using System.Collections;
using UnityEngine;

namespace FSM.Tasks
{
	public class WaitAnimationTriggerTask : WaitAnimationTask
	{
        public string Parameter = "N/A";

		public override void OnBegin()
		{
			Animator.SetTrigger( Parameter );

			base.OnBegin();
		}
	}
}