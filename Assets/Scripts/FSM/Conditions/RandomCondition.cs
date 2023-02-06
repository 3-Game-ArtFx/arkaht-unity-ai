using UnityEngine;

namespace FSM
{
	[CreateAssetMenu( menuName = "FSM/Conditions/Random" )]
	public class RandomCondition : Condition
	{
		public float Chance = 0.5f;

		public override bool Evaluate( StateMachine machine )
		{
			return Random.Range( 0.0f, 1.0f ) <= Chance;
		}
	}
}
