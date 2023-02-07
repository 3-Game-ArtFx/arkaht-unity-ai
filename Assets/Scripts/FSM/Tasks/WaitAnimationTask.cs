using System.Collections;
using UnityEngine;

namespace FSM.Tasks
{
    public class WaitAnimationTask : Task
    {
        public Animator Animator;
        public string Parameter = "N/A";
        public bool Value = true;

        public bool UnsetOnEnd = false;
        public bool WaitEndTransition = true, WaitEndAnimation = true;

        public override void OnBegin()
        {
            Animator.SetBool( Parameter, Value);

            StartCoroutine( CoroutineWait() );
        }

        public override void OnEnd()
        {
            if ( !UnsetOnEnd ) return;

            Animator.SetBool( Parameter, !Value);
        }

        IEnumerator CoroutineWait()
        {
            if ( WaitEndTransition)
                yield return new WaitUntil( () => Animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f );

            if ( WaitEndAnimation )
                yield return new WaitWhile( () => Animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f );

            End();
        }
    }
}