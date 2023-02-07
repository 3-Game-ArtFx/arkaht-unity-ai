using System.Collections.Generic;
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
		public Task[] CurrentTasks { get; set; }
		public State OverrideNextEndState { get; set; }

		[Header( "State" )]
		public Transition[] Transitions;
		public Task[] RunTasks, EndTasks;

		//public UnityEvent OnBeginEvent = new(), OnEndEvent = new(), OnSwitchEvent = new();

		public State GetNextState()
		{
			if ( Status != Status.Running && OverrideNextEndState != null )
			{
			print( "override " + OverrideNextEndState );
				return OverrideNextEndState;

			}

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
			task.State = this;
			Task = task;
			Task.Begin();
		}

		public void NextTask()
		{
			if ( ++TaskID >= CurrentTasks.Length )
			{
				if ( CurrentTasks == EndTasks )
					End();
				else
					SetupEndTasks();
				return;
			}

			SetTask( CurrentTasks[TaskID] );
			print( "task " + TaskID + " " + Task );
		}

		private void SetupEndTasks()
		{
			TaskID = -1;
			CurrentTasks = EndTasks;
			NextTask();
		}

		public void Begin()
		{
			OverrideNextEndState = null;

			Task = null;
			TaskID = -1;

			Status = Status.Running;
			CurrentTasks = RunTasks;

			//OnBeginEvent.Invoke();
			print( "begin state " + this);
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
			else if ( Task.Status != Status.Running )
			{
				SetupEndTasks();
				return;
			}

			Task.DoUpdate( dt );
		}

		public void End( Status status = Status.Success )
		{
			if ( Task != null && Task.Status == Status.Running )
			{
				Task.End( status );
			}

			Status = status;
			//OnEndEvent.Invoke();
        }

		public void OnSwitch()
		{
			//OnSwitchEvent.Invoke();
		}

        public override string ToString()
		{
			return GetType().FullName + ":" + name;
		}
	}
}
