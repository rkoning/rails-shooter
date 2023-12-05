using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MotionFunction
{
    public float Radius;
    public float Frequency;
    public float Spacing = 1f;
    public float InitialOffsetRadians = 1f;
    public bool OffsetFromCenter = false;
    public float CenterOffsetDistance = 1f;

    public override Vector3 GetPositionAtTime(float time, int offset)
    {
        float objectTime = time * Frequency;
        if (Spacing > 0f) {
            objectTime += (offset + 1) * Mathf.PI / Spacing + InitialOffsetRadians * Mathf.PI;
        }
        if (OffsetFromCenter) {
            return (Radius + CenterOffsetDistance * offset)* new Vector3(Mathf.Sin(objectTime), Mathf.Cos(objectTime));
        } else {
            return Radius * new Vector3(Mathf.Sin(objectTime), Mathf.Cos(objectTime));
        }
    }
}
