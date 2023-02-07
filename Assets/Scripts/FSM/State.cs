using UnityEngine;
using UnityEngine.Events;

namespace FSM
{
	public sealed class State : MonoBehaviour
	{
		public bool IsRunning => StateMachine != null && StateMachine.State == this;

		public StateMachine StateMachine { get; set; }
		public Status Status { get; private set; } = Status.Stopped;
		public Task Task { get; private set; }
		public int TaskID { get; private set; } = -1;

		[Header( "State" )]
		public string Name = "State";

		public Transition[] Transitions;
		public Task[] Tasks;
		public UnityEvent OnBeginEvent = new(), OnEndEvent = new(), OnSwitchEvent = new();

		private void Awake()
		{
			if ( Tasks.Length == 0 )
				Tasks = GetComponents<Task>();
		}

		public State GetNextState()
		{
			foreach ( Transition transition in Transitions )
			{
				if ( Status == Status.Running && transition.OnlyEnded )
					continue;

				//  evaluate transition
				State next_state = transition.GetNextState( StateMachine );
				if ( next_state == null )
					continue;

				return next_state;
			}

			return null;
		}

		public void SetTask( Task task )
		{
			//  ensure task is stopped
			if ( Task != null && Task.Status == Status.Running )
			{
				Task.End( Status.Stopped );
			}

			//  begin task
			task.StateMachine = StateMachine;
			Task = task;
			Task.Begin();
		}

		public void NextTask()
		{
			if ( ++TaskID >= Tasks.Length )
			{
				End();
				return;
			}

			SetTask( Tasks[TaskID] );
		}

		public void Begin()
		{
			Status = Status.Running;
			TaskID = -1;
			OnBeginEvent.Invoke();
		}

		public void DoUpdate( float dt )
		{
			if ( Task == null )
			{
				NextTask();
				return;
			}

			if ( Task.Status == Status.Success )
			{
				NextTask();
				return;
			}
			else if ( Task.Status == Status.Failed )
			{
				End( Status.Failed );
			}

			Task.OnUpdate( dt );
		}

		public void End( Status status = Status.Success )
		{
			if ( Task != null && Task.Status == Status.Running )
			{
				Task.End( status );
			}

			Status = status;
			OnEndEvent.Invoke();
        }

		public void OnSwitch()
		{
			OnSwitchEvent.Invoke();
		}

        public override string ToString()
		{
			return GetType().FullName + ":" + Name;
		}
	}
}
