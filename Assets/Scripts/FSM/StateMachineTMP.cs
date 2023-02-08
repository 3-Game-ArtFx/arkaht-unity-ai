using System.Collections;
using TMPro;
using UnityEngine;

namespace FSM
{
	public class StateMachineTMP : MonoBehaviour
	{
		public StateMachine StateMachine;
		public TextMeshPro StateText, TaskText;

		void Update()
		{
			if ( StateMachine.State == null )
			{
				StateText.text = "N/A";
				TaskText.enabled = false;
				return;
			}

			StateText.text = StateMachine.State.name;
			
			TaskText.enabled = true;
			TaskText.text = StateMachine.State.Task != null ? StateMachine.State.Task.GetType().Name : "N/A";
		}
	}
}