using System.Collections;
using UnityEngine;

namespace FSM.Conditions
{
	[CreateAssetMenu( menuName = "FSM/Conditions/Time" )]
	public class TimeCondition : Condition
	{
		public ComparisonCheck Check = ComparisonCheck.Greater;
		public double Seconds = 0.0d;

		public override bool Evaluate( StateMachine machine )
		{
			if ( TimeManager.Instance == null ) return false;

			return Check switch  //  weird return switch statement I admit, but hey, we test new C# 8 features
			{
				ComparisonCheck.Greater => TimeManager.Instance.Seconds > Seconds,
				ComparisonCheck.GreaterEqual => TimeManager.Instance.Seconds >= Seconds,
				ComparisonCheck.Equal => TimeManager.Instance.Seconds == Seconds,
				ComparisonCheck.LessEqual => TimeManager.Instance.Seconds <= Seconds,
				ComparisonCheck.Less => TimeManager.Instance.Seconds < Seconds,
				_ => false,
			};
		}
	}
}