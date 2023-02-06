using UnityEngine;

namespace FSM.States
{
    /*public class WaitState : State
    {
        [SerializeField]
        private float waitingTime = 1.0f;

        private float currentWaitTime = 0.0f;

        public override void OnBegin()
        {
            currentWaitTime = waitingTime;
        }

        public override void OnUpdate(float dt)
        {
            if ( ( currentWaitTime -= dt ) <= 0.0f )
            {
                End();
            }

            print("waiting for " + currentWaitTime );
        }
    }*/
}