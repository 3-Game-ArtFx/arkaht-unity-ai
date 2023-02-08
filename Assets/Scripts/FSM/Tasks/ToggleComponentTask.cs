using System.Collections;
using UnityEngine;

namespace FSM.Tasks
{
	public class ToggleComponentTask : Task
	{
		[Header( "Settings" )]
		public Behaviour Component;

		public override void OnBegin()
		{
			Component.enabled = !Component.enabled;
			End();
		}
	}
}