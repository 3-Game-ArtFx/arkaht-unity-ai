using System.Collections;
using UnityEngine;

namespace FSM
{
	public abstract class Task
	{
		public StateMachine StateMachine { get; set; }

		public Status Status { get; private set; }

		public void Begin()
		{
			Status = Status.Running;
			OnBegin();
		}
		
		public void End( Status status = Status.Finished )
		{
			Status = status;
			OnEnd();
		}

		public virtual void OnBegin() {}
		public virtual void OnUpdate( float dt ) {}
		public virtual void OnEnd() {}
	}
}