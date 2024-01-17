using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorTools
{
    public static Vector3 BezierAt(Vector3 a, Vector3 b, Vector3 c, float t) {
        return (1 - t) * ((1 - t) * a + t * b)
            + t * ((1 - t) * b + t * c);
    }
}
