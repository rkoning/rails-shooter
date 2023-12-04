using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float SmoothTime;
    public float MaxSpeed;
    public float TurnSpeed;

    public float followRatio = .6f;
    private Vector3 vel;

    void Update()
    {
        if (!target)
            return;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            // followRatio * target.position + offset,
            target.position + target.rotation * offset,
            ref vel,
            SmoothTime,
            MaxSpeed
        );

        Vector3 direction = (target.position - transform.position).normalized;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), TurnSpeed * Time.deltaTime);
    }
}
