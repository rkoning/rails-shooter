using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smoothTime, maxSpeed;

    public float followRatio = .6f;
    private Vector3 vel;

    void Update()
    {
        if (!target)
            return;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            followRatio * target.position + offset,
            ref vel,
            smoothTime,
            maxSpeed
        );
    }
}
