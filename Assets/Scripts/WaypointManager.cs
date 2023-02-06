using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    public static WaypointManager Instance { get; private set; }

    private Transform[] waypoints;

    void Awake()
    {
        Instance = this;        
    }

    void Start()
    {
        waypoints = GetComponentsInChildren<Transform>();
        print(waypoints.Length);
    }

    public Transform[] GetRandomWaypoints( int count )
    {
        //  copy waypoints
        List<Transform> transforms = new( waypoints );

        //  shuflle list
        System.Random random = new();
        transforms = transforms.OrderBy( x => random.NextDouble() ).ToList();

        //  remove excess
        for ( int i = count; i < waypoints.Length; i++ )
        {
            transforms.RemoveAt( 0 );
        }

        return transforms.ToArray();
    }
}
