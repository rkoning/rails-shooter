using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formation : MonoBehaviour
{
    public List<Transform> members;
    private List<Vector3> relativeStartingPositions = new();
    public List<MotionFunction> motions;


    public bool FollowPlayer = false;
    public float FollowPlayerDuration = 10f;
    private float followStart = float.MinValue;
    public Vector3 FollowOffset = new Vector3(0, 0, 30);

    protected virtual void Start() {
        members = new List<Transform>();
        foreach(Transform child in transform) {
            members.Add(child);
            relativeStartingPositions.Add(child.localPosition);
        }

        if (motions == null || motions.Count == 0) {
            motions = new(GetComponents<MotionFunction>());
        }
    }

    private void OnEnable() {
        if (FollowPlayer) {
            followStart = Time.fixedTime;
        }
    }

    private void SetPositionRelativeToPlayer() {
        transform.position = RailsMovement.Player.transform.position + RailsMovement.Player.transform.rotation * FollowOffset; 
    }

    private void Update() {
        if (members == null)
            return;
        if (FollowPlayer && followStart + FollowPlayerDuration > Time.fixedTime) 
            SetPositionRelativeToPlayer();

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
