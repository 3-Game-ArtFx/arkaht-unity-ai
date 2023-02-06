using System.Collections;
using UnityEngine;

namespace FSM.Conditions
{
	[CreateAssetMenu( menuName = "FSM/Conditions/Time Range" )]
	public class TimeRangeCondition : Condition
	{
		public double InSeconds = 0.0d, OutSeconds = 60.0d * 60.0d * 24.0d;

		public override bool Evaluate( StateMachine machine )
		{
			if ( TimeManager.Instance == null ) return false;

			double current_seconds = TimeManager.Instance.Seconds;
			return current_seconds >= InSeconds && current_seconds <= OutSeconds;
		}
	}
}