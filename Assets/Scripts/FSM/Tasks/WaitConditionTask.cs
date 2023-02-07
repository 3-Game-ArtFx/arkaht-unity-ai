using System.Collections;
using UnityEngine;

namespace FSM.Tasks
{
    public class WaitConditionTask : Task
    {
        public Condition Condition;
        public bool ExpectedValue = true;

        public override void OnUpdate( float dt )
        {
            if ( Condition == null )
            {
                End( Status.Failed );
                return;
            }

            if ( Condition.Evaluate( StateMachine ) != ExpectedValue ) return;

            End();
        }
    }
}