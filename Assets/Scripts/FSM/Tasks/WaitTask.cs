using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace FSM.Tasks
{
    public class WaitTask : Task
    {
        public Vector2 TimeRange = new( 10.0f, 60.0f );

        private float time = 0.0f;

        public override void OnBegin()
        {
            time = Random.Range( TimeRange.x, TimeRange.y );
        }

        public override void OnUpdate( float dt )
        {
            if ( ( time -= dt * TimeManager.Instance.SpeedMultiplier ) <= 0.0f )
            {
                End();
            }
        }
    }
}