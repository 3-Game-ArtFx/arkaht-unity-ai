using UnityEngine;

namespace FSM
{
	public class RandomCondition : StateTransitionCondition
	{
		public float Chance = 0.5f;

		public override bool Evaluate()
		{
			return Random.Range( 0.0f, 1.0f ) <= Chance;
		}
	}

	public abstract class StateTransitionCondition : ScriptableObject
	{
		public abstract bool Evaluate();
	}
}
