using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private TextMeshProUGUI textUI;

    public void SetScore( int score )
    {
        textUI.text = score.ToString();
        animator.SetTrigger( "Score" );
    }
}
