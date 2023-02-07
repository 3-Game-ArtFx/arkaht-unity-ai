using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class AnimatorEventHandler : MonoBehaviour
    {
        public Animator Animator;
        public string Parameter = "N/A";

        public void SetBool( bool value ) => Animator.SetBool( Parameter, value );
    }
}