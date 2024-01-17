using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public Transform[] sections;

    public float sectionOffset = 32f;
    public float speed = 12f;

    private void Start() {
        SetInitialPositions();
    }

    private void Update() {
        Vector3 prev = transform.position;
        for (int i = 0; i < sections.Length; i++) {
            Vector3 curr = sections[i].position;
            Vector3 followDirection = (curr - prev).normalized;
            Vector3 desiredPos = prev + followDirection * sectionOffset;
            sections[i].position = Vector3.Lerp(sections[i].position, desiredPos, Time.deltaTime * speed);
            sections[i].rotation = Quaternion.LookRotation(followDirection);
            prev = sections[i].position;
        } 
    }

    private void SetInitialPositions() {
        Vector3 prev = transform.position;
        for (int i = 0; i < sections.Length; i++) {
            Vector3 curr = sections[i].position;
            Vector3 followDirection = (curr - prev).normalized;
            Vector3 desiredPos = prev + followDirection * sectionOffset * (i + 1);
            sections[i].position = desiredPos;
            sections[i].rotation = Quaternion.LookRotation(followDirection);
        } 
    }
}
