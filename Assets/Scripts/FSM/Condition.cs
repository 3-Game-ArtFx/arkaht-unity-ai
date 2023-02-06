using System.Collections;
using UnityEngine;

namespace FSM
{
	public abstract class Condition : ScriptableObject
	{
		public abstract bool Evaluate( StateMachine machine );
	}
}