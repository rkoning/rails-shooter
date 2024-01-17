using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float SmoothTime;
    public float MaxSpeed;
    public float TurnSpeed;

    public float followRatio = .6f;
    private Vector3 vel;

    public bool followRotation = false;

    void Update()
    {
        if (!target)
            return;

        Vector3 adjustedOffset = new Vector3(offset.x * followRatio, offset.y * followRatio, offset.z);
        transform.position = Vector3.SmoothDamp(
            transform.position,
            target.position + target.rotation * adjustedOffset,
            // target.position + target.rotation * offset,
            ref vel,
            SmoothTime,
            MaxSpeed
        );

        Vector3 direction = (target.position - transform.position).normalized;

        Quaternion desiredRot = 
            followRotation ? 
                Quaternion.LookRotation(direction) : 
                Quaternion.LookRotation(direction, target.up);

        transform.rotation = Quaternion.Lerp(
            transform.rotation, 
            desiredRot, 
            TurnSpeed * Time.deltaTime
        );
    }
}
