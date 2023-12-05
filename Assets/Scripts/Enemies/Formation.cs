using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formation : MonoBehaviour
{
    public List<Transform> members;
    private List<Vector3> relativeStartingPositions = new();
    public List<MotionFunction> motions;

    public bool showMovementPaths;

    protected virtual void Start() {
        // frequency = 1f / duration;
        members = new List<Transform>();
        foreach(Transform child in transform) {
            members.Add(child);
            relativeStartingPositions.Add(child.localPosition);
        }
    }

    private void Update() {
        for (int i = 0; i < members.Count; i++) {
            var localPosition = relativeStartingPositions[i] + transform.position;
            foreach (var mf in motions) {
                localPosition += mf.GetPositionAtTime(Time.fixedTime, i);
            }
            members[i].position = localPosition;
        }
    }

    public Vector3 GetPositionAtTime(float time, int offset)
    {
        float value = 2 * time;
        return 10f * new Vector3(Mathf.Sin(value), 0f, 0f);
    }
}
