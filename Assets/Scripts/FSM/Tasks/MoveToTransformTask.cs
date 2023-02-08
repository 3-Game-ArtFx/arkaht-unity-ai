using System.Collections;
using UnityEngine;

namespace FSM.Tasks
{
	public class MoveToTransformTask : MoveToTask
	{
		public Transform Target;

		public override void OnBegin()
		{
			Position = Target.position;

			base.OnBegin();
		}

		public override void OnUpdate( float dt )
		{
			Position = Target.position;

			base.OnUpdate( dt );
		}

		public override void OnGizmos()
		{
			if ( Target != null )
				Position = Target.position;

			base.OnGizmos();
		}
	}
}