using System.Collections;
using UnityEngine;

namespace FSM
{
	public abstract class Task : MonoBehaviour
	{
		public StateMachine StateMachine { get; set; }
		public State State { get; set; }

		public Status Status { get; private set; }

		[Header( "Task" )]
		public Condition Decorator;
		public bool DecoratorExpectedValue = true;

		public void Begin()
		{			
			if ( !CheckDecorator() ) return;

			Status = Status.Running;
			OnBegin();
		}
		
		public void End( Status status = Status.Success )
		{
			Status = status;
			OnEnd();
		}

		public void DoUpdate( float dt )
		{
			if ( !CheckDecorator() ) return;

			OnUpdate( dt );
		}

		public bool CheckDecorator()
		{
			if ( Decorator == null ) return true;
			if ( Decorator.Evaluate( StateMachine ) == DecoratorExpectedValue ) return true;

			End( Status.Stopped );
			return false;
		}

		public virtual void OnBegin() {}
		public virtual void OnUpdate( float dt ) {}
		public virtual void OnEnd() {}

        public virtual void OnGizmos() {}
		private void OnDrawGizmosSelected()
		{
			if ( Application.isPlaying ) return;

			OnGizmos();
		}
	}
}