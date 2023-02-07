using System.Collections;
using UnityEngine;

namespace FSM.Tasks
{
	public abstract class WaitAnimationTask : Task
	{
		[Header( "Settings" )]
        public Animator Animator;

        public bool WaitEndTransition = true, WaitEndAnimation = true;

		public override void OnBegin()
		{
            StartCoroutine( CoroutineWait() );
		}

		protected IEnumerator CoroutineWait()
        {
            if ( WaitEndTransition)
                yield return new WaitUntil( () => Animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f );

            if ( WaitEndAnimation )
                yield return new WaitWhile( () => Animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f );

            End();
        }
	}
}