using System.Collections;
using UnityEngine;

namespace FSM.Tasks
{
    public class WaitAnimationBoolTask : WaitAnimationTask
    {
        public string Parameter = "N/A";
        public bool Value = true;
        public bool UnsetOnEnd = false;

        public override void OnBegin()
        {
            Animator.SetBool( Parameter, Value );

            base.OnBegin();
        }

        public override void OnEnd()
        {
            if ( !UnsetOnEnd ) return;

            Animator.SetBool( Parameter, !Value );
        }
    }
}