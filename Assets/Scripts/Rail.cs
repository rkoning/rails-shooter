using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rail : MonoBehaviour
{

    private List<Transform> points;

    private int railIndex = 0;
    public bool Loop = true;
    public float RailThreshold = 1f;
    private float railThresholdSqrd;

    void Start()
    {
        railThresholdSqrd = RailThreshold * RailThreshold;

        points = new();
        foreach(Transform child in transform) {
            points.Add(child);
        }
    }

    public void OnDrawGizmos() {
        if (points == null)
            return;
        for (int i = 0; i < points.Count; i++) {
            Gizmos.color = i == railIndex ? Color.yellow : Color.cyan;
            Gizmos.DrawSphere(points[i].position, 3f);
            Gizmos.DrawLine(points[i].position, points[WrapIndex(i + 1, points.Count)].position);
        }
    }

    public Vector3 GetNextRailPoint(Transform from) {
        if (AtRailPoint(from, points[railIndex])) {
            railIndex = WrapIndex(railIndex + 1, points.Count);
        }
        return points[railIndex].position;
    }

    private bool AtRailPoint(Transform from, Transform point) {
        return (from.position - point.position).sqrMagnitude < railThresholdSqrd;
    }

    private int WrapIndex(int i, int length) {
        if (i >= length)
            return 0;
        else if (i < 0) 
            return length - 1;
        else
            return i;
    }
}
