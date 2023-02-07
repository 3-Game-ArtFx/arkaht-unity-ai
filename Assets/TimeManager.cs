using FSM;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    public float SpeedMultiplier => speedMultiplier;

    public double Seconds = 0.0d;
    private const double MAX_SECONDS = 60.0d * 60.0d * 24.0d;

    [SerializeField]
    private float speedMultiplier = 300.0f, speedUpScale = 5.0f;
    [SerializeField]
    private float timeScale = 10.0f;
    [SerializeField]
    private TextMeshProUGUI hudTMP;
    [SerializeField]
    private StateMachine stateMachine;
    [SerializeField]
    private Condition timeCondition;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        Time.timeScale = ( !timeCondition.Evaluate( stateMachine ) ? speedUpScale : 1.0f ) * timeScale;

        //  update seconds
        Seconds = ( Seconds + Time.deltaTime * speedMultiplier ) % MAX_SECONDS;

        //  update text
        TimeSpan span = TimeSpan.FromSeconds( Seconds );
        hudTMP.text = string.Format( "{0:hh}:{0:mm}", span );
    }
}
