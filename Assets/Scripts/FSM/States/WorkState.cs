using UnityEngine;

namespace FSM.States
{
	public class WorkState : State
	{
		[Header( "Work State" )]
		[SerializeField]
		private float helloTime = 1.0f;

		private float currentHelloTime = 0.0f;

		public override void OnBegin()
		{
			print("begin");
		}

		public override void OnUpdate(float dt)
		{
			if ( ( currentHelloTime -= dt ) <= 0.0f )
			{
				print("hello ");
				currentHelloTime = helloTime;
			}
		}

		public override void OnEnd()
		{
			print("end");
		}
	}
}
