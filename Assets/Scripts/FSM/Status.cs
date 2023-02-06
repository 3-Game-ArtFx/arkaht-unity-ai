﻿namespace FSM
{
	public enum Status
	{
		Running,
		//Paused,
		Stopped,  //  has been ended externally (e.g.: StateMachine)
		Finished,  //  has been ended by the State itself
	}
}