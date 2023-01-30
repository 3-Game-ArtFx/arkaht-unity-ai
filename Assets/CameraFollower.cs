using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float smoothSpeed = 2.0f;
    [SerializeField]
    private float fovMultiplier = 0.5f;

    private new Camera camera;

    void Awake()
    {
        camera = GetComponent<Camera>();    
    }

    void Update()
    {
        float speed = Time.deltaTime * smoothSpeed;
        Vector3 dir = target.position - transform.position;

        transform.rotation = Quaternion.Slerp( transform.rotation, Quaternion.LookRotation( dir ), speed );
        camera.fieldOfView = Mathf.Lerp( camera.fieldOfView, 1.0f / dir.magnitude * fovMultiplier, speed );
    }
}
