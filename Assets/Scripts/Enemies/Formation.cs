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

        if (motions == null || motions.Count == 0) {
            motions = new(GetComponents<MotionFunction>());
        }
    }

    private void Update() {
        if (members == null)
            return;
        for (int i = 0; i < members.Count; i++) {

            var localPosition = relativeStartingPositions[i] + transform.position;
            foreach (var mf in motions) {
                localPosition += mf.GetPositionAtTime(Time.fixedTime, i);
            }
            if (members[i] == null)
                continue;
                
            members[i].position = localPosition;
        }
    }
}
