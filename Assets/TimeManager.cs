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
    private float speedMultiplier = 300.0f;
    [SerializeField]
    private TextMeshProUGUI hudTMP;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        //  update seconds
        Seconds = ( Seconds + Time.deltaTime * speedMultiplier ) % MAX_SECONDS;

        //  update text
        TimeSpan span = TimeSpan.FromSeconds( Seconds );
        hudTMP.text = string.Format( "{0:hh}:{0:mm}", span );
    }
}
