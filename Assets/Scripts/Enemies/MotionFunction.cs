using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MotionFunction : MonoBehaviour
{
    public abstract Vector3 GetPositionAtTime(float time, int offset);
}
